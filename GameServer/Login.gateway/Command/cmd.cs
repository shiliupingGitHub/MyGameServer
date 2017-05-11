using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class cmd : CommandBase<LoginSession, StringRequestInfo>
{
    public override void ExecuteCommand(LoginSession session, StringRequestInfo requestInfo)
    {
        
        session.Send("hello world :");
    }
}

