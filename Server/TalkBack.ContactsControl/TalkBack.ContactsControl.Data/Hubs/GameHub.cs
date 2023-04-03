using DM.Game;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using TalkBack.ContactsControl.Data.Services;

namespace TalkBack.ContactsControl.Data.Hubs
{
    [Authorize]
    public class GameHub : Hub
    {
        private readonly IHubContext<GameHub> _hubContext;
        private readonly IGameService _gameService;
        public GameHub(IHubContext<GameHub> hubContext, IGameService gameService)
        {
            _hubContext = hubContext;
            _gameService = gameService;
        }
        public void SendUpdatedBoard(string name, Board board)
        {
            board = _gameService.GetBoard();
            _hubContext.Clients.User(name).SendAsync("SendUpdatedBoard", board, name).Wait();
        }
    }
}
