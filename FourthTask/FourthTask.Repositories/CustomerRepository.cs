using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

using FourthTask.Data;
using FourthTask.DomainModels;

namespace FourthTask.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly NorthwindEntities _db;

        public CustomerRepository(NorthwindEntities db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<IEnumerable<CustomerListDTO>> GetCustomersByName(string customerName = null)
        {
            var query = _db.Customers.Include(x => x.Orders);
            if (string.IsNullOrWhiteSpace(customerName) == false)
            {
                query = query.Where(x => x.ContactName.StartsWith(customerName));
            }

            return await query
                .GroupBy(x => new { x.CustomerID, x.ContactName })
                .OrderBy(x => x.Key.ContactName)
                .Select(x => new CustomerListDTO
                {
                    CustomerId = x.Key.CustomerID,
                    ContactName = x.Key.ContactName,
                    OrdersCount = x.SelectMany(o => o.Orders).Count()
                })
                .ToListAsync();
        }

        public async Task<FourthTask.DomainModels.CustomerDTO> GetCustomerById(string id)
        {
            ThrowIfInvalidCustomerId(id);

            var dbCustomer = await _db.Customers.FindAsync(id);
            if (dbCustomer == null) return null;

            return new FourthTask.DomainModels.CustomerDTO
            {
                CustomerId = dbCustomer.CustomerID,
                Address = dbCustomer.Address,
                City = dbCustomer.City,
                CompanyName = dbCustomer.CompanyName,
                ContactName = dbCustomer.ContactName,
                ContactTitle = dbCustomer.ContactTitle,
                Country = dbCustomer.Country,
                Fax = dbCustomer.Fax,
                Phone = dbCustomer.Phone,
                PostalCode = dbCustomer.PostalCode,
                Region = dbCustomer.Region
            };
        }

        public async Task<bool> CustomerExists(string id)
        {
            ThrowIfInvalidCustomerId(id);

            return await _db.Customers.AnyAsync(x => x.CustomerID == id);
        }

        private void ThrowIfInvalidCustomerId(string id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("Invalid id", nameof(id));
        }
    }
}
