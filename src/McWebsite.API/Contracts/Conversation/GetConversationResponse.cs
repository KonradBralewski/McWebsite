using McWebsite.API.Contracts.Message;

namespace McWebsite.API.Contracts.Conversation
{
    public sealed record GetConversationResponse(Guid Id,
                                               Guid FirstParticipant,
                                               Guid SecondParticipant,
                                               List<GetMessageResponse> Messages);
}
