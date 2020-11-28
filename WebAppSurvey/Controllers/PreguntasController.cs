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
    [Authorize]
    public class PreguntasController : Controller
    {
        private SystemEncuestas db = new SystemEncuestas();

        // GET: Preguntas
        public ActionResult Index(int? d)
        {
            var preguntas = db.Preguntas.Include(p => p.Encuestas).Where(p => p.IdEncuesta == d);
            ViewBag.identificador = d;
            return View(preguntas.ToList());
            
        }

        //public ActionResult Index2(int d)
        //{
        //    var preguntas = db.Preguntas.Include(p => p.Encuestas).Where(p=>p.IdEncuesta==d);
        //    return View(preguntas.ToList());
        //}

        // GET: Preguntas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Preguntas preguntas = db.Preguntas.Find(id);
            if (preguntas == null)
            {
                return HttpNotFound();
            }
            return View(preguntas);
        }

        // GET: Preguntas/Create
        public ActionResult Create(int? id)
        {
            var encuestas = db.Encuestas.Where(c => c.Id == id);
            ViewBag.IdEncuesta = new SelectList(encuestas, "Id", "Titulo");
            ViewBag.IDEncuestas =id;
            return View();
        }

        // POST: Preguntas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descripcion,Estado,IdEncuesta")] Preguntas preguntas,int? id)
        {
            if (ModelState.IsValid)
            {
                db.Preguntas.Add(preguntas);
                db.SaveChanges();
                return RedirectToAction("Index", "Preguntas", new { d = id });
            }

            ViewBag.IdEncuesta = new SelectList(db.Encuestas, "Id", "Titulo", preguntas.IdEncuesta);
            return View(preguntas);
        }

        // GET: Preguntas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Preguntas preguntas = db.Preguntas.Find(id);
            if (preguntas == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdEncuesta = new SelectList(db.Encuestas, "Id", "Titulo", preguntas.IdEncuesta);
            return View(preguntas);
        }

        // POST: Preguntas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descripcion,Estado,IdEncuesta")] Preguntas preguntas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(preguntas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Preguntas", new { d = preguntas.IdEncuesta });
            }
            ViewBag.IdEncuesta = new SelectList(db.Encuestas, "Id", "Titulo", preguntas.IdEncuesta);
            return View(preguntas);
        }

        // GET: Preguntas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Preguntas preguntas = db.Preguntas.Find(id);
            if (preguntas == null)
            {
                return HttpNotFound();
            }
            return View(preguntas);
        }

        // POST: Preguntas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Preguntas preguntas = db.Preguntas.Find(id);
            try
            {
                db.Preguntas.Remove(preguntas);
                db.SaveChanges();
            }
            catch (Exception)
            {

                return RedirectToAction("Error", "Preguntas", new { d = preguntas.IdEncuesta });
            }
            return RedirectToAction("Index", "Preguntas", new { d = preguntas.IdEncuesta });
        }

        public ActionResult Error(int? d)
        {
            var preguntas = db.Preguntas.Include(p => p.Encuestas).Where(p => p.IdEncuesta == d);
            ViewBag.identificador = d;
            return View(preguntas.ToList());
        }

        public ActionResult Respuestas(int? id)
        {
            var preguntas = db.Respuestas.Include(p => p.Preguntas).Where(p => p.IdPregunta == id);
            if (preguntas == null)
            {
                return RedirectToAction("Index", "Respuestas");
            }
            return RedirectToAction("Index", "Respuestas", new { d = id });

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
