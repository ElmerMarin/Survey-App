using WebAppSurvey.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data;


namespace WebAppSurvey.Controllers
{
    public class AccesoController : Controller
    {
        private Entidades db = new Entidades();
        // GET: Acceso
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Usuario, string password)
        {
            string strMensaje = "El usuario y/o contraseña son incorrectos.";
            int id = 0;
            var objUsu = new Usuarios();

            try
            {
                objUsu = db.Usuarios.Where(c => c.NombreUsuario == Usuario).FirstOrDefault();
                var UsuID = db.Usuarios.Where(c => c.NombreUsuario == Usuario).Select(m => m.Id).FirstOrDefault();
                var UsuIDAdmin = db.Coordinadores.Where(c => c.IdUsuario ==UsuID).Select(m => m.IdCoordinador).FirstOrDefault();
                var UsuIDCoor =db.Coordinadores.Where(c => c.IdUsuario == UsuID).FirstOrDefault();
                var Nombre = db.Usuarios.Where(c => c.NombreUsuario == Usuario).Select(m => m.NombreUsuario).FirstOrDefault();
                var TipoUsuario = db.Usuarios.Where(u => u.NombreUsuario==Usuario && u.Contraseña==password).Select(m=>m.TipoUsuario).FirstOrDefault();

                if (TipoUsuario == "Administrador")
                {

                        id = -1;
                        SessionHelper.AddUserToSession(UsuID.ToString(),true);
                        SessionHelper.ActualizarSessionAdmin(objUsu, UsuIDCoor);
                        if (objUsu.Id == Convert.ToInt32(UsuID))
                        {
                            strMensaje = Url.Content("~/Home/Index");

                        }
                    
                }


            }
            catch (Exception)
            {

                strMensaje = "El usuario no existe";
            }



            return Json(new Response { IsSuccess = true, Message = strMensaje, Id = id }, JsonRequestBehavior.AllowGet);



        }

    }
            
}