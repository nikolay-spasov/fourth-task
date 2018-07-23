using FourthTask.Api.Models;
using FourthTask.DomainModels;

namespace FourthTask.Api.Factories
{
    public class OrderToOrderVMMapper : IOrderToOrderVMMapper
    {
        public OrderVM Map(Order order)
        {
            return new OrderVM
            {
                OrderId = order.OrderId,
                CustomerId = order.CustomerId,
                Freight = order.Freight,
                OrderDate = order.OrderDate,
                RequiredDate = order.RequiredDate,
                ShipAddress = order.ShipAddress,
                ShipCity = order.ShipCity,
                ShipCountry = order.ShipCountry,
                ShipName = order.ShipName,
                ShippedDate = order.ShippedDate,
                ShipPostalCode = order.ShipPostalCode,
                ShipRegion = order.ShipRegion,
                ProductsCount = order.GetProductsCount(),
                TotalPrice = order.CalculateTotalPrice(),
                ContainsDiscontinuedProduct = order.ContainsDiscontinuedProduct(),
                ContainsProductsInStockLessThanOrderedProducts = order.ProductsInStockLessThanOrderedProducts()
            };
        }
    }
}