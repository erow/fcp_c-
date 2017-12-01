using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CSharpFcp
{
    abstract class Node_
    {
        public Path m_path
        {
            get;
            set;
        }
        protected string m_deal;
        protected string m_information;
        protected MemoryStream m_buffer = new MemoryStream();
        protected int msg_size = 0;

        protected Node_ m_gateway = null;
        public void setGateway(Node_ top)
        {
            m_gateway = top;
        }

        public abstract int sendMsg(FcpMessage msg);
        public abstract int handleMsg(FcpMessage msg);

        public int Rx(byte[] data)
        {
            if (m_buffer.Length == 0 && msg_size == 0)
            {
                for (int i = 0; i < data.Length; i++)
                {
                    if (data[i] == ':')
                    {
                        msg_size = int.Parse(Encoding.ASCII.GetString(m_buffer.ToArray()));
                        m_buffer = new MemoryStream(m_buffer.Capacity);
                        var new_data = new byte[data.Length - i-1];
                        Array.Copy(data, i + 1, new_data, 0, data.Length - i-1);
                        return Rx(new_data);
                    }
                    else
                    {
                        m_buffer.WriteByte(data[i]);
                    }
                }
                return 0;
            }
            else
            {
                for (int i = 0; i < data.Length; i++)
                {
                    m_buffer.WriteByte(data[i]);
                    if (m_buffer.Length == msg_size)
                    {
                        FcpMessage msg = FcpMessage.Parser.ParseFrom(m_buffer.ToArray());
                        handleMsg(msg);
                        m_buffer = new MemoryStream(m_buffer.Capacity);
                        msg_size = 0;
                        if (data.Length - i - 1 > 0)
                        {
                            var new_data = new byte[data.Length - i - 1];
                            Array.Copy(data, i + 1, new_data, 0, data.Length - i - 1);
                            return 1 + Rx(new_data);
                        }
                        else
                            return 1;
                    }
                }
                return 0;
            }
        }
        public abstract int Tx(byte[] data);
        public void SetPath(String path)
        {
            m_path = new Path(path);
        }

    }


}
