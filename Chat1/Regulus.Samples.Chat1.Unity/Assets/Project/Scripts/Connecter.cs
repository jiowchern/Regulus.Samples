using Regulus.Network;
using System.Threading.Tasks;
using UnityEngine;

namespace Regulus.Remote.Unity
{
    public abstract class Connecter : MonoBehaviour, Regulus.Network.IStreamable
    {
        protected abstract Task<int> _Receive(byte[] buffer, int offset, int count);
        Task<int> IStreamable.Receive(byte[] buffer, int offset, int count)
        {
            return _Receive(buffer, offset, count);
            
        }
        protected abstract Task<int> _Send(byte[] buffer, int offset, int count);
        Task<int> IStreamable.Send(byte[] buffer, int offset, int count)
        {
            return _Send(buffer , offset , count);            
        }

        public abstract Task<bool> Connect(string address);
        public abstract void Disconnect();
    }
}
