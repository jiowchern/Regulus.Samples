using Regulus.Remote;
using System;

namespace Regulus.Samples.Chat1
{
    internal class User
    {
        private readonly IBinder _Binder;
        private readonly Room _Room;

        readonly Regulus.Utility.StageMachine _Machine;
        public User(IBinder binder, Room room)
        {
            this._Binder = binder;
            this._Room = room;
            _Machine = new Utility.StageMachine();

            _ToLogin();
        }

        private void _ToLogin()
        {
            var stage = new UserLogin(_Binder);
            stage.DoneEvent += _ToChat;
            _Machine.Push(stage);
        }

        private void _ToChat(string name)
        {
            
            var stage = new UserChat(_Binder, _Room, name);
            stage.DoneEvent += _ToLogin;
            _Machine.Push(stage);
        }

        public void Dispose()
        {
            _Machine.Clean();
        }
    }
}