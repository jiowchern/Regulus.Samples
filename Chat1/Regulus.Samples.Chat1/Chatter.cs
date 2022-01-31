using Regulus.Remote;
using Regulus.Samples.Chat1.Common;
using System;

namespace Regulus.Samples.Chat1
{
    internal class Chatter 
    {
        public readonly IMessageable Messager;        
        public readonly System.Action<string> Send;
        public Chatter(IMessageable messageable, System.Action<string> send )
        {
            Messager = messageable;            
            Send = send;
        }        
    }
}