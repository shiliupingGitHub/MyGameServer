using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class cmd_login : CommandBase<LoginSession, StringRequestInfo>
{
    public override void ExecuteCommand(LoginSession session, StringRequestInfo requestInfo)
    {
        string userName = requestInfo.Parameters[0];
        string passwrd = requestInfo.Parameters[1];
        session.Send("hello world :" + userName);
    }
}

