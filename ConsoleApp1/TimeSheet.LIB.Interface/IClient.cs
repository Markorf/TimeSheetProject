using System;

namespace TimeSheet.Shared.Models.Interfaces
{
    public interface IClient
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Address { get; set; }
        string City { get; set; }
        string ZipCode { get; set; }
        Guid? CountryId { get; set; }
    }
}
