### Development Steps
#### Step1. Create projects.
Regulus.Samples.Helloworld.Common  
Regulus.Samples.Helloworld.Protocol  
Regulus.Samples.Helloworld.Client  
Regulus.Samples.Helloworld.Server  
#### Step2. Define a common interface.
```csharp
namespace Regulus.Samples.Helloworld.Common
{
    public class HelloRequest
    {
        public string Name;
    }
    public class HelloReply
    {
        public string Message;
    }
    public interface IGreeter
    {
        Regulus.Remote.Value<HelloReply> SayHello(HelloRequest request);
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
#### Step4. Update the server.
Implement server entry.
```csharp
namespace Regulus.Samples.Helloworld.Server
{
    internal class Entry : Regulus.Remote.IEntry 
    {
        public volatile bool  Enable;

        readonly Greeter _Greeter;
        public Entry()
        {
            _Greeter = new Greeter();
            Enable = true;
        }

        void IBinderProvider.AssignBinder(IBinder binder)
        {
            // IBinder is what you get when your client completes the connection.
            binder.BreakEvent += _End;
            binder.Bind<IGreeter>(_Greeter);
            // unbind : binder.Unbind<IGreeter>(_Greeter);
        }

        private void _End()
        {
            Enable = false;
        }

        void IBootable.Launch()
        {
            Console.WriteLine("Server launch.");
        }

        void IBootable.Shutdown()
        {
            Console.WriteLine("Server shutdown.");
        }
    }
}
```
Implement the Greeter.
```csharp
namespace Regulus.Samples.Helloworld.Server
{
    class Greeter : IGreeter
    {
        Value<HelloReply> IGreeter.SayHello(HelloRequest request)
        {
            return new HelloReply() { Message = $"Hello {request.Name}." };
        }
    }
}
```
Create server.
```csharp
static void Main(string[] args)
{
    int port = int.Parse(args[0]);
    var protocolAsm = Assembly.LoadFrom("Regulus.Samples.Helloworld.Protocol.dll");
    var protocol = Regulus.Remote.Protocol.ProtocolProvider.Create(protocolAsm);

    var greeter = new Entry();
    var service = Regulus.Remote.Server.ServiceProvider.CreateTcp(greeter, port, protocol);
    service.Launch();
    while (greeter.Enable)
    {
        System.Threading.Thread.Sleep(0);
    }
    service.Shutdown();
    System.Console.WriteLine($"Press any key to end.");
    System.Console.ReadKey();
}
```
#### Step5. Update the client.
Create client.
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
    agent.QueryNotifier<Common.IGreeter>().Supply += (greeter)=> {
        String user = "you";
        greeter.SayHello(new HelloRequest() { Name = user}).OnValue += _GetReply;
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


