using Regulus.Remote;
using Regulus.Remote.Client.Tcp;
using Regulus.Samples.Chat1.Common;
using System;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;

public class Chat1Connect : MonoBehaviour
{ 
    public UnityEngine.GameObject Panel;
    public UnityEngine.UI.InputField IP;
    public UnityEngine.UI.InputField Port;
    public UnityEngine.UI.Text Message;
    
    System.Action _OnConnect;

    public Chat1Connect()
    {
        _OnConnect = () => { };
        
    }
    void Start()
    {        
        var agent = Chat1Agent.FindAgent();
        if (agent == null)
            return;

        var connecter = Regulus.Remote.Client.Provider.CreateTcp(agent);

        System.Action onConnect = () => {
            // call connect to connect ...
            _GetResult(connecter.Connect(_GetEndPoint()));
        };
        _OnConnect = onConnect;

        agent.QueryNotifier<IPlayer>().Supply += _Hide;
        _Show();
    }

    private void OnDestroy()
    {
        var agent = Chat1Agent.FindAgent();
        if (agent == null)
            return;
        agent.QueryNotifier<IPlayer>().Supply -= _Hide;


    }

    private void _GetResult(Task<IOnlineable> task)
    {
        
        /*var online = task.Result;
        if (online != null)
            _Hide();
        else
            Message.text = "connect fail";*/
    }

    private void _Show()
    {
        Panel.SetActive(true);
        
    }

    

    private IPEndPoint _GetEndPoint()
    {
        return new IPEndPoint(IPAddress.Parse(IP.text), int.Parse(Port.text));
    }

    private void _Hide(IPlayer player)
    {
        Panel.SetActive(false);

    }

    public void OnConnect()
    {
        _OnConnect();
    }
}
