using ShuttleInfraAPI.Models;
namespace ShuttleInfraAPI.Repositeries
{
    public interface IProviderRepositery
    {
        public Task<List<Provider>> GetAllProviders();

    }
}
