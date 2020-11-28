using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppSurvey.Helpers
{
    public class VerificarEncuesta
    {
        SystemEncuestas db = new SystemEncuestas();
        public int IDEncuesta(int idUsuario, int IDEncuesta) {
            var encuesta = false;
            //var idResultado = db.Resultados.Where(c => c.IdUsuario == idUsuario).Select(p => p.Id).FirstOrDefault();
            //var idResultado = db.DetalleResultado.Where(c => c.IdEncuesta ==IDEncuesta).Select(p => p.IdResultado).FirstOrDefault();
            var idUsuarios = db.Resultados.Where(c => c.IdUsuario == idUsuario).Select(p => p.Id).ToList();
            var Tipousuario = db.Usuarios.Where(c => c.Id == idUsuario).Select(p => p.TipoUsuario).FirstOrDefault();
            for (int i=0;i<idUsuarios.Count;i++) {
                int valor = idUsuarios[i];
                encuesta = db.DetalleResultado.Where(n => n.IdResultado ==valor).Select(n => n.IdEncuesta==IDEncuesta).FirstOrDefault();

                if (encuesta == true) {
                    break;
                }

            }

            if (Tipousuario == "Encuestado" && encuesta==true) { 
                return IDEncuesta;
            }

            return 0;
            
        
        }


    }
}