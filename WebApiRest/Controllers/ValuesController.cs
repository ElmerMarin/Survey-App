using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
namespace WebApiRest.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values

        public IEnumerable<Model.Preguntas> Get()
        {
            IEnumerable<Preguntas> lst;
            using (Model.SystemEncuestas db = new Model.SystemEncuestas())
            {
                lst = db.Preguntas.ToList();

            }
            return lst;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
