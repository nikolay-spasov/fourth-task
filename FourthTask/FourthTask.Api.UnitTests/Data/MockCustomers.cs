using System.Collections.Generic;

using FourthTask.DomainModels;

namespace FourthTask.Api.UnitTests.Data
{
    public class MockCustomers
    {
        public static IEnumerable<CustomerListRow> GetCustomers()
        {
            return new List<CustomerListRow>()
            {
                new CustomerListRow
                {
                    CustomerId = "Test1",
                    ContactName = "Test1",
                    OrdersCount = 1
                },
                new CustomerListRow
                {
                    CustomerId = "Test2",
                    ContactName = "Test2",
                    OrdersCount = 2
                },
                new CustomerListRow
                {
                    CustomerId = "Test3",
                    ContactName = "Test3",
                    OrdersCount = 3
                },
                new CustomerListRow
                {
                    CustomerId = "Test4",
                    ContactName = "Test4",
                    OrdersCount = 4
                },
                new CustomerListRow
                {
                    CustomerId = "Test5",
                    ContactName = "Test5",
                    OrdersCount = 5
                }
            };
        }
    }
}
