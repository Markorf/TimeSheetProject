using System;
using TimeSheet.Shared.Models.Interfaces;

namespace TimeSheet.Shared.Models.Implementation
{
    public class Country : ICountry
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Country(Guid id, string name)
        {

            if (id == Guid.Empty)
            {
                Id = Guid.NewGuid();
            }
            else
            {
                Id = id;
            }
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name can't be null or empty string", nameof(name));
            }
            Name = name;
        }
    }
}
