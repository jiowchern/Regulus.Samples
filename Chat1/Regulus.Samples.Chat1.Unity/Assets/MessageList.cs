using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageList : MonoBehaviour
{
    private readonly List<string> _Messages;
    public UnityEngine.UI.Text Target;
    public MessageList()
    {
        _Messages = new System.Collections.Generic.List<string>();
    }

    public void Push(string name,string message)
    {
        _Messages.Add($"{name}:{message}");
        if(_Messages.Count > 10)
        {
            _Messages.RemoveAt(0);
        }
        Target.text = string.Join("\n", _Messages);
    }
}
