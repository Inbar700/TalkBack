using DM.DTOs;
using DM.Response;
using Microsoft.AspNetCore.Mvc;
using TalkBack.ContactsControl.Data;
using TalkBack.ContactsControl.Data.DTO;
using TalkBack.ContactsControl.Data.Repositories;
using TalkBack.ContactsControl.Data.Services;

namespace TalkBack.ContactsControl.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactRepository _contactRepository;
        private readonly ISignalrService _signalrService;
        
        public ContactsController(IContactRepository contactRepository, ISignalrService signalrService)
        {
            _contactRepository = contactRepository;   
            _signalrService = signalrService;
        }

        [HttpPost("AddChatClient")]
        public Task<IActionResult> AddChatClient([FromBody] NewChatClientConnectionDto newChatClientConnectionDto)
        {
            IActionResult actionResult;
            if (newChatClientConnectionDto.Validate())
            {
                Response response = _signalrService.NewChatClientConnected(newChatClientConnectionDto.ChatClientId, newChatClientConnectionDto.ConnectionId,
                    newChatClientConnectionDto.Name);
                actionResult =Ok(response);
            }
            else
            {
                actionResult = BadRequest();
            }
            return Task.FromResult(actionResult);
        }
      
        [HttpPost("SendMessageToUser")]
        public Task<IActionResult> SendMessageToUser([FromBody] ChatMessageDto chatMessageDto)
        {
            IActionResult actionResult;
            if (chatMessageDto.Validate())
            {
                Response response = _signalrService.SendMessageToUser(chatMessageDto.ConnectionId, chatMessageDto.Message, chatMessageDto.FromUser);
                actionResult =Ok(response);
            }
            else
            {
                actionResult = BadRequest();
            }
            return Task.FromResult(actionResult);
        }

        [HttpPost("InvitePlayer")]
        public Task<IActionResult> InvitePlayer([FromBody] ChatMessageDto chatMessageDto)
        {
            IActionResult actionResult;
            if (chatMessageDto.ValidateUsers())
            {

                Response response = _signalrService.InvitePlayer(chatMessageDto.ConnectionId, chatMessageDto.Message, chatMessageDto.FromUser);
                actionResult = Ok(response);
            }
            else
            {
                actionResult = BadRequest();
            }
            return Task.FromResult(actionResult);
        }

        [HttpGet("{id}")]
        public User GetById(Guid id)
        {
            var ret = _contactRepository.GetById(id);
            return ret;
        }

        // POST api/<ContactsController>
        [HttpPost]
        public async void Post([FromBody] UserDTO user)
        {
           
        }

        [HttpGet("GetConnectedUsers")]
        public async Task<IActionResult> GetConnectedUsers()
        {
            var allConnectedUsers = _signalrService.GetAllConnectedUsers();
      
            return Ok(allConnectedUsers.Entity);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] UserDTO user)
        {
            _contactRepository.Update(id, user.ToUser());
            return Ok();
        }
    }
}
