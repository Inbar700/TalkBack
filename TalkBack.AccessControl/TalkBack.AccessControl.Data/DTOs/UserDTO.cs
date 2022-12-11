using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalkBack.AccessControl.Data.DTOs
{
    public class UserDTO
    {
        public UserDTO()
        {

        }
        //convert model to modelDTO
        public UserDTO(User user)
        {
            Id = user.Id;
            UserName = user.UserName;
            LastLogin = user.LastLogin;
        }

        [Key]
        public Guid Id { get; set; }
        [StringLength(255)]
        public string UserName { get; set; } = null!;

        [Column(TypeName = "datetime")]
        public DateTime? LastLogin { get; set; }

        //convert modelDTO to model
        public User ToUser()
        {
            var user = new User();
            user.Id = Id;
            user.UserName = UserName;
            user.LastLogin = LastLogin;
            return user;
        }
    }
}
