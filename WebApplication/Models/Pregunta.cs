using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class Pregunta
    {
        [Key]
        public int PreguntaId { get; set; }

        public string Descripcion { get; set; }

        public bool Estado { get; set; }

        public int EncuestaId { get; set; }

        public virtual Encuesta Encuesta { get; set; }

        public virtual ICollection<Respuesta> Respuestas { get; set; }

    }
}