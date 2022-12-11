using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TalkBack.AccessControl.Data;
using TalkBack.AccessControl.Data.DTOs;
using TalkBack.AccessControl.Data.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TalkBack.AccessControl.Controllers
{
    [Authorize]
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IWebAPIService _webAPIService;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet("get-all")]
        //[Authorize]
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

        //[HttpPost]
        ////public void Post([FromBody] UserDTO user)
        //public void Post([FromBody] UserPostDTO user)
        //{
        //    //add validation - if not null
        //    //notify registered services
        //    //_userRepository.Add(user.ToUser());
        //    _userRepository.Add(user.ToPostUser());
        //}
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

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            //add validation - if not null
            //notify registered services
            _userRepository.Delete(id);
        }
    }
}
