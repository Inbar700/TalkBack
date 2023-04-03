using Microsoft.AspNetCore.Mvc;
using TalkBack.ContactsControl.Data.Services;

namespace TalkBack.ContactsControl.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet("GetBoard")]
        public async Task<IActionResult> GetBoard()
        {
            var board = _gameService.GetBoard();
            return Ok(board.board);
        }
        [HttpPost("UpdateBoard")]
        public async Task<IActionResult> UpdateBoard(int row, int col)
        {
            var updatedBoard=_gameService.UpdateBoard(row,col);
            if (updatedBoard == null)
            {
                return BadRequest("Invalid move");
            }
            return Ok(updatedBoard);
        }
    }
}
