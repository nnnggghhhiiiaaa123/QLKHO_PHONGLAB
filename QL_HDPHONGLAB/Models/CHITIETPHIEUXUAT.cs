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
    
    public partial class CHITIETPHIEUXUAT
    {
        public int MAPX { get; set; }
        public string MAHC { get; set; }
        public string MAHC_CNTP { get; set; }
        public string MATB { get; set; }
        public string MADC { get; set; }
        public Nullable<int> SOLUONGXUAT { get; set; }
        public string TU { get; set; }
        public string DEN { get; set; }
    
        public virtual DUNGCU DUNGCU { get; set; }
        public virtual HOACHAT HOACHAT { get; set; }
        public virtual HOACHAT_CNTP HOACHAT_CNTP { get; set; }
        public virtual PHIEUXUAT PHIEUXUAT { get; set; }
        public virtual THIETBI THIETBI { get; set; }
    }
}
