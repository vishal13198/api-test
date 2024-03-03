using System.ComponentModel.DataAnnotations.Schema;

namespace ShuttleInfraAPI.Models
{
    public class OrganizationProject
    {

        [Column("id")]
        public int Id { get; set; }
        [Column("orgid")]
        public int OrgId { get; set; }
        [Column("projectid")]
        public int ProjectId { get; set; }
        [Column("status")]
        public string Status { get; set; }
        [Column("lastupdateby")]
        public string LastUpdateBy { get; set; }
        [Column("lastupdatedate")]
        public DateTime? LastUpdateDate { get; set; }
        [Column("isactive")]
        public bool IsActive { get; set; }
    }
}
