using Regulus.Samples.Chat1.Common;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Chat1Room : MonoBehaviour
{
    IPlayer _Player;
    public GameObject Root;
    public UnityEngine.UI.InputField Message;
    public UnityEngine.UI.Text Name;
    public Transform MessageRoot;
    public GameObject MessageItemPrefab;

    readonly System.Collections.Generic.List<IChatter> _Chatter;

    public Chat1Room()
    {
        _Chatter = new System.Collections.Generic.List<IChatter>();
    }
    private void OnDestroy()
    {
        var agent = Chat1Agent.FindAgent();
        if (agent == null)
        {
            return;
        }
        agent.QueryNotifier<IPlayer>().Supply -= _Show;
        agent.QueryNotifier<IPlayer>().Unsupply -= _Hide;
    }
    private void Start()
    {
        var agent = Chat1Agent.FindAgent();
        if (agent == null)
        {
            return;
        }
        agent.QueryNotifier<IPlayer>().Supply += _Show;
        agent.QueryNotifier<IPlayer>().Unsupply += _Hide;
    }

    private void _Hide(IPlayer obj)
    {        
        Root.SetActive(false);
        _Player.Chatters.Base.Supply -= _AddChatter;
        _Player.Chatters.Base.Unsupply -= _RemoveChatter;
        _Player.PublicMessageEvent -= _PublicMessage;
        _Player.PrivateMessageEvent -= _PrivateMessage;
    }

    private void _Show(IPlayer obj)
    {
        _Player = obj;
        _Player.Chatters.Base.Supply += _AddChatter;
        _Player.Chatters.Base.Unsupply += _RemoveChatter;
        _Player.PublicMessageEvent += _PublicMessage;
        _Player.PrivateMessageEvent += _PrivateMessage;
        Root.SetActive(true);
    }

    private void _RemoveChatter(IChatter chatter)
    {
        _Chatter.Add(chatter);
        _PushMessage($"{chatter.Name.Value} leave.");
    }

    private void _AddChatter(IChatter chatter)
    {
        _Chatter.Add(chatter);
        _PushMessage($"{chatter.Name.Value} join." );
    }

    private void _PrivateMessage(Message message)
    {
        _PushMessage("[private]", message.Name, message.Context);
    }

    private void _PublicMessage(Message message)
    {
        _PushMessage("" , message.Name, message.Context);
    }

    private void _PushMessage(string prefix, string name, string message)
    {        
        _PushMessage($"{prefix}{name}:{message}");
    }
    private void _PushMessage(string message)
    {
        var item = GameObject.Instantiate(MessageItemPrefab, MessageRoot);
        item.SetActive(true);
        var msg = item.GetComponent<Text>();
        msg.text = message;
    }

    public void Send()
    {

        var chatters = from chatter in _Chatter
                       where chatter.Name.Value == Name.text
                        select chatter;
        if(!chatters.Any())
        {
            _Player.Send(Message.text);
        }
        else
        {
            foreach (var chatter in chatters)
            {
                chatter.Whisper(Message.text);
            }
        }

        Message.text = "";
    }
    public void Quit()
    {
        _Player.Quit();
    }
}
