using Regulus.Samples.Chat1.Common;
using System;
using UnityEngine;
public class Chat1Login : MonoBehaviour
{
    public UnityEngine.UI.Text Name;
    public GameObject Root;
    ILogin _Login;
    private void OnDestroy()
    {
        var agent = Chat1Agent.FindAgent();
        if (agent == null)
        {
            return;
        }
        agent.QueryNotifier<ILogin>().Supply -= _Show;
        agent.QueryNotifier<ILogin>().Unsupply -= _Hide;
    }
    private void Start()
    {
        var agent = Chat1Agent.FindAgent();
        if(agent == null)
        {
            return;
        }
        agent.QueryNotifier<ILogin>().Supply += _Show;
        agent.QueryNotifier<ILogin>().Unsupply += _Hide;
    }

    private void _Hide(ILogin login)
    {
        _Login = login;
        Root.SetActive(false);
    }

    private void _Show(ILogin login)
    {
        
        _Login = login;
        Root.SetActive(true);
    }

    public void Login()
    {
        if (_Login != null)
            _Login.Login(Name.text);
    }
}
