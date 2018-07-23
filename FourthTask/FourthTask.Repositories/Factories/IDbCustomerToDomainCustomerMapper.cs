namespace FourthTask.Repositories.Factories
{
    public interface IDbCustomerToDomainCustomerMapper
    {
        DomainModels.Customer Map(Data.Customer dbCustomer);
    }
}
