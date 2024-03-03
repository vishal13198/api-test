using Microsoft.EntityFrameworkCore;
using ShuttleInfraAPI.Models;
using ShuttleInfraAPI.Repositeries;

namespace ShuttleInfraAPI.Repositeries
{
    public class ProviderRepositery: IProviderRepositery
    {
        private readonly AppDbContext AppDbContext;

        public ProviderRepositery(AppDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }


        public async Task<List<Provider>> GetAllProviders()
        {
            var providerQuery = AppDbContext.Provider.AsQueryable();
            return await providerQuery
                .Where(r => r.IsActive)
                .ToListAsync<Provider>();
        }

    }
}
