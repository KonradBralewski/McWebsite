using Mapster;
using McWebsite.API.Contracts.Conversation;
using McWebsite.API.Contracts.Message;
using McWebsite.Application.Conversations.Commands.CreateConversationCommand;
using McWebsite.Application.Conversations.Commands.DeleteConversationCommand;
using McWebsite.Application.Conversations.Queries.GetConversationQuery;
using McWebsite.Application.Conversations.Queries.GetConversationsQuery;
using McWebsite.Domain.Conversation;
using McWebsite.Domain.MessageModel.ValueObjects;

namespace McWebsite.API.Common.Mapping
{
    public class ConversationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            RegisterQueriesCommandsMapping(config);

            config.NewConfig<GetConversationResult, GetConversationResponse>()
                .ConstructUsing(src => new GetConversationResponse(src.Conversation.Id.Value,
                                                                   src.Conversation.Participants.FirstParticipantId.Value,
                                                                   src.Conversation.Participants.SecondParticipantId.Value,
                                                                   src.ConversationMessages.Select(m => m.Adapt<GetMessageResponse>())));

            config.NewConfig<GetConversationsResult, GetConversationsResponse>()
                .ConstructUsing(src => new GetConversationsResponse(src.Conversations.Select(c => new SingleConversationEntry(c.Id.Value,
                                                                                                    c.Participants.FirstParticipantId.Value,
                                                                                                    c.Participants.SecondParticipantId.Value,
                                                                                                    c.MessageIds.Select(mi => mi.Value)))));


            config.NewConfig<CreateConversationResult, CreateConversationResponse>()
                .ConstructUsing(src => new CreateConversationResponse(src.Conversation.Id.Value,
                                                              src.Conversation.Participants.FirstParticipantId.Value,
                                                              src.Conversation.Participants.SecondParticipantId.Value,
                                                              src.Conversation.CreatedDateTime));
        }

        public void RegisterQueriesCommandsMapping(TypeAdapterConfig config)
        {
            config.NewConfig<Guid, GetConversationQuery>()
                .Map(dest => dest.ConversationId, src => src)
                .MapToConstructor(true);

            config.NewConfig<(int page, int entriesPerPage), GetConversationsQuery>()
             .Map(dest => dest.Page, src => src.page)
             .Map(dest => dest.EntriesPerPage, src => src.entriesPerPage)
             .MapToConstructor(true);

            config.NewConfig<Guid, DeleteConversationCommand>()
                .Map(dest => dest.ConversationId, src => src)
                .MapToConstructor(true);

            config.NewConfig<(Guid firstParticipantId, CreateConversationRequest request), CreateConversationCommand>()
                .Map(dest => dest.FirstParticipantId, src => src.firstParticipantId)
                .Map(dest => dest.SecondParticipantId, src => src.request.OtherParticipantId)
                .Map(dest => dest.FirstMessageContent, src => src.request.FirstMessageContent)
                .MapToConstructor(true);
        }
    }
}
