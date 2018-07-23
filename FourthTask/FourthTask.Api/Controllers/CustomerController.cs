using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

using FourthTask.Api.Factories;
using FourthTask.Repositories;

namespace FourthTask.Api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CustomerController : ApiController
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerListRowToCustomerListVMMapper _customerListRowToCustomerListVMMapper;
        private readonly ICustomerToCustomerVMMapper _customerToCustomerVMMapper;
        private readonly IOrderToOrderVMMapper _orderToOrderVMMapper;

        public CustomerController(ICustomerRepository customerRepository, 
            IOrderRepository orderRepository, 
            ICustomerListRowToCustomerListVMMapper customerListRowToCustomerListVMMapper,
            ICustomerToCustomerVMMapper customerToCustomerVMMapper,
            IOrderToOrderVMMapper orderToOrderVMMapper)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            // I should have used AutoMapper
            _customerListRowToCustomerListVMMapper = customerListRowToCustomerListVMMapper ?? throw new ArgumentNullException(nameof(customerListRowToCustomerListVMMapper));
            _customerToCustomerVMMapper = customerToCustomerVMMapper ?? throw new ArgumentNullException(nameof(customerToCustomerVMMapper));
            _orderToOrderVMMapper = orderToOrderVMMapper ?? throw new ArgumentNullException(nameof(orderToOrderVMMapper));
        }

        [Route("api/customers")]
        [HttpGet]
        public async Task<IHttpActionResult> GetCustomers(string term = null)
        {
            var customers = await _customerRepository.GetCustomersByName(term);
            return Ok(customers.Select(x => _customerListRowToCustomerListVMMapper.Map(x)));
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

            return Ok(_customerToCustomerVMMapper.Map(customer));
        }

        [Route("api/customer/{id}/orders")]
        [HttpGet]
        public async Task<IHttpActionResult> GetOrders(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest("Invalid customer id.");
            }

            if (await _customerRepository.CustomerExists(id) == false)
            {
                return NotFound();
            }

            var orders = await _orderRepository.GetOrdersForCustomer(id);
            return Ok(orders.Select(x => _orderToOrderVMMapper.Map(x)));
        }
    }
}