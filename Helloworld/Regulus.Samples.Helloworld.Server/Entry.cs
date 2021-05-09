
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

        void IBinderProvider.AssignBinder(IBinder binder,object state)
        {
            // IBinder is what you get when your client completes the connection.
            binder.BreakEvent += _End;
            var soul = binder.Bind<IGreeter>(_Greeter);
            // unbind : binder.Unbind<IGreeter>(soul);
        }

        private void _End()
        {
            Enable = false;
        }
        
    }
}