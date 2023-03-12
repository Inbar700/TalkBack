using DM.Response;

namespace TalkBack.ContactsControl.Data.Services
{
    public interface ISignalrService
    {
        Response NewChatClientConnected(Guid chatClientId, string connectionId, string name);
        Response SendMessageToUser(string name, string message, string fromUserName);
        Response InvitePlayer(string name, string message, string fromUserName);
        Response GetAllConnectedUsers();
    }
}
