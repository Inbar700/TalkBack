using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalkBack.AccessControl.Data
{
    public interface IUserRepository
    {
        User GetById(Guid id);
        User GetByUserName(string userName);
        IQueryable<User> GetAll();
        Guid Add(User user);
        User Update(Guid id,User user);
        User Delete(Guid id);
    }
}
