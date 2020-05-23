using Regulus.Samples.Chat1.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chat1MessageView : MonoBehaviour
{
    readonly System.Collections.Generic.List<string> _Lines;
    public UnityEngine.UI.Text Message;
    public Chat1MessageView()
    {
        _Lines = new List<string>();
    }
    void Start()
    {
        var agent = Chat1Agent.FindAgent();
        if (agent == null)
            return;
        agent.QueryNotifier<Regulus.Samples.Chat1.Common.IBroadcastable>().Supply += _Show;
        agent.QueryNotifier<Regulus.Samples.Chat1.Common.IBroadcastable>().Unsupply += _Hide;
    }

    private void OnDestroy()
    {
        var agent = Chat1Agent.FindAgent();
        if (agent == null)
            return;
        agent.QueryNotifier<Regulus.Samples.Chat1.Common.IBroadcastable>().Supply -= _Show;
        agent.QueryNotifier<Regulus.Samples.Chat1.Common.IBroadcastable>().Unsupply -= _Hide;
    }
    private void _Hide(IBroadcastable broadcastable)
    {
        broadcastable.MessageEvent -= _PushMessage;
    }

    private void _PushMessage(string name, string message)
    {
        _Lines.Add($"{name}:{message}");
        if(_Lines.Count > 20)
        {
            _Lines.RemoveAt(20-1);
        }

        Message.text = string.Join("\n", _Lines);
    }

    private void _Show(IBroadcastable broadcastable)
    {
        broadcastable.MessageEvent += _PushMessage;
    }
}
