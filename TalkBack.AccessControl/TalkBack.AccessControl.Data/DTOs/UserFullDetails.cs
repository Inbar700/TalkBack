using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TalkBack.AccessControl.Data.DTOs
{
    public class UserFullDetails
    {
       // public Guid Id { get; set; }
        public string? DisplayName { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime? LastLogin { get; set; }
    }
}
