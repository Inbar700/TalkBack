using Microsoft.AspNetCore.SignalR;
using TalkBack.ChatControl.DTO;
using TalkBack.ChatControl.Models;
using TalkBack.ChatControl.Services;

namespace TalkBack.ChatControl.Hubs
{
    public class ChatHub:Hub
    {
        //private readonly IWebAPIService _webAPIService;
        //public ChatHub(IWebAPIService webAPIService)
        //{
        //    _webAPIService = webAPIService;
        //}
        //private static List<UserConnection> usersList = new List<UserConnection>();
        //public async Task GetAllUsers()
        //{
        //    var users = await _webAPIService.GetAll();
        //    foreach(var user in users)
        //    {
        //        usersList.Add(new UserConnection
        //        {
        //            personId = user.Id,
        //            DisplayName = user.UserName,
        //            //SignalrId = Context.ConnectionId,
        //            IsOnline = false
        //        });
        //    }
        //    await Clients.All.SendAsync("GetAllUsersResponse", usersList);
          
        //}
        //public override  Task OnConnectedAsync()
        //{
        //    var userName = this.Context.User.Identity.Name;
        //    var connectionId = this.Context.ConnectionId;
   
        //     //var currentUser=usersList.FirstOrDefault(c => c.SignalrId == Context.ConnectionId);
        //        currentUser.IsOnline = true;

        //        Clients.Others.SendAsync("UpdateUsers", usersList);
        //    return base.OnConnectedAsync();
        //}
        public async Task sendMsg(string connId, string msg)
        {
            await Clients.Client(connId).SendAsync("sendMsgResponse", Context.ConnectionId, msg);
        }

        //public async Task<IEnumerable<UserGet>> GetAll()
        //{
        //    return await _webAPIService.GetAll();
        //}
    }
}
