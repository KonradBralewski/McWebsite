```csharp
public class Message : AggregateRoot<MessageId>
{
	public ConversationId ConversationId {get; set;}
	public UserId ReceiverId {get; set;}
	public UserId ShipperId {get; set;}
	public string Description { get; set; }
	piblic DateTime SentDateTime {get; set;}
}
```

```json
{
	"Id" : "",
	"ConversationId" : "",
	"ReceiverId": "",
	"ShipperId": "",
	"Description" : "",
	"SentDateTime" : "",
	"UpdatedDateTime" : "",
	"UpdatedBy" : ""
}
```