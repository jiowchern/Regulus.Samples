using Regulus.Remote;
using System.Collections.Generic;

namespace Regulus.Samples.Chat1
{
    public class Entry : Regulus.Remote.IEntry
    {
        readonly Room _Room;
        readonly List<User> _User;

        public readonly Announceable Announcement;
        public Entry()
        {
            _Room = new Room();
            _User = new List<User>();
            Announcement = _Room;
        }
        void IBinderProvider.AssignBinder(IBinder binder)
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

        ~Entry()
        {
            _Room.Dispose();
        }        
    }
}
