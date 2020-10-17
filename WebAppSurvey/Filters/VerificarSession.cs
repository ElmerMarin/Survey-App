using Model;
using System;
using System.Web;
using System.Web.Mvc;
using WebAppSurvey.Controllers;
using WebAppSurvey.Models;

namespace WebAppSurvey.Filters
{
    public class VerificarSession : ActionFilterAttribute
    {
        private Usuarios usuarios;

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            try
            {
                base.OnActionExecuted(filterContext);

                usuarios = (Usuarios)HttpContext.Current.Session["User"];

                if (usuarios == null)
                {

                    if (filterContext.Controller is AccesoController == false)
                    {
                        filterContext.HttpContext.Response.Redirect("/Acceso/Login");
                    }


                }
            }
            catch (Exception)
            {

                filterContext.Result = new RedirectResult("~/Acceso/Login");
            }



        }


    }
}