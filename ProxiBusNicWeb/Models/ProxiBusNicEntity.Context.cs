﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProxiBusNicWeb.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ProxiBusNicEntityContainer : DbContext
    {
        public ProxiBusNicEntityContainer()
            : base("name=ProxiBusNicEntityContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Parada> Paradas { get; set; }
        public virtual DbSet<Sugerencia> Sugerencias { get; set; }
        public virtual DbSet<BusParada> BusParadas { get; set; }
        public virtual DbSet<Bus> Buses { get; set; }
    }
}
