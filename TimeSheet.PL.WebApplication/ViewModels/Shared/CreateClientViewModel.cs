using System;
using System.ComponentModel.DataAnnotations;
using TimeSheet.Shared.Models.Interfaces;

namespace TimeSheet.PL.WebApplication.ViewModels.Shared
{
    public class CreateClientViewModel : IClient
    {
        public Guid? Id { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public Guid? CountryId { get; set; }

        public CreateClientViewModel() { }

        public CreateClientViewModel(Guid? id, string name, string address = null, string city = null, string zipCode = null, Guid? countryId = null)
        {
            if (id == null || id == Guid.Empty)
            {
                Id = Guid.NewGuid();
            }
            else
            {
                Id = id;
            }

            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Client name can't be null or empty string", nameof(name));
            }

            Name = name;
            CountryId = countryId;
            Address = address;
            City = city;
            ZipCode = zipCode;
        }

    }
}
