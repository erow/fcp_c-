using Google.Protobuf;
using Google.Protobuf.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CSharpFcp
{
    class Program
    {

        static void led(LightMessage l)
        {

        }

        static void Main(string[] args)
        {
            Path a = new Path("/");
            Console.WriteLine(new Path("/asfd:2/a/b").abs_uri("../c"));
            Console.WriteLine(new Path("/asfd:2/a/b").abs_uri("./sf/d"));
            Console.WriteLine(Path.inPath("/a:0/b:2/c", "/a:0/b:2"));
            FcpMessage fcp = new FcpMessage();
            TcpDownNode tcp = new TcpDownNode();
            tcp.SetPath("/tcp:0");

            //tcp.addNode("/tcp:0", new FunctionalNode<LightMessage>(
            //    (LightMessage l) =>
            //        {
            //            var setting = new JsonFormatter.Settings(true);
            //            JsonFormatter json=new JsonFormatter(setting);
            //            Console.WriteLine(json.Format(l));
            //        }));
            tcp.addNode("asr", new StringFunctionalNode((String data) =>
             {
                 Console.WriteLine(data);
             }));

            tcp.Connect("127.0.0.1:1212");
            while(true)
            {
                try
                {
                    tcp.Recv();
                }
                catch
                {
                    break;
                }
            }
            //fcp.Direction = 1;
            //fcp.DstUri = "/a/b";
            //using (var output = new MemoryStream())
            //{
            //    fcp.WriteTo(output);
            //    var t = fcp.ToByteString();
            //    var data = output.ToArray();

            //    var str = Encoding.ASCII.GetString(fcp.ToByteArray());
            //    var fcp1 = FcpMessage.Parser.ParseFrom(Encoding.ASCII.GetBytes(str));

            //    Console.WriteLine(fcp1.GetHashCode() == fcp.GetHashCode());

            //}
        }
    }
}
