using System.Linq;
using System.Reactive.Linq;
using Regulus.Utility;
using Stride.Engine;
using Regulus.Remote.Reactive;
using Stride.UI;
using System;
using Regulus.Remote;

namespace Regulus.Samples.Chat1.Stride
{

    class BuildIpAddressStatus : Regulus.Utility.IStatus
    {

        
        readonly ReleaseHelper _ReleaseHelper;
        readonly UIElement _Root;
        readonly global::Stride.UI.Controls.Button _Connection;
        private readonly global::Stride.UI.Controls.EditText _Address;
        private readonly global::Stride.UI.Controls.EditText _Port;
        public System.Action<System.Net.EndPoint> SuccessEvent;
        public System.Action FailEvent;
        public BuildIpAddressStatus(UIComponent componnent)
        {
            _ReleaseHelper = new ReleaseHelper();

            _Root = componnent.Page.RootElement;
            _Connection = componnent.Page.RootElement.FindName("Connection") as global::Stride.UI.Controls.Button;
            _Address = componnent.Page.RootElement.FindName("Address") as global::Stride.UI.Controls.EditText;
            _Port = componnent.Page.RootElement.FindName("Port") as global::Stride.UI.Controls.EditText;
            _Address.Text = "114.34.92.204";
            _Port.Text = "53772";

        }

        void IStatus.Enter()
        {

            
            

            var connectionObs = from _ in _Connection.TouchUpObs().Take(1)
                                select _;

            var connectionObsDispose = connectionObs.Subscribe(_Connect);
            _ReleaseHelper.Actions.Add(() =>
            {
                connectionObsDispose.Dispose();
            });

            _Root.Visibility = global::Stride.UI.Visibility.Visible;
            _ReleaseHelper.Actions.Add(() => {
                _Root.Visibility = global::Stride.UI.Visibility.Hidden;
            });
            

        }

        private void _Connect(object unit)
        {
            var address = _Address.Text;
            var port = _Port.Text;

            System.Net.IPEndPoint endPoint;
            if (System.Net.IPEndPoint.TryParse($"{address}:{port}", out endPoint))
            {
                SuccessEvent(endPoint);                
            }
            else
            {
                FailEvent();
            }
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
