using McWebsite.API.Contracts.Message;

namespace McWebsite.API.Contracts.Conversation
{
    public sealed record CreateConversationResponse(Guid Id,
                                               Guid FirstParticipant,
                                               Guid SecondParticipant,
                                               List<GetMessageResponse> Messages,
                                               DateTime CreatedDateTime);

}
