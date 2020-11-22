using Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppSurvey.Helpers;
using WebAppSurvey.Models;

namespace WebAppSurvey.Controllers
{
    public class CoordinadorController : Controller
    {
        private SystemEncuestas db = new SystemEncuestas();
        // GET: Coordinador
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Perfil()
        {
            return View();
        }

        public ActionResult ListaCoordinadores(string val, string valSearch, int? page)
        {
            ViewBag.CurrentSort = val;
            ViewBag.Buscar = valSearch;
            List<Coordinadores> objProduct = new List<Coordinadores>();
            if (string.IsNullOrEmpty(valSearch))
                objProduct = db.Coordinadores.Where(c => true).OrderBy(c => c.IdCoordinador).ToList();
            else
                objProduct = db.Coordinadores.Where(c => true && (c.Nombres.Contains(valSearch) || c.Direccion.Contains(valSearch))).OrderBy(c => c.ApellidoPaterno).ToList();

            if (val == "IdCoordinador" || string.IsNullOrEmpty(val))
            {
                val = "IdCoordinador";
                objProduct = objProduct.OrderBy(c => c.IdCoordinador).ToList();
            }

            ViewBag.Order = val;
            int pageSize = 5;
            int pageNumber = page ?? 1;


            return PartialView(objProduct.ToPagedList(pageNumber, pageSize));
        }


        [HttpPost]
        public ActionResult BuscarCoordinadores(string consulta, int? page = null)
        {
            ViewBag.Buscar = consulta;
            List<Coordinadores> objCoordinadores = new List<Coordinadores>();
            if (string.IsNullOrEmpty(consulta))
                objCoordinadores = db.Coordinadores.Where(c => true).OrderBy(c => c.IdCoordinador).ToList();
            else
                objCoordinadores = db.Coordinadores.Where(c => true && (c.Nombres.Contains(consulta) || c.ApellidoPaterno.Contains(consulta) || c.ApellidoMaterno.Contains(consulta))).OrderBy(c => c.IdCoordinador).ToList();


            int pageSize = 5;
            int pageNumber = page ?? 1;


            return PartialView(objCoordinadores.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult Add(Coordinador admin, Usuario usuario)
        {
            
            int id = 0;
            string strMensaje = "No se pudo actualizar la información, intentelo más tarde";
            bool okResult = false;
            if (usuario.Id > 0)
            {
                id = usuario.Id;
                Coordinadores UpdatePaciente = db.Coordinadores.Where(c => c.IdUsuario == usuario.Id).FirstOrDefault();
                if (UpdatePaciente != null)
                {
                    CoordinadorService actualizar = new CoordinadorService();                  
                    strMensaje = actualizar.actualizar(usuario, admin);
                    okResult = true;

                }               
            }
            else
            {
                 id = admin.IdCoordinador;
                 CoordinadorService nuevo = new CoordinadorService();
                 okResult = true;
                 strMensaje = nuevo.crear(usuario, admin);


            }
            return Json(new Response { IsSuccess = okResult, Message = strMensaje, Id = id }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Get(int Id)
        {
            string strMensaje = "No se encontro el Coordinador que desea editar";
            var admin = db.Coordinadores.Where(c => c.IdCoordinador == Id).FirstOrDefault();
            var objUsuario = db.Usuarios.Where(c => c.Id == admin.IdUsuario).FirstOrDefault();
            if (objUsuario != null)
            {

                if (objUsuario.TipoUsuario == "Coordinador")
                {

                    CoordinadorService actualizar = new CoordinadorService();
                    var lista = actualizar.Obtener(objUsuario,admin);

                    return Json(new Response { IsSuccess = true, Id = Id, Result = lista.ElementAt(0), Result2 = lista.ElementAt(1) }, JsonRequestBehavior.AllowGet);

                }

            }
            return Json(new Response { IsSuccess = false, Message = strMensaje, Id = Id }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult Eliminar(int Id)
        {
            string strMensaje = "No se encontro el administrador que desea eliminar";
            bool okResult = false;
            var objUsu = db.Coordinadores.Where(c => c.IdCoordinador == Id).FirstOrDefault();
            if (objUsu != null)
            {
                var objProd = db.Coordinadores.Where(c => c.IdCoordinador == Id).FirstOrDefault();    
                var objUsu2 = db.Usuarios.Where(c => c.Id == objProd.IdUsuario).FirstOrDefault();               
                db.Coordinadores.Remove(objProd);
                db.Usuarios.Remove(objUsu2);
                db.SaveChanges();
                strMensaje = "Se elimino el Coordinador Correctamente";
                okResult = true;

                
            }
            return Json(new Response { IsSuccess = okResult, Message = strMensaje, Id = Id }, JsonRequestBehavior.AllowGet);
        }
    }
}