using System;
using Regulus.Network;

namespace Regulus.Samples.Chat1.Server
{
    class Listener : Remote.Soul.IListenable
    {
        
        public readonly Regulus.Remote.Server.Tcp.Listener Tcp;
        public readonly Regulus.Remote.Server.Web.Listener Web;

        readonly System.Collections.Generic.List<Remote.Soul.IListenable> _Listenables;
        public Listener()
        {
            Tcp = new Remote.Server.Tcp.Listener();
            Web = new Remote.Server.Web.Listener();
            _Listenables = new System.Collections.Generic.List<Remote.Soul.IListenable>();
            _Listenables.Add(Tcp);
            _Listenables.Add(Web);
        }
        event Action<IStreamable> Remote.Soul.IListenable.StreamableEnterEvent
        {
            add
            {
                foreach (var listener in _Listenables)
                {
                    listener.StreamableEnterEvent += value;
                }
            }

            remove
            {
                foreach (var listener in _Listenables)
                {
                    listener.StreamableEnterEvent -= value;
                }
            }
        }

        event Action<IStreamable> Remote.Soul.IListenable.StreamableLeaveEvent
        {
            add
            {
                foreach (var listener in _Listenables)
                {
                    listener.StreamableLeaveEvent += value;
                }
            }

            remove
            {
                foreach (var listener in _Listenables)
                {
                    listener.StreamableLeaveEvent -= value;
                }
            }
        }
    }
}
