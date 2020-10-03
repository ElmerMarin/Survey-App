using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class SurveyContext:DbContext
    {
        public SurveyContext():base("DefaultConnection")
        {

        }

        public System.Data.Entity.DbSet<WebApplication.Models.Encuesta> Encuestas { get; set; }

        public System.Data.Entity.DbSet<WebApplication.Models.Pregunta> Preguntas { get; set; }

        public System.Data.Entity.DbSet<WebApplication.Models.Respuesta> Respuestas { get; set; }
    }
}