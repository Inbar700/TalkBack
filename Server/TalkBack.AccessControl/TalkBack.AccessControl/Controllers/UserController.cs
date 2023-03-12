using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TalkBack.AccessControl.Data;
using TalkBack.AccessControl.Data.DTOs;

namespace TalkBack.AccessControl.Controllers
{
    [Authorize]
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet("get-all")]
        public IEnumerable<UserDTO> GetAll()
        {
            return _userRepository.GetAll().Select(x=> new UserDTO(x)).ToList();
        }

        [HttpGet("{id}")]
        public UserDTO GetById(Guid id)
        {
            var ret= _userRepository.GetById(id);
            return new UserDTO(ret);
        }

        [HttpPost]
        public void Post([FromBody] UserFullDetails user)
        {

        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] UserPostDTO user)
        {
            _userRepository.Update(id, user.ToPostUser());
            return Ok();
        }
    }
}
