using Mapster;
using McWebsite.API.Contracts.Message;
using McWebsite.Application.Messages.Commands.CreateMessageCommand;
using McWebsite.Application.Messages.Commands.DeleteMessageCommand;
using McWebsite.Application.Messages.Commands.UpdateMessageCommand;
using McWebsite.Application.Messages.Queries.GetMessageQuery;
using McWebsite.Application.Messages.Queries.GetMessagesQuery;
using McWebsite.Domain.Message;
using McWebsite.Domain.Message.Entities;

namespace McWebsite.API.Common.Mapping
{
    public class MessageMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            RegisterQueriesCommandsMapping(config);

            config.NewConfig<Message, GetMessageResponse>()
                .ConstructUsing(src => new GetMessageResponse(src.Id.Value,
                                                              src.ConversationId.Value,
                                                              src.ReceiverId.Value,
                                                              src.ShipperId.Value,
                                                              src.MessageContent));

            config.NewConfig<GetMessagesResult, GetMessagesResponse>()
                .Map(dest => dest, src => src.Messages.Select(gsr => gsr.Adapt<GetMessageResponse>()))
                .MapToConstructor(true);

            config.NewConfig<GetMessageResult, GetMessageResponse>()
                .ConstructUsing(src => src.Message.Adapt<GetMessageResponse>());

            config.NewConfig<CreateMessageResult, CreateMessageResponse>()
                .ConstructUsing(src => new CreateMessageResponse(src.Message.Id.Value,
                                                              src.Message.ConversationId.Value,
                                                              src.Message.ReceiverId.Value,
                                                              src.Message.ShipperId.Value,
                                                              src.Message.MessageContent,
                                                              src.Message.SentDateTime));

            config.NewConfig<UpdateMessageResult, UpdateMessageResponse>()
                .ConstructUsing(src => new UpdateMessageResponse(src.Message.Id.Value,
                                                              src.Message.ConversationId.Value,
                                                              src.Message.ReceiverId.Value,
                                                              src.Message.ShipperId.Value,
                                                              src.Message.MessageContent,
                                                              src.Message.UpdatedDateTime));
        }

        public void RegisterQueriesCommandsMapping(TypeAdapterConfig config)
        {
            config.NewConfig<Guid, GetMessageQuery>()
                .Map(dest => dest.MessageId, src => src)
                .MapToConstructor(true);

            config.NewConfig<(int page, int entriesPerPage), GetMessagesQuery>()
             .Map(dest => dest.Page, src => src.page)
             .Map(dest => dest.EntriesPerPage, src => src.entriesPerPage)
             .MapToConstructor(true);

            config.NewConfig<Guid, DeleteMessageCommand>()
                .Map(dest => dest.MessageId, src => src)
                .MapToConstructor(true);

            config.NewConfig<(Guid messageId, UpdateMessageRequest request), UpdateMessageCommand>()
                .Map(dest => dest.MessageId, src => src.messageId)
                .Map(dest => dest.MessageContent, src => src.request.MessageContent)
                .MapToConstructor(true);

            config.NewConfig<(Guid shipperId, CreateMessageRequest request), CreateMessageCommand>()
                .Map(dest => dest.ConversationId, src => src.request.ConversationId)
                .Map(dest => dest.ReceiverId, src => src.request.ReceiverId)
                .Map(dest => dest.ShipperId, src => src.shipperId)
                .Map(dest => dest.MessageContent, src => src.request.MessageContent)
                .MapToConstructor(true);
        }
    }
}
