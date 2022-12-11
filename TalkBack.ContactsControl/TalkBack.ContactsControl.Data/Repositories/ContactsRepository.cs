using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalkBack.ContactsControl.Data.Repositories
{
    public class ContactsRepository : IContactRepository
    {
        private readonly TalkBackDbContext _context;
        public ContactsRepository(TalkBackDbContext context)
        {
            _context = context;
        }
        public User GetById(Guid id)
        {
            return _context.Users.Find(id);
        }
        public IQueryable<User> GetAll()
        {
            return _context.Users;
        }
        public Guid Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user.Id;
        }
        public User Update(Guid id, User user)
        {
            var userToUpdate = _context.Users.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (userToUpdate == null)
            {
                // Handle ID is not existed
                throw new ArgumentNullException("ID is not existed");
            }

            user.Id = userToUpdate.Id;
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
            return user;
        }


    }
}
