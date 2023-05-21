```csharp
public class InGameEvent : AggregateRoot<InGameEventId>
{
	public InGameEventId Id { get; set; }
	public int BuyingPlayerId { get; set; }
	public int InGameId { get; set; }
	public string InGameEventType { get; set; }
	public string Description { get; set; }
}
```

```json
{
	"Id" : "",
	"BuyingPlayerId" : "",
	"InGameId" : "",
	"InGameEventType" : ""
	"Description" : "",
	"UpdatedDateTime" : "",
	"UpdatedBy" : ""
}
```