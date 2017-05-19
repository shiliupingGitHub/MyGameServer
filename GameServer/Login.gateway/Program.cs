using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;

namespace Login.gateway
{
    class Program
    {
        static void Main(string[] args)
        {
            GameServer.Instance.Start();
        }
    }
}
