﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace sistem.manajemen.ppic.dal
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.Infrastructure;
    using System.Linq;

    public partial class PPICEntities : DbContext
    {
        public PPICEntities()
            : base("name=PPICEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Login> Logins { get; set; }
        public DbSet<PAGE> PAGEs { get; set; }
        public DbSet<MST_BARANG_JADI> MST_BARANG_JADI { get; set; }
        public DbSet<MST_WILAYAH> MST_WILAYAH { get; set; }
        public DbSet<CHANGES_HISTORY> CHANGES_HISTORY { get; set; }
        public DbSet<TRN_SPB> TRN_SPB { get; set; }
        public DbSet<TRN_DO> TRN_DO { get; set; }
        public DbSet<MST_BAHAN_BAKU> MST_BAHAN_BAKU { get; set; }
        public DbSet<TRN_PEMAKAIAN_HASIL_PRODUKSI> TRN_PEMAKAIAN_HASIL_PRODUKSI { get; set; }
        public DbSet<TRN_PEMAKAIAN_HASIL_PRODUKSI_DETAILS> TRN_PEMAKAIAN_HASIL_PRODUKSI_DETAILS { get; set; }
        public DbSet<TRN_PENERIMAAN_SUPPLIER> TRN_PENERIMAAN_SUPPLIER { get; set; }
        public DbSet<TRN_PENERIMAAN_SUPPLIER_DETAILS> TRN_PENERIMAAN_SUPPLIER_DETAILS { get; set; }
        public DbSet<TRN_HASIL_PRODUKSI> TRN_HASIL_PRODUKSI { get; set; }
        public DbSet<DOCUMENT_NUMBER> DOCUMENT_NUMBER { get; set; }
        public DbSet<MST_KEMASAN> MST_KEMASAN { get; set; }
        public DbSet<MST_UOM> MST_UOM { get; set; }
        public DbSet<WORKING_HOURS> WORKING_HOURS { get; set; }
        public DbSet<SO_BARANG_JADI> SO_BARANG_JADI { get; set; }
        public DbSet<SO_BARANG_JADI_DETAILS> SO_BARANG_JADI_DETAILS { get; set; }
        public DbSet<TRN_PENGIRIMAN> TRN_PENGIRIMAN { get; set; }
        public DbSet<TRN_SURAT_PENGANTAR_BONGKAR_MUAT> TRN_SURAT_PENGANTAR_BONGKAR_MUAT { get; set; }
        public DbSet<TRN_MUTASI_BARANG> TRN_MUTASI_BARANG { get; set; }
    
        public virtual ObjectResult<SP_RealisasiHarian_Result> SP_RealisasiHarian(string date)
        {
            var dateParameter = date != null ?
                new ObjectParameter("Date", date) :
                new ObjectParameter("Date", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_RealisasiHarian_Result>("SP_RealisasiHarian", dateParameter);
        }
    }
}
