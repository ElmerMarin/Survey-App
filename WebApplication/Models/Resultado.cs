using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class Resultado
    {
        [Key]
        public int ResultadoId { get; set; }

        public int EncuestaId { get; set; }

        public int UsuarioId { get; set; }

        public virtual Encuesta Encuesta { get; set; }

        public virtual Usuario usuario { get; set; }

    }
}