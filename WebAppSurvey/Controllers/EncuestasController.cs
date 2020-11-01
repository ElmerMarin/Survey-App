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
    public class EncuestasController : Controller
    {
        private SystemEncuestas db = new SystemEncuestas();

        // GET: Encuestas
        [HttpPost]
        public ActionResult Index(string valSearch, int? page)
        {
            //return View(db.Encuestas.ToList());
            ViewBag.Buscar = valSearch;
            List<Encuestas> objEncuestas = new List<Encuestas>();
            if (string.IsNullOrEmpty(valSearch))
                objEncuestas = db.Encuestas.Where(c => true).OrderBy(c => c.Id).ToList();
            else
                objEncuestas = db.Encuestas.Where(c => true && (c.Descripcion.Contains(valSearch) || c.Titulo.Contains(valSearch))).OrderBy(c => c.Id).ToList();

            int pageSize = 5;
            int pageNumber = page ?? 1;


            return View(objEncuestas.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Index(string val, string valSearch, int? page)
        {
            ViewBag.Buscar = valSearch;
            List<Encuestas> objEncuestas = new List<Encuestas>();
            if (string.IsNullOrEmpty(valSearch))
                objEncuestas = db.Encuestas.Where(c => true).OrderBy(c => c.Id).ToList();
            else
                objEncuestas = db.Encuestas.Where(c => true && (c.Descripcion.Contains(valSearch) || c.Titulo.Contains(valSearch))).OrderBy(c => c.Id).ToList();

            int pageSize = 5;
            int pageNumber = page ?? 1;


            return View(objEncuestas.ToPagedList(pageNumber, pageSize));
        }

        // GET: Encuestas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Encuestas encuestas = db.Encuestas.Find(id);
            if (encuestas == null)
            {
                return HttpNotFound();
            }
            return View(encuestas);
        }

        // GET: Encuestas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Encuestas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Titulo,Descripcion,Estado")] Encuestas encuestas)
        {
            if (ModelState.IsValid)
            {
                db.Encuestas.Add(encuestas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(encuestas);
        }

        // GET: Encuestas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Encuestas encuestas = db.Encuestas.Find(id);
            if (encuestas == null)
            {
                return HttpNotFound();
            }
            return View(encuestas);
        }

        // POST: Encuestas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Titulo,Descripcion,Estado")] Encuestas encuestas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(encuestas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(encuestas);
        }

        // GET: Encuestas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Encuestas encuestas = db.Encuestas.Find(id);
            if (encuestas == null)
            {
                return HttpNotFound();
            }
            return View(encuestas);
        }

        // POST: Encuestas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Encuestas encuestas = db.Encuestas.Find(id);
            db.Encuestas.Remove(encuestas);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        
        public ActionResult Preguntas(int? id)
        {

            var preguntas = db.Preguntas.Include(p => p.Encuestas).Where(p => p.IdEncuesta == id);
            if (preguntas == null)
            {
                return RedirectToAction("Index","Preguntas");
            }
            return RedirectToAction("Index","Preguntas", new { d=id });

        }


        public ActionResult GenerarEncuesta(int? id)
        {

            var preguntas = db.Preguntas.Where(p => p.IdEncuesta == id).ToList();
            ViewBag.preguntas = preguntas;

            if (preguntas == null)
            {
                return RedirectToAction("Error", "Preguntas");
            }
            return View();

        }

        [HttpPost]
        public ActionResult Resultados(string []Respuestas,string idEncuesta, string idUsuario, string hora_Inicio, string hora_Final, string fecha)
        {

            var ResultadosEncuestas =(new Resultados
            {
                IdUsuario = Convert.ToInt32(idUsuario),
                Hora_Inicio = hora_Inicio,
                Hora_Final = hora_Final,
                Fecha = Convert.ToDateTime(fecha)
            });
            db.Resultados.Add(ResultadosEncuestas);
            db.SaveChanges();

            int idResultado = ResultadosEncuestas.Id;

            for (int i = 0; i < Respuestas.Length;i++) {
                var DetalleResultados = (new DetalleResultado
                {
                    IdEncuesta = Convert.ToInt32(idEncuesta),
                    IdResultado = idResultado,
                    Valor = Respuestas[i]
                });
                db.DetalleResultado.Add(DetalleResultados);
                db.SaveChanges();
            }

            return Json(Url.Action("Index", "Home"));



        }

        
        public ActionResult ResultadosFinales(int? id)
        {
            List<int> respuestas = new List<int>();
            var preguntas = db.Preguntas.Where(p => p.IdEncuesta == id).ToList();
            ViewBag.respuestas = respuestas;
            ViewBag.preguntas = preguntas;
            ViewBag.idEncuesta = id;
            return View();
        }



        [HttpPost]
        public ActionResult BuscarEncuestas(string consulta, int? page = null)
        {
            ViewBag.Buscar = consulta;
            List<Encuestas> objProduct = new List<Encuestas>();
            if (string.IsNullOrEmpty(consulta))
                objProduct = db.Encuestas.Where(c => true).OrderBy(c => c.Id).ToList();
            else
                objProduct = db.Encuestas.Where(c => true && (c.Descripcion.Contains(consulta) || c.Titulo.Contains(consulta) || c.Estado.Contains(consulta))).OrderBy(c => c.Id).ToList();


            int pageSize = 5;
            int pageNumber = page ?? 1;


            return PartialView(objProduct.ToPagedList(pageNumber, pageSize));
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
