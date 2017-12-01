using Google.Protobuf;
using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFcp
{
    abstract class TopNode_ :
        Node_
    {
        protected static ILog Logger = LogManager.GetLogger("console");
        public override int handleMsg(FcpMessage msg)
        {
            if (msg.Direction == 0)
            {
                sendMsg(msg);
            }
            //下节点无法处理的消息，转交网关
            else
            {
                if (m_gateway != null)
                    m_gateway.handleMsg(msg);
            }
            return 0;
        }
        public override int sendMsg(FcpMessage msg)
        {
            Logger.DebugFormat("%s/%d>>>", m_deal, msg.Type);
            Debug.Assert(msg.Direction == 0);
            var data = msg.ToByteArray();
            String t = data.Length + ":" + data;
            return Tx(Encoding.ASCII.GetBytes(t));
        }
       
    }
}
