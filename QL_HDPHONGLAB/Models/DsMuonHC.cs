using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QL_HDPHONGLAB.Models
{
    public class DsMuonHC
    {
        QL_HDPHONGLABEntities db = new QL_HDPHONGLABEntities();
        public int MAPM { get; set; }
        public string MAHC { get; set; }
        public string TENHC { get; set; }
        public string HINHANH { get; set; }
        public int SOLUONG { get; set; }
        public DsMuonHC(string mahc)
        {
            MAHC = mahc;
            HOACHAT hc = db.HOACHATs.Single(n => n.MAHC == mahc);
            TENHC = hc.TENHC;
            HINHANH = hc.HINHANH;
            SOLUONG = 1;
        }
    }
}