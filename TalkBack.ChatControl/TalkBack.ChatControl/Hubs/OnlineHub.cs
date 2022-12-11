using Microsoft.AspNetCore.SignalR;

namespace TalkBack.ChatControl.Hubs
{
    public class OnlineHub : Hub
    {

        //static List<string> users = new List<string>();

        //public void AddUser(string name)
        //{
        //    if (!users.Contains(name))
        //    {
        //        users.Add(name);
        //        Clients.All.UpdateUserList(users);
        //    }
        //    Clients.All.updateUserList(users);
        //}
        //private readonly static Dictionary<string,string> _connections = new Dictionary<string,string>();
        //public async Task SendMessage(string user, string message)
        //{
        //    await Clients.All.SendAsync("SendMessage", user, message);
        //}

        //public string GetConnectionId() => Context.ConnectionId;

        //public override Task OnConnectedAsync()
        //{
        //    _connections.Add("bob", Context.ConnectionId);

        //    return base.OnConnectedAsync();
        //}

        //public override Task OnDisconnectedAsync(Exception ex)
        //{
        //    _connections.Remove("bob", Context.ConnectionId);

        //    return base.OnDisconnectedAsync(ex);
        //}

        //public IEnumerable<string> GetAllActiveConnections()
        //{
        //    return _connections.GetConnections("bob");
        //}
    }

}
