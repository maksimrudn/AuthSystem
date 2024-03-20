using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AuthSystem.Domain.CountryAggregate;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
namespace AuthSystem.Infrastructure.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        [NotNull] private readonly ApplicationIdentityDbContext _dbContext;

        public CountryRepository([NotNull] ApplicationIdentityDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Country> GetByIdAsync(int countryId,
            CancellationToken cancellationToken = default)
        {
            return _dbContext.Countries
                .Include(i => i.Provinces)
                .FirstAsync(c => c.Id == countryId, cancellationToken);
        }

        public Task<Province> GetProvinceByIdAsync(int provinceId, CancellationToken cancellationToken = default)
        {
            return _dbContext.Provinces.FirstAsync(p => p.Id == provinceId, cancellationToken);
        }

        public Task<List<Province>> GetProvincesByCountryIdAsync(int countryId, CancellationToken cancellationToken = default)
        {
            return _dbContext.Provinces.Where(p => p.Country.Id == countryId).ToListAsync(cancellationToken);
        }

        public Task<List<Country>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.Countries.ToListAsync(cancellationToken);
        }

        public async Task AddAsync(Country country, CancellationToken cancellationToken = default)
        {
            await _dbContext.Countries.AddAsync(country, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveAsync(Country country, CancellationToken cancellationToken = default)
        {
            _dbContext.Remove(country);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}