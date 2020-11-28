using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppSurvey.Helpers
{
    public class ContadorDeRecursos
    {
       
        public static int AreasTotales()
        {
            SystemEncuestas db = new SystemEncuestas();
            var areas =db.Areas.Count();
            return areas;

        }

        public static int CategoriasTotales()
        {
            SystemEncuestas db = new SystemEncuestas();
            var categorias = db.Categorias.Count();
            return categorias;

        }

        public static int EncuestasTotales()
        {
            SystemEncuestas db = new SystemEncuestas();
            var encuestas = db.Encuestas.Count();
            return encuestas;

        }

        public static int EncuestadosTotales()
        {
            SystemEncuestas db = new SystemEncuestas();
            var encuestados = db.Encuestados.Count();
            return encuestados;

        }

        public static int EncuestasTotalesMes(int mes)
        {
            SystemEncuestas db = new SystemEncuestas();
            var fechayear = DateTime.Now.Year;
            var fechafinal = "";
            var fechames = "0";
            if (mes >= 1 && mes <= 9)
            {

                fechames = "0" + mes;
                fechafinal = fechayear + "-" + fechames;

            }

            else if (mes >= 10 && mes <= 12)
            {
                fechames = mes.ToString();
                fechafinal = fechayear + "-" + fechames;
            }


            var cantidadDeEncuestas = db.Resultados.Where(c => c.Fecha.ToString().Substring(0, 7).Equals(fechafinal)).Count();

            return cantidadDeEncuestas;

        }

        public static int EncuestadosTotalesSexo(string sexo)
        {
            SystemEncuestas db = new SystemEncuestas();
            var cantidadGenero =db.Encuestados.Where(c => c.Sexo.Equals(sexo)).Count();
            return cantidadGenero;

        }


    }
}