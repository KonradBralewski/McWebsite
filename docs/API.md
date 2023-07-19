# :shark: API :shark:
## Authentication

$\color{orange}{POST}$ Auth/Login
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
$\color{orange}{POST}$ Auth/Register

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


$\color{green}{GET}$ API/GameServers/{gameServerId}
```json
Response
{
        "Id" : "ddf16b84-a2ae-47f1-833f-00c89cc22961",
        "MaximumPlayersNumber" : 50,
        "CurrentPlayersNumber" : 4,
        "ServerLocation" : "EU",
        "ServerType" : "PVE",
        "Description" : "Nothing else matters..."
}
```

$\color{green}{GET}$ API/GameServers?page=1&entriesPerPage=15
```json
Response
{
    "gameServers": [
        {
            "id": "2112e968-1cea-4b28-8f4f-a41c2124e0e9",
            "maximumPlayersNumber": 4500,
            "currentPlayersNumber": 300,
            "serverLocation": "Europe",
            "serverType": "Vanilla",
            "description": "Vanilla REGULAR server. Nothing special. We sometimes leak data."
        },
        {
            "id": "ed7d347c-434c-483c-8061-bd51fcb8a611",
            "maximumPlayersNumber": 1500,
            "currentPlayersNumber": 0,
            "serverLocation": "Antarctica",
            "serverType": "Unknown",
            "description": "Amazing!"
        }...
    ]
}
```
$\color{orange}{POST}$ API/GameServers
```json
Request
{
    "MaximumPlayersNumber" : 9000,
    "ServerLocation" : "Europe",
    "ServerType" : "Paper",
    "Description" : "New and fresh gaming experience!"
}
```
```json
Response
{
    "id": "f881f356-685d-489e-9b3a-888e346d174d",
    "maximumPlayersNumber": 9000,
    "serverLocation": "Europe",
    "serverType": "Paper",
    "description": "New and fresh gaming experience!",
    "createdDateTime": "2023-07-19T12:49:19.2668523Z"
}
```
$\color{lightblue}{PATCH}$ API/GameServers/{gameServerId}
```json
Request
{
    "MaximumPlayersNumber" : 4500,
    "CurrentPlayersNumber" : 300,
    "ServerLocation" : "Europe",
    "ServerType" : "Vanilla",
    "Description" : "Vanilla REGULAR server. Nothing special. We sometimes leak data."
}
```
```json
Response
{
    "id": "2112e968-1cea-4b28-8f4f-a41c2124e0e9",
    "maximumPlayersNumber": 4500,
    "currentPlayersNumber": 300,
    "serverLocation": "Europe",
    "serverType": "Vanilla",
    "description": "Vanilla REGULAR server. Nothing special. We sometimes leak data."
}
```
$\color{red}{DELETE}$ API/GameServers/{gameServerId}
```json
204 NO CONTENT
```

## GameServersReports

$\color{green}{GET}$ API/GameServersReports/{gameServerReportId}
```json
Response
{
    "id": "04942369-393b-4588-9257-58d370767962",
    "reportedGameServerId": "ddf16b84-a2ae-47f1-833f-00c89cc22961",
    "reportType": "DataLoss",
    "reportDescription": "Second report description!!",
    "reportDate": "2023-07-14T10:46:07.645857"
}
```

$\color{green}{GET}$ API/GameServersReports?page=1&entriesPerPage=15
```json
Response
{
    "gameServersReports": [
        {
            "id": "beecf322-811b-4d3c-a331-f409000d7074",
            "reportedGameServerId": "ddf16b84-a2ae-47f1-833f-00c89cc22961",
            "reportType": "DataLoss",
            "reportDescription": "First report description!!",
            "reportDate": "2023-07-14T10:40:28.554431"
        },
        {
            "id": "04942369-393b-4588-9257-58d370767962",
            "reportedGameServerId": "ddf16b84-a2ae-47f1-833f-00c89cc22961",
            "reportType": "DataLoss",
            "reportDescription": "Second report description!!",
            "reportDate": "2023-07-14T10:46:07.645857"
        }...
    ]
}
```
$\color{orange}{POST}$ API/GameServersReports
```json
Request
{
    "ReportedGameServerId" : "ddf16b84-a2ae-47f1-833f-00c89cc22961",
    "ReportType" : "DataLoss",
    "ReportDescription" : "I lost my money!"
}
```
```json
Response
{
    "id": "ff9e282c-738b-4e62-8d06-556c8de73a8c",
    "reportedGameServerId": "ddf16b84-a2ae-47f1-833f-00c89cc22961",
    "reportType": "DataLoss",
    "reportDescription": "I lost my money!",
    "reportDateTime": "2023-07-19T12:55:14.2931282Z"
}
```
$\color{lightblue}{PATCH}$ API/GameServersReports/{gameServerReportId}
```json
Request
{
    "ReportedGameServerId" : "ddf16b84-a2ae-47f1-833f-00c89cc22961",
    "ReportType" : "Other",
    "ReportDescription" : "I still did not get refund."
}
```
```json
Response
{
    "id": "ff9e282c-738b-4e62-8d06-556c8de73a8c",
    "reportedGameServerId": "ddf16b84-a2ae-47f1-833f-00c89cc22961",
    "reportType": "Other",
    "reportDescription": "I still did not get refund.",
    "reportDateTime": "2023-07-19T12:55:14.2931282"
}
```
$\color{red}{DELETE}$ API/GameServersReports/{gameServerReportId}
```json
204 NO CONTENT
```

