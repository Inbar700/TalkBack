using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalkBack.ContactsControl.Data.SignalR;

namespace TalkBack.ContactsControl.Data.Hubs
{
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
        //public void UpdateClientList(string message)
        //{
        //    List<string> clientList = ConnectedClients.Instance.__connectedClientsIds;
        //    _hubContext.Clients.All.SendAsync("ReciveMessage", clientList).Wait();

        //}
        public void UpdateClientList()
        {
            List<string> clientList = ConnectedClients.Instance.__connectedClientsIds;
            _hubContext.Clients.All.SendAsync("ReciveClientList", clientList).Wait();

        }
    }
}
