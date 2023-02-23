using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QL_HDPHONGLAB.Models
{
    public class DsMuonTB
    {
        QL_HDPHONGLABEntities db = new QL_HDPHONGLABEntities();
        public int MAPM { get; set; }
        public string MATB { get; set; }
        public string TENTB { get; set; }
        public string HINHANH { get; set; }
        public int SOLUONG { get; set; }
        public DsMuonTB(string matb)
        {
            MATB = matb;
            THIETBI tb = db.THIETBIs.Single(n => n.MATB == matb);
            TENTB = tb.TENTB;
            HINHANH = tb.HINHANH;
            SOLUONG = 1;
        }
    }
}