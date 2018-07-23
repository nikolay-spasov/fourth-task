using System;
using System.Threading.Tasks;
using System.Web.Mvc;

using FourthTask.Mvc.Infrastructure;

namespace FourthTask.Mvc.Controllers
{
    public class HomeController : Controller
    {
        public IApiClient _apiClient;

        public HomeController(IApiClient apiClient)
        {
            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
        }

        public async Task<ActionResult> Index(string term = "")
        {
            try
            {
                var vm = await _apiClient.GetCustomers(term);
                return View(vm);
            }
            catch (AggregateException)
            {
                return Content("There is a problem with the API, please make sure that the API project is running on http://localhost:57145 !");
            }
        }

        public async Task<ActionResult> CustomerDetails(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return RedirectToAction("Index");
            }

            try
            {
                var vm = await _apiClient.GetCustomerDetails(id);
                if (vm == null)
                {
                    return RedirectToAction("Index");
                }

                vm.Orders = await _apiClient.GetOrdersForCustomer(id);
                return View(vm);
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }
    }
}