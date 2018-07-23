using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using FourthTask.Mvc.Models;

namespace FourthTask.Mvc.Infrastructure
{
    public class ApiClient : IApiClient
    {
        private const string customersListBaseUrl = "http://localhost:57145/api/customers";
        private const string customerDetailsBaseUrl = "http://localhost:57145/api/customer";
        private readonly HttpClient _http;

        public ApiClient(HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<IEnumerable<CustomerListVM>> GetCustomers(string customerName, CancellationToken cancellationToken = default(CancellationToken))
        {
            var url = customersListBaseUrl;
            if (string.IsNullOrWhiteSpace(customerName) == false)
            {
                url += "?term=" + customerName;
            }
            var response = await _http.GetAsync(url, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<CustomerListVM>>();
            }

            return Enumerable.Empty<CustomerListVM>();
        }

        public async Task<CustomerVM> GetCustomerDetails(string customerId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await _http.GetAsync(customerDetailsBaseUrl + "/" + customerId, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<CustomerVM>(cancellationToken);
            }

            return null;
        }

        public async Task<IEnumerable<OrderVM>> GetOrdersForCustomer(string customerId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await _http.GetAsync(customerDetailsBaseUrl + "/" + customerId + "/orders", cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<IEnumerable<OrderVM>>(cancellationToken);
            }

            return Enumerable.Empty<OrderVM>();
        }
    }
}