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
    public class DependentController : Controller
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
            var dependent = new Objects.Dependent()
            {
                QuoteId = quoteId
            };

            return View(dependent);
        }
        [HttpPost]
        public async Task<bool> Save(Objects.Dependent dependent)
        {
            using (var api = Global.Api)
            {
                await api.Request<Objects.Dependent>(HttpMethod.Post, "Dependent", "", null, dependent);
                return true;
            }
        }
        [HttpPost]
        public async Task<bool> Delete(int personId)
        {
            using (var api = Global.Api)
            {
                await api.Request<object>(HttpMethod.Delete, "Dependent", $"personId={personId}", null, null);
                return true;
            }
        }
    }
}