## GameServersSubscriptions

$\color{green}{GET}$ API/GameServersSubscriptions/{gameServerSubscriptionId}
```json
Response
{
    "id": "1ef085ac-bef3-4a91-951c-c1a448a394dc",
    "gameServerId": "2112e968-1cea-4b28-8f4f-a41c2124e0e9",
    "subscriptionType": "Pro",
    "inGameSubscriptionId": 123456789,
    "price": 5000,
    "subscriptionDescription": "First game server subscription.",
    "subscriptionDuration": "05:00:00",
    "createdDateTime": "2023-07-15T14:45:30.3154332"
}
```

$\color{green}{GET}$ API/GameServersSubscriptions?page=1&entriesPerPage=15
```json
Response
{
    "gameServersSubscriptions": [
        {
            "id": "1ef085ac-bef3-4a91-951c-c1a448a394dc",
            "gameServerId": "2112e968-1cea-4b28-8f4f-a41c2124e0e9",
            "subscriptionType": "Pro",
            "inGameSubscriptionId": 123456789,
            "price": 5000,
            "subscriptionDescription": "First game server subscription.",
            "subscriptionDuration": "05:00:00",
            "createdDateTime": "2023-07-15T14:45:30.3154332"
        },
        {
            "id": "0ad91b98-5c1a-43b5-9599-1762bfe37090",
            "gameServerId": "2112e968-1cea-4b28-8f4f-a41c2124e0e9",
            "subscriptionType": "Basic",
            "inGameSubscriptionId": 4895,
            "price": 500,
            "subscriptionDescription": "Basic SUBSCRIPTION",
            "subscriptionDuration": "05:00:00",
            "createdDateTime": "2023-07-19T13:11:27.3230938"
        }...
    ]
}
```
$\color{orange}{POST}$ API/GameServersSubscriptions
```json
Request
{
    "GameServerId" : "2112e968-1cea-4b28-8f4f-a41c2124e0e9",
    "SubscriptionType" : "Basic",
    "InGameSubscriptionId" : 4895,
    "Price" : 500,
    "SubscriptionDescription" : "Basic SUBSCRIPTION",
    "SubscriptionDuration" : "05:00:00"
}
```
```json
Response
{
    "id": "0ad91b98-5c1a-43b5-9599-1762bfe37090",
    "gameServerId": "2112e968-1cea-4b28-8f4f-a41c2124e0e9",
    "subscriptionType": "Basic",
    "inGameSubscriptionId": 4895,
    "price": 500,
    "subscriptionDescription": "Basic SUBSCRIPTION",
    "subscriptionDuration": "05:00:00",
    "createdDateTime": "2023-07-19T13:11:27.3230938Z"
}
```
$\color{lightblue}{PATCH}$ API/GameServersSubscriptions/{gameServerSubscriptionId}
```json
Request
{
    "GameServerId" : "2112e968-1cea-4b28-8f4f-a41c2124e0e9",
    "SubscriptionType" : "Pro",
    "InGameSubscriptionId" : 1008,
    "Price" : 5500,
    "SubscriptionDescription" : "PRO SUBSCRIPTION - many new items and maps.",
    "SubscriptionDuration" : "04:00:00"
}
```
```json
Response
{
    "id": "1ef085ac-bef3-4a91-951c-c1a448a394dc",
    "gameServerId": "2112e968-1cea-4b28-8f4f-a41c2124e0e9",
    "subscriptionType": "Pro",
    "inGameSubscriptionId": 1008,
    "price": 5500,
    "subscriptionDescription": "PRO SUBSCRIPTION - many new items and maps.",
    "subscriptionDuration": "04:00:00",
    "createdDateTime": "2023-07-15T14:45:30.3154332"
}
```
$\color{red}{DELETE}$ API/GameServersSubscriptions/{gameServerSubscriptionId}
```json
204 NO CONTENT
```

