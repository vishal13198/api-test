using System.ComponentModel.DataAnnotations.Schema;

namespace ShuttleInfraAPI.Models
{

    [Table("projects")]
    public class Projects
    {
        public int id { get; set; }

        [Column("projectname")]
        public string? ProjectName { get; set; }
        [Column("projectdescription")]
        public string? ProjectDescription { get; set; }

        [Column("startdate")]
        public DateTime? StartDate { get; set; }
        [Column("enddate")]
        public DateTime? EndDate { get; set; }
        [Column("lastupdateby")]
        public string? LastUpdateBy { get; set; }
        [Column("lastupdatedate")]
        public DateTime? LastUpdateDate { get; set; }
        [Column("isactive")]
        public bool IsActive { get; set; }
    }

    public class OrgMappProjects : Projects
    {
        public int? OrgId { get; set; }
        public int? ProjId { get; set; }
    }
}
