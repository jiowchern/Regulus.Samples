using Regulus.Remote;
using Regulus.Samples.Chat1.Common;
using Regulus.Utility;
using System;

namespace Regulus.Samples.Chat1.Client
{
    internal class ChatStatus : Regulus.Utility.IStatus
    {
        private IPlayer _Player;
        
        private Command _Command;

        public ChatStatus(IPlayer player, Command command)
        {
            _Player = player;
            
            _Command = command;
        }

        void IStatus.Enter()
        {
            _Player.Chatters.Base.Supply += _AddChatter;
            _Player.Chatters.Base.Unsupply += _RemoveChatter;
            _RegistPlayer();
        }

        void IStatus.Update()
        {

        }
        void IStatus.Leave()
        {
            _UnegistPlayer();
            _Player.Chatters.Base.Supply -= _AddChatter;
            _Player.Chatters.Base.Unsupply -= _RemoveChatter;
        }

        private void _UnegistPlayer()
        {
            _Command.Unregister("send");
            _Player.PublicMessageEvent += _PublicMessage;
            _Player.PrivateMessageEvent += _PrivateMessage;
        }

        private void _RegistPlayer()
        {
            _Command.Register<string>("send", msg => _Player.Send(msg));
            _Player.PublicMessageEvent += _PublicMessage;
            _Player.PrivateMessageEvent += _PrivateMessage;
        }

        private void _PrivateMessage(string name, string message)
        {
            System.Console.WriteLine($"[private]{name}:{message}");
        }

        private void _PublicMessage(string name, string message)
        {
            System.Console.WriteLine($"{name}:{message}");
        }

        private void _RemoveChatter(IChatter cahtter)
        {
            _Command.Unregister($"send-{cahtter.Name.Value}");
        }

        private void _AddChatter(IChatter cahtter)
        {
            _Command.Register<string>($"send-{cahtter.Name.Value}", msg => cahtter.Whisper(msg));
        }
    }
}