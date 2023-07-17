using Mapster;
using McWebsite.API.Contracts.Conversation;
using McWebsite.API.Contracts.Message;
using McWebsite.Application.Conversations.Commands.CreateConversationCommand;
using McWebsite.Application.Conversations.Commands.DeleteConversationCommand;
using McWebsite.Application.Conversations.Queries.GetConversationQuery;
using McWebsite.Application.Conversations.Queries.GetConversationsQuery;
using McWebsite.Domain.Conversation;

namespace McWebsite.API.Common.Mapping
{
    public class ConversationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            RegisterQueriesCommandsMapping(config);

            config.NewConfig<GetConversationResult, GetConversationResponse>()
                .ConstructUsing(src => new GetConversationResponse(src.Conversation.Id.Value,
                                                                   src.Conversation.Participants.FirstParticipant.Value,
                                                                   src.Conversation.Participants.SecondParticipant.Value,
                                                                   src.ConversationMessages.Select(m => m.Adapt<GetMessageResponse>())));

            config.NewConfig<GetConversationsResult, GetConversationsResponse>()
                .Map(dest => dest, src => src.Conversations.Select(c => new SingleConversationEntry(c.Id.Value,
                                                                                                    c.Participants.FirstParticipant.Value,
                                                                                                    c.Participants.SecondParticipant.Value,
                                                                                                    c.MessageIds.Select(mId => mId.Value))))
                .MapToConstructor(true);


            //config.NewConfig<CreateConversationResult, CreateConversationResponse>()
            //    .ConstructUsing(src => new CreateConversationResponse(src.Conversation.Id.Value,
            //                                                  src.Conversation.Participants.FirstParticipant.Value,
            //                                                  src.Conversation.Participants.SecondParticipant.Value,
            //                                                  src.Conversation.ShipperId.Value,
            //                                                  src.Conversation.ConversationContent,
            //                                                  src.Conversation.SentDateTime));
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

            config.NewConfig<(Guid FirstParticipant, CreateConversationRequest request), CreateConversationCommand>()
                .Map(dest => dest.FirstParticipant, src => src.FirstParticipant)
                .Map(dest => dest.SecondParticipant, src => src.request.OtherParticipant)
                .Map(dest => dest.FirstMessageContent, src => src.request.FirstMessageContent)
                .MapToConstructor(true);
        }
    }
}
