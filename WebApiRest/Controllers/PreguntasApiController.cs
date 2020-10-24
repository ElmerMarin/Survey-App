using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Model;

namespace WebApiRest.Controllers
{
    public class PreguntasApiController : ApiController
    {
        private SystemEncuestas db = new SystemEncuestas();

        // GET: api/PreguntasApi
        public IQueryable<Preguntas> GetPreguntas()
        {
            return db.Preguntas;
        }

        // GET: api/PreguntasApi/5
        [ResponseType(typeof(Preguntas))]
        public IHttpActionResult GetPreguntas(int id)
        {
            Preguntas preguntas = db.Preguntas.Find(id);
            if (preguntas == null)
            {
                return NotFound();
            }

            return Ok(preguntas);
        }

        // PUT: api/PreguntasApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPreguntas(int id, Preguntas preguntas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != preguntas.Id)
            {
                return BadRequest();
            }

            db.Entry(preguntas).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PreguntasExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/PreguntasApi
        [ResponseType(typeof(Preguntas))]
        public IHttpActionResult PostPreguntas(Preguntas preguntas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Preguntas.Add(preguntas);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = preguntas.Id }, preguntas);
        }

        // DELETE: api/PreguntasApi/5
        [ResponseType(typeof(Preguntas))]
        public IHttpActionResult DeletePreguntas(int id)
        {
            Preguntas preguntas = db.Preguntas.Find(id);
            if (preguntas == null)
            {
                return NotFound();
            }

            db.Preguntas.Remove(preguntas);
            db.SaveChanges();

            return Ok(preguntas);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PreguntasExists(int id)
        {
            return db.Preguntas.Count(e => e.Id == id) > 0;
        }
    }
}