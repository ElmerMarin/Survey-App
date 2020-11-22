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
    public class EncuestadosController : Controller
    {
        // GET: Encuestado
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

        public ActionResult ListaEncuestados(string val, string valSearch, int? page)
        {
            ViewBag.CurrentSort = val;
            ViewBag.Buscar = valSearch;
            List<Encuestados> objProduct = new List<Encuestados>();
            if (string.IsNullOrEmpty(valSearch))
                objProduct = db.Encuestados.Where(c => true).OrderBy(c => c.IdEncuestado).ToList();
            else
                objProduct = db.Encuestados.Where(c => true && (c.Nombres.Contains(valSearch) || c.Direccion.Contains(valSearch))).OrderBy(c => c.ApellidoPaterno).ToList();

            if (val == "IdCoordinador" || string.IsNullOrEmpty(val))
            {
                val = "IdCoordinador";
                objProduct = objProduct.OrderBy(c => c.IdEncuestado).ToList();
            }

            ViewBag.Order = val;
            int pageSize = 5;
            int pageNumber = page ?? 1;


            return PartialView(objProduct.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public ActionResult BuscarEncuestados(string consulta, int? page = null)
        {
            ViewBag.Buscar = consulta;
            List<Encuestados> objEncuestados = new List<Encuestados>();
            if (string.IsNullOrEmpty(consulta))
                objEncuestados = db.Encuestados.Where(c => true).OrderBy(c => c.IdEncuestado).ToList();
            else
                objEncuestados = db.Encuestados.Where(c => true && (c.Nombres.Contains(consulta) || c.ApellidoPaterno.Contains(consulta)|| c.ApellidoMaterno.Contains(consulta))).OrderBy(c => c.IdEncuestado).ToList();


            int pageSize = 5;
            int pageNumber = page ?? 1;


            return PartialView(objEncuestados.ToPagedList(pageNumber, pageSize));
        }


        [HttpPost]
        public ActionResult Add(Encuestado encuestado, Usuario usuario)
        {

            int id = 0;
            string strMensaje = "No se pudo actualizar la información, intentelo más tarde";
            bool okResult = false;
            if (usuario.Id > 0)
            {
                id = usuario.Id;
                Encuestados UpdatePaciente = db.Encuestados.Where(c => c.IdUsuario == usuario.Id).FirstOrDefault();
                if (UpdatePaciente != null)
                {
                    EncuestadoService actualizar = new EncuestadoService();
                    strMensaje = actualizar.actualizar(usuario, encuestado);
                    okResult = true;

                }
            }
            else
            {
                id = encuestado.IdEncuestado;
                EncuestadoService nuevo = new EncuestadoService();
                okResult = true;
                strMensaje = nuevo.crear(usuario, encuestado);


            }
            return Json(new Response { IsSuccess = okResult, Message = strMensaje, Id = id }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Get(int Id)
        {
            string strMensaje = "No se encontro el encuestado que desea editar";
            var encuestado = db.Encuestados.Where(c => c.IdEncuestado == Id).FirstOrDefault();
            var objUsuario = db.Usuarios.Where(c => c.Id == encuestado.IdUsuario).FirstOrDefault();
            if (objUsuario != null)
            {

                if (objUsuario.TipoUsuario == "Encuestado")
                {

                    EncuestadoService actualizar = new EncuestadoService();
                    var lista = actualizar.Obtener(objUsuario,encuestado);

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
            var objUsu = db.Encuestados.Where(c => c.IdEncuestado == Id).FirstOrDefault();
            if (objUsu != null)
            {
                var objProd = db.Encuestados.Where(c => c.IdEncuestado == Id).FirstOrDefault();
                var objUsu2 = db.Usuarios.Where(c => c.Id == objProd.IdUsuario).FirstOrDefault();
                db.Encuestados.Remove(objProd);
                db.Usuarios.Remove(objUsu2);
                db.SaveChanges();
                strMensaje = "Se elimino el Coordinador Correctamente";
                okResult = true;


            }
            return Json(new Response { IsSuccess = okResult, Message = strMensaje, Id = Id }, JsonRequestBehavior.AllowGet);
        }
    }
}