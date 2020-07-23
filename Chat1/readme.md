# Sample
The following example shows how to create a chat project.



## Create Protocol
```
dotnet run --project $(SolutionDir)/Regulus/Regulus.Application.Protocol.CodeWriter  --common $(TargetPath) --output $(SolutionDir)\Chat1\Regulus.Samples.Chat1.Protocol
```

## Run Console 
Client  
```
C:\Regulus.Samples> dotnet run --project .\Chat1\Regulus.Samples.Chat1.Client\ -- --protocolfile .\Chat1\Regulus.Samples.Chat1.Client\bin\Debug\netcoreapp3.1\Regulus.Samples.Chat1.Protocol.dll
```
Server  
```
C:\Regulus.Samples> dotnet run --project .\Chat1\Regulus.Samples.Chat1.Server\ -- --protocolfile .\Chat1\Regulus.Samples.Chat1.Server\bin\Debug\netcoreapp3.1\Regulus.Samples.Chat1.Protocol.dll --port 53771
```

## In Unity

Import library
```
Essential library
C:\Regulus.Samples\Chat1\Regulus.Samples.Chat1.Unity> dotnet publish ../../Regulus/Regulus.Remote.Client -o ./Assets/Project/Server.Library
Chat1's Protocol
C:\Regulus.Samples\Chat1\Regulus.Samples.Chat1.Unity> dotnet publish ../Regulus.Samples.Chat1.Protocol -o ./Assets/Project/Server.Library
```
Create Agent
```csharp
// Regulus.Samples\Chat1\Regulus.Samples.Chat1.Unity\Assets\Project\Scripts\Chat1Agent.cs
public class Chat1Agent : MonoBehaviour
{
    public readonly Regulus.Remote.IAgent Agent;
    public Chat1Agent()
    {
        // get from Regulus.Samples.Chat1.Protocol.dll
        var protocolType = Regulus.Remote.Protocol.ProtocolProvider.GetProtocols().Single();
        var protocol = System.Activator.CreateInstance(protocolType) as Regulus.Remote.IProtocol;
        var agent = Regulus.Remote.Client.AgentProvider.CreateTcp(protocol);
        Agent = agent;
    }
}

```

Get Connect
```csharp
// Regulus.Samples\Chat1\Regulus.Samples.Chat1.Unity\Assets\Project\Scripts\Chat1Connect.cs
public class Chat1Connect : MonoBehaviour
{
    void Start()
    {        
        var agent = Chat1Agent.FindAgent();
        if (agent == null)
            return;
        // regist IConnect
        agent.QueryNotifier<IConnect>().Supply += _Show;
        agent.QueryNotifier<IConnect>().Unsupply += _Hide;
    }

    private void OnDestroy()
    {
        var agent = Chat1Agent.FindAgent();
        if (agent == null)
            return;
        // unregist IConnect
        agent.QueryNotifier<IConnect>().Supply -= _Show;
        agent.QueryNotifier<IConnect>().Unsupply -= _Hide;
    }

    private void _Show(IConnect connect)
    {
        Panel.SetActive(true);
        System.Action onConnect = () => {
            // call connect to connect ...
            connect.Connect(_GetEndPoint()).OnValue += _ConnectResult;
        };
        _OnConnect = onConnect;
    }
}
```
Receive Message
```csharp
// Regulus.Samples\Chat1\Regulus.Samples.Chat1.Unity\Assets\Project\Scripts\Chat1MessageView.cs
public class Chat1MessageView : MonoBehaviour
{
    void Start()
    {
        var agent = Chat1Agent.FindAgent();
        if (agent == null)
            return;
        agent.QueryNotifier<Regulus.Samples.Chat1.Common.IBroadcastable>().Supply += _Show;
        agent.QueryNotifier<Regulus.Samples.Chat1.Common.IBroadcastable>().Unsupply += _Hide;
    }
    private void OnDestroy()
    {
        var agent = Chat1Agent.FindAgent();
        if (agent == null)
            return;
        agent.QueryNotifier<Regulus.Samples.Chat1.Common.IBroadcastable>().Supply -= _Show;
        agent.QueryNotifier<Regulus.Samples.Chat1.Common.IBroadcastable>().Unsupply -= _Hide;
    }
    private void _Show(IBroadcastable broadcastable)
    {
        broadcastable.MessageEvent += _PushMessage;
    }
    private void _PushMessage(string name, string message)
    {
        _Lines.Add($"{name}:{message}");
        if(_Lines.Count > 20)
        {
            _Lines.RemoveAt(20-1);
        }
        Message.text = string.Join("\n", _Lines);
    }
}
```
Send Message
```csharp
// Regulus.Samples\Chat1\Regulus.Samples.Chat1.Unity\Assets\Project\Scripts\Chat1MessageSend.cs
public class Chat1MessageSend : MonoBehaviour
{
    void Start()
    {
        var agent = Chat1Agent.FindAgent();
        if (agent == null)
            return;
        agent.QueryNotifier<Regulus.Samples.Chat1.Common.IChatable>().Supply += _Show;
        agent.QueryNotifier<Regulus.Samples.Chat1.Common.IChatable>().Unsupply += _Hide;
    }
    private void OnDestroy()
    {
        var agent = Chat1Agent.FindAgent();
        if (agent == null)
            return;
        agent.QueryNotifier<Regulus.Samples.Chat1.Common.IChatable>().Supply -= _Show;
        agent.QueryNotifier<Regulus.Samples.Chat1.Common.IChatable>().Unsupply -= _Hide;
    }
    private void _Show(IChatable obj)
    {
        _Send = () =>{ obj.Send(Name.text, Message.text); Message.text = ""; };
        Panel.SetActive(true);
    }
}
```