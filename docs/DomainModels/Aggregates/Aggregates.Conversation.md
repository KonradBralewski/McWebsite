```csharp
public class User : AggregateRoot<UserId>
{
	public Tuple<UserId, UserId> Performers { get; set; }
	public ReadOnlyCollection<Message> Messages { get; set; }
}
```

```json
{
	"Id" : "",
	"Performers" : {
		"First" : "",
		"Second" : ""
	},
	"Messages": {
		"Message" : {...},
		"Message" : {...},
	},
	"UpdatedDateTime" : "",
	"UpdatedBy" : ""
}
```