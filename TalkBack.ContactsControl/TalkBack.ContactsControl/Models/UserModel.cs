namespace TalkBack.ContactsControl.Models
{
    public class UserModel
    {
        public string UserName { get; set; }
        public string ConnectionId { get; set; }

        public UserModel(string userName, string connectionId)
        {
            UserName=userName;
            ConnectionId=connectionId;
        }

    }
}
