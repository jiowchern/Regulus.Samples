using Regulus.Remote;
using Regulus.Remote.Ghost;
using Regulus.Samples.Chat1.Common;
using Regulus.Utility;
using System;

namespace Regulus.Samples.Chat1.Client
{    

    internal class ChatroomStatus : IStatus
    {

        readonly IPlayer _Player;
        
        private readonly Command _Command;
        
        public ChatroomStatus(IPlayer player, Command command)
        {
            _Player = player;        
            this._Command = command;
        }

        void IStatus.Enter()
        {

            _PlayerEnter(_Player);


        }

        void IStatus.Leave()
        {

            _PlayerLeave(_Player);
        }

        private void _PlayerLeave(IPlayer player)
        {
            _Command.Unregister("send");
            _Command.Unregister("exitroom");

            player.Chatters.Supply -= _ChatterEnter;
            player.Chatters.Unsupply -= _ChatterLeave;

            player.PrivateMessageEvent -= _PrivateMessage;
            player.PublicMessageEvent -= _PublicMessage;


        }

        private void _PlayerEnter(IPlayer player)
        {            

            player.PrivateMessageEvent += _PrivateMessage;
            player.PublicMessageEvent += _PublicMessage;

            player.Chatters.Supply += _ChatterEnter;
            player.Chatters.Unsupply += _ChatterLeave;

            _Command.Register<string>("send", player.Send);
            _Command.Register("exitroom", player.Quit);
        }

        private void _PublicMessage(string name, string message)
        {
            System.Console.WriteLine($"{name}:{message}");
        }

        private void _PrivateMessage(string name, string message)
        {
            System.Console.WriteLine($"[private]{name}:{message}");
        }

        private void _ChatterLeave(IChatter chatter)
        {
            System.Console.WriteLine($"{chatter.Name.Value} leave.");
            _Command.Register<string>($"to.{chatter.Name.Value}", chatter.Whisper);
        }

        private void _ChatterEnter(IChatter chatter)
        {
            _Command.Unregister($"to.{chatter.Name.Value}");
            System.Console.WriteLine($"{chatter.Name.Value} enter.");
        }

        void IStatus.Update()
        {
            
        }

        
    }
}