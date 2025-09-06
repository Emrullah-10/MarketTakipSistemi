using MarketSitesi.Models;

namespace MarketSitesi.Repositories
{
    public interface IKategoriRepository : IGenericRepository<Kategori>
    {
        Task<IEnumerable<Kategori>> GetKategorilerWithYiyeceklerAsync();
        Task<Kategori?> GetKategoriWithYiyeceklerAsync(int id);
    }
}
