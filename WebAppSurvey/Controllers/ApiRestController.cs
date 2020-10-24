using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAppSurvey.Controllers
{
    public class ApiRestController : ApiController
    {
        // GET: api/ApiRest
        public IEnumerable<Model.Preguntas> Get()
        {
            IEnumerable<Model.Preguntas> lst;
            using (Model.SystemEncuestas db=new Model.SystemEncuestas())
            {
                lst = db.Preguntas.ToList();

            }
            return lst;
        }

        // GET: api/ApiRest/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ApiRest
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ApiRest/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApiRest/5
        public void Delete(int id)
        {
        }
    }
}
