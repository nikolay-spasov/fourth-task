using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

using FourthTask.Repositories;

namespace FourthTask.Api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CustomerController : ApiController
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;

        public CustomerController(ICustomerRepository customerRepository, IOrderRepository orderRepository)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        [Route("api/customers")]
        [HttpGet]
        public async Task<IHttpActionResult> GetCustomers(string term = null)
        {
            var result = await _customerRepository.GetCustomersByName(term);
            return Ok(result);
        }

        [Route("api/customer/{id}")]
        [HttpGet]
        public async Task<IHttpActionResult> GetCustomer(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest("Invalid customer id.");
            }

            var customer = await _customerRepository.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        [Route("api/customer/{id}/orders")]
        [HttpGet]
        public async Task<IHttpActionResult> GetOrders(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Invalid customer id.");
            }

            if (await _customerRepository.CustomerExists(id) == false)
            {
                return NotFound();
            }

            var orders = await _orderRepository.GetOrdersForCustomer(id);
            return Ok(orders);
        }
    }
}