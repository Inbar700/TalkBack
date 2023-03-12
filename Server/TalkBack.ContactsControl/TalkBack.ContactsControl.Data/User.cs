using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TalkBack.ContactsControl.Data
{
    [Table("User")]
    public partial class User
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(255)]
        public string? DisplayName { get; set; }
    }
}
