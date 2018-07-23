using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

using FourthTask.Api.Controllers;
using FourthTask.Api.Factories;
using FourthTask.Api.Models;
using FourthTask.Api.UnitTests.Data;
using FourthTask.DomainModels;
using FourthTask.Repositories;

namespace FourthTask.Api.UnitTests
{
    [TestClass]
    public class CustomerControllerTests
    {
        [TestClass]
        public class CustomersControllerTests
        {
            [TestMethod]
            public async Task GetCustomersReturnsAllCustomers()
            {
                // Arrange
                var data = MockCustomers.GetCustomers();

                var customerRepo = new Mock<ICustomerRepository>();
                customerRepo.Setup(x => x.GetCustomersByName(It.IsAny<string>()))
                    .Returns(Task.FromResult(data));
                var mockOrderRepository = new Mock<IOrderRepository>();

                var controller = CreateControllerInstance(customerRepo.Object, mockOrderRepository.Object);

                // Act
                var response = await controller.GetCustomers() as OkNegotiatedContentResult<IEnumerable<CustomerListVM>>;
                var content = response.Content;

                // Assert
                Assert.IsNotNull(content);
                Assert.AreEqual(data.Count(), content.Count());
            }
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        public async Task GetCustomerShouldReturnBadRequestIfCustomerIdIsInvalid(string customerId)
        {
            // Arrange
            var data = MockCustomers.GetCustomers();
            var mockCustomersRepository = new Mock<ICustomerRepository>();
            var mockOrderRepository = new Mock<IOrderRepository>();

            var controller = CreateControllerInstance(mockCustomersRepository.Object, mockOrderRepository.Object);

            // Act
            IHttpActionResult result = await controller.GetCustomer(customerId) as BadRequestErrorMessageResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public async Task GetCustomerShouldReturn404IfCustomerIdNotFound()
        {
            // Arrange
            var mockCustomersRepository = new Mock<ICustomerRepository>();
            mockCustomersRepository
                .Setup(x => x.GetCustomerById(It.IsAny<string>()))
                .Returns(Task.FromResult<Customer>(null));

            var mockOrderRepository = new Mock<IOrderRepository>();

            var controller = CreateControllerInstance(mockCustomersRepository.Object, mockOrderRepository.Object);

            // Act
            IHttpActionResult result = await controller.GetCustomer("TEST-MISSING-ID");

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        public async Task GetOrdersShouldReturnBadRequestIfInvalidCustomerId(string customerId)
        {
            // Arrange
            var data = MockCustomers.GetCustomers();
            var mockCustomersRepository = new Mock<ICustomerRepository>();
            var mockOrderRepository = new Mock<IOrderRepository>();

            var controller = CreateControllerInstance(mockCustomersRepository.Object, mockOrderRepository.Object);

            // Act
            IHttpActionResult result = await controller.GetOrders(customerId) as BadRequestErrorMessageResult;

            // Assert
            Assert.IsInstanceOfType(result, typeof(BadRequestErrorMessageResult));
        }

        [TestMethod]
        public async Task GetOrdersShouldReturn404IfCustomerIdNotFound()
        {
            // Arrange
            var mockCustomersRepository = new Mock<ICustomerRepository>();
            mockCustomersRepository
                .Setup(x => x.GetCustomerById(It.IsAny<string>()))
                .Returns(Task.FromResult<Customer>(null));
            mockCustomersRepository.Setup(x => x.CustomerExists(It.IsAny<string>()))
                .Returns(Task.FromResult(false));

            var mockOrderRepository = new Mock<IOrderRepository>();

            var controller = CreateControllerInstance(mockCustomersRepository.Object, mockOrderRepository.Object);

            // Act
            IHttpActionResult result = await controller.GetOrders("TEST-MISSING-ID");

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        private static CustomerController CreateControllerInstance(ICustomerRepository customerRepo, IOrderRepository orderRepo)
        {
            var controller = new CustomerController(
                    customerRepo,
                    orderRepo,
                    new CustomerListRowToCustomerListVMMapper(),
                    new CustomerToCustomerVMMapper(),
                    new OrderToOrderVMMapper());

            return controller;
        }
    }
}
