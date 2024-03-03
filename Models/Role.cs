using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShuttleInfraAPI.Models
{
    [Table("roles")]
    public class Role
    {
        [Key]
        public int id { get; set; }
        [Column("rolename")]
        public string? RoleName { get; set; }
        [Column("lastupdateby")]
        public string? LastUpdateBy { get; set; }
        [Column("lastupdatedate")]
        public DateTime LastUpdateDate { get; set; }
        [Column("isactive")]
        public bool IsActive { get; set; }
    }
}
