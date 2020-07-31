
using Regulus.Framework;
using Regulus.Remote;
using Regulus.Samples.Chat1.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Regulus.Samples.Chat1
{
    public class Service : Regulus.Remote.IEntry
    {
        readonly Room _Room;
        readonly List<User> _User;
        public Service()
        {
            _Room = new Room();
            _User = new List<User>();
        }
        void IBinderProvider.AssignBinder(IBinder binder)
        {
            User user = new User(binder, _Room);
            binder.BreakEvent += () =>
            {
                user.Dispose();
                _User.Remove(user);
            };
            _User.Add(user);
        }

        void IBootable.Launch()
        {
            
        }

        void IBootable.Shutdown()
        {
            _Room.Dispose();
        }
    }
}
