using Regulus.Remote;
using System.Net;
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
        // regist IConnect
        agent.QueryNotifier<IConnect>().Supply += _Show;
        agent.QueryNotifier<IConnect>().Unsupply += _Hide;
    }

    private void OnDestroy()
    {
        var agent = Chat1Agent.FindAgent();
        if (agent == null)
            return;
        // unregist IConnect
        agent.QueryNotifier<IConnect>().Supply -= _Show;
        agent.QueryNotifier<IConnect>().Unsupply -= _Hide;
    }

    private void _Show(IConnect connect)
    {
        Panel.SetActive(true);
        System.Action onConnect = () => {
            // call connect to connect ...
            connect.Connect(_GetEndPoint()).OnValue += _ConnectResult;
        };
        _OnConnect = onConnect;
    }

    private void _ConnectResult(bool success)
    {
        if (!success)
            Message.text = "connect fail";
    }

    private IPEndPoint _GetEndPoint()
    {
        return new IPEndPoint(IPAddress.Parse(IP.text), int.Parse(Port.text));
    }

    private void _Hide(IConnect obj)
    {
        Panel.SetActive(false);

    }

    public void OnConnect()
    {
        _OnConnect();
    }
}
