using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QL_HDPHONGLAB.Models
{
    public class LABRegisterViewModel
    {
        public int MADK { get; set; }
        public string HOTEN_GV { get; set; }
        public string DIACHI_GV { get; set; }
        public string SDT_GV { get; set; }
        public DateTime NGAY_DKPH { get; set; }
        public DateTime NGAY_TRAPH { get; set; }
        public string SOPHONG { get; set; } 
        public int SOLUONG { get; set; }
        public int SONGAY { get; internal set; }
    }
}