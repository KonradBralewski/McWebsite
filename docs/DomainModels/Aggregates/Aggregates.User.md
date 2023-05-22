```csharp
public class User : AggregateRoot<UserId>
{
	public UserId Id { get; set; }
	public MinecraftAccountId McId {get; set;}
	public string Email { get; set; }
	public string Password { get; set; }
}
```

```json
{
	"Id" : "",
	"MinecraftAccountId" : "",
	"Email": "",
	"Password": "",
	"UpdatedDateTime" : "",
	"UpdatedBy" : ""
}
```