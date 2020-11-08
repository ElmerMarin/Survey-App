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
    public class DetalleEncuestasController : Controller
    {
        private SystemEncuestas db = new SystemEncuestas();

        // GET: DetalleEncuestas
        public ActionResult Index()
        {
            var detalleEncuesta = db.DetalleEncuesta.Include(d => d.Areas).Include(d => d.Categorias).Include(d => d.Encuestas);
            return View(detalleEncuesta.ToList());
        }

        // GET: DetalleEncuestas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleEncuesta detalleEncuesta = db.DetalleEncuesta.Find(id);
            if (detalleEncuesta == null)
            {
                return HttpNotFound();
            }
            return View(detalleEncuesta);
        }

        // GET: DetalleEncuestas/Create
        public ActionResult Create()
        {
            ViewBag.IdArea = new SelectList(db.Areas, "Id", "Nombre");
            ViewBag.IdCategoria = new SelectList(db.Categorias, "Id", "Nombre");
            ViewBag.IdEncuesta = new SelectList(db.Encuestas, "Id", "Titulo");
            return View();
        }

        // POST: DetalleEncuestas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FechaInicio,Fechafinal,Estado,IdEncuesta,IdArea,IdCategoria")] DetalleEncuesta detalleEncuesta)
        {
            if (ModelState.IsValid)
            {
                db.DetalleEncuesta.Add(detalleEncuesta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdArea = new SelectList(db.Areas, "Id", "Nombre", detalleEncuesta.IdArea);
            ViewBag.IdCategoria = new SelectList(db.Categorias, "Id", "Nombre", detalleEncuesta.IdCategoria);
            ViewBag.IdEncuesta = new SelectList(db.Encuestas, "Id", "Titulo", detalleEncuesta.IdEncuesta);
            return View(detalleEncuesta);
        }

        // GET: DetalleEncuestas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleEncuesta detalleEncuesta = db.DetalleEncuesta.Find(id);
            if (detalleEncuesta == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdArea = new SelectList(db.Areas, "Id", "Nombre", detalleEncuesta.IdArea);
            ViewBag.IdCategoria = new SelectList(db.Categorias, "Id", "Nombre", detalleEncuesta.IdCategoria);
            ViewBag.IdEncuesta = new SelectList(db.Encuestas, "Id", "Titulo", detalleEncuesta.IdEncuesta);
            return View(detalleEncuesta);
        }

        // POST: DetalleEncuestas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FechaInicio,Fechafinal,Estado,IdEncuesta,IdArea,IdCategoria")] DetalleEncuesta detalleEncuesta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detalleEncuesta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdArea = new SelectList(db.Areas, "Id", "Nombre", detalleEncuesta.IdArea);
            ViewBag.IdCategoria = new SelectList(db.Categorias, "Id", "Nombre", detalleEncuesta.IdCategoria);
            ViewBag.IdEncuesta = new SelectList(db.Encuestas, "Id", "Titulo", detalleEncuesta.IdEncuesta);
            return View(detalleEncuesta);
        }

        // GET: DetalleEncuestas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleEncuesta detalleEncuesta = db.DetalleEncuesta.Find(id);
            if (detalleEncuesta == null)
            {
                return HttpNotFound();
            }
            return View(detalleEncuesta);
        }

        // POST: DetalleEncuestas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DetalleEncuesta detalleEncuesta = db.DetalleEncuesta.Find(id);
            db.DetalleEncuesta.Remove(detalleEncuesta);
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
