using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFcp
{
    class TcpDownNode : DownNode_
    {

        private IPAddress target; //Socket address information
        private Socket m_socket;

        public override int Tx(byte[] data)
        {
            
            return m_socket.Send(data);
        }

        public int Connect(string deal)
        {
            m_deal = deal;
           
            try
            {
                var parts = deal.Split(':');
                target = IPAddress.Parse(parts[0]);
                m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                m_socket.Connect(new IPEndPoint(target, Int32.Parse(parts[1]))); //配置服务器IP与端口  
                Console.WriteLine("连接服务器成功");
                return 0;
            }
            catch
            {
                Console.WriteLine("连接服务器失败，请按回车键退出！");
                return -1;
            }
        }

        public int Recv()
        {
            byte[] data = new byte[100];
            int len= m_socket.Receive(data);
            var new_data = new byte[len];
            Array.Copy(data, 0, new_data, 0, len);
            return Rx(new_data);
        }

    };
}
