using System.Security.Claims;
using TalkBack.AccessControl.Data.Models;

namespace TalkBack.AccessControl.Data.Services
{
    public interface ITokenService
    {
        AuthenticatedResponse Login(LoginModel loginModel);
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        AuthenticatedResponse Refresh(TokenApiModel tokenApiModel);
    }
}