## InGameEvents

$\color{green}{GET}$ API/InGameEvents/{inGameEventId}
```json
Response
{
    "id": "aaaf0cec-f5b6-4842-8854-9ea2ebaccb5e",
    "gameServerId": "2112e968-1cea-4b28-8f4f-a41c2124e0e9",
    "inGameId": 1,
    "inGameEventType": "CombatEvent",
    "description": "First CombatEvent inGameEvent",
    "price": 5000
}
```

$\color{green}{GET}$ API/InGameEvents?page=1&entriesPerPage=15
```json
Response
{
    "inGameEvents": [
        {
            "id": "aaaf0cec-f5b6-4842-8854-9ea2ebaccb5e",
            "gameServerId": "2112e968-1cea-4b28-8f4f-a41c2124e0e9",
            "inGameId": 1,
            "inGameEventType": "CombatEvent",
            "description": "First CombatEvent inGameEvent",
            "price": 5000
        },
        {
            "id": "e837c84a-ea47-4ccc-8cd8-1010f4b83eec",
            "gameServerId": "2112e968-1cea-4b28-8f4f-a41c2124e0e9",
            "inGameId": 2,
            "inGameEventType": "CombatEvent",
            "description": "Second CombatEvent inGameEvent",
            "price": 10000
        }...
    ]
}
```
$\color{orange}{POST}$ API/InGameEvents
```json
Request
{
    "GameServerId" : "2112e968-1cea-4b28-8f4f-a41c2124e0e9",
    "InGameId" : 1324,
    "InGameEventType" : "CombatEvent",
    "Description" : "Enhanced Monster Strength event",
    "Price" : 5000
}
```
```json
Response
{
    "id": "78f090c7-37d2-4d90-a5fc-ecade61cf4cc",
    "gameServerId": "2112e968-1cea-4b28-8f4f-a41c2124e0e9",
    "inGameId": 1324,
    "inGameEventType": "CombatEvent",
    "description": "Enhanced Monster Strength event",
    "price": 5000,
    "createdDateTime": "2023-07-19T13:16:47.8333043Z"
}
```
$\color{lightblue}{PATCH}$ API/InGameEvents/{inGameEventId}
```json
Request
{
    "GameServerId" : "2112e968-1cea-4b28-8f4f-a41c2124e0e9",
    "InGameId" : 9980,
    "InGameEventType" : "CombatEvent",
    "Description" : "Third CombatEvent inGameEvent",
    "Price" : 15000
}
```
```json
Response
{
    "id": "fad6c489-684b-4e5a-8c29-8219326769a3",
    "gameServerId": "2112e968-1cea-4b28-8f4f-a41c2124e0e9",
    "inGameId": 9980,
    "inGameEventType": "CombatEvent",
    "description": "Third CombatEvent inGameEvent",
    "price": 15000,
    "updatedDateTime": "2023-07-19T13:17:36.1725225Z"
}
```
$\color{red}{DELETE}$ API/InGameEvents/{inGameEventId}
```json
204 NO CONTENT
```

## InGameEventOrders

$\color{green}{GET}$ API/InGameEventOrders/{inGameEventOrderId}
```json
Response
{
    "id": "7cd55e44-d87d-48fd-960e-3a0b4e417cc4",
    "buyingUserId": "270e610a-7295-4771-910f-e8a65327a54e",
    "boughtInGameEventId": "aaaf0cec-f5b6-4842-8854-9ea2ebaccb5e"
}
```

