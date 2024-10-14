using Regulus.Network;
using Regulus.Remote;
using System.Linq;
using UnityEngine;

public class Chat1Agent : MonoBehaviour 
{
    public readonly Regulus.Remote.Ghost.IAgent Agent;

    public Chat1Agent()
    {

        
        var protocol = Regulus.Samples.Chat1.Common.ProtocolCreater.Create();
        var agent = Regulus.Remote.Client.Provider.CreateAgent(protocol );
        
        Agent = agent;
        
    }
    public void ConnectSuccess(Regulus.Network.IStreamable streamable )
    {
        
        Agent.Enable(streamable);
    }
    public void Start()
    {
        
    }

    public void Update()
    {
        Agent.Update();
    }

    public void OnDestroy()
    {
        Agent.Disable();
    }

    public static Regulus.Remote.Ghost.IAgent FindAgent()
    {        
        return UnityEngine.GameObject.FindAnyObjectByType<Chat1Agent>()?.Agent;        
    }

}
