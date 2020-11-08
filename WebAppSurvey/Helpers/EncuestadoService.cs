using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebAppSurvey.Models;

namespace WebAppSurvey.Helpers
{

    public class EncuestadoService:Encuestados 
    {
        SystemEncuestas db = new SystemEncuestas();
        public string actualizar(Usuario usuario, Encuestado encuestado)
        {
            string strMensaje = "Error";
            Usuarios UpdateEncuestado = db.Usuarios.Where(c => c.Id == usuario.Id).FirstOrDefault();
            string strPass = CryproHelper.ComputeHash(usuario.Contraseña, CryproHelper.Supported_HA.SHA512, null);
            Encuestados objUsuID = db.Encuestados.Where(c => c.IdUsuario == UpdateEncuestado.Id).FirstOrDefault();
            if (objUsuID != null)
            {
                UpdateEncuestado.NombreUsuario = usuario.NombreUsuario;
                UpdateEncuestado.Contraseña = strPass;
                UpdateEncuestado.Correo = usuario.Correo;
                db.Entry(UpdateEncuestado).State = EntityState.Modified;
                db.SaveChanges();

                objUsuID.Dni = encuestado.Dni;
                objUsuID.Nombres = encuestado.Nombres;
                objUsuID.ApellidoPaterno = encuestado.ApellidoPaterno;
                objUsuID.ApellidoMaterno = encuestado.ApellidoMaterno;
                objUsuID.Direccion = encuestado.Direccion;
                objUsuID.Edad = encuestado.Edad;
                objUsuID.Sexo = encuestado.Sexo;
                objUsuID.Telefono = encuestado.Telefono;
                db.Entry(objUsuID).State = EntityState.Modified;
                db.SaveChanges();

                strMensaje = "Se actualizaron sus datos";
            }

            return strMensaje;

        }

        public string crear(Usuario usuario, Encuestado encuestado)
        {
            string strMensaje = "Se agrego el Encuestado correctamente";
            
            try
            {
                string strPass = CryproHelper.ComputeHash(usuario.Contraseña, CryproHelper.Supported_HA.SHA512, null);
                var objUsuarios =(new Usuarios
                {
                    TipoUsuario = "Encuestado",
                    NombreUsuario = usuario.NombreUsuario,
                    Contraseña = strPass,
                    Correo = usuario.Correo

                });
                db.Usuarios.Add(objUsuarios);
                db.SaveChanges();

                var objUsuID = db.Usuarios.Where(c => c.NombreUsuario == objUsuarios.NombreUsuario).Select(b=>b.Id).FirstOrDefault();

                var objUsuNew = (new Encuestados
                {
                    Dni = encuestado.Dni,
                    Nombres = encuestado.Nombres,
                    ApellidoPaterno = encuestado.ApellidoPaterno,
                    ApellidoMaterno = encuestado.ApellidoMaterno,
                    Direccion = encuestado.Direccion,
                    Edad = encuestado.Edad,
                    Sexo = encuestado.Sexo,
                    Telefono = encuestado.Telefono,
                    IdUsuario = objUsuID
                });
            }
            catch (Exception e)
            {

                strMensaje = e.Message;
            }
            return strMensaje;

        }

        public List<object> Obtener(Usuarios usuario, Encuestados encuestado)
        {

            List<object> objetos = new List<object>();
            Encuestado Psico = new Encuestado
            {
                IdEncuestado = encuestado.IdEncuestado,
                Dni = encuestado.Dni,
                Nombres = encuestado.Nombres,
                ApellidoPaterno = encuestado.ApellidoPaterno,
                ApellidoMaterno = encuestado.ApellidoMaterno,
                Direccion = encuestado.Direccion,
                Edad = encuestado.Edad,
                Sexo = encuestado.Sexo,
                Telefono = encuestado.Telefono,
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