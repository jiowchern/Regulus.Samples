﻿using System;
using Regulus.Network;

namespace Regulus.Samples.Chat1.Server
{
    class Listener : Remote.Soul.IListenable
    {
        
        

        readonly System.Collections.Generic.List<Remote.Soul.IListenable> _Listenables;
        public Listener()
        {            
            _Listenables = new System.Collections.Generic.List<Remote.Soul.IListenable>();            
        }
        public void Add(Remote.Soul.IListenable listenable)
        {
            _Listenables.Add(listenable);
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
