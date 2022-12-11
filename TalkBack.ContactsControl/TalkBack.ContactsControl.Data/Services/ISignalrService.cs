using DM.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalkBack.ContactsControl.Data.Services
{
    public interface ISignalrService
    {
        Response NewChatClientConnected(Guid chatClientId, string connectionId);
        Response SendMessageToAll(string connectionId, string message);
    }
}
