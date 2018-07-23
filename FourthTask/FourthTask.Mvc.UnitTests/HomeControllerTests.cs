using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

using FourthTask.Mvc.Controllers;
using FourthTask.Mvc.Infrastructure;
using FourthTask.Mvc.Models;
using FourthTask.Mvc.UnitTests.Data;

namespace FourthTask.Mvc.UnitTests
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public async Task Index_ShouldReturnDefaultView()
        {
            // Arrange
            var mockApiClient = new Mock<IApiClient>();
            mockApiClient.Setup(x => x.GetCustomers(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(Enumerable.Empty<CustomerListVM>()));
            var controller = new HomeController(mockApiClient.Object);

            // Act
            var result = await controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(string.Empty, result.ViewName);
        }

        [TestMethod]
        public async Task Index_ModelShouldReturnCustomersFromTheApi()
        {
            // Arrange
            var customers = MockCustomers.GetCustomers();
            var mockApiClient = new Mock<IApiClient>();
            mockApiClient.Setup(x => x.GetCustomers(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(customers));
            var controller = new HomeController(mockApiClient.Object);

            // Act
            var result = await controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(IEnumerable<CustomerListVM>));
            Assert.AreEqual(customers.Count(), (result.Model as IEnumerable<CustomerListVM>).Count());
        }

        [TestMethod]
        public async Task Index_ShouldReturnErrorMessageIfApiIsDownOrInaccessible()
        {
            var mockApiClient = new Mock<IApiClient>();
            mockApiClient.Setup(x => x.GetCustomers(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Throws<AggregateException>();
            var controller = new HomeController(mockApiClient.Object);

            // Act
            var result = await controller.Index() as ContentResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Content);
            Assert.IsTrue(result.Content.StartsWith("There is a problem with the API"));
        }

        [TestMethod]
        public async Task CustomerDetails_ShouldReturnDefaultView()
        {
            // Arrange
            var mockApiClient = new Mock<IApiClient>();
            mockApiClient
                .Setup(x => x.GetCustomerDetails(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new CustomerVM()));
            mockApiClient
                .Setup(x => x.GetOrdersForCustomer(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(Enumerable.Empty<OrderVM>()));

            var controller = new HomeController(mockApiClient.Object);

            // Act
            var result = await controller.CustomerDetails("TEST-ID") as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(string.Empty, result.ViewName);
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        public async Task CustomerDetails_ShouldRedirectToIndexIfProvidedInvalidCustomerId(string customerId)
        {
            // Arrange
            var mockApiClient = new Mock<IApiClient>();
            mockApiClient
                .Setup(x => x.GetCustomerDetails(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(new CustomerVM()));
            mockApiClient
                .Setup(x => x.GetOrdersForCustomer(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(Enumerable.Empty<OrderVM>()));

            var controller = new HomeController(mockApiClient.Object);

            // Act
            var result = await controller.CustomerDetails(customerId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            Assert.AreEqual("Index", (result as RedirectToRouteResult).RouteValues["action"]);
        }

        [TestMethod]
        public async Task CustomerDetails_ShouldRedirectToIndexOnExceptionInTheApi()
        {
            // Arrange
            var mockApiClient = new Mock<IApiClient>();
            mockApiClient
                .Setup(x => x.GetCustomerDetails(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Throws<AggregateException>();

            var controller = new HomeController(mockApiClient.Object);

            // Act
            var result = await controller.CustomerDetails("TEST-ID");

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            Assert.AreEqual("Index", (result as RedirectToRouteResult).RouteValues["action"]);
        }

        [TestMethod]
        public async Task CustomerDetails_ShouldRedirectToIndexIfCustomerNotFound()
        {
            // Arrange
            var mockApiClient = new Mock<IApiClient>();
            mockApiClient
                .Setup(x => x.GetCustomerDetails(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult<CustomerVM>(null));

            var controller = new HomeController(mockApiClient.Object);

            // Act
            var result = await controller.CustomerDetails("TEST-ID");

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
            Assert.AreEqual("Index", (result as RedirectToRouteResult).RouteValues["action"]);
        }
    }
}
