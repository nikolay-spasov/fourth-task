﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using FourthTask.Data;
using FourthTask.DomainModels;
using FourthTask.Repositories.Factories;

namespace FourthTask.Repositories
{
    public class CustomerRepository : ICustomerRepository, IDisposable
    {
        private readonly NorthwindEntities _db;
        private readonly IDbCustomerToDomainCustomerMapper _dbCustomerToDomainCustomerMapper;
        private bool _disposed = false;

        public CustomerRepository(NorthwindEntities db, IDbCustomerToDomainCustomerMapper dbCustomerToDomainCustomerMapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _dbCustomerToDomainCustomerMapper = dbCustomerToDomainCustomerMapper ?? throw new ArgumentNullException(nameof(dbCustomerToDomainCustomerMapper));
        }

        public async Task<IEnumerable<CustomerListRow>> GetCustomersByName(string customerName = null, 
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var query = _db.Customers.Include(x => x.Orders);
            if (string.IsNullOrWhiteSpace(customerName) == false)
            {
                query = query.Where(x => x.ContactName.StartsWith(customerName));
            }

            return await query
                .GroupBy(x => new { x.CustomerID, x.ContactName })
                .OrderBy(x => x.Key.ContactName)
                .Select(x => new CustomerListRow
                {
                    CustomerId = x.Key.CustomerID,
                    ContactName = x.Key.ContactName,
                    OrdersCount = x.SelectMany(o => o.Orders).Count()
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<DomainModels.Customer> GetCustomerById(string id, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfInvalidCustomerId(id);

            var dbCustomer = await _db.Customers.FindAsync(cancellationToken, id);
            if (dbCustomer == null) return null;

            return _dbCustomerToDomainCustomerMapper.Map(dbCustomer);
        }

        public async Task<bool> CustomerExists(string id, CancellationToken cancellationToken = default(CancellationToken))
        {
            ThrowIfInvalidCustomerId(id);

            return await _db.Customers.AnyAsync(x => x.CustomerID == id, cancellationToken);
        }

        // https://stackoverflow.com/questions/538060/proper-use-of-the-idisposable-interface
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_db != null)
                    {
                        _db.Dispose();
                    }
                }
                _disposed = true;
            }
        }

        private void ThrowIfInvalidCustomerId(string id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));
            if (string.IsNullOrWhiteSpace(id)) throw new ArgumentException("Invalid id", nameof(id));
        }
    }
}
