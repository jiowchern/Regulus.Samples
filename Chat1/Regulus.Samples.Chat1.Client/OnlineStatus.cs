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

        readonly Regulus.Utility.StatusMachine _Machine;

        public OnlineStatus(IAgent agent, IOnline online, Command command)
        {
            _Agent = agent;
            this._Online = online;
            this._Command = command;
            _Machine = new StatusMachine();
        }

        void IStatus.Leave()
        {
            _Agent.QueryNotifier<ILogin>().Supply -= _ToLogin;
            _Agent.QueryNotifier<IPlayer>().Supply -= _ToChat;
            _Command.Unregister("disconnect");
            _Command.Unregister("ping");
            _Machine.Termination();
        }

        void IStatus.Update()
        {
            _Machine.Update();
        }


        void IStatus.Enter()
        {
            _Command.Register("ping" , () => System.Console.WriteLine($"ping:{_Online.Ping}") );
            _Command.Register("disconnect", () => _Online.Disconnect() );

            _Agent.QueryNotifier<ILogin>().Supply += _ToLogin;
            _Agent.QueryNotifier<IPlayer>().Supply += _ToChat;
        }

        private void _ToChat(IPlayer player)
        {
            var status = new ChatStatus(player, _Command);
            _Machine.Push(status);
        }

        private void _ToLogin(ILogin login)
        {
            var status = new LoginStatus(login, _Command);
            _Machine.Push(status);
        }

        
    }
}