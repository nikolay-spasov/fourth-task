using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using FourthTask.DomainModels;

namespace FourthTask.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrdersForCustomer(string customerId, CancellationToken cancelationToken = default(CancellationToken));
    }
}
