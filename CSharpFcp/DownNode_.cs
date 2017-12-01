using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using System.Diagnostics;
using static FcpMessage.Types;

namespace CSharpFcp
{
    abstract class DownNode_ :
        Node_
    {
        protected string m_uri = null;//为了解决转发URI的问题
        protected List<Tuple<string, Node_>> m_table = new List<Tuple<string, Node_>>();
        public static ILog Logger = LogManager.GetLogger("console");
        protected int handlePublish(FcpMessage fcp)
        {
            Debug.Assert(fcp.Type == FcpType.Publish);
            bool find = false;
            string uri = fcp.DstUri;
            foreach (var t in m_table)
            {
                var path = m_path.abs_uri(t.Item1);
                if (Path.inPath(uri, path))
                {
                    fcp.Direction = 0;
                    Logger.DebugFormat("%s/%d>>>%s", m_deal, fcp.Type, t.Item1);
                    t.Item2.handleMsg(fcp);
                    find = true;
                }
            }
            if (find)
                return 1;
            else
                return 0;
        }
        protected int handleLocal(FcpMessage fcp)
        {
            if (FcpType.Publish == fcp.Type)
                return handlePublish(fcp);

            return -1;
        }

        public DownNode_()
        {
            m_deal = "un init";
            m_information = "DownNode_";
        }
        public override int handleMsg(FcpMessage fcp)
        {
            string uri = fcp.DstUri;
            relType rel = m_path.relation(uri);

            if (rel == relType.parent)
            {
                sendMsg(fcp);
            }
            else if (rel == relType.self)
            {
                handleLocal(fcp);
            }
            //与自己与父亲有关
            else if (rel == relType.brother)
            {
                //handleLocal(fcp);
                if (fcp.Direction == 1)
                    sendMsg(fcp);
                else
                    handleLocal(fcp);
            }
            //与自己无关，只与孩子有关
            else if (rel == relType.child)
            {
                FcpMessage t_fcp = fcp;
                t_fcp.Direction = 0;
                handleLocal(t_fcp);
            }

            return 0;
        }
        public override int sendMsg(FcpMessage msg)
        {

            Logger.DebugFormat("<<<%s/%s", m_deal, msg.Type);
            System.Diagnostics.Debug.Assert(msg.Direction == 1);
            var output = new MemoryStream();
            msg.WriteTo(output);
            var data = output.ToArray();
            
            String t = data.Length + ":" + Encoding.ASCII.GetString(data);

            return Tx(Encoding.ASCII.GetBytes(t));
        }



        public void addSubscribe(string dst_uri, Node_ node)
        {
            Logger.DebugFormat("-----------------\nSubscribe from:%s to: %s\n", m_deal, dst_uri);

            int n = -1;
            foreach (var t in m_table)
            {
                if (Path.belong(t.Item1, "subscriber"))
                {
                    string number = t.Item1.Split(':')[1];
                    int num = Int32.Parse(number);
                    n = Math.Max(num, n);
                }
            }
            string call_uri = "subscriber:" + (n + 1);
            FcpMessage msg = new FcpMessage();
            msg.DstUri = (m_path.abs_uri(dst_uri));
            msg.SrcUri = (m_path.abs_uri(call_uri));
            msg.Type = (FcpType.Subscribe);
            //msg.Data("");
            sendMsg(msg);
            m_table.Add(new Tuple<string, Node_>(call_uri, node));
        }

        public void publish<T>(string uri, T data) where T : IMessage
        {

            FcpMessage fcp = new FcpMessage();
            fcp.DstUri = (m_path.abs_uri(uri));
            fcp.SrcUri = (m_path.abs_uri());
            fcp.Type = (FcpType.Publish);
            fcp.Data = Encoding.ASCII.GetString(data.ToByteArray());
            relType rel = m_path.relation(uri);
            //if protected unsafe List<Tuple<string, Node_*>> Table { get => table; set => table = value; }

            if (rel == relType.child)
            {
                fcp.Direction = (0);
                handleMsg(fcp);
            }
            else
            {
                fcp.Direction = (1);
                sendMsg(fcp);
            }

            Logger.DebugFormat("publishEx -----------------------");
            fcp.DstUri = (m_path.abs_uri(uri));
            fcp.Direction = (1);
            fcp.Type = (FcpType.ExtPublish);
            //额外发送一个消息给master，由master转发给订阅者
            sendMsg(fcp);
            Logger.DebugFormat("publish end-----------------------");
        }

        public void addNode(string path, Node_ node)
        {
            int n = -1;
            foreach (var t in m_table)
            {
                if (Path.belong(t.Item1, path))
                {
                    string number = t.Item1.Split(':')[1];
                    int num = Int32.Parse(number);
                    n = Math.Max(num, n);
                }
            }
            string node_uri = path + ":" + (n + 1);
            m_table.Add(new Tuple<string, Node_>(node_uri, node));
            node.setGateway(this);
            node.m_path = new Path(m_path.abs_uri(node_uri));
        }
    }
}
