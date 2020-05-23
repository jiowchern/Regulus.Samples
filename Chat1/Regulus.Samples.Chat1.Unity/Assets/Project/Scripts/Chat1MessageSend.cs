using Regulus.Samples.Chat1.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chat1MessageSend : MonoBehaviour
{
    public UnityEngine.GameObject Panel;
    public UnityEngine.UI.Text Name;
    public UnityEngine.UI.InputField Message;
    private Action _Send;

    public Chat1MessageSend()
    {
        _Send = () => { };
    }
    void Start()
    {
        var agent = Chat1Agent.FindAgent();
        if (agent == null)
            return;
        agent.QueryNotifier<Regulus.Samples.Chat1.Common.IChatable>().Supply += _Show;
        agent.QueryNotifier<Regulus.Samples.Chat1.Common.IChatable>().Unsupply += _Hide;
    }
    private void OnDestroy()
    {
        var agent = Chat1Agent.FindAgent();
        if (agent == null)
            return;
        agent.QueryNotifier<Regulus.Samples.Chat1.Common.IChatable>().Supply -= _Show;
        agent.QueryNotifier<Regulus.Samples.Chat1.Common.IChatable>().Unsupply -= _Hide;
    }

    private void _Hide(IChatable obj)
    {
        Panel.SetActive(false);
    }

    private void _Show(IChatable obj)
    {
        _Send = () =>{ obj.Send(Name.text, Message.text); Message.text = ""; };
        Panel.SetActive(true);
    }

    public void OnSend()
    {
        _Send();
    }
}
