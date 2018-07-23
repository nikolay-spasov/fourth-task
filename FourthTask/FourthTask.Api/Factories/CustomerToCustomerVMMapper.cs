using FourthTask.Api.Models;
using FourthTask.DomainModels;

namespace FourthTask.Api.Factories
{
    public class CustomerToCustomerVMMapper : ICustomerToCustomerVMMapper
    {
        public CustomerVM Map(Customer customer)
        {
            return new CustomerVM
            {
                CustomerId = customer.CustomerId,
                Address = customer.Address,
                City = customer.City,
                CompanyName = customer.CompanyName,
                ContactName = customer.ContactName,
                ContactTitle = customer.ContactName,
                Country = customer.Country,
                Fax = customer.Fax,
                Phone = customer.Phone,
                PostalCode = customer.PostalCode,
                Region = customer.Region
            };
        }
    }
}