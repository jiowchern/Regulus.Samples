using Regulus.Remote;
using Regulus.Samples.Chat1.Common;
using System;

namespace Regulus.Samples.Chat1.Test
{
    public class Chatter : IBinder
    {
        public readonly System.Collections.Generic.List<System.Tuple<string, string>> Messages;
        public Regulus.Samples.Chat1.Common.IChatable Chatable;
        public IBroadcastable Broadcastable;
        public Chatter()
        {
            Messages = new System.Collections.Generic.List<Tuple<string, string>>();
        }
        event Action IBinder.BreakEvent
        {
            add
            {
                
            }

            remove
            {
                
            }
        }

        void IBinder.Bind<TSoul>(TSoul soul)
        {
            if (typeof(TSoul) == typeof(Regulus.Samples.Chat1.Common.IChatable))
            {
                Chatable = (Regulus.Samples.Chat1.Common.IChatable)soul;
            }
            if (typeof(TSoul) == typeof(Regulus.Samples.Chat1.Common.IBroadcastable))
            {
                Broadcastable = (Regulus.Samples.Chat1.Common.IBroadcastable)soul;
                Broadcastable.MessageEvent += (name, message) => {
                    Messages.Add(new System.Tuple<string, string>(name, message));
                };
            }
        }

        void IBinder.Return<TSoul>(TSoul soul)
        {
            
        }

        void IBinder.Unbind<TSoul>(TSoul soul)
        {
        }
    }
}
