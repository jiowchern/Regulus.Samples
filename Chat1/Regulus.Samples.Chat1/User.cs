using Regulus.Remote;
using System;

namespace Regulus.Samples.Chat1
{
    internal class User
    {
        public readonly IBinder Binder;
        private readonly Room _Room;

        readonly Regulus.Utility.StageMachine _Machine;
        public User(IBinder binder, Room room)
        {
            this.Binder = binder;
            this._Room = room;
            _Machine = new Utility.StageMachine();

            _ToLogin();
        }

        private void _ToLogin()
        {
            var stage = new UserLogin(Binder);
            stage.DoneEvent += _ToChat;
            _Machine.Push(stage);
        }

        private void _ToChat(string name)
        {
            
            var stage = new UserChat(Binder, _Room, name);
            stage.DoneEvent += _ToLogin;
            _Machine.Push(stage);
        }

        public void Dispose()
        {
            _Machine.Clean();
        }
    }
}