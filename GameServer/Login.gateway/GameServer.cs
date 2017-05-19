using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperSocket.SocketBase.Config;

public  class GameServer : AppServer<LoginSession>
{
    EasyClient mClient = new EasyClient();
    protected override bool Setup(IRootConfig rootConfig, IServerConfig config)
    {
        mClient.SetUp();
        return base.Setup(rootConfig, config);
    }
}

