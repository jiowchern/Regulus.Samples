using Regulus.Utility;
using Stride.Engine;
using Stride.UI;
using System;

namespace Regulus.Samples.Chat1.Stride
{
    internal class MessageStatus : Regulus.Utility.IStatus
    {
        private readonly ReleaseHelper _ReleaseHelper;
        public event System.Action DoneEvent;
        public MessageStatus(UIComponent component , string message)
        {
            component.Page.RootElement.Visibility = global::Stride.UI.Visibility.Visible;
            var text = component.Page.RootElement.FindName("Text") as global::Stride.UI.Controls.TextBlock;
            text.Text = message;

            var submit = component.Page.RootElement.FindName("Submit") as global::Stride.UI.Controls.Button;
            submit.TouchUp += _Submit;

            _ReleaseHelper = new ReleaseHelper();
            _ReleaseHelper.Actions.Add(() => {
                component.Page.RootElement.Visibility = global::Stride.UI.Visibility.Hidden;
                submit.TouchUp -= _Submit;
            });
        }

        private void _Submit(object sender, TouchEventArgs e)
        {
            DoneEvent();
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