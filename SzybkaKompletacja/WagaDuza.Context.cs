﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KpInfohelp
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class KOMPLETACJAEntities : DbContext
    {
        public KOMPLETACJAEntities()
            : base("name=KOMPLETACJAEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<IHP_CENNIK> IHP_CENNIK { get; set; }
        public virtual DbSet<IHP_CENNIKHISTORIA> IHP_CENNIKHISTORIA { get; set; }
        public virtual DbSet<IHP_COMCONFIG> IHP_COMCONFIG { get; set; }
        public virtual DbSet<IHP_DANEFIRMY> IHP_DANEFIRMY { get; set; }
        public virtual DbSet<IHP_DEFCENY> IHP_DEFCENY { get; set; }
        public virtual DbSet<IHP_DOSTAWA> IHP_DOSTAWA { get; set; }
        public virtual DbSet<IHP_ERPCONNECTOR> IHP_ERPCONNECTOR { get; set; }
        public virtual DbSet<IHP_GRUPAPARAMETRY> IHP_GRUPAPARAMETRY { get; set; }
        public virtual DbSet<IHP_KIEROWCA> IHP_KIEROWCA { get; set; }
        public virtual DbSet<IHP_KONTRAHENT> IHP_KONTRAHENT { get; set; }
        public virtual DbSet<IHP_KONTRAHENT_ARCH> IHP_KONTRAHENT_ARCH { get; set; }
        public virtual DbSet<IHP_MIERNIK> IHP_MIERNIK { get; set; }
        public virtual DbSet<IHP_MODEL> IHP_MODEL { get; set; }
        public virtual DbSet<IHP_NUMERACJA> IHP_NUMERACJA { get; set; }
        public virtual DbSet<IHP_PRODUCENT> IHP_PRODUCENT { get; set; }
        public virtual DbSet<IHP_RODZAJKART> IHP_RODZAJKART { get; set; }
        public virtual DbSet<IHP_ROZCHDOST> IHP_ROZCHDOST { get; set; }
        public virtual DbSet<IHP_SAMOCHOD> IHP_SAMOCHOD { get; set; }
        public virtual DbSet<IHP_STAWKAVAT> IHP_STAWKAVAT { get; set; }
        public virtual DbSet<IHP_WAZENIE_USLUGA> IHP_WAZENIE_USLUGA { get; set; }
        public virtual DbSet<IHP_WYST_GRUPAPARAM> IHP_WYST_GRUPAPARAM { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<IHP_ZAM_USERS> IHP_ZAM_USERS { get; set; }
        public virtual DbSet<IHP_PARAMETRY> IHP_PARAMETRY { get; set; }
        public virtual DbSet<IHP_DEFSTATUS> IHP_DEFSTATUS { get; set; }
        public virtual DbSet<IHP_STATUSHISTORIA> IHP_STATUSHISTORIA { get; set; }
        public virtual DbSet<IHP_RODZGRUPKART> IHP_RODZGRUPKART { get; set; }
        public virtual DbSet<IHP_GRUPAKART> IHP_GRUPAKART { get; set; }
        public virtual DbSet<IHP_FOTO> IHP_FOTO { get; set; }
        public virtual DbSet<IHP_TRASY> IHP_TRASY { get; set; }
        public virtual DbSet<IHP_JZ> IHP_JZ { get; set; }
        public virtual DbSet<IHP_WYST_JZ> IHP_WYST_JZ { get; set; }
        public virtual DbSet<IHP_HARMONOGRAM_DZIENNY> IHP_HARMONOGRAM_DZIENNY { get; set; }
        public virtual DbSet<IHP_MASZYNA> IHP_MASZYNA { get; set; }
        public virtual DbSet<IHP_JM> IHP_JM { get; set; }
        public virtual DbSet<IHP_KARTOTEKA> IHP_KARTOTEKA { get; set; }
        public virtual DbSet<IHP_WYSTGRKART> IHP_WYSTGRKART { get; set; }
        public virtual DbSet<IHP_RODZAJDOK> IHP_RODZAJDOK { get; set; }
        public virtual DbSet<IHP_WYSTCECHKART> IHP_WYSTCECHKART { get; set; }
        public virtual DbSet<IHP_PRIORYTET> IHP_PRIORYTET { get; set; }
        public virtual DbSet<IHP_WYSTTRASAKONTRAH> IHP_WYSTTRASAKONTRAH { get; set; }
        public virtual DbSet<IHP_NAGLDOK> IHP_NAGLDOK { get; set; }
        public virtual DbSet<IHP_POZDOK> IHP_POZDOK { get; set; }
    }
}
