using Microsoft.EntityFrameworkCore;
using MarketSitesi.Data;
using MarketSitesi.Models;

namespace MarketSitesi.Repositories
{
    public class KategoriRepository : GenericRepository<Kategori>, IKategoriRepository
    {
        public KategoriRepository(MarketDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Kategori>> GetKategorilerWithYiyeceklerAsync()
        {
            return await _dbSet
                .Include(k => k.Yiyecekler)
                .ToListAsync();
        }

        public async Task<Kategori?> GetKategoriWithYiyeceklerAsync(int id)
        {
            return await _dbSet
                .Include(k => k.Yiyecekler)
                .FirstOrDefaultAsync(k => k.Id == id);
        }
    }
}
