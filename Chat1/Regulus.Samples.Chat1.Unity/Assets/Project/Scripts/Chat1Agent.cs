using System.Linq;
using UnityEngine;

public class Chat1Agent : MonoBehaviour
{
    public readonly Regulus.Remote.Ghost.IAgent Agent;
    public Chat1Agent()
    {
        // get from Regulus.Samples.Chat1.Protocol.dll
        var protocolTypss = Regulus.Remote.Protocol.ProtocolProvider.GetProtocols().ToArray();
        var protocolType = protocolTypss.First();
        var protocol = System.Activator.CreateInstance(protocolType) as Regulus.Remote.IProtocol;
        var agent = Regulus.Remote.Client.Provider.CreateAgent(protocol);
        
        Agent = agent;
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
    }

    public static Regulus.Remote.Ghost.IAgent FindAgent()
    {
        return UnityEngine.GameObject.FindObjectOfType<Chat1Agent>()?.Agent;        
    }
}
