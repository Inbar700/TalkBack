namespace TalkBack.ContactsControl.Data.Repositories
{
    public interface IContactRepository
    {
        User GetById(Guid id);
        IQueryable<User> GetAll();
        Guid Add(User user);
        User Update(Guid id, User user);
        //User Delete(Guid id);
    }
}
