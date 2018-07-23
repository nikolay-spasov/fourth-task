using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using FourthTask.Data;
using FourthTask.Repositories.Factories;

namespace FourthTask.Repositories
{
    public class OrderRepository : IOrderRepository, IDisposable
    {
        private readonly NorthwindEntities _db;
        private readonly IDbOrderToDomainOrderMapper _dbOrderToDomainOrderMapper;
        private bool _disposed = false;

        public OrderRepository(NorthwindEntities db, IDbOrderToDomainOrderMapper dbOrderToDomainOrderMapper)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _dbOrderToDomainOrderMapper = dbOrderToDomainOrderMapper ?? throw new ArgumentNullException(nameof(dbOrderToDomainOrderMapper));
        }

        public async Task<IEnumerable<FourthTask.DomainModels.Order>> GetOrdersForCustomer(string customerId, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (customerId == null) throw new ArgumentNullException(nameof(customerId));
            if (string.IsNullOrWhiteSpace(customerId)) throw new ArgumentException("Invalid customerId", nameof(customerId));

            return await _db.Orders
                .Include(x => x.Order_Details)
                .Include(x => x.Order_Details.Select(p => p.Product))
                .Where(x => x.CustomerID == customerId)
                .Select(_dbOrderToDomainOrderMapper.Map())
                .OrderByDescending(x => x.OrderDate)
                .ToListAsync(cancellationToken);
        }

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
    }
}
