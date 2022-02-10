using System.Linq;
using UnityEngine;

public class Chat1Agent : MonoBehaviour
{
    public readonly Regulus.Remote.Ghost.IAgent Agent;
    public Chat1Agent()
    {

        
        var protocol = Regulus.Samples.Chat1.Common.ProtocolCreator.Create();
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
