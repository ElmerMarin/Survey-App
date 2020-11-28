using Model;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using WebAppSurvey.Helpers;
using WebAppSurvey.Models;


namespace WebAppSurvey.Controllers
{
    public class AccesoController : Controller
    {
        private SystemEncuestas db = new SystemEncuestas();
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
                var UsuContrasena = db.Usuarios.Where(c => c.NombreUsuario == Usuario).Select(m => m.Contraseña).FirstOrDefault();
                var UsuIDCoor = db.Coordinadores.Where(c => c.IdUsuario == UsuID).FirstOrDefault();
                var UsuIDEncuestado = db.Encuestados.Where(c => c.IdUsuario == UsuID).FirstOrDefault();
                var Nombre = db.Usuarios.Where(c => c.NombreUsuario == Usuario).Select(m => m.NombreUsuario).FirstOrDefault();
                var TipoUsuario = db.Usuarios.Where(u => u.NombreUsuario == Usuario).Select(m => m.TipoUsuario).FirstOrDefault();

                if (TipoUsuario == "Encuestado")
                {
                    if (CryproHelper.Confirm(password, objUsu.Contraseña, CryproHelper.Supported_HA.SHA512))
                    {
                        id = -1;
                        SessionHelper.AddUserToSession(objUsu.Id.ToString(), true);
                        SessionHelper.ActualizarSessionEncuestadosUser(objUsu, UsuIDEncuestado);
                        if (objUsu.Id == UsuID)
                        {
                            strMensaje = Url.Content("~/Home/Index");

                        }
                    }
                }


                else if (TipoUsuario == "Coordinador")
                {

                    if (objUsu.Contraseña == password)
                    {
                        id = -1;
                        SessionHelper.AddUserToSession(objUsu.Id.ToString(), true);
                        SessionHelper.ActualizarSessionAdmin(objUsu, UsuIDCoor);
                        if (objUsu.Id == Convert.ToInt32(UsuID))
                        {
                            strMensaje = Url.Content("~/Home/Index");

                        }
                    }
                    else
                    {
                        try
                        {
                            if (CryproHelper.Confirm(password, UsuContrasena, CryproHelper.Supported_HA.SHA512))
                            {
                                id = -1;
                                SessionHelper.AddUserToSession(objUsu.Id.ToString(), true);
                                SessionHelper.ActualizarSessionAdmin(objUsu, UsuIDCoor);
                                if (objUsu.Id == UsuID)
                                {
                                    strMensaje = Url.Content("~/Home/Index");

                                }
                            }

                        }
                        catch (Exception)
                        {

                            return Json(new Response { IsSuccess = true, Message = strMensaje, Id = id }, JsonRequestBehavior.AllowGet);
                        }

                    }

                }


            }
            catch (Exception)
            {

                strMensaje = "El usuario no existe";
            }



