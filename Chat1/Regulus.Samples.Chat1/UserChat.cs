﻿using Regulus.Utiliey;
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
        readonly Chatter _Chatter;
        readonly string _Name;
        readonly ItemNotifier<IChatter> _Chatters;

        INotifier<IChatter> IPlayer.Chatters => _Chatters;

        string IMessageable.Name => _Name;

        public event System.Action DoneEvent;
        public UserChat(IBinder binder, Room room, string name)
        {
            _Binder = binder;
            _Room = room;
            _Name = name;
            _Chatters = new ItemNotifier<IChatter>();
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
            _Chatters.Clear();
            if (_Chatter == null)
            {
                DoneEvent();
                return;
            }
            _Room.Chatters.Supply += _Add;
            _Room.Chatters.Unsupply += _Leave;

            _Binder.Bind<IPlayer>(this);
        }

        private void _Leave(Chatter chatter)
        {
            var whispeableChatter = _Chatters.Items.FirstOrDefault(i => i.Name == chatter.Messager.Name);
            if (whispeableChatter != null)
                _Chatters.Remove(whispeableChatter);
        }

        private void _Add(Chatter chatter)
        {
            var whispeableChatter = new WhispeableChatter(_Chatter, chatter);
            _Chatters.Add(whispeableChatter);
        }

        void IBootable.Shutdown()
        {
            _Binder.Unbind<IPlayer>(this);

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