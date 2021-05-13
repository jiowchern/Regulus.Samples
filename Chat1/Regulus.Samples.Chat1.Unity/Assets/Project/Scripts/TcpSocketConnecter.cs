using System.Threading.Tasks;

namespace Regulus.Remote.Unity
{
    public class TcpSocketConnecter : Connecter
    {
        readonly Regulus.Network.Tcp.Connecter _Connecter;
        readonly Regulus.Network.IConnectable _Connectable;
        public TcpSocketConnecter()
        {
            _Connecter = new Network.Tcp.Connecter();
            _Connectable = _Connecter;
        }
        public override async void Connect(string address)
        {
            var result = System.Text.RegularExpressions.Regex.Match(address , "(\\d+\\.\\d+\\.\\d+\\.\\d+):(\\d+)");
            if (!result.Success)
                return ;
            var ip = result.Groups[1].Value;
            var port = int.Parse(result.Groups[2].Value);
            await _Connectable.Connect(new System.Net.IPEndPoint(System.Net.IPAddress.Parse(ip), port));
        }

        public override void Disconnect()
        {
            _Connectable.Disconnect();
        }

        protected override Network.IWaitableValue<int> _Receive(byte[] buffer, int offset, int count)
        {
            return _Connectable.Receive(buffer,offset,count);
        }

        protected override Network.IWaitableValue<int> _Send(byte[] buffer, int offset, int count)
        {
            return _Connectable.Send(buffer, offset, count);
        }
    }
}