            return Json(new Response { IsSuccess = true, Message = strMensaje, Id = id }, JsonRequestBehavior.AllowGet);



        }



        public ActionResult Registrarme()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrarme(string Dni, string Nombres, string APaterno, string AMaterno, string Direccion, int edad, string sexo, string correo, string telefono, string contraseña, string NombreUsu)
        {
            string TipoUsu = "Encuestado";
            var objUsuNom = db.Usuarios.Where(c => c.NombreUsuario == NombreUsu).FirstOrDefault();
            var objUsuNom2 = db.Usuarios.Where(c => c.Correo == correo).FirstOrDefault();
            string strMensaje = "";
            int id = 0;
            if (objUsuNom != null || objUsuNom2 != null)
            {
                strMensaje = "El usuario ya existe en nuestra base de datos, intente recuperar su cuenta para cambiar su contraseña.";
            }
            else
            {
                string strPass = CryproHelper.ComputeHash(contraseña, CryproHelper.Supported_HA.SHA512, null);
                var objUsuarios =(new Usuarios
                {
                    TipoUsuario = TipoUsu,
                    NombreUsuario = NombreUsu,
                    Contraseña = strPass,
                    Correo = correo,
                    Token=""

                });
                db.Usuarios.Add(objUsuarios);
                db.SaveChanges();
                var objUsuID = db.Usuarios.Where(c => c.NombreUsuario == NombreUsu).Select(m=>m.Id).FirstOrDefault();
                var objUsuNew = new Encuestados();
                if (objUsuarios != null)
                {
                    if (TipoUsu == "Encuestado")
                    {
                        try
                        {
                            objUsuNew =(new Encuestados
                            {
                                Dni = Dni,
                                Nombres = Nombres,
                                ApellidoPaterno = APaterno,
                                ApellidoMaterno = AMaterno,
                                Direccion = Direccion,
                                Edad = edad,
                                Sexo = sexo,
                                Telefono = telefono,
                                IdUsuario = objUsuID
                            });
                            db.Encuestados.Add(objUsuNew);
                            db.SaveChanges();
                        }
                        catch (Exception e)
                        {
                            strMensaje = e.Message;
                            return Json(new Response { IsSuccess = true, Message = strMensaje, Id = id }, JsonRequestBehavior.AllowGet);
                        }

                    }
                    if (objUsuNew != null)
                    {
                        var baseAddress = new Uri(ToolsHelper.UrlOriginal(Request));
                        string Mensaje = "Gracias por inscribirse al sistema de Encuestas, puede entrar con el usuario " +
                            "y contraseña registrada. <a href='" + baseAddress + "'>Survey</a>";
                        ToolsHelper.SendMail(correo, "Gracias por registrarte ", Mensaje);
                        strMensaje = "Te registraste correctamente, ya puedes entrar al sistema.";
                        strMensaje = Url.Content("~/Home/Index");
                        return Json(new Response { IsSuccess = true, Message = strMensaje, Id = -1 }, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {
                        strMensaje = "Disculpe las molestias, por el momento no podemos conectarnos con el servidor, intentelo nuevamente.";
                    }
                }
                else
                {
                    strMensaje = "Disculpe las molestias, por el momento no podemos conectarnos con el servidor, intentelo nuevamente.";
                }
            }
            return Json(new Response { IsSuccess = true, Message = strMensaje, Id = id }, JsonRequestBehavior.AllowGet);
        }



        public ActionResult RecuperarCuenta()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RecuperarCuenta(string CorreoElectronico)
        {
            
            var objUsu = db.Usuarios.Where(c => c.Correo == CorreoElectronico).FirstOrDefault();
            int id = 0;
            string strMensaje = "El correo no se encuentra registrado.";
            if (objUsu != null)
            {
                string strToken = objUsu.Id.ToString() + objUsu.Correo;
                string strTknAjax = CryproHelper.ComputeHash(strToken, CryproHelper.Supported_HA.SHA512, null);
                objUsu.Token = Server.UrlEncode(strTknAjax);
                db.Entry(objUsu).State = EntityState.Modified;
                db.SaveChanges();

                var baseAddress = ToolsHelper.UrlOriginal(Request) + "/Acceso/ResetPass/?tkn=" + objUsu.Token;
                string Mensaje = "Para restaurar tu cuenta , entra a la siguiente liga y crea una nueva contraseña. <br/><br/> <a href='" + baseAddress + "'>recuperar cuenta</a>";
                ToolsHelper.SendMail(CorreoElectronico, "Recuperar ", Mensaje);
                strMensaje = "Se envío un correo con la información requerida para recuperar su cuenta.";
            }
            return Json(new Response { IsSuccess = true, Message = strMensaje, Id = id }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ResetPass(string tkn)
        {
            if (!string.IsNullOrEmpty(tkn))
            {
               
                tkn = Server.UrlEncode(tkn);
                ViewBag.tkn = tkn;
                var objUsu = db.Usuarios.Where(c => c.Token == tkn);
                if (objUsu != null)
                {
                    return View();
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult ResetPass(string Password, string tkn)
        {

            var objUsu = db.Usuarios.Where(c => c.Token == tkn).FirstOrDefault();
            string strMensaje = "";
            int id = 0;
            if (objUsu != null)
            {
                string strPass = CryproHelper.ComputeHash(Password, CryproHelper.Supported_HA.SHA512, null);
                objUsu.Contraseña = strPass;
                objUsu.Token = "";
                db.Entry(objUsu).State = EntityState.Modified;
                db.SaveChanges();
                strMensaje = "Se actualizó la contraseña correctamente ";
            }
            else
            {
                strMensaje = "El token se encuentra vencido, necesita recuperar nuevamente su cuenta.";
            }
            return Json(new Response { IsSuccess = true, Message = strMensaje, Id = id }, JsonRequestBehavior.AllowGet);
        }


    }

}