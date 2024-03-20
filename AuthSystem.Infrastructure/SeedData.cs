using System.Collections.Generic;
using System.Linq;
using AuthSystem.Domain.CountryAggregate;

namespace AuthSystem.Infrastructure
{
    public static class SeedData
    {
        public static void Initialize(ApplicationIdentityDbContext applicationIdentityDbContext)
        {
            if(applicationIdentityDbContext.Countries.Any())
                    return;
            
            PopulateTestData(applicationIdentityDbContext);
        }

        private static void PopulateTestData(ApplicationIdentityDbContext context)
        {
            var ruCountry = new Country("Russia", new List<Province> { new("Moscow"), new("Leningrad") });
            var gerCountry = new Country("Germany",
                new List<Province> { new("Berlin"), new("Bremen") });

            context.Countries.AddRange(new []{ruCountry, gerCountry});
            context.SaveChanges();
        }
    }
}