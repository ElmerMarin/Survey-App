using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model;
using PagedList;

namespace WebAppSurvey.Controllers
{
    [Authorize]
    public class CategoriasController : Controller
    {
        private SystemEncuestas db = new SystemEncuestas();

        // GET: Categorias
        public ActionResult Index(string valSearch, int? page)
        {
            ViewBag.Buscar = valSearch;
            List<Categorias> objCategorias = new List<Categorias>();
            if (string.IsNullOrEmpty(valSearch))
                objCategorias = db.Categorias.Where(c => true).OrderBy(c => c.Id).ToList();
            else
                objCategorias = db.Categorias.Where(c => true && (c.Nombre.Contains(valSearch) || c.Estado.Contains(valSearch) || c.Areas.Nombre.Contains(valSearch))).OrderBy(c => c.Id).ToList();

            int pageSize = 5;
            int pageNumber = page ?? 1;


            return View(objCategorias.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult BuscarCategorias(string consulta, int? page = null)
        {
            ViewBag.Buscar = consulta;
            List<Categorias> objCategorias = new List<Categorias>();
            if (string.IsNullOrEmpty(consulta))
                objCategorias = db.Categorias.Where(c => true).OrderBy(c => c.Id).ToList();
            else
                objCategorias = db.Categorias.Where(c => true && (c.Nombre.Contains(consulta) || c.Estado.Contains(consulta))).OrderBy(c => c.Id).ToList();


            int pageSize = 5;
            int pageNumber = page ?? 1;


            return PartialView(objCategorias.ToPagedList(pageNumber, pageSize));
        }

        // GET: Categorias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categorias categorias = db.Categorias.Find(id);
            if (categorias == null)
            {
                return HttpNotFound();
            }
            return View(categorias);
        }

        // GET: Categorias/Create
        public ActionResult Create()
        {
            ViewBag.IdArea = new SelectList(db.Areas, "Id", "Nombre");
            return View();
        }

        // POST: Categorias/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Fecha,Estado,IdArea")] Categorias categorias)
        {
            if (ModelState.IsValid)
            {
                db.Categorias.Add(categorias);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdArea = new SelectList(db.Areas, "Id", "Nombre", categorias.IdArea);
            return View(categorias);
        }

        // GET: Categorias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categorias categorias = db.Categorias.Find(id);
            if (categorias == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdArea = new SelectList(db.Areas, "Id", "Nombre", categorias.IdArea);
            return View(categorias);
        }

        // POST: Categorias/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Fecha,Estado,IdArea")] Categorias categorias)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categorias).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdArea = new SelectList(db.Areas, "Id", "Nombre", categorias.IdArea);
            return View(categorias);
        }

        // GET: Categorias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categorias categorias = db.Categorias.Find(id);
            if (categorias == null)
            {
                return HttpNotFound();
            }
            return View(categorias);
        }

        // POST: Categorias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Categorias categorias = db.Categorias.Find(id);
            db.Categorias.Remove(categorias);
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
