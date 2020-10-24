
using WebAppSurvey.Attributes;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppSurvey.Helpers;

namespace WebAppSurvey.Controllers
{

    [Autenticado]
    public class HomeController : Controller
    {
        // GET: Home
        [Authorize]
        public ActionResult Index()
        {

            SystemEncuestas db = new SystemEncuestas();

            if (HttpContext.Session["TipoUsuario"] == null)
            {
                return RedirectToAction("Login", "Acceso");

            }
            else
            {
                
                var objResult = db.Usuarios.ToList();
                return View(objResult);
            }


        }

        public ActionResult Cerrar()
        {
            SessionHelper.DestroyUserSession();
            HttpContext.Session["TipoUsuario"] = null;
            return RedirectToAction("Login","Acceso");
        }
    }
}