﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PastebookEntities
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PASTEBOOK_LIZBETHEntities : DbContext
    {
        public PASTEBOOK_LIZBETHEntities()
            : base("name=PASTEBOOK_LIZBETHEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<COMMENT> COMMENTs { get; set; }
        public virtual DbSet<FRIEND> FRIENDs { get; set; }
        public virtual DbSet<LIKE> LIKEs { get; set; }
        public virtual DbSet<NOTIFICATION> NOTIFICATIONs { get; set; }
        public virtual DbSet<POST> POSTs { get; set; }
        public virtual DbSet<REF_COUNTRY> REF_COUNTRY { get; set; }
        public virtual DbSet<USER> USERs { get; set; }
    }
}