using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model;

namespace WebAppSurvey.Controllers
{
    public class RespuestasController : Controller
    {
        private SystemEncuestas db = new SystemEncuestas();

        // GET: Respuestas
        public ActionResult Index(int? d)
        {
            var respuestas = db.Respuestas.Include(r => r.Preguntas).Where(p => p.IdPregunta == d);
            ViewBag.idRespuesta = d;
            return View(respuestas.ToList());
        }

        // GET: Respuestas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Respuestas respuestas = db.Respuestas.Find(id);
            if (respuestas == null)
            {
                return HttpNotFound();
            }
            return View(respuestas);
        }

        // GET: Respuestas/Create
        public ActionResult Create()
        {
            ViewBag.IdPregunta = new SelectList(db.Preguntas, "Id", "Descripcion");
            return View();
        }

        // POST: Respuestas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion,Estado,IdPregunta")] Respuestas respuestas, int id)
        {
            if (ModelState.IsValid)
            {
                db.Respuestas.Add(respuestas);
                db.SaveChanges();
                return RedirectToAction("Index", "Respuestas", new { d = id });
            }

            ViewBag.IdPregunta = new SelectList(db.Preguntas, "Id", "Descripcion", respuestas.IdPregunta);
            return View(respuestas);
        }

        // GET: Respuestas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Respuestas respuestas = db.Respuestas.Find(id);
            if (respuestas == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdPregunta = new SelectList(db.Preguntas, "Id", "Descripcion", respuestas.IdPregunta);
            return View(respuestas);
        }

        // POST: Respuestas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descripcion,Estado,IdPregunta")] Respuestas respuestas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(respuestas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdPregunta = new SelectList(db.Preguntas, "Id", "Descripcion", respuestas.IdPregunta);
            return View(respuestas);
        }

        // GET: Respuestas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Respuestas respuestas = db.Respuestas.Find(id);
            if (respuestas == null)
            {
                return HttpNotFound();
            }
            return View(respuestas);
        }

        // POST: Respuestas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Respuestas respuestas = db.Respuestas.Find(id);
            db.Respuestas.Remove(respuestas);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
