using System.Collections.Generic;

using FourthTask.Mvc.Models;

namespace FourthTask.Mvc.UnitTests.Data
{
    public class MockCustomers
    {
        public static IEnumerable<CustomerListVM> GetCustomers()
        {
            return new List<CustomerListVM>()
            {
                new CustomerListVM
                {
                    CustomerId = "Test1",
                    ContactName = "Test1",
                    OrdersCount = 1
                },
                new CustomerListVM
                {
                    CustomerId = "Test2",
                    ContactName = "Test2",
                    OrdersCount = 2
                },
                new CustomerListVM
                {
                    CustomerId = "Test3",
                    ContactName = "Test3",
                    OrdersCount = 3
                },
                new CustomerListVM
                {
                    CustomerId = "Test4",
                    ContactName = "Test4",
                    OrdersCount = 4
                },
                new CustomerListVM
                {
                    CustomerId = "Test5",
                    ContactName = "Test5",
                    OrdersCount = 5
                }
            };
        }
    }
}
