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
        void Save();
    }
}
