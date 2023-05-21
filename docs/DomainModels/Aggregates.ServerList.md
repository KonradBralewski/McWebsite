```csharp
public class GameServer : AggregateRoot<GameServerId>
{
	public GameServerId Id { get; set; }
	public string ServerLocation { get; set; }
	public int MaximumPlayersNumber { get; set; }
	public string ServerType { get; set; }
	public string Description { get; set; }
}
```

```json
{
	"Id" : "",
	"ServerLocation" : "",
	"MaximumPlayersNumber" : "",
	"ServerType" : "",
	"Description" : "",
	"UpdatedDateTime" : "",
	"UpdatedBy" : ""
}
```