using MarketSitesi.Models;

namespace MarketSitesi.Repositories
{
    public interface IYiyecekRepository : IGenericRepository<Yiyecek>
    {
        Task<IEnumerable<Yiyecek>> GetYiyeceklerWithKategoriAsync();
        Task<Yiyecek?> GetYiyecekWithKategoriAsync(int id);
        Task<IEnumerable<Yiyecek>> GetYiyeceklerByKategoriAsync(int kategoriId);
    }
}
