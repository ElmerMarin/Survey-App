﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SystemEncuestas : DbContext
    {
        public SystemEncuestas()
            : base("name=SystemEncuestas")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Areas> Areas { get; set; }
        public virtual DbSet<Categorias> Categorias { get; set; }
        public virtual DbSet<Coordinadores> Coordinadores { get; set; }
        public virtual DbSet<DetalleEncuesta> DetalleEncuesta { get; set; }
        public virtual DbSet<DetalleResultado> DetalleResultado { get; set; }
        public virtual DbSet<Encuestados> Encuestados { get; set; }
        public virtual DbSet<Encuestas> Encuestas { get; set; }
        public virtual DbSet<Preguntas> Preguntas { get; set; }
        public virtual DbSet<Respuestas> Respuestas { get; set; }
        public virtual DbSet<Resultados> Resultados { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
    }
}
