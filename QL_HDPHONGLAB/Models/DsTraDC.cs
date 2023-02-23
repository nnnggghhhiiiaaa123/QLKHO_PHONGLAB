using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QL_HDPHONGLAB.Models
{
    public class DsTraDC
    {
        QL_HDPHONGLABEntities db = new QL_HDPHONGLABEntities();
        public string MADC { get; set; }
        public string TENDC { get; set; }
        public string HINHANH { get; set; }
        public int SOLUONG { get; set; }
        public DsTraDC(string madc)
        {
            MADC = madc;
            DUNGCU dc = db.DUNGCUs.Single(n => n.MADC == madc);
            TENDC = dc.TENDC;
            HINHANH = dc.HINHANH;
            SOLUONG = 1;
        }
    }
}