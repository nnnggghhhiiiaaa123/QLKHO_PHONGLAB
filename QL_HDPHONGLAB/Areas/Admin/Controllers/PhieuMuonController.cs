using QL_HDPHONGLAB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_HDPHONGLAB.Areas.Admin.Controllers
{
    public class PhieuMuonController : Controller
    {
        private QL_HDPHONGLABEntities db = new QL_HDPHONGLABEntities();
        // GET: Admin/PhieuMuon
        public ActionResult DanhSachPhieuMuon()
        {
            var listPM = db.PHIEUMUONs.OrderByDescending(n => n.MAPM).ToList();
            return View(listPM);
        }

        // Xem chi tiết phiếu mượn
        public ActionResult XemChiTiet(int iMaPM)
        {
            var chitiet = db.CT_PHIEUMUON.Where(n => n.MAPM == iMaPM).ToList();
            ViewBag.PhieuMuon = db.PHIEUMUONs.SingleOrDefault(n => n.MAPM == iMaPM);
            return View(chitiet);
        }

        // Xoá phiếu mượn
        public ActionResult XoaPhieuMuon(int iMaPM)
        {
            var phieumuon = db.PHIEUMUONs.SingleOrDefault(n => n.MAPM == iMaPM);
            var listDsMuon = db.CT_PHIEUMUON.Where(n => n.MAPM == iMaPM).ToList();
            if (listDsMuon != null)
            {
                foreach (var item in listDsMuon)
                {
                    db.CT_PHIEUMUON.Remove(item);
                }
            }

            db.PHIEUMUONs.Remove(phieumuon);
            db.SaveChanges();
            return RedirectToAction("DanhSachPhieuMuon", "PhieuMuon");
        }  
        

    }
}