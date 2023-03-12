using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalkBack.AccessControl.Data.DTOs
{
    public class UserPostDTO
    {
        public UserPostDTO()
        {

        }
        public UserPostDTO(User user)
        {
            UserName= user.UserName;
            Password= user.Password;
            LastLogin= user.LastLogin;
        }
        [StringLength(255)]
        public string UserName { get; set; } = null!;
        [StringLength(255)]
        public string Password { get; set; } = null!;
        [Column(TypeName = "datetime")]
        public DateTime? LastLogin { get; set; }
        public User ToPostUser()
        {
            var user = new User();
            user.UserName = UserName;
            user.Password = Password;
            user.LastLogin = LastLogin;
            return user;
        }
    }
}
