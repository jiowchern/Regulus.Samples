﻿using Regulus.Remote;
using System.Collections.Generic;

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
            Regulus.Utility.Log.Instance.WriteInfo("aaa");
            Regulus.Utility.Log.Instance.WriteInfo("aaa");
        }
        void IBinderProvider.AssignBinder(IBinder binder,object state)
        {
            
            User user = new User(binder, _Room);
            binder.BreakEvent += () =>
            {
                user.Dispose();
                lock (_User)
                    _User.Remove(user);
            };
            lock(_User)
                _User.Add(user);
        }

        ~Service()
        {
            _Room.Dispose();
        }        
    }
}
