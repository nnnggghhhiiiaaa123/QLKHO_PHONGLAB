//------------------------------------------------------------------------------
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
    using System.Collections.Generic;
    
    public partial class HOACHAT_CNTP
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HOACHAT_CNTP()
        {
            this.CHITIETPHIEUNHAP_KHOA = new HashSet<CHITIETPHIEUNHAP_KHOA>();
            this.CHITIETPHIEUXUATs = new HashSet<CHITIETPHIEUXUAT>();
            this.PHIEUNHAP_KHOA = new HashSet<PHIEUNHAP_KHOA>();
        }
    
        public string MAHC_CNTP { get; set; }
        public string TENHC_CNTP { get; set; }
        public Nullable<int> MALHC { get; set; }
        public Nullable<int> THONGSO { get; set; }
        public string CASNO { get; set; }
        public string DONVI { get; set; }
        public Nullable<int> LUONGNHAP { get; set; }
        public Nullable<int> LUONGTON { get; set; }
        public Nullable<int> LUONGTHANHLY { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETPHIEUNHAP_KHOA> CHITIETPHIEUNHAP_KHOA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETPHIEUXUAT> CHITIETPHIEUXUATs { get; set; }
        public virtual HOACHAT HOACHAT { get; set; }
        public virtual LOAIHOACHAT LOAIHOACHAT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHIEUNHAP_KHOA> PHIEUNHAP_KHOA { get; set; }
    }
}
