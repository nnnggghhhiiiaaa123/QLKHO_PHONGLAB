﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QL_HDPHONGLAB.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class QL_HDPHONGLABEntities : DbContext
    {
        public QL_HDPHONGLABEntities()
            : base("name=QL_HDPHONGLABEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BANGGIA> BANGGIAs { get; set; }
        public virtual DbSet<CHITIETKIEMDUYET> CHITIETKIEMDUYETs { get; set; }
        public virtual DbSet<CHITIETPHIEUNHAP> CHITIETPHIEUNHAPs { get; set; }
        public virtual DbSet<CHITIETPHIEUNHAP_KHOA> CHITIETPHIEUNHAP_KHOA { get; set; }
        public virtual DbSet<CHITIETPHIEUTHANHLY> CHITIETPHIEUTHANHLies { get; set; }
        public virtual DbSet<CHITIETPHIEUXUAT> CHITIETPHIEUXUATs { get; set; }
        public virtual DbSet<CT_DUNGCU> CT_DUNGCU { get; set; }
        public virtual DbSet<CT_HOACHAT> CT_HOACHAT { get; set; }
        public virtual DbSet<CT_PHIEUMUON> CT_PHIEUMUON { get; set; }
        public virtual DbSet<CT_PHIEUTRA> CT_PHIEUTRA { get; set; }
        public virtual DbSet<CT_THIETBI> CT_THIETBI { get; set; }
        public virtual DbSet<DANGKYPHONG> DANGKYPHONGs { get; set; }
        public virtual DbSet<DOIMATKHAU> DOIMATKHAUs { get; set; }
        public virtual DbSet<DUNGCU> DUNGCUs { get; set; }
        public virtual DbSet<DUNGCU_CNTP> DUNGCU_CNTP { get; set; }
        public virtual DbSet<DUTRUDUNGCU> DUTRUDUNGCUs { get; set; }
        public virtual DbSet<DUTRUHOACHAT> DUTRUHOACHATs { get; set; }
        public virtual DbSet<DUTRUTHIETBI> DUTRUTHIETBIs { get; set; }
        public virtual DbSet<GIANGVIEN> GIANGVIENs { get; set; }
        public virtual DbSet<HOACHAT> HOACHATs { get; set; }
        public virtual DbSet<HOACHAT_CNTP> HOACHAT_CNTP { get; set; }
        public virtual DbSet<HOANTRA> HOANTRAs { get; set; }
        public virtual DbSet<LISTTHANHLY> LISTTHANHLies { get; set; }
        public virtual DbSet<LOAIHOACHAT> LOAIHOACHATs { get; set; }
        public virtual DbSet<LOAIPHONGLAB> LOAIPHONGLABs { get; set; }
        public virtual DbSet<LOAITTB> LOAITTBs { get; set; }
        public virtual DbSet<MUONDC> MUONDCs { get; set; }
        public virtual DbSet<MUONHC> MUONHCs { get; set; }
        public virtual DbSet<MUONTB> MUONTBs { get; set; }
        public virtual DbSet<NCC> NCCs { get; set; }
        public virtual DbSet<NGUOIDUNG> NGUOIDUNGs { get; set; }
        public virtual DbSet<NSX> NSXes { get; set; }
        public virtual DbSet<PHANQUYEN> PHANQUYENs { get; set; }
        public virtual DbSet<PHIEUKIEMDUYET> PHIEUKIEMDUYETs { get; set; }
        public virtual DbSet<PHIEUMUON> PHIEUMUONs { get; set; }
        public virtual DbSet<PHIEUNHAP> PHIEUNHAPs { get; set; }
        public virtual DbSet<PHIEUNHAP_KHOA> PHIEUNHAP_KHOA { get; set; }
        public virtual DbSet<PHIEUTHANHLY> PHIEUTHANHLies { get; set; }
        public virtual DbSet<PHIEUTRA> PHIEUTRAs { get; set; }
        public virtual DbSet<PHIEUXUAT> PHIEUXUATs { get; set; }
        public virtual DbSet<PHIEUXUATPHONGLAB> PHIEUXUATPHONGLABs { get; set; }
        public virtual DbSet<PHONGBAN> PHONGBANs { get; set; }
        public virtual DbSet<PHONGLAB> PHONGLABs { get; set; }
        public virtual DbSet<SINHVIEN> SINHVIENs { get; set; }
        public virtual DbSet<SUKIEN> SUKIENs { get; set; }
        public virtual DbSet<THIETBI> THIETBIs { get; set; }
        public virtual DbSet<THIETBI_CNTP> THIETBI_CNTP { get; set; }
        public virtual DbSet<TRADC> TRADCs { get; set; }
        public virtual DbSet<TRAHC> TRAHCs { get; set; }
        public virtual DbSet<TRATB> TRATBs { get; set; }
        public virtual DbSet<TTBTRA> TTBTRAs { get; set; }
        public virtual DbSet<TTDANGKY> TTDANGKies { get; set; }
    }
}
