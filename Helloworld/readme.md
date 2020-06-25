### Development Steps
#### Step1. Create projects.
Regulus.Samples.Helloworld.Common  
Regulus.Samples.Helloworld.Protocol  
Regulus.Samples.Helloworld.Client  
Regulus.Samples.Helloworld.Server  
#### Step2. Define a common interface.
```csharp
using System;

namespace Regulus.Samples.Helloworld.Common
{    
    public interface IEcho
    {
        Regulus.Remote.Value<string> Speak(string message);
    }
}
```
#### Step3. Generate protocols.
Directory.Build.targets
```xml
<Project>
    <Target Condition="'$(ProjectName)'=='Regulus.Samples.Helloworld.Common' And '$(SolutionDir)'!='*Undefined*'" Name="CreateProtocol" AfterTargets="Build">
        <Exec Command="del $(SolutionDir)Helloworld\Regulus.Samples.Helloworld.Protocol\*.cs /q"/>
        <Exec Command="dotnet run --project $(SolutionDir)/Regulus/Regulus.Application.Protocol.CodeWriter  --common $(TargetPath) --output $(SolutionDir)\Helloworld\Regulus.Samples.Helloworld.Protocol" />
    </Target>   
</Project>
```
#### Step4. Implement client and server.
Client  
```csharp
static void Main(string[] args)
{
    var ip = IPAddress.Parse(args[0]);
    var port = int.Parse(args[1]);
    var protocolAsm = System.Reflection.Assembly.LoadFrom("Regulus.Samples.Helloworld.Protocol.dll");
    var protocol = Regulus.Remote.Protocol.ProtocolProvider.Create(protocolAsm);
    var agent = Regulus.Remote.Client.AgentProvider.CreateTcp(protocolAsm) ;
    agent.Launch();
           
    agent.QueryNotifier<IConnect>().Supply += (connect) => {
        System.Console.WriteLine($"Connect to {ip}:{port} ... ");
        connect.Connect(new IPEndPoint(ip, port));
    };
    agent.QueryNotifier<Common.IEcho>().Supply += (echo)=> {
        System.Console.WriteLine($"Send message :Hello World!");
        echo.Speak("Hello World!").OnValue += _GetEcho;
    };
    while (Enable)
    {
        System.Threading.Thread.Sleep(0);
        agent.Update();
    }
            
    agent.Shutdown();
    System.Console.WriteLine($"Press any key to end.");
    System.Console.ReadKey();
}
```
Server  
```csharp
static void Main(string[] args)
{
    int port = int.Parse(args[0]);
    var protocolAsm = Assembly.LoadFrom("Regulus.Samples.Helloworld.Protocol.dll");
    var protocol = Regulus.Remote.Protocol.ProtocolProvider.Create(protocolAsm);

    var echo = new Echo();
    var service = Regulus.Remote.Server.ServiceProvider.CreateTcp(echo, port, protocol);
    service.Launch();
    while (echo.Enable)
    {
        System.Threading.Thread.Sleep(0);
    }
    service.Shutdown();
    System.Console.WriteLine($"Press any key to end.");
    System.Console.ReadKey();
}
```


