namespace McWebsite.API.Contracts.Message
{
    public sealed record GetMessagesResponse(IEnumerable<GetMessageResponse> Messages);
}
