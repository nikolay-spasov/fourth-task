using System;

namespace FourthTask.DomainModels
{
    public class OrderDetailsDTO
    {
        public int OrderId { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipRegion { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }
        public decimal? Freight { get; set; }
        public int ProductsCount { get; set; }
        public decimal Total { get; set; }
        public bool ContainsDiscontinuedProduct { get; set; }
        public bool UnitsInStockAreLessThanOrdered { get; set; }
    }
}
