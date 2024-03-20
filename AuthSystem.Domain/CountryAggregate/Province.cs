using Ardalis.GuardClauses;
using AuthSystem.Domain.SeedWork;

namespace AuthSystem.Domain.CountryAggregate
{
    public sealed class Province: Entity
    {
        private Province(){}
        
        public Province(string name)
        {
            Name = Guard.Against.NullOrEmpty(name, nameof(name));
        }
        public string Name { get; private set; }
        public Country Country { get; set; }
    }
}