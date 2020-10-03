using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }

        public string Nombres { get; set; }

        public string ApellidoPaterno { get; set; }

        public string ApellidoMaterno { get; set; }

        public virtual ICollection<Resultado> Resultados { get; set; }

    }
}