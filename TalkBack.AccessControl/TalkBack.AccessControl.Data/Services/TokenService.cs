using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TalkBack.AccessControl.Data.Services
{
    public class TokenService:ITokenService
    {
        public IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public TokenService(IConfiguration config, IUserRepository userRepository)
        {
            _configuration = config;
            _userRepository = userRepository;
        }
        public string? GetToken(string userName, string password)
        {
            var user = _userRepository.GetByUserName(userName);
            if (user != null && user.Password == password)
            {
                var claims = new[]
               {      
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim(JwtRegisteredClaimNames.GivenName, user.UserName),
                    new Claim("userId", user.Id.ToString()),
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(10),
                    signingCredentials: signIn);

                //signingCredentials is object of type SigningCredentials, that get some secuirty key and save it in secure way
                //var signingCredentials = new SigningCredentials(
                //    new SymmetricSecurityKey(
                //        Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
                //    SecurityAlgorithms.HmacSha256);

                //_configuration["Jwt:Key"] => "talkbacksecuritykey"

                //var securityToken = new JwtSecurityToken(
                //    _configuration["Jwt:Issuer"],
                //    _configuration["Jwt:Audience"],
                //    //issuer: "", //it's me
                //    //audience: "", //everyone
                //    expires: DateTime.Now.AddMinutes(30), //expires after 30 min if the user didn't request any token
                //    claims: claims,
                //    signingCredentials: signingCredentials
                //    );
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            else
                return null;
        }
    }
}
