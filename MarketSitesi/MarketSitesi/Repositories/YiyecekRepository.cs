using Microsoft.EntityFrameworkCore;
using MarketSitesi.Data;
using MarketSitesi.Models;

namespace MarketSitesi.Repositories
{
    public class YiyecekRepository : GenericRepository<Yiyecek>, IYiyecekRepository
    {
        public YiyecekRepository(MarketDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Yiyecek>> GetYiyeceklerWithKategoriAsync()
        {
            return await _dbSet
                .Include(y => y.Kategori)
                .ToListAsync();
        }

        public async Task<Yiyecek?> GetYiyecekWithKategoriAsync(int id)
        {
            return await _dbSet
                .Include(y => y.Kategori)
                .FirstOrDefaultAsync(y => y.Id == id);
        }

        public async Task<IEnumerable<Yiyecek>> GetYiyeceklerByKategoriAsync(int kategoriId)
        {
            return await _dbSet
                .Include(y => y.Kategori)
                .Where(y => y.KategoriId == kategoriId)
                .ToListAsync();
        }
    }
}
