# Leader Board System

## Setup

Na classe `LeaderBoardRequest` estão as configurações do serviço de LeaderBoardApi.

```csharp
private const string Protocol = "https";
private const string Host = "localhost";
private const string Port = "44376";
```

Essa classe utiliza o `WebRequests` desenvolvido no namespace `Assets.UnityFoundation.Code.Web`.

Para realizar as requisições à API é necessário importar o `leaderboards_request_pf` para a cena desejada.