using FourthTask.Api.Models;
using FourthTask.DomainModels;

namespace FourthTask.Api.Factories
{
    public interface IOrderToOrderVMMapper
    {
        OrderVM Map(Order order);
    }
}
