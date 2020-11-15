using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebAppSurvey.Models;

namespace WebAppSurvey.Helpers
{
    public class CoordinadorService
    {
        SystemEncuestas db = new SystemEncuestas();
        public string actualizar(Usuario usuario, Coordinador coordinador)
        {
            string strMensaje = "Error";
            Usuarios UpdateEncuestado = db.Usuarios.Where(c => c.Id == usuario.Id).FirstOrDefault();
            string strPass = CryproHelper.ComputeHash(usuario.Contraseña, CryproHelper.Supported_HA.SHA512, null);
            Coordinadores objUsuID = db.Coordinadores.Where(c => c.IdUsuario == UpdateEncuestado.Id).FirstOrDefault();
            if (objUsuID != null)
            {
                UpdateEncuestado.NombreUsuario = usuario.NombreUsuario;
                UpdateEncuestado.Contraseña = strPass;
                UpdateEncuestado.Correo = usuario.Correo;
                UpdateEncuestado.TipoUsuario = usuario.TipoUsuario;
                UpdateEncuestado.Token = "";
                db.Entry(UpdateEncuestado).State = EntityState.Modified;
                db.SaveChanges();

                objUsuID.Dni = coordinador.Dni;
                objUsuID.Nombres = coordinador.Nombres;
                objUsuID.ApellidoPaterno = coordinador.ApellidoPaterno;
                objUsuID.ApellidoMaterno = coordinador.ApellidoMaterno;
                objUsuID.Direccion = coordinador.Direccion;
                objUsuID.Edad = coordinador.Edad;
                objUsuID.Sexo = coordinador.Sexo;
                objUsuID.Telefono = coordinador.Telefono;
                db.Entry(objUsuID).State = EntityState.Modified;
                db.SaveChanges();

                strMensaje = "Se actualizaron sus datos";
            }

            return strMensaje;

        }

        public string crear(Usuario usuario, Coordinador coordinador)
        {
            string strMensaje = "Se agrego el Coordinador correctamente";

            try
            {
                string strPass = CryproHelper.ComputeHash(usuario.Contraseña, CryproHelper.Supported_HA.SHA512, null);
                var objUsuarios = (new Usuarios
                {
                    TipoUsuario = "Coordinador",
                    NombreUsuario = usuario.NombreUsuario,
                    Contraseña = strPass,
                    Correo = usuario.Correo,
                    Token = ""

                });
                db.Usuarios.Add(objUsuarios);
                db.SaveChanges();

                var objUsuID = db.Usuarios.Where(c => c.NombreUsuario == objUsuarios.NombreUsuario).Select(b => b.Id).FirstOrDefault();

                var objUsuNew = (new Coordinadores
                {
                    Dni = coordinador.Dni,
                    Nombres = coordinador.Nombres,
                    ApellidoPaterno = coordinador.ApellidoPaterno,
                    ApellidoMaterno = coordinador.ApellidoMaterno,
                    Direccion = coordinador.Direccion,
                    Edad = coordinador.Edad,
                    Sexo = coordinador.Sexo,
                    Telefono = coordinador.Telefono,
                    IdUsuario = objUsuID
                });
                db.Coordinadores.Add(objUsuNew);
                db.SaveChanges();
            }
            catch (Exception e)
            {

                strMensaje = e.Message;
            }
            return strMensaje;

        }

        public List<object> Obtener(Usuarios usuario, Coordinadores coordinador)
        {

            List<object> objetos = new List<object>();
            Coordinador Psico = new Coordinador
            {
                IdCoordinador = coordinador.IdCoordinador,
                Dni = coordinador.Dni,
                Nombres = coordinador.Nombres,
                ApellidoPaterno = coordinador.ApellidoPaterno,
                ApellidoMaterno = coordinador.ApellidoMaterno,
                Direccion = coordinador.Direccion,
                Edad = coordinador.Edad,
                Sexo = coordinador.Sexo,
                Telefono = coordinador.Telefono,
                
            };

            objetos.Add(Psico);
            Usuario usuarios = new Usuario
            {
                Id = usuario.Id,
                NombreUsuario = usuario.NombreUsuario,
                Contraseña = usuario.Contraseña,
                TipoUsuario = usuario.TipoUsuario,
                Correo = usuario.Correo

            };
            objetos.Add(usuarios);
            return objetos;
        }

    }
}