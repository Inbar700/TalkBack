using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
