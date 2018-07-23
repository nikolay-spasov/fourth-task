using System;
using System.Linq.Expressions;

namespace FourthTask.Repositories.Factories
{
    public interface IDbOrderToDomainOrderMapper
    {
        Expression<Func<Data.Order, DomainModels.Order>> Map();
    }
}
