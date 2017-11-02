using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UI.Models;

namespace UI.Controllers
{
    public class EmployeeController : Controller
    {
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            using (var api = Global.Api)
            {
                var quotes = await api.Request<List<Objects.Quote>>(HttpMethod.Get, "Quote", "", null, null);
            }

            return View();
        }
        [HttpGet]
        public ViewResult Item(int quoteId)
        {
            var employee = new Objects.Employee()
            {
                QuoteId = quoteId
            };

            return View(employee);
        }
        [HttpPost]
        public async Task<bool> Save(Objects.Employee employee)
        {
            using (var api = Global.Api)
            {
                await api.Request<Objects.Employee>(HttpMethod.Post, "Employee", "", null, employee);
                return true;
            }
        }
        [HttpPost]
        public async Task<bool> Delete(int personId)
        {
            using (var api = Global.Api)
            {
                await api.Request<object>(HttpMethod.Delete, "Employee", $"personId={personId}", null, null);
                return true;
            }
        }
    }
}