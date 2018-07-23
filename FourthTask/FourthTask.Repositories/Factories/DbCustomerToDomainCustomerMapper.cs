namespace FourthTask.Repositories.Factories
{
    public class DbCustomerToDomainCustomerMapper : IDbCustomerToDomainCustomerMapper
    {
        public DomainModels.Customer Map(Data.Customer dbCustomer)
        {
            return new DomainModels.Customer
            {
                Address = dbCustomer.Address,
                City = dbCustomer.City,
                CompanyName = dbCustomer.CompanyName,
                ContactName = dbCustomer.ContactName,
                ContactTitle = dbCustomer.ContactTitle,
                Country = dbCustomer.Country,
                CustomerId = dbCustomer.CustomerID,
                Fax = dbCustomer.Fax,
                Phone = dbCustomer.Phone,
                PostalCode = dbCustomer.PostalCode,
                Region = dbCustomer.Region,
            };
        }
    }
}
