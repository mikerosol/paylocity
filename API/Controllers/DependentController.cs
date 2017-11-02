using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class DependentController : ApiController
    {
        public IHttpActionResult Get(int? quoteId = null)
        {
            return Ok(Api.Code.Dependent.Get(quoteId));
        }

        public IHttpActionResult Post([FromBody]Objects.Dependent Dependent)
        {
            Api.Code.Dependent.Save(Dependent);
            return Ok();
        }

        public IHttpActionResult Put([FromBody]Objects.Dependent Dependent)
        {
            Api.Code.Dependent.Save(Dependent);
            return Ok();
        }

        public IHttpActionResult Delete(int personId)
        {
            Api.Code.Dependent.Delete(personId);
            return Ok();
        }
    }
}
