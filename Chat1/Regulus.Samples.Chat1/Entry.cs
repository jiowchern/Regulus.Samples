using Regulus.Remote;
using System.Linq;
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
        void IBinderProvider.RegisterClientBinder(IBinder binder)
        {
            
            User user = new User(binder, _Room);
            
            lock(_User)
                _User.Add(user);
        }
        void IBinderProvider.UnregisterClientBinder(IBinder binder)
        {
            lock (_User)
            {
                var user = _User.First(u=>u.Binder == binder);
                _User.Remove(user);
                user.Dispose();
            }                
        } 

        void IEntry.Update() 
        {
            
        }

        ~Entry()
        {
            _Room.Dispose();
        }        
    }
}
