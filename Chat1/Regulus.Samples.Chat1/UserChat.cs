using Regulus.Utility;
using Regulus.Remote;
using Regulus.Samples.Chat1.Common;
using System;
using System.Linq;


namespace Regulus.Samples.Chat1
{
    internal class UserChat : IBootable , Common.IPlayer , IMessageable
    {
        private IBinder _Binder;
        private Room _Room;
        private ISoul _This;
        readonly Chatter _Chatter;
        readonly string _Name;
        
        readonly Regulus.Remote.NotifiableCollection<IChatter> _Chatters;

        Regulus.Remote.Notifier<IChatter> IPlayer.Chatters => new Remote.Notifier<IChatter>(_Chatters);

        string IMessageable.Name => _Name;

        public event System.Action DoneEvent;
        public UserChat(IBinder binder, Room room, string name)
        {
            _Binder = binder;
            _Room = room;
            _Name = name;
            _Chatters = new Regulus.Remote.NotifiableCollection<IChatter>();
            _Chatter = _Room.RegistChatter(this);
        }

        event Action<string, string> _PublicMessageEvent;
        event Action<string, string> IPlayer.PublicMessageEvent
        {
            add
            {
                _PublicMessageEvent += value;
            }

            remove
            {
                _PublicMessageEvent -= value;
            }
        }

        event Action<string, string> _PrivateMessageEvent;
        event Action<string, string> IPlayer.PrivateMessageEvent
        {
            add
            {
                _PrivateMessageEvent += value;
            }

            remove
            {
                _PrivateMessageEvent -= value;
            }
        }

        

        void IPlayer.Quit()
        {
            DoneEvent();
        }

        void IPlayer.Send(string message)
        {
            _Chatter.Send(message);
        }

        void IBootable.Launch()
        {
            _Chatters.Items.Clear();
            if (_Chatter == null)
            {
                DoneEvent();
                return;
            }
            _Room.Chatters.Supply += _Add;
            _Room.Chatters.Unsupply += _Leave;

            _This = _Binder.Bind<IPlayer>(this);
        }

        private void _Leave(Chatter chatter)
        {
            var whispeableChatter = _Chatters.Items.FirstOrDefault(i => i.Name == chatter.Messager.Name);
            if (whispeableChatter != null)
                _Chatters.Items.Remove(whispeableChatter);
        }

        private void _Add(Chatter chatter)
        {
            var whispeableChatter = new WhispeableChatter(_Chatter, chatter);
            _Chatters.Items.Add(whispeableChatter);
        }

        void IBootable.Shutdown()
        {
            _Binder.Unbind(_This);

            _Room.Chatters.Supply += _Add;
            _Room.Chatters.Unsupply += _Leave;

            _Room.UnregistChatter(_Chatter);
        }

        void IMessageable.PublicReceive(string name, string message)
        {
            _PublicMessageEvent(name,message);
        }

        void IMessageable.PrivateReceive(string name, string message)
        {
            _PrivateMessageEvent(name, message);
        }
    }
}