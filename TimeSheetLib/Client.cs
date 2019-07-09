using System;
using TimeSheet.Shared.Models.Interfaces;

namespace TimeSheet.Shared.Models.Implementation
{
    public class Client : IClient
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public Guid? CountryId { get; set; }

        public Client(string name, Guid? countryId = null, string address = null, string city = null, string zipCode = null)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Client name can't be null or empty string", nameof(name));
            }
            Id = Guid.NewGuid();
            Name = name;
            CountryId = countryId;
            Address = address;
            City = city;
            ZipCode = zipCode;
        }
       
    }
}
