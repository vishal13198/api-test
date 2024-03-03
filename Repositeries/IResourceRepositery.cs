using ShuttleInfraAPI.Models;

namespace ShuttleInfraAPI.Repositeries
{
    public interface IResourceRepositery
    {
        Task<List<Category>> GetCategories(int? providerId = 1);
        Task<List<Resource>> GetResources(int providerId, int categoryId);
        Task<List<SubResource>> GetSubResources(int resourceId);
    }
}
