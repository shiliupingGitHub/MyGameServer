using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.SocketBase.Config;

public  class GameServer : SingleTon<GameServer>
{
    AppServer<LoginSession> mServer = new AppServer<LoginSession>();
    EasyClient mClient = new EasyClient();
    int mPort = 12021;
    bool mQuit = false;
    public void Start()
    {
        if(mServer.Setup(mPort))
        {
            mServer.Logger.Error("fail to listen port:" + mPort);
        }
        if (!mServer.Start())
        {
            mServer.Logger.Error("Failed to start!");
            return;
        }
        while (!mQuit)
        {
            string  command = Console.ReadLine();
            if (!string.IsNullOrEmpty(command))
                ExcuteConsleComand(command);
            continue;
        }
        mServer.Stop();
    }
    void ExcuteConsleComand(string command)
    {

    }
}

