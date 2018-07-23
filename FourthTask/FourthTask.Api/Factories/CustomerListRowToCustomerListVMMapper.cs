using FourthTask.Api.Models;
using FourthTask.DomainModels;

namespace FourthTask.Api.Factories
{
    public class CustomerListRowToCustomerListVMMapper : ICustomerListRowToCustomerListVMMapper
    {
        public CustomerListVM Map(CustomerListRow row)
        {
            return new CustomerListVM
            {
                CustomerId = row.CustomerId,
                ContactName = row.ContactName,
                OrdersCount = row.OrdersCount
            };
        }
    }
}