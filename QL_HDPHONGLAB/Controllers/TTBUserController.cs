using QL_HDPHONGLAB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_HDPHONGLAB.Controllers
{
    public class TTBUserController : Controller
    {
        private QL_HDPHONGLABEntities db = new QL_HDPHONGLABEntities();

        public ActionResult HCPartial()
        {
            var listHoaChat = db.HOACHATs.Take(5).OrderBy(n => n.MAHC).ToList();
            return View(listHoaChat);
        }

        public ActionResult TBPartial()
        {
            var listThietBi = db.THIETBIs.Take(5).OrderBy(n => n.MATB).ToList();
            return View(listThietBi);
        }
        public ActionResult DCPartial()
        {
            var listDungCu = db.DUNGCUs.Take(5).OrderBy(n => n.MADC).ToList();
            return View(listDungCu);
        }

        #region Đăng Ký Mượn Trang Thiết Bị
        public ActionResult DangKyMuonHC(string msp)
        {
            var dkyHC = db.HOACHATs.SingleOrDefault(n => n.MAHC == msp);
            if(dkyHC == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(dkyHC);
        }

        public ActionResult DangKyMuonTB(string msp)
        {
            var dkyTB = db.THIETBIs.SingleOrDefault(n => n.MATB == msp);
            if (dkyTB == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(dkyTB);
        }

        public ActionResult DangKyMuonDC(string msp)
        {
            var dkyDC = db.DUNGCUs.SingleOrDefault(n => n.MADC == msp);
            if (dkyDC == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(dkyDC);
        }
        #endregion

        #region Đăng Ký Trả Trang Thiết Bị
        public ActionResult DangKyTraHC(string msp)
        {
            var trahc = db.HOACHATs.SingleOrDefault(n => n.MAHC == msp);
            if(trahc == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(trahc);
        }

        public ActionResult DangKyTraTB(string msp)
        {
            var tratb = db.THIETBIs.SingleOrDefault(n => n.MATB == msp);
            if (tratb == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(tratb);
        }

        public ActionResult DangKyTraDC(string msp)
        {
            var tradc = db.DUNGCUs.SingleOrDefault(n => n.MADC == msp);
            if (tradc == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(tradc);
        }
        #endregion
    }
}