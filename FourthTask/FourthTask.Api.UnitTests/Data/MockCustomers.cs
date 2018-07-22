using System.Collections.Generic;

using FourthTask.DomainModels;

namespace FourthTask.Api.UnitTests.Data
{
    public class MockCustomers
    {
        public static IEnumerable<CustomerListDTO> GetCustomers()
        {
            return new List<CustomerListDTO>()
            {
                new CustomerListDTO
                {
                    CustomerId = "Test1",
                    ContactName = "Test1",
                    OrdersCount = 1
                },
                new CustomerListDTO
                {
                    CustomerId = "Test2",
                    ContactName = "Test2",
                    OrdersCount = 2
                },
                new CustomerListDTO
                {
                    CustomerId = "Test3",
                    ContactName = "Test3",
                    OrdersCount = 3
                },
                new CustomerListDTO
                {
                    CustomerId = "Test4",
                    ContactName = "Test4",
                    OrdersCount = 4
                },
                new CustomerListDTO
                {
                    CustomerId = "Test5",
                    ContactName = "Test5",
                    OrdersCount = 5
                }
            };
        }
    }
}
