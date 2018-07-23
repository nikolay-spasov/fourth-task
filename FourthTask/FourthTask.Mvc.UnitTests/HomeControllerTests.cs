using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using FourthTask.Mvc.Controllers;

namespace FourthTask.Mvc.UnitTests
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void Index_ShouldReturnDefaultViews()
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
        public void CustomerDetails_ShouldReturnDefaultViews()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.CustomerDetails("Test") as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(string.Empty, result.ViewName);
        }

        [TestMethod]
        public void CustomerDetails_ShouldPassIdAsModel()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.CustomerDetails("Test") as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.AreEqual("Test", result.Model);
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow(" ")]
        public void CustomerDetails_ShoulRedirectToIndexIfInvalidId(string customerId)
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.CustomerDetails(customerId) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(false, result.Permanent);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }
    }
}
