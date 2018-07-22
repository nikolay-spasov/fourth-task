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

            var orders = await _db.Orders
                .Include(x => x.Order_Details)
                .Include(x => x.Order_Details.Select(p => p.Product))
                .Where(x => x.CustomerID == customerId)
                .OrderByDescending(x => x.OrderDate)
                .ToListAsync();

            return MapToOrderDetailsDTO(orders);
        }

        private IEnumerable<OrderDetailsDTO> MapToOrderDetailsDTO(IEnumerable<Order> orders)
        {
            // Total price should be calculated in memory since LINQ-to-Entities is unable to cast Discount to decimal
            var ls = new List<OrderDetailsDTO>();
            foreach (var o in orders)
            {
                ls.Add(new OrderDetailsDTO
                {
                    OrderId = o.OrderID,
                    Freight = o.Freight,
                    OrderDate = o.OrderDate,
                    RequiredDate = o.RequiredDate,
                    ShippedDate = o.ShippedDate,
                    ShipAddress = o.ShipAddress,
                    ShipCity = o.ShipCity,
                    ShipCountry = o.ShipCountry,
                    ShipName = o.ShipName,
                    ShipPostalCode = o.ShipPostalCode,
                    ShipRegion = o.ShipRegion,
                    ProductsCount = o.Order_Details.Sum(x => x.Quantity),
                    ContainsDiscontinuedProduct = o.Order_Details.Any(p => p.Product.Discontinued),
                    UnitsInStockAreLessThanOrdered = o.Order_Details.Any(p => p.Product.UnitsInStock < p.Product.UnitsOnOrder),
                    Total = o.Order_Details.Sum(c => c.UnitPrice * c.Quantity * (1 - (decimal)c.Discount) / 100) * 100,
                });
            }

            return ls;
        }
    }
}
