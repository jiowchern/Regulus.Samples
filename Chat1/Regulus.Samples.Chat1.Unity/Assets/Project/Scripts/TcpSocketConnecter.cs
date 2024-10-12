using System.Threading.Tasks;

namespace Regulus.Remote.Unity
{
    public class TcpSocketConnector : Connector  
    {
        readonly Regulus.Network.Tcp.Connector _Connector;
        readonly Regulus.Network.IStreamable _Stream;
        public TcpSocketConnector()
        {
            _Connector = new Network.Tcp.Connector();
            _Stream = _Connector;
        }
        public override async void Connect(string address)
        {
            var result = System.Text.RegularExpressions.Regex.Match(address , "(\\d+\\.\\d+\\.\\d+\\.\\d+):(\\d+)");
            if (!result.Success)
                return ;
            var ip = result.Groups[1].Value;
            var port = int.Parse(result.Groups[2].Value);
            await _Connector.Connect(new System.Net.IPEndPoint(System.Net.IPAddress.Parse(ip), port));
        }

        public override void Disconnect()
        {
            _Connector.Disconnect();
        }

        protected override Remote.IWaitableValue<int> _Receive(byte[] buffer, int offset, int count)
        {
            
            return _Stream.Receive(buffer,offset,count);
        }

        protected override Remote.IWaitableValue<int> _Send(byte[] buffer, int offset, int count)
        {
            return _Stream.Send(buffer, offset, count);
        }
    }
}
