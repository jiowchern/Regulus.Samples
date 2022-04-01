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
        /// <param name="standaloneport"></param>
        /// <param name="webport"></param>
        static void Main(int standaloneport, string webport)
        {

            var protocol = Regulus.Samples.Chat1.Common.ProtocolCreater.Create();
            var room = new Regulus.Samples.Chat1.Service();


            var listener = new Listener();
            var service = Regulus.Remote.Server.Provider.CreateService(room, protocol, listener);

            listener.Tcp.Bind(standaloneport);
            listener.Web.Bind($"http://*:{webport}/");
            var console = new Console();
            console.Run();
            listener.Tcp.Close();
            listener.Web.Close();
            service.Dispose();
        }

       
    }
}
