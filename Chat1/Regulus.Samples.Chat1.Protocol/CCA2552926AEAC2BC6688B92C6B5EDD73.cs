
            using System;  
            using System.Collections.Generic;
            
            
                public class CCA2552926AEAC2BC6688B92C6B5EDD73 : Regulus.Remote.IProtocol
                {
                    Regulus.Remote.InterfaceProvider _InterfaceProvider;
                    Regulus.Remote.EventProvider _EventProvider;
                    Regulus.Remote.MemberMap _MemberMap;
                    Regulus.Serialization.ISerializer _Serializer;
                    public CCA2552926AEAC2BC6688B92C6B5EDD73()
                    {
                        var types = new Dictionary<Type, Type>();
                        types.Add(typeof(Regulus.Samples.Chat1.Common.IBroadcastable) , typeof(Regulus.Samples.Chat1.Common.Ghost.CIBroadcastable) );
types.Add(typeof(Regulus.Samples.Chat1.Common.IChatable) , typeof(Regulus.Samples.Chat1.Common.Ghost.CIChatable) );
                        _InterfaceProvider = new Regulus.Remote.InterfaceProvider(types);

                        var eventClosures = new List<Regulus.Remote.IEventProxyCreator>();
                        eventClosures.Add(new Regulus.Samples.Chat1.Common.Invoker.IBroadcastable.MessageEvent() );
                        _EventProvider = new Regulus.Remote.EventProvider(eventClosures);

                        _Serializer = new Regulus.Serialization.Serializer(new Regulus.Serialization.DescriberBuilder(typeof(Regulus.Remote.ClientToServerOpCode),typeof(Regulus.Remote.PackageAddEvent),typeof(Regulus.Remote.PackageCallMethod),typeof(Regulus.Remote.PackageErrorMethod),typeof(Regulus.Remote.PackageInvokeEvent),typeof(Regulus.Remote.PackageLoadSoul),typeof(Regulus.Remote.PackageLoadSoulCompile),typeof(Regulus.Remote.PackageProtocolSubmit),typeof(Regulus.Remote.PackageRelease),typeof(Regulus.Remote.PackageRemoveEvent),typeof(Regulus.Remote.PackageReturnValue),typeof(Regulus.Remote.PackageSetProperty),typeof(Regulus.Remote.PackageSetPropertyDone),typeof(Regulus.Remote.PackageUnloadSoul),typeof(Regulus.Remote.RequestPackage),typeof(Regulus.Remote.ResponsePackage),typeof(Regulus.Remote.ServerToClientOpCode),typeof(System.Boolean),typeof(System.Byte[]),typeof(System.Byte[][]),typeof(System.Char),typeof(System.Char[]),typeof(System.Int32),typeof(System.Int64),typeof(System.String)).Describers);


                        _MemberMap = new Regulus.Remote.MemberMap(new System.Reflection.MethodInfo[] {new Regulus.Utility.Reflection.TypeMethodCatcher((System.Linq.Expressions.Expression<System.Action<Regulus.Samples.Chat1.Common.IChatable,System.String,System.String>>)((ins,_1,_2) => ins.Send(_1,_2))).Method} ,new System.Reflection.EventInfo[]{ typeof(Regulus.Samples.Chat1.Common.IBroadcastable).GetEvent("MessageEvent") }, new System.Reflection.PropertyInfo[] { }, new System.Tuple<System.Type, System.Func<Regulus.Remote.IProvider>>[] {new System.Tuple<System.Type, System.Func<Regulus.Remote.IProvider>>(typeof(Regulus.Samples.Chat1.Common.IBroadcastable),()=>new Regulus.Remote.TProvider<Regulus.Samples.Chat1.Common.IBroadcastable>()),new System.Tuple<System.Type, System.Func<Regulus.Remote.IProvider>>(typeof(Regulus.Samples.Chat1.Common.IChatable),()=>new Regulus.Remote.TProvider<Regulus.Samples.Chat1.Common.IChatable>())});
                    }

                    byte[] Regulus.Remote.IProtocol.VerificationCode { get { return new byte[]{202,37,82,146,106,234,194,188,102,136,185,44,107,94,221,115};} }
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
            
            