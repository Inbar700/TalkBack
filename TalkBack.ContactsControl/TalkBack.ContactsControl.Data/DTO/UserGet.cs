using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalkBack.ContactsControl.Data.DTO
{
    public class UserGet
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
       // public string Password { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}
