using FourthTask.Api.Models;
using FourthTask.DomainModels;

namespace FourthTask.Api.Factories
{
    public interface ICustomerToCustomerVMMapper
    {
        CustomerVM Map(Customer customer);
    }
}
