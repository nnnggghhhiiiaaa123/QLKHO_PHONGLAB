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
    
    public partial class PHIEUMUON
    {
        public int MAPM { get; set; }
        public System.DateTime NGAYMUON { get; set; }
        public System.DateTime NGAYTRA { get; set; }
        public string NOIDUNG { get; set; }
        public Nullable<bool> CHAPNHAN { get; set; }
        public Nullable<int> TINHTRANG { get; set; }
        public Nullable<int> MAND { get; set; }
    
        public virtual NGUOIDUNG NGUOIDUNG { get; set; }
    }
}