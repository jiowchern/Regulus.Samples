using Regulus.Remote;
using Regulus.Samples.Chat1.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Regulus.Samples.Chat1
{
    internal class Room 
    {
        readonly ItemNotifier<Chatter> _Chatters;
        public INotifier<Chatter> Chatters => _Chatters;

        public Room()
        {
            _Chatters = new ItemNotifier<Chatter>();
        }
        internal Chatter RegistChatter(IMessageable messageable)
        {            
            var chatter = new Chatter(messageable , (msg)=> _Broadcast(msg , messageable) );
            lock(_Chatters)
                _Chatters.Add(chatter);
            return chatter;
        }

        internal void UnregistChatter(Chatter chatter)
        {
            lock (_Chatters)
                _Chatters.Remove(chatter);
        }

        void _Broadcast(string message , IMessageable sender)
        {
            lock (_Chatters)
            {
                foreach (var chatter in _Chatters.Items)
                {
                    chatter.Messager.PublicReceive(new Message() { Name = sender.Name, Context = message });
                }
            }

            
        }

        public void Dispose()
        {
            lock(_Chatters)
                _Chatters.Clear();
        }
    }
}