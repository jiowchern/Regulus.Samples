using System.Linq;
using System.Reactive.Linq;
using Regulus.Utility;
using Stride.Engine;
using Regulus.Remote.Reactive;
using Stride.UI;
using System;
using Regulus.Remote;
using Regulus.Samples.Chat1.Common;

namespace Regulus.Samples.Chat1.Stride
{
    static class ControlsExtension
    {
        public static IObservable<System.Reactive.EventPattern<TouchEventArgs>> TouchUpObs(this global::Stride.UI.Controls.Button button)
        {
            return System.Reactive.Linq.Observable.FromEventPattern<TouchEventArgs>((h) => button.TouchUp += h, (h) => button.TouchUp -= h);
        }
    }
    internal class ChatRoomPlayStatus : Regulus.Utility.IStatus
    {
        
        readonly ReleaseHelper _ReleaseHelper;
        private readonly UILibrary library;
        public System.Action DoneEvent;
        public ChatRoomPlayStatus(INotifierQueryable queryer, UIComponent room, UILibrary library)
        {
            _ReleaseHelper = new ReleaseHelper();
            room.Page.RootElement.Visibility = Visibility.Visible;

            _ReleaseHelper.Actions.Add(() => {
                room.Page.RootElement.Visibility = Visibility.Hidden;
            });


            var messageContol = room.Page.RootElement.FindName("Message") as global::Stride.UI.Controls.EditText;
            var targetContol = room.Page.RootElement.FindName("Target") as global::Stride.UI.Controls.EditText;
            var sendControl = room.Page.RootElement.FindName("Send") as global::Stride.UI.Controls.Button;
            var quitControl = room.Page.RootElement.FindName("Quit") as global::Stride.UI.Controls.Button;
            var listPanel = room.Page.RootElement.FindName("List") as global::Stride.UI.Panels.Grid;
            



           var sendObs = from handler in sendControl.TouchUpObs()
                          where messageContol.Text.Length > 0 && string.IsNullOrWhiteSpace(targetContol.Text)
                         from player in queryer.QueryNotifier<Regulus.Samples.Chat1.Common.IPlayer>().SupplyEvent()
                          from unit in new System.Action(() => player.Send(messageContol.Text)).ReturnVoid()
                          select unit;

            var sendObsDispose = sendObs.Subscribe(_=> messageContol.Text = "");
            _ReleaseHelper.Actions.Add(() => {
                sendObsDispose.Dispose();
            });

            var privateSendObs = from handler in sendControl.TouchUpObs()
                          where messageContol.Text.Length > 0 && !string.IsNullOrWhiteSpace(targetContol.Text)
                          from player in queryer.QueryNotifier<Regulus.Samples.Chat1.Common.IPlayer>().SupplyEvent()
                          from target in player.Chatters.SupplyEvent()
                          where target.Name.Value == targetContol.Text
                          from unit in new System.Action(() => target.Whisper(messageContol.Text)).ReturnVoid()
                          select unit;

            var privateSendObsDispose = privateSendObs.Subscribe(_ => messageContol.Text = "");
            _ReleaseHelper.Actions.Add(() => {
                privateSendObsDispose.Dispose();
            });


            var quitObs = from handler in quitControl.TouchUpObs().Take(1)
                          from player in queryer.QueryNotifier<Regulus.Samples.Chat1.Common.IPlayer>().SupplyEvent()
                          from unit in new System.Action(() => player.Quit()).ReturnVoid()
                          select unit;

            var quitObsDispose = quitObs.Subscribe(unit=> DoneEvent());
            _ReleaseHelper.Actions.Add(() => {
                quitObsDispose.Dispose();
            });


            var publicMessageObs = 
                          from player in queryer.QueryNotifier<Regulus.Samples.Chat1.Common.IPlayer>().SupplyEvent()
                          from message in Regulus.Remote.Reactive.Extensions.EventObservable<Common.Message>(h => player.PublicMessageEvent+=h , h => player.PublicMessageEvent -= h)
                          select message;

            var publicMessageObsDispose = publicMessageObs.Subscribe(msg => _ReceiveMessage(listPanel , $"{msg.Name}:{msg.Context}"));
            _ReleaseHelper.Actions.Add(() => {
                publicMessageObsDispose.Dispose();
            });

            var privateMessageObs =
                          from player in queryer.QueryNotifier<Regulus.Samples.Chat1.Common.IPlayer>().SupplyEvent()
                          from message in Regulus.Remote.Reactive.Extensions.EventObservable<Common.Message>(h => player.PrivateMessageEvent += h, h => player.PrivateMessageEvent -= h)
                          select message;

            var privateMessageObsDispose = privateMessageObs.Subscribe(msg => _ReceiveMessage(listPanel, $"<private>{msg.Name}:{msg.Context}"));
            _ReleaseHelper.Actions.Add(() => {
                privateMessageObsDispose.Dispose();
            });
            this.library = library;
        }

        private void _ReceiveMessage(global::Stride.UI.Panels.Grid list, string msg)
        {
            var text = library.InstantiateElement<global::Stride.UI.Controls.TextBlock>("ChatMessage");
            text.Text = msg;
            


            list.Children.Add(text);
            if(list.Children.Count > 20)
            {
                list.Children.Remove(list.Children[0]);
            }
            int row = 0;
            foreach (var item in list.Children)
            {                
                item.SetGridRow(row++);
            }

            

        }

        void IStatus.Enter()
        {
            
        }

        void IStatus.Leave()
        {
            _ReleaseHelper.Release();
        }

        void IStatus.Update()
        {
            
        }
    }
}