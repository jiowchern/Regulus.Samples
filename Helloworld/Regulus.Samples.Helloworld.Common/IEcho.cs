using System;

namespace Regulus.Samples.Helloworld.Common
{
    public interface IEcho
    {
        Regulus.Remote.Value<string> Speak(string message);
    }
}
