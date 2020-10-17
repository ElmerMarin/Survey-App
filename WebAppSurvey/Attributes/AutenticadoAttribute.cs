using Model;
using WebAppSurvey.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;


namespace WebAppSurvey.Attributes
{
    public class AutenticadoAttribute : ActionFilterAttribute
    {
        private SystemEncuestas db = new SystemEncuestas();
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (!SessionHelper.ExistUserInSession())
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Acceso",
                    action = "Login"
                }));
            }
            else
            {
                
                int idUser = SessionHelper.GetUser();
                var Usuario = db.Usuarios.Where(c => c.Id == idUser).FirstOrDefault();
                if (Usuario != null)
                {
                    SessionHelper.ActualizarSession(Usuario);
                }
            }
        }
    }


    public class CloseSesionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            SessionHelper.DestroyUserSession();

            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
             {
                    controller = "Acceso",
                    action = "Login"
             }));
            
        }
    }

    public class NoLoginAttribute : ActionFilterAttribute
    {
        private SystemEncuestas db = new SystemEncuestas();
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (SessionHelper.ExistUserInSession())
            {
                
                int idUser = SessionHelper.GetUser();
                var Usuario = db.Usuarios.Where(c => c.Id == idUser).FirstOrDefault();
                var UsuarioID = db.Usuarios.Where(c => c.Id == idUser).Select(m=>m.Id).FirstOrDefault();
                if (Usuario != null)
                {
                    SessionHelper.ActualizarSession(Usuario);
                    if (Usuario.Id == UsuarioID)
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                        {
                            controller = "Admin",
                            action = "Index"
                        }));
                    }
                }

            }
        }
    }

    public class NoCache : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            filterContext.HttpContext.Response.Cache.SetValidUntilExpires(false);
            filterContext.HttpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            filterContext.HttpContext.Response.Cache.SetNoStore();

            base.OnResultExecuting(filterContext);
        }
    }


}