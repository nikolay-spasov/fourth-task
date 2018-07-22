using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

using FourthTask.Data;
using FourthTask.DomainModels;

namespace FourthTask.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly NorthwindEntities _db;

        public OrderRepository(NorthwindEntities db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<IEnumerable<OrderDetailsDTO>> GetOrdersForCustomer(string customerId)
        {
            if (customerId == null) throw new ArgumentNullException(nameof(customerId));
            if (string.IsNullOrWhiteSpace(customerId)) throw new ArgumentException("Invalid customerId", nameof(customerId));

            return await _db.Orders
                .Include(x => x.Order_Details)
                .Include(x => x.Order_Details.Select(p => p.Product))
                .Where(x => x.CustomerID == customerId)
                .Select(x => new OrderDetailsDTO
                {
                    OrderId = x.OrderID,
                    Freight = x.Freight,
                    OrderDate = x.OrderDate,
                    RequiredDate = x.RequiredDate,
                    ShippedDate = x.ShippedDate,
                    ShipAddress = x.ShipAddress,
                    ShipCity = x.ShipCity,
                    ShipCountry = x.ShipCountry,
                    ShipName = x.ShipName,
                    ShipPostalCode = x.ShipPostalCode,
                    ShipRegion = x.ShipRegion,
                    ProductsCount = x.Order_Details.Sum(o => o.Quantity),
                    Total = x.Order_Details.Sum(c => c.UnitPrice * c.Quantity),
                    ContainsDiscontinuedProduct = x.Order_Details.Any(p => p.Product.Discontinued),
                    UnitsInStockAreLessThanOrdered = x.Order_Details.Any(p => p.Product.UnitsInStock < p.Product.UnitsOnOrder),
                })
                .OrderByDescending(x => x.OrderDate)
                .ToListAsync();
        }
    }
}
