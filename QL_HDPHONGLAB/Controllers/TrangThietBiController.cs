using QL_HDPHONGLAB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_HDPHONGLAB.Controllers
{
    public class TrangThietBiController : Controller
    {
        private QL_HDPHONGLABEntities db;
        public TrangThietBiController()
        {
            db = new QL_HDPHONGLABEntities();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TTBPartial()
        {
            var listLoai = db.LOAITTBs.Take(3).OrderBy(loai => loai.MALOAI).ToList();
            return View(listLoai);
        }

        #region Trang Thiết BỊ Theo Loại
        public ActionResult HCTheoLoai(string maloai)
        {
            var listHC = db.HOACHATs.Where(n => n.MALOAI == maloai).OrderBy(n => n.LUONGTON).ToList();
            if (listHC.Count == 0)
            {
                ViewBag.HCTheoLoai = "Không có hoá chất nào thuộc loại này";
            }
            return View(listHC);
        }

        public ActionResult TBTheoLoai(string maloai)
        {
            var listTB = db.THIETBIs.Where(n => n.MALOAI == maloai).OrderBy(n => n.SLTON).ToList();
            if(listTB.Count == 0)
            {
                ViewBag.TBTheoLoai = "Không có thiết bị nào thuộc loại này";
            }
            return View(listTB);
        }

        public ActionResult DCTheoLoai(string maloai)
        {
            var listDC = db.DUNGCUs.Where(n => n.MALOAI == maloai).OrderBy(n => n.LUONGTON).ToList();
            if(listDC.Count == 0)
            {
                ViewBag.DCTheoLoai = "Không có dụng cụ nào thuộc loại này";
            }
            return View(listDC);
        }
        #endregion

    }
}