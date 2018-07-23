using System;
using System.Linq;
using System.Collections.Generic;

namespace FourthTask.DomainModels
{
    public class Order
    {
        public int OrderId { get; set; }
        public string CustomerId { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public decimal? Freight { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipRegion { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }

        public IEnumerable<OrderDetails> OrderDetails { get; set; }

        public int GetProductsCount()
        {
            return OrderDetails.Sum(x => x.Quantity);
        }

        public decimal CalculateTotalPrice()
        {
            return OrderDetails.Sum(x => x.UnitPrice * x.Quantity * (1 - (decimal)x.Discount) / 100) * 100;
        }

        public bool ContainsDiscontinuedProduct()
        {
            return OrderDetails.Any(x => x.Product.Discontinued);
        }

        public bool ProductsInStockLessThanOrderedProducts()
        {
            return OrderDetails.Any(x => x.Product.UnitsInStock < x.Product.UnitsOnOrder);
        }
    }
}
