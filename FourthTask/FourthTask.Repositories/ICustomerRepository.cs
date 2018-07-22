using System.Collections.Generic;
using System.Threading.Tasks;

using FourthTask.DomainModels;

namespace FourthTask.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<CustomerListDTO>> GetCustomersByName(string customerName);

        Task<FourthTask.DomainModels.CustomerDTO> GetCustomerById(string id);

        Task<bool> CustomerExists(string id);
    }
}
