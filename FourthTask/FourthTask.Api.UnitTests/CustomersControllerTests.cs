using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

using FourthTask.Api.Controllers;
using FourthTask.DomainModels;
using FourthTask.Repositories;
using FourthTask.Api.UnitTests.Data;

namespace FourthTask.Api.UnitTests
{
    //[TestClass]
    //public class CustomersControllerTests
    //{
    //    [TestMethod]
    //    public async Task GetReturnsAllCustomers()
    //    {
    //        // Arrange
    //        var data = MockCustomers.GetCustomers();

    //        var moq = new Mock<ICustomerRepository>();
    //        moq.Setup(x => x.GetCustomersByName(It.IsAny<string>()))
    //            .Returns(Task.FromResult(data));

    //        var controller = new CustomersController(moq.Object);

    //        // Act
    //        var response = await controller.Get() as OkNegotiatedContentResult<IEnumerable<CustomerListDTO>>;
    //        var content = response.Content;

    //        // Assert
    //        Assert.IsNotNull(content);
    //        Assert.AreEqual(data.Count(), content.Count());
    //    }
    //}
}
