using Regulus.Remote.Server.Tcp;
using Regulus.Remote.Soul;
using System;
namespace Regulus.Samples.Chat1.Server
{
    class Console : Regulus.Utility.WindowConsole
    {
        private readonly Announceable _Announcement;

        public Console(Announceable announcement)
        {
            _Announcement = announcement;
            
        }
        

        protected override void _Launch()
        {
            Command.Register<string, string>("Announce", _Announce);
        }


        protected override void _Shutdown()
        {
            Command.Unregister("Announce");
        }

        protected override void _Update()
        {
            
        }
        private void _Announce(string name, string message)
        {
            _Announcement.Announce(name, message);
        }
    }
}
