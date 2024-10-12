using Regulus.Utility.WindowConsoleAppliction;
using Regulus.Samples.Chat1.Common;
using System.Linq;

namespace Regulus.Samples.Chat1.Server
{
    class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tcpport"></param>
        /// <param name="webport"></param>
        static void Main(int tcpport, int webport)
        {
            Regulus.Utility.Log.Instance.RecordEvent += (message) => System.Console.WriteLine(message);
            var protocol = Regulus.Samples.Chat1.Common.ProtocolCreater.Create();
            var room = new Regulus.Samples.Chat1.Entry();


            var listener = new Listener();
            
            var Closes = new System.Collections.Generic.List<System.Action>();
            if(tcpport != 0)
            {
                System.Console.WriteLine($"Tcp port : {tcpport}");
                var tcp = new Regulus.Remote.Server.Tcp.Listener();                
                listener.Add(tcp);
                tcp.Bind(tcpport);
                Closes.Add(()=> tcp.Close());
            }
                
            if(webport != 0)
            {
                System.Console.WriteLine($"Web port : {webport}");
                var web = new Regulus.Remote.Server.Web.Listener();
                listener.Add(web);
                web.Bind($"http://*:{webport}/");
                Closes.Add(() => web.Close());
            }
            var service = Regulus.Remote.Server.Provider.CreateService(room, protocol, listener);
            var console = new Console(room.Announcement);
            console.Run();
            foreach(var action in Closes)
            {
                action();
            }
            service.Dispose();
        }

       
    }
}
