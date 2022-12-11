using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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
            var tokenString=_tokenService.GetToken(user.UserName, user.Password);
            if(tokenString == null)
                return StatusCode(StatusCodes.Status401Unauthorized);
            //return Ok(ret);

            return Ok(new AuthenticatedResponse { Token = tokenString });
        }
     

  
    }
}
