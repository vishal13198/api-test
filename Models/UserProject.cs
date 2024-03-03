using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShuttleInfraAPI.Models
{
    [Table("user_projects")]
    public class UserProject
    {
        [Key]
        public int id { get; set; }
        [Column("userid")]
        public int UserID { get; set; }
        [Column("projectid")]
        public int ProjectID { get; set; }

        [Column("lastupdateby")]
        public string? LastUpdateBy { get; set; }
        [Column("lastupdatedate")]
        public DateTime LastUpdateDate { get; set; }
        [Column("isactive")]
        public bool IsActive { get; set; }

    }
}
