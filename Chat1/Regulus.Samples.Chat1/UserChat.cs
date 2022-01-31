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
        
        readonly string _Name;
        
        readonly Regulus.Remote.NotifiableCollection<IChatter> _Chatters;
        private ISoul _This;
        Chatter _Chatter;
        Regulus.Remote.Notifier<IChatter> IPlayer.Chatters => new Remote.Notifier<IChatter>(_Chatters);

        string IMessageable.Name => _Name;

        

        public event System.Action DoneEvent;
        public UserChat(IBinder binder, Room room, string name)
        {
            _Binder = binder;
            _Room = room;
            _Name = name;
            _Chatters = new Regulus.Remote.NotifiableCollection<IChatter>();            
            _PublicMessageEvent += (m) => { };
            _PrivateMessageEvent += (m) => { };
        }

        event Action<Common.Message> _PublicMessageEvent;
        event Action<Common.Message> IPlayer.PublicMessageEvent
        {
            add
            {
                _PublicMessageEvent += value;
                foreach (var msg in _Room.Histroyable.Query())
                {
                    value(msg);
                }
            }

            remove
            {
                _PublicMessageEvent -= value;
            }
        }

        event Action<Common.Message> _PrivateMessageEvent;
        event Action<Common.Message> IPlayer.PrivateMessageEvent
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
            
            _This = _Binder.Bind<IPlayer>(this);
            _Chatter = _Room.RegistChatter(this);
            _Chatters.Items.Clear();

            _Room.Chatters.Supply += _Add;
            _Room.Chatters.Unsupply += _Leave;
            
            
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
            _Room.UnregistChatter(_Chatter);
            _Binder.Unbind(_This);

            _Room.Chatters.Supply -= _Add;
            _Room.Chatters.Unsupply -= _Leave;

            
        }

        void IMessageable.PublicReceive(Common.Message msg)
        {
            _PublicMessageEvent(msg);
        }

        void IMessageable.PrivateReceive(Common.Message msg)
        {
            _PrivateMessageEvent(msg);
        }
    }
}