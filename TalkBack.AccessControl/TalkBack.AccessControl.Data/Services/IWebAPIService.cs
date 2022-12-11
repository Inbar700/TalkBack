using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalkBack.AccessControl.Data.DTOs;

namespace TalkBack.AccessControl.Data.Services
{
    public interface IWebAPIService
    {
        Task<UserFullDetails> GetUserAsyncById(Guid id);
    }
}
