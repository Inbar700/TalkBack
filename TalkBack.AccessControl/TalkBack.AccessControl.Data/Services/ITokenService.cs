using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalkBack.AccessControl.Data.Services
{
    public interface ITokenService
    {
        string? GetToken(string userName, string password);
    }
}
