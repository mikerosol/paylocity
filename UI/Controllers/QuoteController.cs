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
    public class QuoteController : Controller
    {
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            using (var api = Global.Api)
            {
                var quotes = await api.Request<List<Objects.Quote>>(HttpMethod.Get, "Quote", "", null, null);
                return View(quotes);
            }            
        }

        [HttpGet]
        public async Task<ActionResult> Details(int? quoteId = null)
        {
            using (var api = Global.Api)
            {
                var quote = new Objects.Quote();

                //NEW QUOTE
                if (quoteId == null)
                {
                    quote = await api.Request<Objects.Quote>(HttpMethod.Post, "Quote", "", null, quote);                    
                }
                //EXISITNG QUOTE
                else
                {
                    var quotes = await api.Request<List<Objects.Quote>>(HttpMethod.Get, "Quote", $"quoteId={quoteId}", null, null);
                    quote = quotes.Single();
                }
                return View(quote);
            }
        }

        [HttpPost]
        public async Task<bool> Delete(int quoteId)
        {
            using (var api = Global.Api)
            {
                await api.Request<Objects.Quote>(HttpMethod.Delete, "Quote", $"quoteId={quoteId}", null, null);
                return true;
            }
        }
    }
}