using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public   class LoginSession : AppSession<LoginSession, StringRequestInfo>
{
    
    protected override void OnSessionClosed(CloseReason reason)
    {
        
        base.OnSessionClosed(reason);
    }
}

