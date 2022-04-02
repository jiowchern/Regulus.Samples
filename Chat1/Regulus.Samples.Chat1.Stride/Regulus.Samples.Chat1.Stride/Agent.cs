using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stride.Core.Mathematics;
using Stride.Input;
using Stride.Engine;
using Regulus.Remote.Client;
using System.Diagnostics;
using Stride.UI.Controls;
using Stride.UI;
using System;

namespace Regulus.Samples.Chat1.Stride
{

    public class Agent : SyncScript
    {        
        TcpConnectSet _TcpSet;
        Regulus.Utility.StatusMachine _Machine;
        public global::Stride.Engine.UIComponent Connect;
        public global::Stride.Engine.UIComponent Message;
        public global::Stride.Engine.UIComponent Login;
        public global::Stride.Engine.UIComponent Room;
        
         
        public UILibrary Library { get; set; }
        global::Stride.Engine.UIComponent[] _Components;
        

        public Agent()
        {
            
            _Machine = new Regulus.Utility.StatusMachine();
        }
        
        public override void Start()
        {
            _Components = new UIComponent[]
            {
                Connect,
                Message,
                Login,
                Room,
            };

            foreach (var component in _Components)
            {
                component.Page.RootElement.Visibility = Visibility.Hidden;
            }

            var protocol = Regulus.Samples.Chat1.Common.ProtocolCreater.Create();
            _TcpSet = Regulus.Remote.Client.Provider.CreateTcpAgent(protocol);
            base.Start();
            _ToBuildIPAddress();
        }

        
        
        public override void Update()
        {
            _TcpSet.Agent.Update();
            _Machine.Update();
        }

        public override void Cancel()
        {

            base.Cancel();
        }


        private void _ToBuildIPAddress()
        {
            var state = new BuildIpAddressStatus(Connect);
            state.SuccessEvent += _ToConnect;
            state.FailEvent += () => { _ToMessageState("Please enter the correct IPAddress format.", _ToBuildIPAddress); };
            _Machine.Push(state);
        }

        private void _ToConnect(System.Net.EndPoint point)
        {
            var state = new ConnectStatus(_TcpSet.Connecter.Connect(point).GetAwaiter());
            state.SuccessEvent += _ToChatRoomLogin;
            state.FailEvent += () => { _ToMessageState("Connect fail.", _ToBuildIPAddress); }; ;
            _Machine.Push(state);
        }

        private void _ToChatRoomLogin()
        {
            var state = new ChatRoomLoginStatus(_TcpSet.Agent , Login);
            state.SuccessEvent += _ToChatRoom;
            state.FailEvent += _ToDisconnect;
            _Machine.Push(state);
        }

        private void _ToChatRoom()
        {
            var state = new ChatRoomPlayStatus(_TcpSet.Agent, Room, Library);
            state.DoneEvent += _ToChatRoomLogin;
            _Machine.Push(state);
        }

        private void _ToDisconnect()
        {
            _TcpSet.Connecter.Disconnect();
            _ToMessageState("login fail" , _ToBuildIPAddress);
        }

        void _ToMessageState(string message , System.Action next)
        {            
            var state = new MessageStatus(Message, message);
            state.DoneEvent += next;
            _Machine.Push(state);
            
        }
    }
}
