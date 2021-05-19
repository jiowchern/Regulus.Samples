using Regulus.Remote;
using System.Linq;
using System.Reactive.Linq;
using Regulus.Remote.Reactive;
using Regulus.Remote.Client.Tcp;
using Regulus.Remote.Ghost;
using System;
using System.Net;
using System.Threading.Tasks;
using Regulus.Samples.Chat1.Common;

namespace Regulus.Samples.Chat1.Bots
{
    internal class Bot : System.IDisposable
    {
        private readonly Task _Task;
        private readonly IAgent _Agent;

        volatile bool _Enable; 
        readonly System.Collections.Concurrent.ConcurrentQueue<System.Action> _Actions;
        readonly System.Reactive.Disposables.CompositeDisposable _Disposables;

        public Bot(IAgent agent)
        {
            
            
            _Disposables = new System.Reactive.Disposables.CompositeDisposable();
            _Actions = new System.Collections.Concurrent.ConcurrentQueue<Action>();

            _Agent =agent;
            _Enable = true;
            _Task = System.Threading.Tasks.Task.Factory.StartNew(_Update);
        }

        private void _Update()
        {
            
            
            var random = new System.Random();
            var loginObs = from login in _Agent.QueryNotifier<Regulus.Samples.Chat1.Common.ILogin>().SupplyEvent()
                           from loginResult in login.Login($"bot-{_Agent.GetHashCode()}-{login.GetHashCode()}").RemoteValue()
                           select loginResult ;

            _Disposables.Add(loginObs.Subscribe(_LoginResult));

            /*var chatterAddObs = from chatter in _Agent.QueryNotifier<Common.IChatter>().SupplyEvent()
                                select chatter;
            _Disposables.Add(chatterAddObs.Subscribe(_Chatters.Add));
            var chatterRemoveObs = from chatter in _Agent.QueryNotifier<Common.IChatter>().UnsupplyEvent()
                                select chatter;
            _Disposables.Add(chatterRemoveObs.Subscribe((c) =>_Chatters.Remove(c)));*/

            
            var sendPlayerObs = from player in _Agent.QueryNotifier<Common.IPlayer>().SupplyEvent()
                                from seconds in Observable.Defer(() => Observable.Return(TimeSpan.FromSeconds(random.Next(1, 4))))
                                from elapsed in Observable.Timer(seconds)
                                select player;
            _Disposables.Add(sendPlayerObs.Subscribe(_SendMessage));
            
            var quitPlayerObs = from player in _Agent.QueryNotifier<Common.IPlayer>().SupplyEvent()
                                from seconds in Observable.Defer(() => Observable.Return(TimeSpan.FromSeconds(random.Next(5, 10))))
                                from elapsed in Observable.Timer(seconds)
                                select player;
            _Disposables.Add(quitPlayerObs.Subscribe(_PlayerQuit));

            var playerMessageObs = from player in _Agent.QueryNotifier<Common.IPlayer>().SupplyEvent()
                                from msg in System.Reactive.Linq.Observable.FromEvent<System.Action<Common.Message>, Common.Message>(h => player.PublicMessageEvent += h ,  h => player.PublicMessageEvent -= h)
                                select msg;
            _Disposables.Add(playerMessageObs.Subscribe(_PlayerMessage));

            var ar = new Regulus.Utility.AutoPowerRegulator(new Utility.PowerRegulator());

            while (_Enable)
            {
                _Agent.Update();

                Action action;
                while (_Actions.TryDequeue(out action))
                {
                    action();
                }
                ar.Operate();
            }
            _Disposables.Dispose();
            //onlinable.Disconnect();
        }

        private void _PlayerMessage(Message msg)
        {
            _Actions.Enqueue(()=> Regulus.Utility.Log.Instance.WriteInfo($"{_Agent.GetHashCode()}[{msg.Name}]:{msg.Context}"));
            
        }

        private void _PlayerQuit(IPlayer obj)
        {
            Utility.Log.Instance.WriteInfo($"[{_Agent.GetHashCode()}]quit.");
            _Actions.Enqueue(() => obj.Quit());
            
        }

        private void _SendMessage(IPlayer player)
        {
            Utility.Log.Instance.WriteInfo($"[{_Agent.GetHashCode()}]send.");
            _Actions.Enqueue(()=> player.Send(System.DateTime.Now.ToString()));
            
        }
    

        private void _LoginResult(bool result)
        {
            Utility.Log.Instance.WriteInfo($"[{_Agent.GetHashCode()}] login {result}.");
        }

        async void IDisposable.Dispose()
        {
            _Enable = false;
            await _Task;
        }
    }
}