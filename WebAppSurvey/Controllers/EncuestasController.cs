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
    public class EncuestasController : Controller
    {
        private SystemEncuestas db = new SystemEncuestas();

        // GET: Encuestas
        public ActionResult Index()
        {
            return View(db.Encuestas.ToList());
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

            List<int> respuestas=new List<int>();
            List<int> ides = new List<int>();
            var respuestasfinales =0;
            var preguntas = db.Preguntas.Where(p => p.IdEncuesta == id).ToList();
            for (int i = 0; i < preguntas.Count; i++)
            {
                int idpregunta = preguntas[i].Id;
                var resultado = db.Respuestas.Where(p => p.IdPregunta ==idpregunta).ToList();
                for (int j = 0; j < resultado.Count; j++)
                {
                    respuestasfinales = resultado[j].Id;
                    respuestas.Add(respuestasfinales);
                   
                }
                ides.Add(preguntas[i].Id);
            }
            ViewBag.respuestas = respuestas;
            ViewBag.preguntas = preguntas;
            ViewBag.idEncuesta = id;

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
