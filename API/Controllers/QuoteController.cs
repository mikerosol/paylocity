using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Api.Controllers
{
    public class QuoteController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get(int? quoteId = null)
        {
            return Ok(Api.Code.Quote.Get(quoteId));
        }

        public IHttpActionResult Post([FromBody]Objects.Quote quote)
        {
            return Ok(Api.Code.Quote.Save(quote));
        }

        public IHttpActionResult Delete(int quoteId)
        {
            Api.Code.Quote.Delete(quoteId);
            return Ok();
        }
    }
}
