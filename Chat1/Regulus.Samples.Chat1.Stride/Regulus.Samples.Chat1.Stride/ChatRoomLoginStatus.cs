using System.Linq;
using System.Reactive.Linq;
using Regulus.Utility;
using Stride.Engine;
using Regulus.Remote.Reactive;
using Stride.UI;
using System;

namespace Regulus.Samples.Chat1.Stride
{
    internal class ChatRoomLoginStatus : Regulus.Utility.IStatus
    {
        
        readonly ReleaseHelper _ReleaseHelper;

        public event System.Action SuccessEvent;
        public event System.Action FailEvent;
        public ChatRoomLoginStatus(Remote.INotifierQueryable queryer, UIComponent component)
        {
            _ReleaseHelper = new ReleaseHelper();
            
            component.Page.RootElement.Visibility = Visibility.Visible;
            var name = component.Page.RootElement.FindName("Name") as global::Stride.UI.Controls.EditText;

            var enter = component.Page.RootElement.FindName("Enter") as global::Stride.UI.Controls.Button;            

            var loginObs  = from _ in System.Reactive.Linq.Observable.FromEventPattern<TouchEventArgs>((h) => enter.TouchUp += h, (h) => enter.TouchUp -= h).Take(1)
                       from login in queryer.QueryNotifier<Regulus.Samples.Chat1.Common.ILogin>().SupplyEvent()
                       from result in login.Login(name.Text).RemoteValue()
                       select result;

            var loginObsDispose = loginObs.Subscribe(_LoginResult);
            _ReleaseHelper.Actions.Add(() =>
            {
                component.Page.RootElement.Visibility = Visibility.Hidden;            
                loginObsDispose.Dispose();
            });

        }

        private void _LoginResult(bool result)
        {
            if (result)
                SuccessEvent();
            else
                FailEvent();

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