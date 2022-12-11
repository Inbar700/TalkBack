using DM.Response;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public Response NewChatClientConnected(Guid chatClientId, string connectionId)
        {
            Response response;
            bool isSuccess = false;
            UserHub hub = new UserHub(_userHubContext);
            if (hub.CheckIfConnectionAlive(connectionId))
            {
                isSuccess = ConnectedClients.Instance.AddOrUpdateConnectionId(chatClientId, connectionId);
            }
            response = new Response
            {
                IsSuccess = isSuccess
            };
            return response;
        }

        public Response SendMessageToAll(string connectionId, string message)
        {
            Response response;

            UserHub hub = new UserHub(_userHubContext);
            hub.SendMessageToAll(message);

            response = new Response
            {
                Message = message,
                IsSuccess = true
            };

            return response;
        }
    }
}
