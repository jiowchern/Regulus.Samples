
using Regulus.Remote;
using Regulus.Samples.Helloworld.Common;
using System;

namespace Regulus.Samples.Helloworld.Server
{
    internal class Entry : Regulus.Remote.IEntry 
    {
        public volatile bool Enable;

        readonly Greeter _Greeter;
        public Entry()
        {
            _Greeter = new Greeter();
            Enable = true;
        }

        void IBinderProvider.RegisterClientBinder(Regulus.Remote.IBinder binder)
        {
            // IBinder is what you get when your client completes the connection.            
            var soul = binder.Bind<IGreeter>(_Greeter);
            // unbind : binder.Unbind<IGreeter>(soul);
        }
        void IBinderProvider.UnregisterClientBinder(Regulus.Remote.IBinder binder)
        {
            _End();
        }

        private void _End()
        {
            Enable = false;
        }

        void IEntry.Update()
        {
         
        }
    }
}