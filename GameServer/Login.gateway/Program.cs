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
            var appSever = new GameServer();
            if(!appSever.Setup(2013))
            {
                Console.WriteLine("Failed to setup!");
                Console.ReadKey();
                return;
            }

            if (!appSever.Start())
            {
                Console.WriteLine("Failed to start!");
                Console.ReadKey();
                return;
            }
            appSever.Logger.Debug("hello log");
            Console.WriteLine("The server started successfully, press key 'q' to stop it!");
           // appSever.NewRequestReceived += new RequestHandler<AppSession, StringRequestInfo>(appServer_NewRequestReceived);
            while (Console.ReadKey().KeyChar != 'q')
            {
                Console.WriteLine();
                continue;
            }
            //Stop the appServer
            appSever.Stop();
            Console.WriteLine("The server was stopped!");
            Console.ReadKey();
        }
    }
}
