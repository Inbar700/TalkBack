using Microsoft.AspNetCore.Mvc;
using TalkBack.AccessControl.Data;
using TalkBack.AccessControl.Data.Models;
using TalkBack.AccessControl.Data.Services;

namespace TalkBack.AccessControl.Controllers
{
    [Route("api/access")]
    [ApiController]
    public class AccessController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;
        public AccessController(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel user)
        {
            return Ok(_tokenService.Login(user));
        }
        [HttpPost("refresh")]
        public IActionResult Refresh([FromBody] TokenApiModel tokenApiModel)
        {
            var token=_tokenService.Refresh(tokenApiModel);
            return Ok(token);
        }




    }
}
