using MarketSitesi.Models;

namespace MarketSitesi.Repositories
{
    public interface IIcecekRepository : IGenericRepository<Icecek>
    {
        Task<IEnumerable<Icecek>> GetIceceklerWithYiyecekAsync();
        Task<Icecek?> GetIcecekWithYiyecekAsync(int id);
        Task<IEnumerable<Icecek>> GetIceceklerByYiyecekAsync(int yiyecekId);
    }
}
