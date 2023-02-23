using QL_HDPHONGLAB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_HDPHONGLAB.Areas.Admin.Controllers
{
    public class NhapVeKhoaController : Controller
    {
        private QL_HDPHONGLABEntities db;
        public NhapVeKhoaController()
        {
            db = new QL_HDPHONGLABEntities();
        }

        public ActionResult DanhSachPhieuNhapKhoa()
        {
            var lstDSPNKhoa = db.PHIEUNHAP_KHOA.OrderByDescending(n => n.MAPN_KHOA).ToList();
            return View(lstDSPNKhoa);
        }

        //Nhập hoá chất về khoa
        #region Nhập hoá chất về khoa
        public ActionResult NhapKhoa()
        {
            ViewBag.LoaiHoaChat = db.LOAIHOACHATs.ToList();
            ViewBag.HoaChat_CNTP = db.HOACHAT_CNTP.OrderBy(n => n.TENHC_CNTP).ToList();
            ViewBag.ThietBi_CNTP = db.THIETBI_CNTP.OrderBy(n => n.TENTB).ToList();
            ViewBag.DungCu_CNTP = db.DUNGCU_CNTP.OrderBy(n => n.TENDC).ToList();
            ViewBag.All = db.LOAIHOACHATs.Count();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NhapKhoa([Bind(Include ="MAPN_KHOA,MAHC_CNTP,MATB_CNTP,MADC_CNTP,NGAYNHAP,NOIDUNG,NGUOIKIEMDUYET,GHICHU")] PHIEUNHAP_KHOA pn_khoa, IEnumerable<CHITIETPHIEUNHAP_KHOA> list)
        {
            if (ModelState.IsValid)
            {
                if (pn_khoa.NGAYNHAP == null)
                {
                    pn_khoa.NGAYNHAP = DateTime.Now;
                }
                db.PHIEUNHAP_KHOA.Add(pn_khoa);
                db.SaveChanges();

                foreach(var view in list)
                {
                    // Lấy mã phiếu nhập
                    view.MAPN_KHOA = pn_khoa.MAPN_KHOA;

                    #region
                    var sl_hccntp = db.HOACHAT_CNTP.SingleOrDefault(n => n.MAHC_CNTP == view.MAHC_CNTP);
                    sl_hccntp.LUONGTON += view.SOLUONGNHAP;

                    //var sl_tbcntp = db.THIETBI_CNTP.SingleOrDefault(n => n.MATB == view.MATB_CNTP);
                    //sl_tbcntp.SLTON += view.SOLUONGNHAP;
                    #endregion
                }

                db.SaveChanges();

                return RedirectToAction("DanhSachPhieuNhapKhoa", "NhapVeKhoa");
            }
            return View(pn_khoa);
        }

        public ActionResult Xoa(int iMaPhieuNhap)
        {
            var phieuNhap = db.PHIEUNHAP_KHOA.SingleOrDefault(n => n.MAPN_KHOA == iMaPhieuNhap); // Lấy mã phiếu nhập
            var listDsNhap = db.CHITIETPHIEUNHAP_KHOA.Where(n => n.MAPN_KHOA == iMaPhieuNhap).ToList();
            foreach (var item in listDsNhap)
            {
                db.CHITIETPHIEUNHAP_KHOA.Remove(item);
            }
            db.PHIEUNHAP_KHOA.Remove(phieuNhap);
            db.SaveChanges();
            return RedirectToAction("DanhSachPhieuNhapKhoa", "NhapVeKhoa");
        }
        #endregion


        #region Nhập thiết bị về khoa
        public ActionResult NhapKhoa2()
        {
            ViewBag.ThietBi_CNTP = db.THIETBI_CNTP.OrderBy(n => n.TENTB).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NhapKhoa2([Bind(Include = "MAPN_KHOA,MAHC_CNTP,MATB_CNTP,MADC_CNTP,NGAYNHAP,NOIDUNG,NGUOIKIEMDUYET,GHICHU")] PHIEUNHAP_KHOA pn_khoa, IEnumerable<CHITIETPHIEUNHAP_KHOA> list)
        {
            if (ModelState.IsValid)
            {
                if (pn_khoa.NGAYNHAP == null)
                {
                    pn_khoa.NGAYNHAP = DateTime.Now;
                }
                db.PHIEUNHAP_KHOA.Add(pn_khoa);
                db.SaveChanges();

                foreach (var view in list)
                {
                    // Lấy mã phiếu nhập
                    view.MAPN_KHOA = pn_khoa.MAPN_KHOA;

                    #region
                    var sl_tbcntp = db.THIETBI_CNTP.SingleOrDefault(n => n.MATB == view.MATB_CNTP);
                    sl_tbcntp.SLTON += view.SOLUONGNHAP;
                    #endregion
                }

                db.SaveChanges();

                return RedirectToAction("DanhSachPhieuNhapKhoa", "NhapVeKhoa");
            }
            return View(pn_khoa);
        }
        #endregion

        #region Nhập thiết bị về khoa
        public ActionResult NhapKhoa3()
        {
            ViewBag.DungCu_CNTP = db.DUNGCU_CNTP.OrderBy(n => n.TENDC).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NhapKhoa3([Bind(Include = "MAPN_KHOA,MAHC_CNTP,MATB_CNTP,MADC_CNTP,NGAYNHAP,NOIDUNG,NGUOIKIEMDUYET,GHICHU")] PHIEUNHAP_KHOA pn_khoa, IEnumerable<CHITIETPHIEUNHAP_KHOA> list)
        {
            if (ModelState.IsValid)
            {
                if (pn_khoa.NGAYNHAP == null)
                {
                    pn_khoa.NGAYNHAP = DateTime.Now;
                }
                db.PHIEUNHAP_KHOA.Add(pn_khoa);
                db.SaveChanges();

                foreach (var view in list)
                {
                    // Lấy mã phiếu nhập
                    view.MAPN_KHOA = pn_khoa.MAPN_KHOA;

                    #region
                    var sl_dccntp = db.DUNGCU_CNTP.SingleOrDefault(n => n.MADC == view.MADC_CNTP);
                    sl_dccntp.LUONGTON += view.SOLUONGNHAP;
                    #endregion
                }

                db.SaveChanges();

                return RedirectToAction("DanhSachPhieuNhapKhoa", "NhapVeKhoa");
            }
            return View(pn_khoa);
        }
        #endregion
    }
}