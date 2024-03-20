using System.Collections.Generic;
using Ardalis.GuardClauses;
using AuthSystem.Domain.SeedWork;

namespace AuthSystem.Domain.CountryAggregate
{
    public sealed class Country : Entity, IAggregateRoot
    {
        private Country()
        {
            Provinces = new List<Province>();
        }

        public Country(string name)
        {
            Name = Guard.Against.NullOrEmpty(name, nameof(name));
        }
        
        public Country(string name, ICollection<Province> provinces): this(name)
        {
            Provinces = provinces;
        }
        public string Name { get; private set; }
        public ICollection<Province> Provinces { get; private set; }

        public void AddProvince(Province newProvince)
        {
            Provinces.Add(Guard.Against.Null(newProvince, nameof(newProvince)));
        }
    }
}