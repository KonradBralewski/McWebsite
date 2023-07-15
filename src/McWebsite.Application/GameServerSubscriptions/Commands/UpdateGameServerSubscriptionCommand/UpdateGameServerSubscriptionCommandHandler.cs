using ErrorOr;
using McWebsite.Application.Common.Interfaces.Persistence;
using MediatR;
using McWebsite.Application.Common.Utilities;
using McWebsite.Application.GameServerReports.Commands.UpdateGameServerReportCommand;
using McWebsite.Domain.GameServerReport.ValueObjects;
using McWebsite.Domain.GameServerReport;
using McWebsite.Domain.GameServerReport.Enums;
using McWebsite.Domain.GameServer.ValueObjects;
using McWebsite.Domain.GameServerSubscription.ValueObjects;
using McWebsite.Domain.GameServerSubscription;
using McWebsite.Domain.GameServerSubscription.Enums;

namespace McWebsite.Application.GameServerSubscriptions.Commands.UpdateGameServerSubscriptionCommand
{
    public sealed class UpdateGameServerSubscriptionCommandHandler : IRequestHandler<UpdateGameServerSubscriptionCommand, ErrorOr<UpdateGameServerSubscriptionResult>?>
    {
        private readonly IGameServerSubscriptionRepository _gameServerSubscriptionRepository;
        private readonly IGameServerRepository _gameServerRepository;
        public UpdateGameServerSubscriptionCommandHandler(IGameServerSubscriptionRepository gameServerSubscriptionRepository, IGameServerRepository gameServerRepository)
        {
            _gameServerSubscriptionRepository = gameServerSubscriptionRepository;
            _gameServerRepository = gameServerRepository;
        }
        public async Task<ErrorOr<UpdateGameServerSubscriptionResult>?> Handle(UpdateGameServerSubscriptionCommand command, CancellationToken cancellationToken)
        {
            var gameServerSubscriptionSearchResult = await _gameServerSubscriptionRepository.GetGameServerSubscription(
                GameServerSubscriptionId.Create(command.GameServerSubscriptionId));

            if (gameServerSubscriptionSearchResult.IsError)
            {
                return gameServerSubscriptionSearchResult.Errors;
            }

            var gameServerSearchResult = await _gameServerRepository.GetGameServer(GameServerId.Create(command.GameServerId));

            if (gameServerSearchResult.IsError)
            {
                return gameServerSearchResult.Errors;
            }

            GameServerSubscription foundGameServerSubscription = gameServerSubscriptionSearchResult.Value;

            if (ApplyModfications(foundGameServerSubscription, command) is not GameServerSubscription gameServerSubscriptionAfterUpdate)
            {
                return null;
            }

            var updatedGameServerSubscriptionResult = await _gameServerSubscriptionRepository.UpdateGameServerSubscription(gameServerSubscriptionAfterUpdate);

            if (updatedGameServerSubscriptionResult.IsError)
            {
                return updatedGameServerSubscriptionResult.Errors;
            }

            GameServerSubscription updatedGameServerSubscription = updatedGameServerSubscriptionResult.Value;


            return new UpdateGameServerSubscriptionResult(updatedGameServerSubscription);
        }

        private GameServerSubscription? ApplyModfications(GameServerSubscription toBeUpdated, UpdateGameServerSubscriptionCommand command)
        {
            bool hasSomethingChanged = false;

            if (toBeUpdated.GameServerId.Value != command.GameServerId
                || toBeUpdated.SubscriptionType.Value.ToString() != command.SubscriptionType
                || toBeUpdated.InGameSubscriptionId != command.InGameSubscriptionId
                || toBeUpdated.SubscriptionDescription != command.SubscriptionDescription
                || toBeUpdated.SubscriptionDuration != command.SubscriptionDuration)
            {
                hasSomethingChanged = true;
            }

            if (!hasSomethingChanged)
            {
                return null;
            }

            return GameServerSubscription.Recreate(toBeUpdated.Id.Value,
                                       command.GameServerId,
                                       command.SubscriptionType.ToEnum<SubscriptionType>(),
                                       command.InGameSubscriptionId,
                                       command.Price,
                                       command.SubscriptionDescription,
                                       command.SubscriptionDuration,
                                       toBeUpdated.CreatedDateTime,
                                       DateTime.UtcNow);
        }
    }
}
