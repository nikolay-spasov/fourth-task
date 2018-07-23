using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using FourthTask.DomainModels;

namespace FourthTask.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<CustomerListRow>> GetCustomersByName(string customerName, CancellationToken cancellationToken = default(CancellationToken));

        Task<Customer> GetCustomerById(string id, CancellationToken cancellationToken = default(CancellationToken));

        Task<bool> CustomerExists(string id, CancellationToken cancellationToken = default(CancellationToken));
    }
}
