using System;
using System.Collections.Generic;
using System.Text;

namespace Regulus.Samples.Chat1.Common
{

    public interface IChatable
    {
        
        void Send(string name , string message);
    }
}
