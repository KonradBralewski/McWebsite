# :shark: API :shark:
## Authentication
### Login Request
```json
Request BODY
{
	"email": "xyz@gmail.com",
	"password": "xyzXYZxyz"
}
```
```json
Response
{
        "UserId" : 000-000-000-000-000,
        "UserEmail" : "xyz@gmail.com",
        "Token" : "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c"
}
```
### Register Request

```json
Request BODY
{
	"email": "xyz@gmail.com",
	"password": "xyzXYZxyz"
}
```
```json
Response
{
        "UserId" : 000-000-000-000-000,
        "UserEmail" : "xyz@gmail.com",
        "Token" : "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c"
}
```

## GameServers

### API/GameServers?page=1&entriesPerPage=15
```json
Response
{
        "GameServers" : 
        [
            "GameServer" : 
              [
                  "Id" : 0000-0000-0000-0000,
                  "ServerLocation" : "EU",
                  "MaximumPlayersNumber" : 50,
                  "ServerType" : "PVE",
                  "Description" "Nothing else matters..."
                  
              ],
              [
              ...
              ]
        ]
}
```
### API/GameServers/(gameServerId)
```json
Response
{
        "Id" : 0000-0000-0000-0000,
        "ServerLocation" : "EU",
        "MaximumPlayersNumber" : 50,
        "ServerType" : "PVE",
        "Description" "Nothing else matters..."
}
```