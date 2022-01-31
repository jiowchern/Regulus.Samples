using Regulus.Remote;
using Regulus.Samples.Chat1.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Regulus.Samples.Chat1
{
    internal class Room  : IHistroyable
    {
        readonly ItemNotifier<Chatter> _Chatters;
        readonly System.Collections.Generic.List<Message> _Messages;
        public INotifier<Chatter> Chatters => _Chatters;
        public readonly IHistroyable Histroyable;
        public Room()
        {
            Histroyable = this;
            _Messages = new System.Collections.Generic.List<Message>();
            _Chatters = new ItemNotifier<Chatter>();
        }
        internal Chatter RegistChatter(IMessageable messageable)
        {            
            var chatter = new Chatter(messageable , (msg)=> _Broadcast(msg , messageable));
            lock(_Chatters)
            {
                _Chatters.Add(chatter);                
            }
                
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
                var m = new Message() { Name = sender.Name, Context = message };
                
                lock(_Messages)
                {
                    if (_Messages.Count > 10)
                    {
                        _Messages.RemoveAt(0);
                    }
                    _Messages.Add(m);
                }
                
                foreach (var chatter in _Chatters.Items)
                {
                    chatter.Messager.PublicReceive(m);
                }
            }

            
        }

        public void Dispose()
        {
            lock(_Chatters)
                _Chatters.Clear();
        }

        Message[] IHistroyable.Query()
        {
            Message[] result = new Message[0];
            lock(_Messages)
            {
                result = _Messages.ToArray();
            }

            return result;
        }
    }
}