using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShuttleInfraAPI.Models
{
     [Table("users")]
    public class Users
    {
        [Key]
        [Column("id")]
        public int? Id { get; set; }

        [Column("username")]
        public string? Username { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("phone")]
        public string? Phone { get; set; }

        [Column("password")]
        public string? Password { get; set; }

 
        [Column("is_active")]
        public bool? IsActive { get; set; }


        [Column("createddate")]
        public DateTime? CreatedDate { get; set; }

        [Column("created_by")]
        public string? CreatedBy { get; set; }

        [Column("updated_by")]
        public string? UpdatedBy { get; set; }

        [Column("updated_date")]
        public DateTime? UpdatedDate { get; set; }

    }

    public class UserProjectRole: Users
    {
        public int? ProjectId { get; set; }
        public int? RoleId { get; set; }
    }

 
    public class UserData
    {
        public int UserID { get; set; }
        public string? Username { get; set; }
        public string? UserEmail { get; set; }
        public int RoleID { get; set; }
        public int ProjectID { get; set; }
        public string? RoleName { get; set; }
        public string? ProjectName { get; set; }
        public UserRole? UserRole { get; set; }
        public UserProject? UserProject { get; set; }
    }
}
