using Microsoft.EntityFrameworkCore;
using MarketSitesi.Data;
using MarketSitesi.Models;

namespace MarketSitesi.Repositories
{
    public class IcecekRepository : GenericRepository<Icecek>, IIcecekRepository
    {
        public IcecekRepository(MarketDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Icecek>> GetIceceklerWithYiyecekAsync()
        {
            return await _dbSet
                .Include(i => i.Yiyecek)
                .ToListAsync();
        }

        public async Task<Icecek?> GetIcecekWithYiyecekAsync(int id)
        {
            return await _dbSet
                .Include(i => i.Yiyecek)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Icecek>> GetIceceklerByYiyecekAsync(int yiyecekId)
        {
            return await _dbSet
                .Include(i => i.Yiyecek)
                .Where(i => i.YiyecekId == yiyecekId)
                .ToListAsync();
        }
    }
}
