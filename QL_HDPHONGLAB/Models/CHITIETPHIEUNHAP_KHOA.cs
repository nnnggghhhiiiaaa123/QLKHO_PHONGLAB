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
    
    public partial class CHITIETPHIEUNHAP_KHOA
    {
        public int MAPN_KHOA { get; set; }
        public string MAHC_CNTP { get; set; }
        public string MATB_CNTP { get; set; }
        public string MADC_CNTP { get; set; }
        public Nullable<int> SOLUONGNHAP { get; set; }
    
        public virtual HOACHAT_CNTP HOACHAT_CNTP { get; set; }
        public virtual DUNGCU_CNTP DUNGCU_CNTP { get; set; }
        public virtual THIETBI_CNTP THIETBI_CNTP { get; set; }
    }
}
