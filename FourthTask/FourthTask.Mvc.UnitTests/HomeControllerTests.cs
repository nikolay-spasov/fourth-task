using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using FourthTask.Mvc.Controllers;

namespace FourthTask.Mvc.UnitTests
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void IndexReturnsDefaultViews()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(string.Empty, result.ViewName);
        }

        [TestMethod]
        public void CustomerReturnsDefaultViews()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.Customer("Test") as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(string.Empty, result.ViewName);
        }

        [TestMethod]
        public void CustomerShouldPassIdAsModel()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.Customer("Test") as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.AreEqual("Test", result.Model);
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        public void CustomerShoulRedirectToIndexIfInvalidId(string customerId)
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.Customer(customerId) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(false, result.Permanent);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }
    }
}
