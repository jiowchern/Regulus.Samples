using Regulus.Network;
using Regulus.Remote;
using System.Linq;
using UnityEngine;

public class Chat1Agent : MonoBehaviour , Regulus.Network.IStreamable
{
    public readonly Regulus.Remote.Ghost.IAgent Agent;
    public Regulus.Remote.Unity.Connector Connector;


    Regulus.Network.IStreamable _Stream;
    public Chat1Agent()
    {

        _Stream = new Stream();
        var protocol = Regulus.Samples.Chat1.Common.ProtocolCreater.Create();
        var agent = Regulus.Remote.Client.Provider.CreateAgent(protocol , this);
        
        Agent = agent;
        
    }

    public void Start()
    {
        _Stream = Connector;
    }

    public void Update()
    {
        Agent.Update();
    }

    public void OnDestroy()
    {
    }

    public static Regulus.Remote.Ghost.IAgent FindAgent()
    {        
        return UnityEngine.GameObject.FindAnyObjectByType<Chat1Agent>()?.Agent;        
    }

    IWaitableValue<int> IStreamable.Receive(byte[] buffer, int offset, int count)
    {
        return _Stream.Receive(buffer, offset, count);
    }

    IWaitableValue<int> IStreamable.Send(byte[] buffer, int offset, int count)
    {
        return _Stream.Send(buffer, offset, count);
    }
}
