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
    public class AreasController : Controller
    {
        private SystemEncuestas db = new SystemEncuestas();

        // GET: Areas
        public ActionResult Index(string valSearch, int? page)
        {
            
            ViewBag.Buscar = valSearch;
            List<Areas> objAreas = new List<Areas>();
            if (string.IsNullOrEmpty(valSearch))
                objAreas = db.Areas.Where(c => true).OrderBy(c => c.Id).ToList();
            else
                objAreas = db.Areas.Where(c => true && (c.Nombre.Contains(valSearch) || c.Estado.Contains(valSearch))).OrderBy(c => c.Id).ToList();

            int pageSize = 5;
            int pageNumber = page ?? 1;


            return View(objAreas.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult BuscarAreas(string consulta, int? page = null)
        {
            ViewBag.Buscar = consulta;
            List<Areas> objAreas = new List<Areas>();
            if (string.IsNullOrEmpty(consulta))
                objAreas = db.Areas.Where(c => true).OrderBy(c => c.Id).ToList();
            else
                objAreas = db.Areas.Where(c => true && (c.Nombre.Contains(consulta) || c.Estado.Contains(consulta))).OrderBy(c => c.Id).ToList();


            int pageSize = 5;
            int pageNumber = page ?? 1;


            return PartialView(objAreas.ToPagedList(pageNumber, pageSize));
        }


        // GET: Areas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Areas areas = db.Areas.Find(id);
            if (areas == null)
            {
                return HttpNotFound();
            }
            return View(areas);
        }

        // GET: Areas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Areas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Fecha,Estado")] Areas areas)
        {
            if (ModelState.IsValid)
            {
                db.Areas.Add(areas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(areas);
        }

        // GET: Areas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Areas areas = db.Areas.Find(id);
            if (areas == null)
            {
                return HttpNotFound();
            }
            return View(areas);
        }

        // POST: Areas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Fecha,Estado")] Areas areas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(areas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(areas);
        }

        // GET: Areas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Areas areas = db.Areas.Find(id);
            if (areas == null)
            {
                return HttpNotFound();
            }
            return View(areas);
        }

        // POST: Areas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Areas areas = db.Areas.Find(id);
            db.Areas.Remove(areas);
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
