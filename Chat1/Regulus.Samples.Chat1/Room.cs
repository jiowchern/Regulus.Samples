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
            var hasName = _Chatters.Items.Where(c=>c.Messager.Name == messageable.Name).Any();
            if (hasName)
                return null;
            var chatter = new Chatter(messageable , (msg)=> _Broadcast(msg , messageable) );
            _Chatters.Add(chatter);
            return chatter;
        }

        internal void UnregistChatter(Chatter chatter)
        {
            _Chatters.Remove(chatter);
        }

        void _Broadcast(string message , IMessageable sender)
        {
            foreach (var chatter  in _Chatters.Items)
            {
                chatter.Messager.PublicReceive(sender.Name, message);
            }
        }

        public void Dispose()
        {
            _Chatters.Clear();
        }
    }
}