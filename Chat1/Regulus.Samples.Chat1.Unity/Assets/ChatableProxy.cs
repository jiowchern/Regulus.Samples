using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatableProxy : Regulus.Samples.Chat1.Common.Adsorption.ChatableAdsorber
{
    public string Name;
    public UnityEngine.UI.InputField Message;
    public void Send()
    {
        base.Send(Name, Message.text);
    }
}
