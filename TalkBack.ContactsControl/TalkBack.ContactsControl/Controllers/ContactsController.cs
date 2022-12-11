using DM.DTOs;
using DM.Response;
using Microsoft.AspNetCore.Mvc;
using TalkBack.ContactsControl.Data;
using TalkBack.ContactsControl.Data.DTO;
using TalkBack.ContactsControl.Data.Repositories;
using TalkBack.ContactsControl.Data.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TalkBack.ContactsControl.Controllers
{
    //[Route("api/contacts")]
    //[ApiController]

    [ApiController]
    [Route("[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactRepository _contactRepository;
        private readonly IWebAPIService _webAPIService;
        private readonly ISignalrService _signalrService;
        
        public ContactsController(IContactRepository contactRepository,IWebAPIService webAPIService, ISignalrService signalrService)
        {
            _contactRepository = contactRepository;
            _webAPIService = webAPIService;

            _signalrService = signalrService;
        }

        [HttpPost("AddChatClient")]
        public Task<IActionResult> AddChatClient([FromBody] NewChatClientConnectionDto newChatClientConnectionDto)
        {
            IActionResult actionResult;
            if (newChatClientConnectionDto.Validate())
            {
                Response response = _signalrService.NewChatClientConnected(newChatClientConnectionDto.ChatClientId, newChatClientConnectionDto.ConnectionId);
                actionResult=Ok(response);
            }
            else
            {
                actionResult = BadRequest();
            }
            return Task.FromResult(actionResult);

        }
        [HttpPost("SendMessageToAll")]
        public Task<IActionResult> SendMessageToAll([FromBody] ChatMessageDto chatMessageDto)
        {
            IActionResult actionResult;
            if (chatMessageDto.Validate())
            {
                Response response = _signalrService.SendMessageToAll(chatMessageDto.ConnectionId, chatMessageDto.Message);
                actionResult = Ok(response);
            }
            else
            {
                actionResult= BadRequest();
            }
            return Task.FromResult(actionResult);
        }

        [HttpGet("get-all")]
        public async Task<IEnumerable<UserGet>> GetAll()
        {
            return await _webAPIService.GetAll();
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

        // PUT api/<ContactsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] UserDTO user)
        {
            _contactRepository.Update(id, user.ToUser());

            return Ok();

        }

        // DELETE api/<ContactsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
