using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class Encuesta
    {
        [Key]
        public int EncuestaId { get; set; }

        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        public bool Estado { get; set; }
        
        public virtual ICollection<Pregunta> Preguntas { get; set; }

        public virtual ICollection<Resultado> Resultados { get; set; }
    }
}