namespace ShuttleInfraAPI.Models
{
    public class Resource
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string? Description { get; set; }
        public int ProviderId { get; set; }
        public int CategoryId { get; set; }

        public string? LastUpdatedBy { get; set; }
        public DateTime LastUpdatedDate{ get; set; }
        public bool IsActive { get; set; }


    }
}
