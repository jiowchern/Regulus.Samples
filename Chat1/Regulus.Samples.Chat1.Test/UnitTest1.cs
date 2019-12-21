using NUnit.Framework;
using System.Collections.Generic;

namespace Regulus.Samples.Chat1.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void Test1()
        {
            var room = new Regulus.Samples.Chat1.Server.Room();

            var chatter1 = new Chatter();
            var chatter2 = new Chatter();

            

            room.Join(chatter1);
            chatter1.Chatable.Send("chat1", "hello0");
            room.Join(chatter2);


            chatter1.Chatable.Send("chat1" , "hello1");
            chatter1.Chatable.Send("chat2", "hello2");

            room.Leave(chatter1);
            room.Leave(chatter2);
            

            Assert.AreEqual("chat1", chatter1.Messages[0].Item1);
            Assert.AreEqual("hello0", chatter1.Messages[0].Item2);

            Assert.AreEqual("chat1", chatter1.Messages[1].Item1);
            Assert.AreEqual("hello1", chatter1.Messages[1].Item2);

            Assert.AreEqual("chat2", chatter1.Messages[2].Item1);
            Assert.AreEqual("hello2", chatter1.Messages[2].Item2);

            Assert.AreEqual("chat1", chatter2.Messages[0].Item1);
            Assert.AreEqual("hello1", chatter2.Messages[0].Item2);

            Assert.AreEqual("chat2", chatter2.Messages[1].Item1);
            Assert.AreEqual("hello2", chatter2.Messages[1].Item2);

        }
    }
}
