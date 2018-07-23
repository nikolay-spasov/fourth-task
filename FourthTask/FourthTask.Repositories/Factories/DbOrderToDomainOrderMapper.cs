using System;
using System.Linq;
using System.Linq.Expressions;

namespace FourthTask.Repositories.Factories
{
    public class DbOrderToDomainOrderMapper : IDbOrderToDomainOrderMapper
    {
        public Expression<Func<Data.Order, DomainModels.Order>> Map()
        {
            return dbOrder => new DomainModels.Order
            {
                CustomerId = dbOrder.CustomerID,
                Freight = dbOrder.Freight,
                OrderDate = dbOrder.OrderDate,
                OrderId = dbOrder.OrderID,
                RequiredDate = dbOrder.RequiredDate,
                ShipAddress = dbOrder.ShipAddress,
                ShipCity = dbOrder.ShipCity,
                ShipCountry = dbOrder.ShipCountry,
                ShipName = dbOrder.ShipName,
                ShippedDate = dbOrder.ShippedDate,
                ShipPostalCode = dbOrder.ShipPostalCode,
                ShipRegion = dbOrder.ShipRegion,
                OrderDetails = dbOrder.Order_Details.Select(x => new DomainModels.OrderDetails
                {
                    Discount = x.Discount,
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice,
                    Product = new DomainModels.Product
                    {
                        Discontinued = x.Product.Discontinued,
                        ReorderLevel = x.Product.ReorderLevel,
                        UnitsInStock = x.Product.UnitsInStock,
                        UnitsOnOrder = x.Product.UnitsOnOrder
                    }
                })
            };
        }
    }
}
