
            using System;  
            using System.Collections.Generic;
            
            
                public class C7F49C4D217A64C72A4B64F9FEDD924E6 : Regulus.Remote.IProtocol
                {
                    Regulus.Remote.InterfaceProvider _InterfaceProvider;
                    Regulus.Remote.EventProvider _EventProvider;
                    Regulus.Remote.MemberMap _MemberMap;
                    Regulus.Serialization.ISerializer _Serializer;
                    public C7F49C4D217A64C72A4B64F9FEDD924E6()
                    {
                        var types = new Dictionary<Type, Type>();
                        types.Add(typeof(Regulus.Samples.Helloworld.Common.IGreeter) , typeof(Regulus.Samples.Helloworld.Common.Ghost.CIGreeter) );
                        _InterfaceProvider = new Regulus.Remote.InterfaceProvider(types);

                        var eventClosures = new List<Regulus.Remote.IEventProxyCreator>();
                        
                        _EventProvider = new Regulus.Remote.EventProvider(eventClosures);

                        _Serializer = new Regulus.Serialization.Serializer(new Regulus.Serialization.DescriberBuilder(typeof(Regulus.Remote.ClientToServerOpCode),typeof(Regulus.Remote.PackageCallMethod),typeof(Regulus.Remote.PackageErrorMethod),typeof(Regulus.Remote.PackageInvokeEvent),typeof(Regulus.Remote.PackageLoadSoul),typeof(Regulus.Remote.PackageLoadSoulCompile),typeof(Regulus.Remote.PackageProtocolSubmit),typeof(Regulus.Remote.PackageRelease),typeof(Regulus.Remote.PackageReturnValue),typeof(Regulus.Remote.PackageSetProperty),typeof(Regulus.Remote.PackageSetPropertyDone),typeof(Regulus.Remote.PackageUnloadSoul),typeof(Regulus.Remote.PackageUpdateProperty),typeof(Regulus.Remote.RequestPackage),typeof(Regulus.Remote.ResponsePackage),typeof(Regulus.Remote.ServerToClientOpCode),typeof(Regulus.Samples.Helloworld.Common.HelloReply),typeof(Regulus.Samples.Helloworld.Common.HelloRequest),typeof(System.Boolean),typeof(System.Byte[]),typeof(System.Byte[][]),typeof(System.Char),typeof(System.Char[]),typeof(System.Guid),typeof(System.Int32),typeof(System.String)).Describers);


                        _MemberMap = new Regulus.Remote.MemberMap(new System.Reflection.MethodInfo[] {new Regulus.Utility.Reflection.TypeMethodCatcher((System.Linq.Expressions.Expression<System.Action<Regulus.Samples.Helloworld.Common.IGreeter,Regulus.Samples.Helloworld.Common.HelloRequest>>)((ins,_1) => ins.SayHello(_1))).Method} ,new System.Reflection.EventInfo[]{  }, new System.Reflection.PropertyInfo[] { }, new System.Tuple<System.Type, System.Func<Regulus.Remote.IProvider>>[] {new System.Tuple<System.Type, System.Func<Regulus.Remote.IProvider>>(typeof(Regulus.Samples.Helloworld.Common.IGreeter),()=>new Regulus.Remote.TProvider<Regulus.Samples.Helloworld.Common.IGreeter>())});
                    }

                    byte[] Regulus.Remote.IProtocol.VerificationCode { get { return new byte[]{127,73,196,210,23,166,76,114,164,182,79,159,237,217,36,230};} }
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
            
            