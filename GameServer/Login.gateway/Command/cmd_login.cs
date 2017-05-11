using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class cmd_login : CommandBase<LoginSession, BinaryRequestInfo>
{
    public override void ExecuteCommand(LoginSession session, BinaryRequestInfo requestInfo)
    {
       
        session.Send("hello world :");
    }
}

