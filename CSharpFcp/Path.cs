using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections;
namespace CSharpFcp
{
    enum relType
    {
        parent,
        self,
        brother,
        child
    };

    class Path
    {
        public string[] m_path;
        //path 需为绝对路径
        public Path(string path)
        {
            m_path = path.Substring(1).Split('/');
        }

        public relType Relation(Path path)
        {
            bool match = true;
            string[] vec = path.m_path;
            for (int i = 0; i < m_path.Length; i++)
            {
                
                if (vec[i ] != m_path[i])
                    match = false;
                if (!belong(m_path[i], vec[i ]))
                    return relType.parent;
            }
            if (m_path.Length > vec.Length)
                return relType.parent;
            if (vec.Length == m_path.Length )
                return match ? relType.self : relType.brother;
            return relType.child;
        }

        public static bool belong(string v1, string v2)
        {
            var p1 = v1.Split(':');
            var p2 = v2.Split(':');
            if (p1[0] != p2[0])
                return false;
            if (p2.Length == 1)
                return true;
            return p1[1] == p2[1];
        }

        public relType relation(string abs_uri)
        {
            return Relation(new Path(abs_uri));
        }
        public string abs_uri(string uri)
        {
            var path = new Stack<String>(m_path);
            string absuri = "";
            if (uri[0] == '/')
                return uri;
            else if ((uri.Substring(0, 2) == "./") || (uri.Substring(0, 3) == "../"))
            {
                var vec = uri.Split('/');
                foreach (var t in vec)
                {
                    if (t == "." || t == "")
                        continue;
                    else if (t == "..")
                        path.Pop();
                    else
                        path.Push(t);
                }
                if (path.Count() == 0)
                    return "/";
                foreach (var t in path.Reverse())
                    absuri += "/" + t;
                return absuri;
            }
            else
            {
                //相对目录
                foreach (var t in path.Reverse())
                    absuri += "/" + t;
                return absuri + "/" + uri;
            }
        }
        public string abs_uri()
        {
            return abs_uri("./");
        }

        /* check if target in path*/
        public static bool inPath(string target, string path)
        {
            var p1 = target.Split('/');
            var p2 = path.Split('/');
            int index = 0;
            foreach (var t in p1)
            {
                if (t == "")
                    continue;
                while (p2[index] == "")
                {
                    index++;
                }
                if (belong(p2[index], t))
                {
                    index++;
                    if (index==p2.Length)
                        return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }


}
