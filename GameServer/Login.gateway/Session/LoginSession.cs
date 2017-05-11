using SuperSocket.SocketBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public   class LoginSession : AppSession<LoginSession>
{
    
    protected override void OnSessionClosed(CloseReason reason)
    {
        
        base.OnSessionClosed(reason);
    }
}

