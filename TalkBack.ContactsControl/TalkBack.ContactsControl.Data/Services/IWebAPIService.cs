using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TalkBack.ContactsControl.Data.DTO;

namespace TalkBack.ContactsControl.Data.Services
{
    public interface IWebAPIService
    {
        //Task<UserGet> GetUserAsyncById(Guid id);
        Task<List<UserGet>> GetAll();
    }
}
