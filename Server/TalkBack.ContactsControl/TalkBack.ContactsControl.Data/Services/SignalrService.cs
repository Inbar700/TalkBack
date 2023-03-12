using DM.Response;
using Microsoft.AspNetCore.SignalR;
using TalkBack.ContactsControl.Data.Hubs;
using TalkBack.ContactsControl.Data.SignalR;

namespace TalkBack.ContactsControl.Data.Services
{
    public class SignalrService : ISignalrService
    {
        private IHubContext<UserHub> _userHubContext;
        public SignalrService(IHubContext<UserHub> userHubContext)
        {
            _userHubContext = userHubContext;
        }
        public Response GetAllConnectedUsers()
        {
            Response response;
            var allConnectedClients = ConnectedClients.Instance.GetAllConnectedUsers();
            response = new Response
            {
                Entity = allConnectedClients,
                IsSuccess = true
            };

            return response;
        }
        public Response NewChatClientConnected(Guid chatClientId, string connectionId, string name)
        {
            Response response;
            bool isSuccess = false;
            UserHub hub = new UserHub(_userHubContext);
            if (hub.CheckIfConnectionAlive(connectionId))
            {
                isSuccess = ConnectedClients.Instance.AddOrUpdateConnectionId(chatClientId, connectionId, name);
            }
            response = new Response
            {
                IsSuccess = isSuccess
            };
            return response;
        }
        public Response SendMessageToUser(string name, string message, string fromUser)
        {
            Response response;
            var userName=ConnectedClients.Instance.GetAllConnectedUsers().Where(x => x.Name.Equals(name)).FirstOrDefault();

            UserHub hub = new UserHub(_userHubContext);
            hub.SendMessageToUser(userName.Name, message, fromUser);

            response = new Response
            {
                Message = message,
                IsSuccess = true
            };

            return response;
        }
        public Response InvitePlayer(string name, string message, string fromUser)
        {
            Response response;
            var userName = ConnectedClients.Instance.GetAllConnectedUsers().Where(x => x.Name.Equals(name)).FirstOrDefault();

            UserHub hub = new UserHub(_userHubContext);
            hub.InvitePlayer(userName.Name, message, fromUser);

            response = new Response
            {
                Message = message,
                IsSuccess = true
            };

            return response;
        }
    }
}
