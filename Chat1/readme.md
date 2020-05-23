# 教學說明
以下範例為介紹如何建立起一個聊天室專案。
## 功能開發
### 1.引入程式庫  
![程式庫](./doc/lib-refs.jpg)  
### 2.建立主專案檔案結構  
![檔案路徑](./doc/files-struct.jpg)  
Common - Client 與 Server 的共通定義。  
Main - 聊天室的主要邏輯。   
Protocol - 空專案，目的是產生共通物件定義的實作。  
## 建立 Protocol
此步驟為建立 Client 與 Server 之間協定程式碼。 執行期需要它。  
使用 **Regulus.Application.Protocol.CodeWriter** 產生程式碼到 Regulus.Samples.Chat1.Protocol 。  
建立 Directory.Build.targets 內容如下。  
``` xml
<Project>
    <Target Condition="'$(ProjectName)'=='Regulus.Samples.Chat1.Common' And '$(SolutionDir)'!='*Undefined*'" Name="CreateProtocol" BeforeTargets="Build">
        <Exec Command="del $(SolutionDir)Chat1\Regulus.Samples.Chat1.Protocol\*.cs /q"/>
        <Exec Command="dotnet run --project $(SolutionDir)/Regulus/Regulus.Application.Protocol.CodeWriter  --common $(TargetPath) --output $(SolutionDir)\Chat1\Regulus.Samples.Chat1.Protocol" />
    </Target>
</Project>
```  
之後當編譯 **Regulus.Samples.Chat1.Common** 時會自動產生程式碼。  
![檔案路徑](./doc/protocol-codes.jpg)  


## Console 執行
Client  
```
C:\Regulus.Samples> dotnet run --project .\Regulus\Regulus.Application.Client --protocol .\Chat1\Regulus.Samples.Chat1.Protocol\bin\Debug\netstandard2.0\Regulus.Samples.Chat1.Protocol.dll
```
Server  
```
C:\Regulus.Samples> dotnet run --project .\Regulus\Regulus.Application.Server --port 53771 --protocol .\Chat1\Regulus.Samples.Chat1.Protocol\bin\Debug\netstandard2.0\Regulus.Samples.Chat1.Protocol.dll --entryname Regulus.Samples.Chat1.Main.Room --entry .\Chat1\Regulus.Samples.Chat1.Main\bin\Debug\netstandard2.0\Regulus.Samples.Chat1.Main.dll
```

## Unity Client

### 1.環境佈署
```
必要開發套件
C:\Regulus.Samples\Chat1\Regulus.Samples.Chat1.Unity> dotnet publish ../../Regulus/Regulus.Remote.Client -o ./Assets/Project/Server.Library
Chat1's Protocol
C:\Regulus.Samples\Chat1\Regulus.Samples.Chat1.Unity> dotnet publish ../Regulus.Samples.Chat1.Protocol -o ./Assets/Project/Server.Library
```
### 2.Create Agent
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

### 3.Get Connect
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
### 4.Receive Message
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
### 5.Send Message
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