$\color{green}{GET}$ API/InGameEventOrders?page=1&entriesPerPage=15
```json
Response
{
    "inGameEventOrders": [
        {
            "id": "7cd55e44-d87d-48fd-960e-3a0b4e417cc4",
            "buyingUserId": "270e610a-7295-4771-910f-e8a65327a54e",
            "boughtInGameEventId": "aaaf0cec-f5b6-4842-8854-9ea2ebaccb5e"
        },
        {
            "id": "c3ba4acb-0c62-46ee-bcc5-67d864d546dd",
            "buyingUserId": "270e610a-7295-4771-910f-e8a65327a54e",
            "boughtInGameEventId": "78f090c7-37d2-4d90-a5fc-ecade61cf4cc"
        }...
    ]
}
```
$\color{orange}{POST}$ API/InGameEventOrders
```json
Request
{
    "BoughtInGameEventId" : "78f090c7-37d2-4d90-a5fc-ecade61cf4cc"
}
```
```json
Response
{
    "id": "c3ba4acb-0c62-46ee-bcc5-67d864d546dd",
    "buyingUserId": "270e610a-7295-4771-910f-e8a65327a54e",
    "boughtInGameEventId": "78f090c7-37d2-4d90-a5fc-ecade61cf4cc",
    "orderDate": "2023-07-19T13:19:39.8979093Z"
}
```
$\color{lightblue}{PATCH}$ API/InGameEventOrders/{inGameEventOrderId}
```json
Request
{
   "BoughtInGameEventId" : "fad6c489-684b-4e5a-8c29-8219326769a3"
}
```
```json
Response
{
    "id": "7cd55e44-d87d-48fd-960e-3a0b4e417cc4",
    "buyingUserId": "270e610a-7295-4771-910f-e8a65327a54e",
    "boughtInGameEventId": "fad6c489-684b-4e5a-8c29-8219326769a3",
    "updatedDateTime": "2023-07-19T13:21:23.0306426Z"
}
```
$\color{red}{DELETE}$ API/InGameEventOrders/{inGameEventOrderId}
```json
204 NO CONTENT
```

## Messages

$\color{green}{GET}$ API/Messages/{messageId}
```json
Response
{
    "id": "1851fcbc-0ae6-4286-8b2a-5ad349e8458d",
    "conversationId": "5dd45a1d-4ecd-4d88-b83d-268684e9e402",
    "receiverId": "17d87e90-7b1c-4c10-b8de-da2fab7b6a2b",
    "shipperId": "270e610a-7295-4771-910f-e8a65327a54e",
    "messageContent": "Hey!"
}
```

$\color{green}{GET}$ API/Messages?page=1&entriesPerPage=15
```json
Response
{
    "messages": [
        {
            "id": "1851fcbc-0ae6-4286-8b2a-5ad349e8458d",
            "conversationId": "5dd45a1d-4ecd-4d88-b83d-268684e9e402",
            "receiverId": "17d87e90-7b1c-4c10-b8de-da2fab7b6a2b",
            "shipperId": "270e610a-7295-4771-910f-e8a65327a54e",
            "messageContent": "Hey!"
        },
        {
            "id": "3a3b5849-2a8c-414f-8f0d-4fcc40877385",
            "conversationId": "5dd45a1d-4ecd-4d88-b83d-268684e9e402",
            "receiverId": "17d87e90-7b1c-4c10-b8de-da2fab7b6a2b",
            "shipperId": "270e610a-7295-4771-910f-e8a65327a54e",
            "messageContent": "Hey! Please respond!!"
        }...
    ]
}
```
$\color{orange}{POST}$ API/Messages
```json
Request
{
    "ReceiverId" : "17d87e90-7b1c-4c10-b8de-da2fab7b6a2b",
    "MessageContent" : "I heard about something interesting..."
}
```
```json
Response
{
    "id": "ff2e5f35-c6f1-4093-a4af-8bb30456d48d",
    "conversationId": "5dd45a1d-4ecd-4d88-b83d-268684e9e402",
    "receiverId": "17d87e90-7b1c-4c10-b8de-da2fab7b6a2b",
    "shipperId": "270e610a-7295-4771-910f-e8a65327a54e",
    "messageContent": "I heard about something interesting...",
    "sentDateTime": "2023-07-19T13:28:09.5608585Z"
}
```
$\color{lightblue}{PATCH}$ API/Messages/{messageId}
```json
Request
{
    "MessageContent" : "Hey! Please respond!!!!!"
}
```
```json
Response
{
    "id": "3a3b5849-2a8c-414f-8f0d-4fcc40877385",
    "conversationId": "5dd45a1d-4ecd-4d88-b83d-268684e9e402",
    "receiverId": "17d87e90-7b1c-4c10-b8de-da2fab7b6a2b",
    "shipperId": "270e610a-7295-4771-910f-e8a65327a54e",
    "messageContent": "Hey! Please respond!!!!!",
    "updatedDateTime": "2023-07-19T13:28:39.5757171Z"
}
```
$\color{red}{DELETE}$ API/Messages/{messageId}
```json
204 NO CONTENT
```

