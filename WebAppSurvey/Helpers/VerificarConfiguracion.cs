using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppSurvey.Helpers
{
    public class VerificarConfiguracion
    {
        SystemEncuestas db = new SystemEncuestas();
        public int VerificarFecha(int IDEncuesta)
        {   
            string fechaActual = "";
            fechaActual = DateTime.Now.ToString("dd/MM/yyyy");
            var titulo = db.Encuestas.Where(c => c.Id ==IDEncuesta).Select(p => p.Titulo).FirstOrDefault();
            var FechaFinal = db.DetalleEncuesta.Where(c => c.Encuestas.Titulo==titulo && c.Estado.Equals("Activo")).Select(p => p.Fechafinal).FirstOrDefault();
            var Fechacorta = FechaFinal.ToString("dd/MM/yyyy");

            if (fechaActual.Equals(Fechacorta))
            { 
                return IDEncuesta;
            }
            return 0;
        }
    }
}