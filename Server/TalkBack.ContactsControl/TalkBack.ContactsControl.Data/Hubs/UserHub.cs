using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using TalkBack.ContactsControl.Data.SignalR;

namespace TalkBack.ContactsControl.Data.Hubs
{
    [Authorize]
    public class UserHub : Hub
    {
        private readonly IHubContext<UserHub> _hubContext;

        public UserHub(IHubContext<UserHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public override async Task OnConnectedAsync()
        {
            ConnectedClients.Instance.OnClientConnected(Context.ConnectionId);
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            ConnectedClients.Instance.OnClientDisconnected(Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
        public bool CheckIfConnectionAlive(string connectionId)
        {
            return ConnectedClients.Instance.CheckIfConnected(connectionId);
        }
        public void SendMessageToAll(string message)
        {
            _hubContext.Clients.All.SendAsync("ReceiveMessage", message).Wait();
        }

        public string GetSenderName()
        {
            return Context.User.Identity.Name;
        }

        public void  SendMessageToUser(string name, string message, string fromUser)
        //public void SendMessageToUser(string name, string message)
        {
            //_hubContext.Clients.Client(connectionId).SendAsync("GetMessage", message).Wait();
            //var mySelf = Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //var connId = ConnectedClients.Instance.GetByConnId(Context.ConnectionId);
            //var myself = GetSenderName();
            _hubContext.Clients.Users(fromUser, name).SendAsync("GetMessage", message, fromUser, name).Wait();
            //_hubContext.Clients.Users(Context.User.Identity.Name,name).SendAsync("ReceiveMessage", message, Context.User.Identity.Name, name).Wait();
        }
        public void InvitePlayer(string name, string message, string fromUser)
        {
            _hubContext.Clients.User(name).SendAsync("InvitePlayer", message, name).Wait();
        }

        //public void UpdateClientList(string message)
        //{
        //    List<string> clientList = ConnectedClients.Instance.__connectedClientsIds;
        //    _hubContext.Clients.All.SendAsync("ReciveMessage", clientList).Wait();

        //}
 
    }
}
