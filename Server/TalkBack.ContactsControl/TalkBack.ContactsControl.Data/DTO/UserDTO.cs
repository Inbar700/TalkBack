using System.ComponentModel.DataAnnotations;

namespace TalkBack.ContactsControl.Data.DTO
{
    public class UserDTO
    {
        public UserDTO()
        {

        }
        public UserDTO(User user)
        {
            DisplayName = user.DisplayName;
        }
        [StringLength(255)]
        public string? DisplayName { get; set; }
        public User ToUser()
        {
            var user = new User();
            user.DisplayName = DisplayName;
            return user;
        }
    }
}
