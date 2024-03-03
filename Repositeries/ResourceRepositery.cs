using Microsoft.EntityFrameworkCore;
using ShuttleInfraAPI.Models;
using ShuttleInfraAPI.Repositeries;

namespace ShuttleInfraAPI.Repositeries
{
    public class ResourceRepositery : IResourceRepositery

    {
        private readonly AppDbContext AppDbContext;

        public ResourceRepositery(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }


        public async Task<List<Resource>> GetResources(int providerId, int categoryId)
        {
            var resourcesQuery = AppDbContext.Resource.AsQueryable();
            return await resourcesQuery
                .Where(r => r.ProviderId == providerId && r.CategoryId == categoryId)
                .ToListAsync<Resource>();
        }

        public async Task<List<SubResource>> GetSubResources(int resourceId)
        {
            var subResourcesQuery = AppDbContext.SubResource.AsQueryable();
            return await subResourcesQuery
                .Where(r => r.ResourceId == resourceId)
                .ToListAsync<SubResource>();
        }

        public async Task<List<Category>> GetCategories(int? providerId = 1)
        {
            return await AppDbContext.Category.ToListAsync<Category>();
        }

    }
}
