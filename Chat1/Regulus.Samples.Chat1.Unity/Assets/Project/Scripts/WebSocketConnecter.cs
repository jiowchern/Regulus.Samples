using Regulus.Remote.Client.Tcp;
using System;
using System.Threading.Tasks;
using NativeWebSocket;

namespace Regulus.Remote.Unity
{
    public class WebSocketConnecter : Connecter
    {

        NativeWebSocket.WebSocket _Socket;

        readonly System.Collections.Concurrent.ConcurrentQueue<byte> _Reads;
        public event System.Action CloseEvent; 
        public WebSocketConnecter()
        {
            CloseEvent += () => { };
            _Socket = new WebSocket("ws://127.0.0.1:1111");
            _Reads = new System.Collections.Concurrent.ConcurrentQueue<byte>();
            
        }
        private void Update()
        {
            _Socket.DispatchMessageQueue();
        }


        private void _Close(WebSocketCloseCode closeCode)
        {

            CloseEvent();
        }

        private void _Message(byte[] data)
        {
            foreach (var b in data)
            {
                _Reads.Enqueue(b);
            }
            
        }

        private void _Error(string error)
        {
            throw new NotImplementedException();
        }

        private void _Open()
        {
            
        }

        protected override Task<int> _Receive(byte[] buffer, int offset, int count)
        {
            return Task<int>.Factory.StartNew(() => {

                byte data;
                int i = 0;
                while (_Reads.TryDequeue(out data))
                {
                    buffer[offset + i++] = data;
                    if (i == count)
                        break;
                }
                return i;
            });
        }

        protected override Task<int> _Send(byte[] buffer, int offset, int count)
        {
            return Task<int>.Factory.StartNew(() => {
                byte[] buf = new byte[count];

                for (int i = 0; i < count; i++)
                {
                    buf[i] = buffer[offset + i];
                }
                _Socket.Send(buf);
                return count;
            });
            
        }

        public override Task<bool> Connect(string address)
        {
            var result = System.Text.RegularExpressions.Regex.Match(address, "([\\w\\.]+):(\\d+)");
            if (!result.Success)
                return Task<bool>.FromResult(false);
            var ip = result.Groups[1].Value;
            var port = int.Parse(result.Groups[2].Value);


            Disconnect();
            _Socket = new WebSocket($"ws://{ip}:{port}");
            _Socket.OnOpen += _Open;
            _Socket.OnError += _Error;
            _Socket.OnMessage += _Message;
            _Socket.OnClose += _Close;
            _Socket.Connect();

            return null;
        }

        public override void Disconnect()
        {
            _Socket.OnOpen -= _Open;
            _Socket.OnError -= _Error;
            _Socket.OnMessage -= _Message;
            _Socket.OnClose -= _Close;
            _Socket.Close();
        }
    }
}
