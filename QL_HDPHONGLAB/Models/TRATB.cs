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
    
    public partial class TRATB
    {
        public int MATTB { get; set; }
        public string MATB { get; set; }
        public string TENTB { get; set; }
        public string HINHANH { get; set; }
        public Nullable<int> SOLUONG { get; set; }
    
        public virtual THIETBI THIETBI { get; set; }
    }
}
