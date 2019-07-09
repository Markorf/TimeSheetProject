using System;
using TimeSheet.Shared.Models.Interfaces;

namespace TimeSheet.Shared.Models.Implementation
{
    public class Country : ICountry
    {
        public Guid Id { get;}
        public string Name { get; }
        public Country(string countryName)
        {
            if (String.IsNullOrEmpty(countryName))
            {
                throw new ArgumentException("Name can't be null or empty string", nameof(countryName));
            }
            Id = Guid.NewGuid();
            Name = countryName;
        }
    }
}
