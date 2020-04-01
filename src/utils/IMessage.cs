using System;
namespace NeoFS.Utils
{
    public static class MessageExtension
    {
        public static void Say(this Google.Protobuf.IMessage msg)
        {
            Console.WriteLine("{0} = {1}", msg.GetType().Name, msg);
        }
    }
}
