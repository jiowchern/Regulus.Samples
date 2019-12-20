
using Regulus.Framework;
using Regulus.Remote;
using Regulus.Samples.Chat1.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Regulus.Samples.Chat1.Server
{
    public class Room : Regulus.Remote.IEntry , IChatable , IBroadcastable
    {
        readonly List<IBinder> _Chatters;
        
        public Room()
        {
            _Chatters = new List<IBinder>();
        }

        public event Action<string, string> MessageEvent;
        event Action<string, string> IBroadcastable.MessageEvent
        {
            add
            {
                MessageEvent += value;
            }

            remove
            {
                MessageEvent -= value;
            }
        }

        void IBinderProvider.AssignBinder(IBinder binder)
        {
            var chatter = binder;
            binder.BreakEvent += () => {

                Leave(chatter);                
            };
            Join(chatter);            
        }

        public void Leave(IBinder chatter)
        {
            chatter.Unbind<IChatable>(this);
            chatter.Unbind<IBroadcastable>(this);
            _Chatters.RemoveAll( b => chatter == b);
        }

        public void Join(IBinder chatter)
        {

            _Chatters.Add(chatter);
            chatter.Bind<IChatable>(this);
            chatter.Bind<IBroadcastable>(this);
        }

        

        void IBootable.Launch()
        {
        }

        void IBootable.Shutdown()
        {
        }

        void IChatable.Send(string name, string message)
        {
            MessageEvent.Invoke(name , message);
        }
    }
}
