using System.ComponentModel.DataAnnotations.Schema;

namespace ShuttleInfraAPI.Models
{

    [Table("organizations")]
    public class Organizations
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("organizationcode")]
        public string OrganizationCode { get; set; }

        [Column("organizationname")]
        public string OrganizationName { get; set; }
        [Column("organizationUrl")]
        public string OrganizationUrl { get; set; }
        [Column("createddate")]
        public DateTime CreatedDate { get; set; }
        [Column("updateddate")]
        public DateTime? UpdatedDate { get; set; }
        [Column("lastupdateby")]
        public string LastUpdateBy { get; set; }
        [Column("lastupdatedate")]
        public DateTime? LastUpdateDate { get; set; }
        [Column("isactive")]
        public bool IsActive { get; set; }
    
    }
}
