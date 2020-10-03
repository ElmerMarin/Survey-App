using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class Respuesta
    {
        [Key]
        public int RespuestaId { get; set; }

        public string Descripcion { get; set; }

        public bool Estado { get; set; }

        public int PreguntaId { get; set; }

        public virtual Pregunta Pregunta { get; set; }

    }
}