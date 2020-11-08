using Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppSurvey.Helpers;
using WebAppSurvey.Models;

namespace WebAppSurvey.Controllers
{

    [Authorize]
    public class UsersController : Controller
    {
        // GET: Users
        SystemEncuestas db = new SystemEncuestas();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Usuario usuario, Encuestado encuestado, Coordinador admin)
        {
            int id = 0;
            string strMensaje = "No se pudo actualizar la información, intentelo más tarde";
            bool okResult = false;
            if (usuario.Id > 0)
            {
                id = usuario.Id;
                Usuarios UpdatePaciente = db.Usuarios.Where(c => c.Id == usuario.Id).FirstOrDefault();
                var Tipo = db.Usuarios.Where(c => c.Id == usuario.Id).Select(m => m.TipoUsuario).FirstOrDefault();
                if (Tipo == "Encuestado")
                {
                    EncuestadoService actualizar = new EncuestadoService();
                    strMensaje = actualizar.actualizar(usuario, encuestado);
                    okResult = true;

                }
                else if (Tipo == "Coordinador")
                {
                    CoordinadorService actualizar = new CoordinadorService();
                    strMensaje = actualizar.actualizar(usuario, admin);
                    okResult = true;
                }


            }
            else
            {


                if (usuario.TipoUsuario == "Encuestado")
                {
                    id = encuestado.IdEncuestado;
                    EncuestadoService nuevo = new EncuestadoService();
                    okResult = true;
                    strMensaje = nuevo.crear(usuario,encuestado);
                }

                else if (usuario.TipoUsuario == "Coordinador")
                {
                    id = admin.IdCoordinador;
                    CoordinadorService nuevo = new CoordinadorService();
                    okResult = true;
                    strMensaje = nuevo.crear(usuario, admin);

                }

            }
            return Json(new Response { IsSuccess = okResult, Message = strMensaje, Id = id }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Get(int Id)
        {
            string strMensaje = "No se encontro el usuario que desea editar";
            var objUsuario = db.Usuarios.Where(c => c.Id == Id).FirstOrDefault();
            if (objUsuario != null)
            {

                if (objUsuario.TipoUsuario== "Encuestado")
                {

                    var objPaciente = db.Encuestados.Where(c => c.IdUsuario == objUsuario.Id).FirstOrDefault();
                    EncuestadoService encuestado = new EncuestadoService();
                    var lista = encuestado.Obtener(objUsuario,objPaciente);

                    return Json(new Response { IsSuccess = true, Id = Id, Result = lista.ElementAt(0), Result2 = lista.ElementAt(1) }, JsonRequestBehavior.AllowGet);

                }

                else if (objUsuario.TipoUsuario == "Coordinador")
                {
                    var admin = db.Coordinadores.Where(c => c.IdUsuario == objUsuario.Id).FirstOrDefault();
                    CoordinadorService coordinador = new CoordinadorService();
                    var lista = coordinador.Obtener(objUsuario,admin);

                    return Json(new Response { IsSuccess = true, Id = Id, Result = lista.ElementAt(0), Result2 = lista.ElementAt(1)}, JsonRequestBehavior.AllowGet);

                }

            }
            return Json(new Response { IsSuccess = false, Message = strMensaje, Id = Id }, JsonRequestBehavior.AllowGet);
        }





        public ActionResult ListaUsuarios(string val, string valSearch, int? page)
        {
            ViewBag.CurrentSort = val;
            ViewBag.Buscar = valSearch;
            List<Usuarios> objProduct = new List<Usuarios>();
            if (string.IsNullOrEmpty(valSearch))
                objProduct = db.Usuarios.Where(c => true).OrderBy(c => c.Id).ToList();
            else
                objProduct = db.Usuarios.Where(c => true && (c.NombreUsuario.ToString().Contains(valSearch) || c.TipoUsuario.Contains(valSearch))).OrderBy(c => c.Id).ToList();

            if (val == "IdUsuario)" || string.IsNullOrEmpty(val))
            {
                val = "IdUsuario)";
                objProduct = objProduct.OrderBy(c => c.Id).ToList();
            }

            ViewBag.Order = val;
            int pageSize = 5;
            int pageNumber = page ?? 1;


            return PartialView(objProduct.ToPagedList(pageNumber, pageSize));
        }


        public ActionResult Users()
        {
            ViewBag.User = SessionHelper.GetUser();
            if (ViewBag.User == 0)
            {
                return RedirectToAction("Index", "Account");

            }
            else
            {
                return View();
            }

        }

        [HttpPost]
        public ActionResult Eliminar(int Id)
        {
            string strMensaje = "No se encontro el usuario que desea eliminar";
            bool okResult = false;
            var objUsu = db.Usuarios.Where(c => c.Id == Id).Select(n=>n.TipoUsuario).FirstOrDefault();
            if (objUsu != null)
            {
                if (objUsu == "Encuestado")
                {
                    var objProd = db.Encuestados.Where(c => c.IdUsuario == Id).FirstOrDefault();
                    var objUsu2 = db.Usuarios.Where(c => c.Id == objProd.IdUsuario).FirstOrDefault();
                    db.Encuestados.Remove(objProd);
                    db.Usuarios.Remove(objUsu2);
                    db.SaveChanges();
                    strMensaje = "Se elimino el usuario correctamente";
                    okResult = true;
                }
                else if (objUsu == "Coordinador")
                {
                    var objProd = db.Coordinadores.Where(c => c.IdUsuario == Id).FirstOrDefault();
                    var objUsu2 = db.Usuarios.Where(c => c.Id == objProd.IdUsuario).FirstOrDefault();
                    db.Coordinadores.Remove(objProd);
                    db.Usuarios.Remove(objUsu2);
                    db.SaveChanges();
                    strMensaje = "Se elimino el usuario correctamente";
                    okResult = true;

                }

            }
            return Json(new Response { IsSuccess = okResult, Message = strMensaje, Id = Id }, JsonRequestBehavior.AllowGet);
        }
    }


   
}