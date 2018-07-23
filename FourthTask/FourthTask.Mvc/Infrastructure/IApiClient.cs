using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using FourthTask.Mvc.Models;

namespace FourthTask.Mvc.Infrastructure
{
    public interface IApiClient
    {
        Task<IEnumerable<CustomerListVM>> GetCustomers(string customerName, CancellationToken cancellationToken = default(CancellationToken));

        Task<CustomerVM> GetCustomerDetails(string customerId, CancellationToken cancellationToken = default(CancellationToken));

        Task<IEnumerable<OrderVM>> GetOrdersForCustomer(string customerId, CancellationToken cancellationToken = default(CancellationToken));
    }
}
