using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AuthSystem.Domain.CountryAggregate
{
    public interface ICountryRepository
    {
        Task<Country> GetByIdAsync(int countryId, CancellationToken cancellationToken = default(CancellationToken));
        Task<Province> GetProvinceByIdAsync(int provinceId, CancellationToken cancellationToken = default(CancellationToken));
        Task<List<Province>> GetProvincesByCountryIdAsync(int countryId, CancellationToken cancellationToken = default(CancellationToken));
        Task<List<Country>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task AddAsync(Country country, CancellationToken cancellationToken = default(CancellationToken));
        Task RemoveAsync(Country country, CancellationToken cancellationToken = default(CancellationToken));
    }
}