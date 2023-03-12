using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TalkBack.AccessControl.Data
{
    [Table("User")]
    public partial class User
    {
        [Key]
        public Guid Id { get; set; }
        [StringLength(255)]
        public string UserName { get; set; } = null!;
        [StringLength(255)]
        public string Password { get; set; } = null!;
        [Column(TypeName = "datetime")]
        public DateTime? LastLogin { get; set; }

        public string? RefreshToken { get; set; } = null;
        public DateTime? RefreshTokenExpiryTime { get; set; } = null;

    }
}
