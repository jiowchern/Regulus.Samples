using Regulus.Network;
using System.Threading.Tasks;
using UnityEngine;

namespace Regulus.Remote.Unity
{
    public abstract class Connecter : MonoBehaviour, Regulus.Network.IStreamable
    {
        protected abstract Regulus.Network.IWaitableValue<int> _Receive(byte[] buffer, int offset, int count);
        Regulus.Network.IWaitableValue<int> IStreamable.Receive(byte[] buffer, int offset, int count)
        {            
            UnityEngine.Debug.Log($"pre receive {count}");
            return _Receive(buffer, offset, count);
            
        }
        protected abstract Regulus.Network.IWaitableValue<int> _Send(byte[] buffer, int offset, int count);
        Regulus.Network.IWaitableValue<int> IStreamable.Send(byte[] buffer, int offset, int count)
        {
            UnityEngine.Debug.Log($"pre send {count}");
            return _Send(buffer , offset , count);            
        }

        public abstract void Connect(string address);
        public abstract void Disconnect();
    }
}
