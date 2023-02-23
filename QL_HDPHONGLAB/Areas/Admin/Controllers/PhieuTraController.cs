using QL_HDPHONGLAB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_HDPHONGLAB.Areas.Admin.Controllers
{
    public class PhieuTraController : Controller
    {
        private QL_HDPHONGLABEntities db = new QL_HDPHONGLABEntities();
        // GET: Admin/PhieuTra
        public ActionResult DanhSachPhieuTra()
        {
            var listPT = db.PHIEUTRAs.OrderByDescending(n => n.MAPT).ToList();
            return View(listPT);
        }

        // Xem chi tiết phiếu mượn
        public ActionResult XemChiTiet(int iMaPT)
        {
            var chitiet = db.CT_PHIEUTRA.Where(n => n.MAPT == iMaPT).ToList();
            ViewBag.PhieuTra = db.PHIEUTRAs.SingleOrDefault(n => n.MAPT == iMaPT);
            return View(chitiet);
        }

        // Xoá phiếu mượn
        public ActionResult XoaPhieuTra(int iMaPT)
        {
            var phieutra = db.PHIEUTRAs.SingleOrDefault(n => n.MAPT == iMaPT);
            var listDsTra = db.CT_PHIEUTRA.Where(n => n.MAPT == iMaPT).ToList();
            if (listDsTra != null)
            {
                foreach (var item in listDsTra)
                {
                    db.CT_PHIEUTRA.Remove(item);
                }
            }

            db.PHIEUTRAs.Remove(phieutra);
            db.SaveChanges();
            return RedirectToAction("DanhSachPhieuTra", "PhieuTra");
        }
    }
}