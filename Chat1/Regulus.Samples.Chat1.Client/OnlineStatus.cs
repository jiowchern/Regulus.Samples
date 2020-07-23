using Regulus.Remote;
using Regulus.Samples.Chat1.Common;
using Regulus.Utility;
using System;

namespace Regulus.Samples.Chat1.Client
{
    internal class OnlineStatus : IStatus
    {
        private readonly IAgent _Agent;
        private readonly IOnline _Online;
        private readonly Command _Command;

        public OnlineStatus(IAgent agent, IOnline online, Command command)
        {
            _Agent = agent;
            this._Online = online;
            this._Command = command;
        }

        void IStatus.Enter()
        {
            _Command.Register("ping" , () => System.Console.WriteLine($"ping:{_Online.Ping}") );
            _Command.Register("disconnect", () => _Online.Disconnect() );
            _Agent.QueryNotifier<Regulus.Samples.Chat1.Common.IBroadcastable>().Supply += _Broadcast;
            _Agent.QueryNotifier<Regulus.Samples.Chat1.Common.IChatable>().Supply += _Chat;
        }

        private void _Chat(IChatable chatable)
        {
            _Command.Register<string,string>("send", (n,m) => chatable.Send(n,m));
        }

        private void _Broadcast(IBroadcastable broadcastable)
        {
            broadcastable.MessageEvent += (n,m)=>System.Console.WriteLine($"{n}:{m}");
        }

        void IStatus.Leave()
        {
            _Command.Unregister("disconnect");
            _Command.Unregister("ping");
            _Command.Unregister("send");
        }

        void IStatus.Update()
        {
            
        }
    }
}