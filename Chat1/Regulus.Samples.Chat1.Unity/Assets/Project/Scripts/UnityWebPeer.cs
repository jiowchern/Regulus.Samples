using UnityEngine.Networking;
using UnityEngine;
using WebSocketSharp;
using Regulus.Network; // 假設 IWaitableValue 和 NoWaitValue 定義在這個命名空間

namespace Regulus.Remote.Unity
{
    

    public class UnityWebPeer : Regulus.Remote.Unity.Connector
    {
        private WebSocket _webSocket;
        readonly System.Collections.Concurrent.BlockingCollection<byte> _Receives;
        private bool _isConnected = false;
        public Chat1Agent Agent;

        public UnityWebPeer()
        {
            _Receives = new System.Collections.Concurrent.BlockingCollection<byte>();
        }





        protected override IWaitableValue<int> _Send(byte[] buffer, int offset, int count)
        {
            if (_isConnected && _webSocket != null && _webSocket.IsAlive)
            {
                byte[] dataToSend = new byte[count];
                System.Array.Copy(buffer, offset, dataToSend, 0, count);

                // 發送數據
                _webSocket.Send(dataToSend);

                return new NoWaitValue<int>(count);
            }

            return new NoWaitValue<int>(0); // 返回 0 表示沒有發送
        }

        protected override IWaitableValue<int> _Receive(byte[] buffer, int offset, int count)
        {
            for (int i = 0; i < count; i++)
            {
                buffer[offset + i] = _Receives.Take();
            }
            return new NoWaitValue<int>(count);            
            
        }

        private void OnDestroy()
        {
            Disconnect();
        }

        

        public override void Connect(string address)
        {
            var result = System.Text.RegularExpressions.Regex.Match(address, "([\\w\\.]+):(\\d+)");
            if (!result.Success)
                return;
            var ip = result.Groups[1].Value;
            var port = int.Parse(result.Groups[2].Value);


            //Disconnect();
            

            _webSocket = new WebSocket($"ws://{ip}:{port}");

            // 設置事件處理程序
            _webSocket.OnOpen += (sender, e) =>
            {
                _isConnected = true;
                Debug.Log("WebSocket Connected");
                Agent.ConnectSuccess(this);
            };

            _webSocket.OnMessage += (sender, e) =>
            {
                Debug.Log("Message Received: " + e.RawData);
                for (int i = 0; i < e.RawData.Length; i++)
                {
                    _Receives.Add(e.RawData[i]);
                }

            };

            _webSocket.OnError += (sender, e) =>
            {
                Debug.LogError("WebSocket Error: " + e.Message);
            };

            _webSocket.OnClose += (sender, e) =>
            {
                _isConnected = false;
                Debug.Log("WebSocket Closed");
            };

            // 開始連接
            _webSocket.ConnectAsync();
        }

        public override void Disconnect()
        {
            if (_webSocket != null && _webSocket.IsAlive)
            {
                _webSocket.CloseAsync();
            }
        }
    }

}
