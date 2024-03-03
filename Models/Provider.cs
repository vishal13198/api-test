using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShuttleInfraAPI.Models
{
    public class Provider
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public string? LastUpdatedBy { get; set; }

        public DateTime LastUpdatedDate { get; set; }

        public bool IsActive { get; set; }

    }
}
