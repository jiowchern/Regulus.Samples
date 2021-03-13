
            using System;  
            using System.Collections.Generic;
            
            
                public class C25A52A3862985A273D2A79CD71358AA3 : Regulus.Remote.IProtocol
                {
                    readonly Regulus.Remote.InterfaceProvider _InterfaceProvider;
                    readonly Regulus.Remote.EventProvider _EventProvider;
                    readonly Regulus.Remote.MemberMap _MemberMap;
                    readonly Regulus.Serialization.ISerializer _Serializer;
                    readonly System.Reflection.Assembly _Base;
                    public C25A52A3862985A273D2A79CD71358AA3()
                    {
                        _Base = System.Reflection.Assembly.Load("Regulus.Samples.Chat1.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
                        var types = new Dictionary<Type, Type>();
                        types.Add(typeof(Regulus.Samples.Chat1.Common.IChatter) , typeof(Regulus.Samples.Chat1.Common.Ghost.CIChatter) );
types.Add(typeof(Regulus.Samples.Chat1.Common.ILogin) , typeof(Regulus.Samples.Chat1.Common.Ghost.CILogin) );
types.Add(typeof(Regulus.Samples.Chat1.Common.IPlayer) , typeof(Regulus.Samples.Chat1.Common.Ghost.CIPlayer) );
                        _InterfaceProvider = new Regulus.Remote.InterfaceProvider(types);
                        var eventClosures = new List<Regulus.Remote.IEventProxyCreator>();
                        eventClosures.Add(new Regulus.Samples.Chat1.Common.Invoker.IPlayer.PublicMessageEvent() );
eventClosures.Add(new Regulus.Samples.Chat1.Common.Invoker.IPlayer.PrivateMessageEvent() );
                        _EventProvider = new Regulus.Remote.EventProvider(eventClosures);
                        _Serializer = new Regulus.Serialization.Serializer(new Regulus.Serialization.DescriberBuilder(typeof(Regulus.Remote.ClientToServerOpCode),typeof(Regulus.Remote.PackageAddEvent),typeof(Regulus.Remote.PackageCallMethod),typeof(Regulus.Remote.PackageErrorMethod),typeof(Regulus.Remote.PackageInvokeEvent),typeof(Regulus.Remote.PackageLoadSoul),typeof(Regulus.Remote.PackageLoadSoulCompile),typeof(Regulus.Remote.PackagePropertySoul),typeof(Regulus.Remote.PackageProtocolSubmit),typeof(Regulus.Remote.PackageRelease),typeof(Regulus.Remote.PackageRemoveEvent),typeof(Regulus.Remote.PackageReturnValue),typeof(Regulus.Remote.PackageSetProperty),typeof(Regulus.Remote.PackageSetPropertyDone),typeof(Regulus.Remote.PackageUnloadSoul),typeof(Regulus.Remote.RequestPackage),typeof(Regulus.Remote.ResponsePackage),typeof(Regulus.Remote.ServerToClientOpCode),typeof(System.Boolean),typeof(System.Byte[]),typeof(System.Byte[][]),typeof(System.Char),typeof(System.Char[]),typeof(System.Int32),typeof(System.Int64),typeof(System.String)).Describers);
                        _MemberMap = new Regulus.Remote.MemberMap(new System.Reflection.MethodInfo[] {new Regulus.Utility.Reflection.TypeMethodCatcher((System.Linq.Expressions.Expression<System.Action<Regulus.Samples.Chat1.Common.IChatter,System.String>>)((ins,_1) => ins.Whisper(_1))).Method,new Regulus.Utility.Reflection.TypeMethodCatcher((System.Linq.Expressions.Expression<System.Action<Regulus.Samples.Chat1.Common.ILogin,System.String>>)((ins,_1) => ins.Login(_1))).Method,new Regulus.Utility.Reflection.TypeMethodCatcher((System.Linq.Expressions.Expression<System.Action<Regulus.Samples.Chat1.Common.IPlayer,System.String>>)((ins,_1) => ins.Send(_1))).Method,new Regulus.Utility.Reflection.TypeMethodCatcher((System.Linq.Expressions.Expression<System.Action<Regulus.Samples.Chat1.Common.IPlayer>>)((ins) => ins.Quit())).Method} ,new System.Reflection.EventInfo[]{ typeof(Regulus.Samples.Chat1.Common.IPlayer).GetEvent("PublicMessageEvent"),typeof(Regulus.Samples.Chat1.Common.IPlayer).GetEvent("PrivateMessageEvent") }, new System.Reflection.PropertyInfo[] {typeof(Regulus.Samples.Chat1.Common.IChatter).GetProperty("Name"),typeof(Regulus.Samples.Chat1.Common.IPlayer).GetProperty("Chatters") }, new System.Tuple<System.Type, System.Func<Regulus.Remote.IProvider>>[] {new System.Tuple<System.Type, System.Func<Regulus.Remote.IProvider>>(typeof(Regulus.Samples.Chat1.Common.IChatter),()=>new Regulus.Remote.TProvider<Regulus.Samples.Chat1.Common.IChatter>()),new System.Tuple<System.Type, System.Func<Regulus.Remote.IProvider>>(typeof(Regulus.Samples.Chat1.Common.ILogin),()=>new Regulus.Remote.TProvider<Regulus.Samples.Chat1.Common.ILogin>()),new System.Tuple<System.Type, System.Func<Regulus.Remote.IProvider>>(typeof(Regulus.Samples.Chat1.Common.IPlayer),()=>new Regulus.Remote.TProvider<Regulus.Samples.Chat1.Common.IPlayer>())});
                    }
                    System.Reflection.Assembly Regulus.Remote.IProtocol.Base => _Base;
                    byte[] Regulus.Remote.IProtocol.VerificationCode { get { return new byte[]{37,165,42,56,98,152,90,39,61,42,121,205,113,53,138,163};} }
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
            
            