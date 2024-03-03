using System.ComponentModel.DataAnnotations.Schema;

namespace ShuttleInfraAPI.Models
{
    [Table("user_role")]
    public class UserRole
    {
        [Column("id")]
        public int id { get; set; }
        [Column("userid")]
        public int UserId { get; set; }

        [Column("roleid")]
        public int RoleId { get; set; }
        [Column("lastupdateby")]
        public string? LastUpdateBy { get; set; }
        [Column("lastupdatedate")]
        public DateTime LastUpdateDate { get; set; }
        [Column("isactive")]
        public bool IsActive { get; set; }
    }
}
