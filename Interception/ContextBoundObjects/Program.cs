using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;

namespace ContextBoundObjects
{
    class Program
    {
        static void Main(string[] args)
        {
            var target = new TargetObject();
            target.DoSomething(50);

            Console.ReadLine();
        }
    }

    [Intercepting]
    public class TargetObject : ContextBoundObject
    {
        public int DoSomething(int c)
        {
            Console.WriteLine("Target object called with input: " + c);
            return c * 100;
        }
    }


    public class MySink : IMessageSink

    {
        private ObjRef realobject;

        public MySink(IMessageSink next)
        {
            this.NextSink = next;
        }


        public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
        {
            return NextSink.AsyncProcessMessage(msg, replySink);
        }
        
        public IMessageSink NextSink { get; set; }

        public IMessage SyncProcessMessage(IMessage msg)
        {
            IMethodMessage methodMessage = (IMethodMessage)msg;

            if (methodMessage.MethodName == ".ctor")
            {
                IMethodReturnMessage ret = (IMethodReturnMessage)NextSink.SyncProcessMessage(msg);
                this.realobject = (ObjRef)ret.ReturnValue;
                return ret;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Intercepted {methodMessage.MethodName}");
                Console.ResetColor();
                TargetObject obj = (TargetObject)RemotingServices.Unmarshal(this.realobject);
                obj.DoSomething(20);
                return NextSink.SyncProcessMessage(msg);
            }

        }

    }


    [AttributeUsage(AttributeTargets.Class)]
    public class InterceptingAttribute : ContextAttribute, IContributeClientContextSink, IContributeServerContextSink
    {
        public InterceptingAttribute(): base("SomeNameNotUsed")
        {
        }

        public override bool IsContextOK(Context ctx, System.Runtime.Remoting.Activation.IConstructionCallMessage ctorMsg)
        {
            Console.WriteLine("Check context");
            return base.IsContextOK(ctx, ctorMsg);
        }

        public IMessageSink GetClientContextSink(IMessageSink nextSink)
        {
            return new MySink(nextSink);
        }
        
        public IMessageSink GetServerContextSink(IMessageSink nextSink)
        {
            return new MySink(nextSink);
        }
    }
    





}
