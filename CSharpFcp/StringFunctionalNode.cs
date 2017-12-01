using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FcpMessage.Types;

namespace CSharpFcp
{
    class StringFunctionalNode :
     DownNode_
    {
        public delegate void FunctionType(String fun);

        public FunctionType m_fun = null;
        private Action<String> p;

        public override int handleMsg(FcpMessage msg)
        {
            Debug.Assert((msg.Type == FcpType.Publish) || msg.Type == FcpType.ExtPublish);
            //Logger->debug("exec from:{} to:{}", msg.src_uri().c_str(), msg.dst_uri().c_str());
            p(msg.Data);
            return 0;
        }

        StringFunctionalNode(FunctionType fun)
        {
            m_fun = fun;
            m_information = typeof(String).Name;
            m_deal = "functional";
        }

        public StringFunctionalNode(Action<String> p)
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
