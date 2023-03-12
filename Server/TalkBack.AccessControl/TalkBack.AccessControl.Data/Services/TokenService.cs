using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TalkBack.AccessControl.Data.Models;

namespace TalkBack.AccessControl.Data.Services
{
    public class TokenService:ITokenService
    {
        public IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private SymmetricSecurityKey key;
        private SigningCredentials signinCredentials;

        public TokenService(IConfiguration config, IUserRepository userRepository)
        {
            _configuration = config;
            _userRepository = userRepository;
            key= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            signinCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        }
        public AuthenticatedResponse Login(LoginModel loginModel)
        {
            var user = _userRepository.GetByUserName(loginModel.UserName);
            if (user != null && user.Password == loginModel.Password)
            {
                var claims = new[]
               {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, "Manager"), //for managers
                    new Claim(ClaimTypes.NameIdentifier, user.UserName),
                    new Claim("userId", user.Id.ToString()),
                    new Claim(ClaimTypes.Expired, DateTime.Now.AddMinutes(5).ToString()),
            };    
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(100),
                    signingCredentials: signinCredentials
                    );
                var tokenString= new JwtSecurityTokenHandler().WriteToken(token);
                var refreshToken = GenerateRefreshToken();
                user.RefreshToken= refreshToken;
                user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
                _userRepository.Save();
                return new AuthenticatedResponse
                {
                    Token = tokenString,
                    RefreshToken = refreshToken,
                };
            }
            else
                return null;
        }

        public string GenerateAccessToken(IEnumerable<Claim> claims)
        {
            var tokenOptions = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(1),
                    signingCredentials: signinCredentials
                    );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return tokenString;
        }
        public string GenerateRefreshToken()
        {
            var randomNumber=new byte[32];
            using(var rng= RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }
        public AuthenticatedResponse Refresh(TokenApiModel tokenApiModel)
        {
            if (tokenApiModel is null)
                return null;
            string accessToken=tokenApiModel.AccessToken;
            string refreshToken=tokenApiModel.RefreshToken;
            var principal=GetPrincipalFromExpiredToken(accessToken);
            var userName = principal.Identity.Name;
            var user = _userRepository.GetByUserName(userName);
            
            if (user == null || user.RefreshToken!= refreshToken || user.RefreshTokenExpiryTime<=DateTime.Now)
                return null;
            var newAccessToken = GenerateAccessToken(principal.Claims);
            var newRefreshToken = GenerateRefreshToken();
            user.RefreshToken = newRefreshToken;
            return new AuthenticatedResponse()
            {
                Token = newAccessToken,
                RefreshToken = newRefreshToken,
            };
        }
    }
}
