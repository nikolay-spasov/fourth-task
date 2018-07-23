using System.Collections.Generic;
using System.Threading.Tasks;

using FourthTask.DomainModels;

namespace FourthTask.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<CustomerListRow>> GetCustomersByName(string customerName);

        Task<Customer> GetCustomerById(string id);

        Task<bool> CustomerExists(string id);
    }
}
