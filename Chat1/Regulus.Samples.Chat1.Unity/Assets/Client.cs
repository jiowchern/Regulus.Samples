using System.Collections;
using System.Collections.Generic;
using Regulus.Remote;
using UnityEngine;

public class Client : Regulus.Samples.Chat1Agent
{
    public UnityEngine.UI.InputField IPAddress;
    public UnityEngine.UI.InputField Port;

    public void Connect()
    {
        base.Connect(IPAddress.text , int.Parse(Port.text));
    }
    public override IAgent _GetAgent()
    {
        return Regulus.Remote.Client.AgentProivder.CreateTcp(new Regulus.Samples.Chat1Protocol());
    }
}
