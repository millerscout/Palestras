﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Core.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Core.EntityClient;
    using System.Data.Entity.Infrastructure;
    
    public partial class CoreEntities : BaseContext
    {
       public static string ContainerName = "name=CoreEntities";
        public CoreEntities(EntityConnection conn)
            : base(ContainerName, conn)
        {
        }
        public CoreEntities()
            : base("name=CoreEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Contato> ContatoSet { get; set; }
    }
}