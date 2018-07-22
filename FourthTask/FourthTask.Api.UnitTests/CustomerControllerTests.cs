using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

using FourthTask.Api.Controllers;
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

                var controller = new CustomerController(customerRepo.Object, mockOrderRepository.Object);

                // Act
                var response = await controller.GetCustomers() as OkNegotiatedContentResult<IEnumerable<CustomerListDTO>>;
                var content = response.Content;

                // Assert
                Assert.IsNotNull(content);
                Assert.AreEqual(data.Count(), content.Count());
            }
        }

        [TestMethod]
        public async Task GetCustomerShouldReturnBadRequestIfCustomerIdIsInvalid()
        {
            // Arrange
            var data = MockCustomers.GetCustomers();
            var mockCustomersRepository = new Mock<ICustomerRepository>();
            var mockOrderRepository = new Mock<IOrderRepository>();

            var controller = new CustomerController(mockCustomersRepository.Object, mockOrderRepository.Object);

            foreach (var id in new[] { null, "", " " })
            {
                // Act
                IHttpActionResult result = await controller.GetCustomer(id) as BadRequestErrorMessageResult;

                // Assert
                Assert.IsInstanceOfType(result, typeof(BadRequestErrorMessageResult));
            }
        }

        [TestMethod]
        public async Task GetCustomerShouldReturn404IfCustomerIdNotFound()
        {
            // Assert
            var mockCustomersRepository = new Mock<ICustomerRepository>();
            mockCustomersRepository
                .Setup(x => x.GetCustomerById(It.IsAny<string>()))
                .Returns(Task.FromResult<CustomerDTO>(null));

            var mockOrderRepository = new Mock<IOrderRepository>();

            var controller = new CustomerController(mockCustomersRepository.Object, mockOrderRepository.Object);

            // Act
            IHttpActionResult result = await controller.GetCustomer("TEST-MISSING-ID");

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