## Conversations

$\color{green}{GET}$ API/Conversations/{conversationId}
```json
Response
{
    "id": "5dd45a1d-4ecd-4d88-b83d-268684e9e402",
    "firstParticipant": "270e610a-7295-4771-910f-e8a65327a54e",
    "secondParticipant": "17d87e90-7b1c-4c10-b8de-da2fab7b6a2b",
    "messages": [
        {
            "id": "1851fcbc-0ae6-4286-8b2a-5ad349e8458d",
            "conversationId": "5dd45a1d-4ecd-4d88-b83d-268684e9e402",
            "receiverId": "17d87e90-7b1c-4c10-b8de-da2fab7b6a2b",
            "shipperId": "270e610a-7295-4771-910f-e8a65327a54e",
            "messageContent": "Hey!"
        },
        {
            "id": "3a3b5849-2a8c-414f-8f0d-4fcc40877385",
            "conversationId": "5dd45a1d-4ecd-4d88-b83d-268684e9e402",
            "receiverId": "17d87e90-7b1c-4c10-b8de-da2fab7b6a2b",
            "shipperId": "270e610a-7295-4771-910f-e8a65327a54e",
            "messageContent": "Hey! Please respond!!!!!"
        },
    ]
}
```

$\color{green}{GET}$ API/Conversations?page=1&entriesPerPage=15
```json
Response
{
    "conversations": [
        {
            "id": "5dd45a1d-4ecd-4d88-b83d-268684e9e402",
            "firstParticipant": "270e610a-7295-4771-910f-e8a65327a54e",
            "secondParticipant": "17d87e90-7b1c-4c10-b8de-da2fab7b6a2b",
            "conversationMessagesId": [
                "1851fcbc-0ae6-4286-8b2a-5ad349e8458d",
                "3a3b5849-2a8c-414f-8f0d-4fcc40877385",
                "1bf2707c-1eeb-48a2-82bb-d1f86350a14b",
                "7b6fdf0d-f722-482f-a314-5143d4e8e5f5",
                "d1f96c3c-fce9-4493-91ae-e1a8a90dbdca",
                "737dd735-fffd-4a5f-b2de-69216c26142b",
                "fd326e65-66c6-43bc-b78b-21b153c20a63",
                "3a671df2-f0d2-46aa-ac2b-14a6c3059961",
                "1e0bd3db-5e40-45d2-8935-950a2e24b7b1",
                "51506340-3042-4898-bb37-b271cd413283",
                "0d87d2c7-391c-43fa-92ef-52e20fc575ff",
                "93b31acf-f977-43b2-a1da-59f122eb7420",
                "1ee955e0-ac69-4ea2-b706-dc134fdbb13a",
                "358aabda-4f20-40c4-ab29-e3cfebf668b5",
                "5d6d3f25-1f71-47fc-ad8e-33250be2218a",
                "ff2e5f35-c6f1-4093-a4af-8bb30456d48d"
            ]
        },
        {
            "id": "8eac349e-1f41-45dd-ab1a-e2c7bb0b2184",
            "firstParticipant": "270e610a-7295-4771-910f-e8a65327a54e",
            "secondParticipant": "9638d58d-0904-4a4e-91e3-20232cbd4e5b",
            "conversationMessagesId": [
                "5a2c4a7b-792e-432e-87e9-6027d4f490b1"
            ]
        }
    ]
}
```
$\color{orange}{POST}$ API/Conversations
```json
Request
{
    "OtherParticipantId" : "17d87e90-7b1c-4c10-b8de-da2fab7b6a2b",
    "FirstMessageContent" : "Our first message!"
}
```
```json
Response
{
    "id": "620d037a-f996-4dc3-81f9-611cbcf5298e",
    "firstParticipant": "270e610a-7295-4771-910f-e8a65327a54e",
    "secondParticipant": "17d87e90-7b1c-4c10-b8de-da2fab7b6a2b",
    "createdDateTime": "2023-07-19T13:31:52.0247619Z"
}
```
$\color{red}{DELETE}$ API/Conversations/{conversationId}
```json
204 NO CONTENT
```