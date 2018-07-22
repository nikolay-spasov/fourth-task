using System.Collections.Generic;
using System.Threading.Tasks;

using FourthTask.DomainModels;

namespace FourthTask.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<CustomerListDTO>> GetCustomersByName(string customerName);

        Task<CustomerDTO> GetCustomerById(string id);

        Task<bool> CustomerExists(string id);
    }
}
