using System;
using System.Collections.Generic;
using System.Diagnostics;
using static FcpMessage.Types;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Protobuf;

namespace CSharpFcp
{
    class FunctionalNode<T> :
     DownNode_ where T : IMessage<T>, new()
    {
        private static readonly MessageParser<T> _parser = new MessageParser<T>(() => new T());
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public static MessageParser<T> Parser { get { return _parser; } }

        public delegate void FunctionType(T fun);
        
        public FunctionType m_fun = null;
        private Action<T> p;

        public override int handleMsg(FcpMessage msg)
        {
            Debug.Assert((msg.Type == FcpType.Publish) || msg.Type == FcpType.ExtPublish);
            T t = Parser.ParseFrom(Encoding.ASCII.GetBytes(msg.Data));
            //Logger->debug("exec from:{} to:{}", msg.src_uri().c_str(), msg.dst_uri().c_str());
            p(t);
            return 0;
        }

        FunctionalNode(FunctionType fun)
        {
            m_fun = fun;
            m_information = typeof(T).Name;
            m_deal = "functional";
        }

        public FunctionalNode(Action<T> p)
        {
            this.p = p;
        }

        public override int Tx(byte[] data)
        {
            if (m_gateway != null)
                return m_gateway.Rx(data);
            return 0;
        }
    }


}
