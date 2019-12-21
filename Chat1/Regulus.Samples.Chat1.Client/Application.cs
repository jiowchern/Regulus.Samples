using System;
using Regulus.Remote;
using Regulus.Samples.Chat1.Common;

namespace Regulus.Samples.Chat1.Client
{
    class Application : Regulus.Utility.WindowConsole
    {
        private readonly IAgent _Agent;
        private readonly System.Net.IPEndPoint _IPEndPoint;

        readonly string _Name;

        public Application(string name,System.Net.IPEndPoint ipendpoint)
        {
            _Name = name;
            _IPEndPoint = ipendpoint;
            var commonAssembly = System.Reflection.Assembly.LoadFile(System.IO.Path.GetFullPath("Regulus.Samples.Chat1.Common.dll"));
            _Agent = Regulus.Remote.Client.JIT.AgentProivder.CreateTcp(Remote.Protocol.Essential.CreateFromDomain(commonAssembly)); 
        }

        

        protected override void _Launch()
        {
            _Agent.Launch();
            _Agent.Connect(_IPEndPoint).OnValue += (result) =>
            {
                if(!result )
                    Command.Run("quit", new string[0]);
            };
            _Agent.QueryNotifier<Regulus.Samples.Chat1.Common.IChatable>().Supply += _SupplyChat;
            _Agent.QueryNotifier<Regulus.Samples.Chat1.Common.IBroadcastable>().Supply += _SupplyBroadcast;
        }
        void  _SupplyBroadcast(Regulus.Samples.Chat1.Common.IBroadcastable broadcastable)
        {
            broadcastable.MessageEvent += (name ,message) =>
            {
                Console.WriteLine($"{name}:{message}");
            };
        }
        private void _SupplyChat(IChatable chatter)
        {
            Command.Register< string>("Send", (message) => {
                chatter.Send(_Name, message);
                Console.WriteLine();
            });
            
        }

        protected override void _Shutdown()
        {
            _Agent.Shutdown();
        }

        protected override void _Update()
        {
            _Agent.Update();
        }
    }
}
