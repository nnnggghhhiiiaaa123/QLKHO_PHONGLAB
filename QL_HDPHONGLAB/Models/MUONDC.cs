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
    
    public partial class MUONDC
    {
        public int MAMDC { get; set; }
        public string MADC { get; set; }
        public string TENDC { get; set; }
        public string HINHANH { get; set; }
        public Nullable<int> SOLUONG { get; set; }
    
        public virtual DUNGCU DUNGCU { get; set; }
    }
}
