using FourthTask.Api.Models;
using FourthTask.DomainModels;

namespace FourthTask.Api.Factories
{
    public interface ICustomerListRowToCustomerListVMMapper
    {
        CustomerListVM Map(CustomerListRow row);
    }
}
