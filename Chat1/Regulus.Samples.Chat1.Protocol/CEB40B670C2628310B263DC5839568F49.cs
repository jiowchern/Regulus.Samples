
            using System;  
            using System.Collections.Generic;
            
            
                public class CEB40B670C2628310B263DC5839568F49 : Regulus.Remote.IProtocol
                {
                    Regulus.Remote.InterfaceProvider _InterfaceProvider;
                    Regulus.Remote.EventProvider _EventProvider;
                    Regulus.Remote.MemberMap _MemberMap;
                    Regulus.Serialization.ISerializer _Serializer;
                    public CEB40B670C2628310B263DC5839568F49()
                    {
                        var types = new Dictionary<Type, Type>();
                        types.Add(typeof(Regulus.Samples.Chat1.Common.IChatter) , typeof(Regulus.Samples.Chat1.Common.Ghost.CIChatter) );
types.Add(typeof(Regulus.Samples.Chat1.Common.ILogin) , typeof(Regulus.Samples.Chat1.Common.Ghost.CILogin) );
types.Add(typeof(Regulus.Samples.Chat1.Common.IPlayer) , typeof(Regulus.Samples.Chat1.Common.Ghost.CIPlayer) );
                        _InterfaceProvider = new Regulus.Remote.InterfaceProvider(types);

                        var eventClosures = new List<Regulus.Remote.IEventProxyCreator>();
                        eventClosures.Add(new Regulus.Samples.Chat1.Common.Invoker.IPlayer.PublicMessageEvent() );
eventClosures.Add(new Regulus.Samples.Chat1.Common.Invoker.IPlayer.PrivateMessageEvent() );
                        _EventProvider = new Regulus.Remote.EventProvider(eventClosures);

                        _Serializer = new Regulus.Serialization.Serializer(new Regulus.Serialization.DescriberBuilder(typeof(Regulus.Remote.ClientToServerOpCode),typeof(Regulus.Remote.PackageAddEvent),typeof(Regulus.Remote.PackageCallMethod),typeof(Regulus.Remote.PackageErrorMethod),typeof(Regulus.Remote.PackageInvokeEvent),typeof(Regulus.Remote.PackageLoadSoul),typeof(Regulus.Remote.PackageLoadSoulCompile),typeof(Regulus.Remote.PackageNotifierEvent),typeof(Regulus.Remote.PackageProtocolSubmit),typeof(Regulus.Remote.PackageRelease),typeof(Regulus.Remote.PackageRemoveEvent),typeof(Regulus.Remote.PackageReturnValue),typeof(Regulus.Remote.PackageSetProperty),typeof(Regulus.Remote.PackageSetPropertyDone),typeof(Regulus.Remote.PackageUnloadSoul),typeof(Regulus.Remote.RequestPackage),typeof(Regulus.Remote.ResponsePackage),typeof(Regulus.Remote.ServerToClientOpCode),typeof(System.Boolean),typeof(System.Byte[]),typeof(System.Byte[][]),typeof(System.Char),typeof(System.Char[]),typeof(System.Int32),typeof(System.Int64),typeof(System.String)).Describers);


                        _MemberMap = new Regulus.Remote.MemberMap(new System.Reflection.MethodInfo[] {new Regulus.Utility.Reflection.TypeMethodCatcher((System.Linq.Expressions.Expression<System.Action<Regulus.Samples.Chat1.Common.IChatter,System.String>>)((ins,_1) => ins.Whisper(_1))).Method,new Regulus.Utility.Reflection.TypeMethodCatcher((System.Linq.Expressions.Expression<System.Action<Regulus.Samples.Chat1.Common.ILogin,System.String>>)((ins,_1) => ins.Login(_1))).Method,new Regulus.Utility.Reflection.TypeMethodCatcher((System.Linq.Expressions.Expression<System.Action<Regulus.Samples.Chat1.Common.IPlayer,System.String>>)((ins,_1) => ins.Send(_1))).Method,new Regulus.Utility.Reflection.TypeMethodCatcher((System.Linq.Expressions.Expression<System.Action<Regulus.Samples.Chat1.Common.IPlayer>>)((ins) => ins.Quit())).Method} ,new System.Reflection.EventInfo[]{ typeof(Regulus.Samples.Chat1.Common.IPlayer).GetEvent("PublicMessageEvent"),typeof(Regulus.Samples.Chat1.Common.IPlayer).GetEvent("PrivateMessageEvent") }, new System.Reflection.PropertyInfo[] {typeof(Regulus.Samples.Chat1.Common.IChatter).GetProperty("Name"),typeof(Regulus.Samples.Chat1.Common.IPlayer).GetProperty("Chatters") }, new System.Tuple<System.Type, System.Func<Regulus.Remote.IProvider>>[] {new System.Tuple<System.Type, System.Func<Regulus.Remote.IProvider>>(typeof(Regulus.Samples.Chat1.Common.IChatter),()=>new Regulus.Remote.TProvider<Regulus.Samples.Chat1.Common.IChatter>()),new System.Tuple<System.Type, System.Func<Regulus.Remote.IProvider>>(typeof(Regulus.Samples.Chat1.Common.ILogin),()=>new Regulus.Remote.TProvider<Regulus.Samples.Chat1.Common.ILogin>()),new System.Tuple<System.Type, System.Func<Regulus.Remote.IProvider>>(typeof(Regulus.Samples.Chat1.Common.IPlayer),()=>new Regulus.Remote.TProvider<Regulus.Samples.Chat1.Common.IPlayer>())});
                    }

                    byte[] Regulus.Remote.IProtocol.VerificationCode { get { return new byte[]{235,64,182,112,194,98,131,16,178,99,220,88,57,86,143,73};} }
                    Regulus.Remote.InterfaceProvider Regulus.Remote.IProtocol.GetInterfaceProvider()
                    {
                        return _InterfaceProvider;
                    }

                    Regulus.Remote.EventProvider Regulus.Remote.IProtocol.GetEventProvider()
                    {
                        return _EventProvider;
                    }

                    Regulus.Serialization.ISerializer Regulus.Remote.IProtocol.GetSerialize()
                    {
                        return _Serializer;
                    }

                    Regulus.Remote.MemberMap Regulus.Remote.IProtocol.GetMemberMap()
                    {
                        return _MemberMap;
                    }
                    
                }
            
            