using QL_HDPHONGLAB.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace QL_HDPHONGLAB.Areas.Admin.Controllers
{
    public class PhieuNhapController : Controller
    {
        private QL_HDPHONGLABEntities db = new QL_HDPHONGLABEntities();

        // GET: Admin/PhieuNhap
        // Khởi tạo danh sách phiếu nhập
        public ActionResult DanhSachPhieuNhap()
        {
            var lstDSPN = db.PHIEUNHAPs.OrderByDescending(n => n.MAPN).ToList();
            return View(lstDSPN);
        }

        // Hiện thị thông tin chi tiết của phiếu nhập
        public ActionResult ChiTietNhap(int iMaPhieuNhap)
        {
            var PhieuNhap = db.PHIEUNHAPs.SingleOrDefault(n => n.MAPN == iMaPhieuNhap);
            ViewBag.ChiTiet = db.CHITIETPHIEUNHAPs.Where(n => n.MAPN == iMaPhieuNhap).ToList();
            ViewBag.NhanVien = db.NGUOIDUNGs.Where(n => n.MAQUYEN == 4).ToList();
            return View(PhieuNhap);
        }



        public ActionResult XoaPhieuNhap(int iMaPhieuNhap)
        {
            var phieuNhap = db.PHIEUNHAPs.SingleOrDefault(n => n.MAPN == iMaPhieuNhap); // Lấy mã phiếu nhập
            var listDsNhap = db.CHITIETPHIEUNHAPs.Where(n => n.MAPN == iMaPhieuNhap).ToList();
            foreach (var item in listDsNhap)
            {
                db.CHITIETPHIEUNHAPs.Remove(item);
            }
            db.PHIEUNHAPs.Remove(phieuNhap);
            db.SaveChanges();
            return RedirectToAction("DanhSachPhieuNhap", "PhieuNhap");
        }

        //Cập nhật chi tiết phiếu nhập
        [HttpGet]
        public ActionResult CapNhat(int iMaPhieuNhap)
        {
            var PhieuNhap = db.PHIEUNHAPs.SingleOrDefault(n => n.MAPN == iMaPhieuNhap);
            ViewBag.NhanVien = db.NGUOIDUNGs.Where(n => n.MAQUYEN == 4).ToList();
            ViewBag.ChiTiet = db.CHITIETPHIEUNHAPs.Where(n => n.MAPN == iMaPhieuNhap).ToList();
            return View(PhieuNhap);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "MAPN,NGAYNHAP,NOIDUNG,MANCC,NGUOINHAN,MAPHLAB,TONGTIEN,GHICHU")] PHIEUNHAP phieunhap)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(phieunhap).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("DanhSachPhieuNhap", "PhieuNhap");
        //    }
        //    return View(phieunhap);
        //}

        [HttpPost]
        public ActionResult CapNhat(int iMaPhieuNhap, string iMaHoaChat, string strURL, FormCollection f)
        {

            int soLuongNhap = int.Parse(f["txtSoLuongNhap"].ToString());

            //float GiaNhap = float.Parse(f["txtGiaNhap"].ToString());

            // Lấy hoá chất nhập tương ứng
            var chiTietNhap = db.CHITIETPHIEUNHAPs.SingleOrDefault(n => n.MAPN == iMaPhieuNhap && n.MAHC == iMaHoaChat);
            // Truy xuất hoá chất
            var hoachat = db.HOACHATs.SingleOrDefault(n => n.MAHC == iMaHoaChat);
            if (soLuongNhap < chiTietNhap.SOLUONGNHAP) // Giảm số lượng nhập xuống => trừ trong số lượng hiện còn
            {
                if (hoachat.MAHC == "HC04" || hoachat.MAHC == "HC01") // Hoá chất có mã là hc04 hoặc 01
                {
                    if (soLuongNhap == (float)soLuongNhap) // kiểm tra có nguyên hay không
                    {
                        #region Giảm bớt số lượng hiện còn hoá chất
                        hoachat.LUONGTON = hoachat.LUONGTON - (chiTietNhap.SOLUONGNHAP - soLuongNhap);
                        //hoachat.GIANHAP = GiaNhap;
                        #endregion
                        chiTietNhap.SOLUONGNHAP = soLuongNhap;
                        //chiTietNhap.GIANHAP = GiaNhap;
                        db.SaveChanges();
                    }
                    else
                    {
                    }
                }
                else // còn lại có thể nhập lẽ
                {
                    #region Giảm bớt số lượng hiện còn của hoá chất
                    hoachat.LUONGTON = hoachat.LUONGTON - (chiTietNhap.SOLUONGNHAP - soLuongNhap);
                    //hoachat.GIANHAP = GiaNhap;
                    #endregion
                    chiTietNhap.SOLUONGNHAP = soLuongNhap;
                    //chiTietNhap.GIANHAP = GiaNhap;
                    db.SaveChanges();
                }
            }
            else if (soLuongNhap > chiTietNhap.SOLUONGNHAP) //cập nhật số lượng nhập tăng lên so với ban đầu
            {
                if (hoachat.MAHC == "HC04" || hoachat.MAHC == "HC01")
                {
                    if (soLuongNhap == (float)soLuongNhap)
                    {
                        #region giảm bớt số lượng hiện còn hoá chất
                        hoachat.LUONGTON = hoachat.LUONGTON + (soLuongNhap - chiTietNhap.SOLUONGNHAP);
                        //hoachat.GIANHAP = GiaNhap;
                        #endregion
                        chiTietNhap.SOLUONGNHAP = soLuongNhap;
                        //chiTietNhap.GIANHAP = GiaNhap;
                        db.SaveChanges();
                    }
                    else
                    {
                    }
                }
                else // còn lại có thể nhập lẽ
                {
                    #region giảm bớt số lượng hiện còn của hoá chất
                    hoachat.LUONGTON = hoachat.LUONGTON + (soLuongNhap - chiTietNhap.SOLUONGNHAP);
                    //hoachat.GIANHAP = GiaNhap;
                    #endregion
                    chiTietNhap.SOLUONGNHAP = soLuongNhap;
                    //chiTietNhap.GIANHAP = GiaNhap;
                    db.SaveChanges();
                }
            }
            else // Giữ số lượng nhập nhưng thay đổi giá tiền
            {
                #region giảm bớt số lượng hiện có của hoá chất
                //hoachat.GIANHAP = GiaNhap;
                #endregion
                //chiTietNhap.GIANHAP = GiaNhap;
                db.SaveChanges();
            }
            return Redirect(strURL);
        }


        public ActionResult XoaHoaChatNhap(string iMaHoaChatNhap, int iMaPhieuNhap)
        {
            var hoachatNhap = db.CHITIETPHIEUNHAPs.SingleOrDefault(n => n.MAHC == iMaHoaChatNhap);
            #region Xóa số lượng hiện còn

            #endregion
            db.CHITIETPHIEUNHAPs.Remove(hoachatNhap);
            var PhieuNhap = db.PHIEUNHAPs.SingleOrDefault(n => n.MAPN == iMaPhieuNhap);
            ViewBag.ChiTiet = db.CHITIETPHIEUNHAPs.Where(n => n.MAPN == iMaPhieuNhap).ToList();
            return View(PhieuNhap);
        }
    }
}