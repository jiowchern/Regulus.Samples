using System.Linq;
using UnityEngine;

public class Chat1Agent : MonoBehaviour
{
    public readonly Regulus.Remote.IAgent Agent;
    public Chat1Agent()
    {
        // get from Regulus.Samples.Chat1.Protocol.dll
        var protocolType = Regulus.Remote.Protocol.ProtocolProvider.GetProtocols().Single();
        var protocol = System.Activator.CreateInstance(protocolType) as Regulus.Remote.IProtocol;
        var agent = Regulus.Remote.Client.AgentProvider.CreateWeb(protocol);
        Agent = agent;
    }

    public void Start()
    {
        Agent.Launch();
    }

    public void Update()
    {
        Agent.Update();
    }

    public void OnDestroy()
    {
        Agent.Shutdown();
    }

    public static Regulus.Remote.IAgent FindAgent()
    {
        return UnityEngine.GameObject.FindObjectOfType<Chat1Agent>()?.Agent;        
    }
}
