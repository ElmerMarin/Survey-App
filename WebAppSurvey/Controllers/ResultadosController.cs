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
    public class ResultadosController : Controller
    {
        private SystemEncuestas db = new SystemEncuestas();

        // GET: Resultados
        public ActionResult Index(string valSearch, int? page)
        {

            ViewBag.Buscar = valSearch;
            List<Resultados> objResultados = new List<Resultados>();
            if (string.IsNullOrEmpty(valSearch))
                objResultados = db.Resultados.Where(c => true).OrderBy(c => c.Id).ToList();
            else
                objResultados = db.Resultados.Where(c => true && (c.Usuarios.NombreUsuario.Contains(valSearch) || c.Usuarios.TipoUsuario.Contains(valSearch))).OrderBy(c => c.Id).ToList();

            int pageSize = 5;
            int pageNumber = page ?? 1;


            return View(objResultados.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult BuscarResultados(string consulta, int? page = null)
        {
            ViewBag.Buscar = consulta;
            List<Resultados> objResultados = new List<Resultados>();
            if (string.IsNullOrEmpty(consulta))
                objResultados = db.Resultados.Where(c => true).OrderBy(c => c.Id).ToList();
            else
                objResultados = db.Resultados.Where(c => true && (c.Usuarios.NombreUsuario.Contains(consulta) || c.Usuarios.TipoUsuario.Contains(consulta))).OrderBy(c => c.Id).ToList();


            int pageSize = 5;
            int pageNumber = page ?? 1;


            return PartialView(objResultados.ToPagedList(pageNumber, pageSize));
        }

        // GET: Resultados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resultados resultados = db.Resultados.Find(id);
            if (resultados == null)
            {
                return HttpNotFound();
            }
            return View(resultados);
        }

        // GET: Resultados/Create
        public ActionResult Create()
        {
            ViewBag.IdUsuario = new SelectList(db.Usuarios, "Id", "TipoUsuario");
            return View();
        }

        // POST: Resultados/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdUsuario,Hora_Inicio,Hora_Final,Fecha")] Resultados resultados)
        {
            if (ModelState.IsValid)
            {
                db.Resultados.Add(resultados);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdUsuario = new SelectList(db.Usuarios, "Id", "TipoUsuario", resultados.IdUsuario);
            return View(resultados);
        }

        // GET: Resultados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resultados resultados = db.Resultados.Find(id);
            if (resultados == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdUsuario = new SelectList(db.Usuarios, "Id", "TipoUsuario", resultados.IdUsuario);
            return View(resultados);
        }

        // POST: Resultados/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdUsuario,Hora_Inicio,Hora_Final,Fecha")] Resultados resultados)
        {
            if (ModelState.IsValid)
            {
                db.Entry(resultados).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdUsuario = new SelectList(db.Usuarios, "Id", "TipoUsuario", resultados.IdUsuario);
            return View(resultados);
        }

        // GET: Resultados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Resultados resultados = db.Resultados.Find(id);
            if (resultados == null)
            {
                return HttpNotFound();
            }
            return View(resultados);
        }

        // POST: Resultados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var DetalleResultados = db.DetalleResultado.Where(c=>c.IdResultado==id).ToList();
            for (int i = 0; i < DetalleResultados.Count; i++)
            {
                db.DetalleResultado.Remove(DetalleResultados[i]);
                db.SaveChanges();
            }
            var resultado= db.Resultados.Where(c => c.Id == id).FirstOrDefault();
            db.Resultados.Remove(resultado);
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
