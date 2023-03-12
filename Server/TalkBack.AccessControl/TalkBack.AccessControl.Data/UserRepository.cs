using Microsoft.EntityFrameworkCore;

namespace TalkBack.AccessControl.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly TalkBackDbContext _context;
        public UserRepository(TalkBackDbContext context)
        {
            _context = context;
        }
        public User GetById(Guid id)
        {
            return _context.Users.Find(id);
        }
        public User GetByUserName(string userName)
        {
            return _context.Users.FirstOrDefault(user => user.UserName == userName);
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
        public User Delete(Guid id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }   
            return user;
        }
        public User Update(Guid id, User user)
        {
            var userToUpdate = _context.Users.AsNoTracking().FirstOrDefault(x => x.Id == id);
            if (userToUpdate == null)
            {
                throw new ArgumentNullException("ID is not existed");
            }

            user.Id = userToUpdate.Id;
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
            return user;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
