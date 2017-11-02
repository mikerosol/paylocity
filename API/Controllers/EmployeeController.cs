using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class EmployeeController : ApiController
    {
        public IHttpActionResult Get(int? quoteId = null)
        {
            return Ok(Api.Code.Employee.Get(quoteId));
        }

        public IHttpActionResult Post([FromBody]Objects.Employee employee)
        {
            Api.Code.Employee.Save(employee);
            return Ok();
        }

        public IHttpActionResult Put([FromBody]Objects.Employee employee)
        {
            Api.Code.Employee.Save(employee);
            return Ok();
        }

        public IHttpActionResult Delete(int personId)
        {
            Api.Code.Employee.Delete(personId);
            return Ok();
        }
    }
}
