using QL_HDPHONGLAB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_HDPHONGLAB.Areas.Admin.Controllers
{
    public class PhieuXuatController : Controller
    {
        private QL_HDPHONGLABEntities db = new QL_HDPHONGLABEntities();

        // Danh sách phiếu xuất kho
        public ActionResult DanhSachPhieuXuat()
        {
            var listPhieuXuat = db.PHIEUXUATs.OrderByDescending(n => n.MAPX).ToList();
            return View(listPhieuXuat);
        }

        // Xem chi tiết phiếu xuất cho khoa
        public ActionResult XemChiTiet(int iMAPX)
        {
            var chitiet = db.CHITIETPHIEUXUATs.Where(n => n.MAPX == iMAPX).ToList();
            ViewBag.PhieuXuat = db.PHIEUXUATs.SingleOrDefault(n => n.MAPX == iMAPX);
            return View(chitiet);
        }

        // Xoá phiếu xuất ra khỏi danh sách phiếu xuất
        public ActionResult XoaPhieuXuat(int iMAPX)
        {
            var phieuxuat = db.PHIEUXUATs.SingleOrDefault(n => n.MAPX == iMAPX);
            var listDsXuat = db.CHITIETPHIEUXUATs.Where(n => n.MAPX == iMAPX).ToList();
            if(listDsXuat != null)
            {
                foreach(var item in listDsXuat)
                {
                    db.CHITIETPHIEUXUATs.Remove(item);
                }    
            }

            db.PHIEUXUATs.Remove(phieuxuat);
            db.SaveChanges();
            return RedirectToAction("DanhSachPhieuXuat", "PhieuXuat");
        }

        #region Thêm mới vào phiếu xuất hoá chất
        [HttpGet]
        public ActionResult ThemPhieuXuat()
        {
            ViewBag.PhongBan = db.PHONGBANs.ToList();
            ViewBag.HoaChat = db.HOACHATs.ToList().OrderBy(n => n.TENHC);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemPhieuXuatHC([Bind(Include = "MAPX,THOIGIANXUAT,NOIDUNG,NGAYXUAT,MAPHBAN,CHAPNHAN,NGUOICHAPNHAN,GHICHU")] PHIEUXUAT model, IEnumerable<CHITIETPHIEUXUAT> lstModel)
        {
            if(ModelState.IsValid)
            {
                #region Lưu phiếu xuất vào database
                PHIEUXUAT px = new PHIEUXUAT();
                if (model.NGAYXUAT == null)
                {
                    model.NGAYXUAT = DateTime.Now;
                }
                db.PHIEUXUATs.Add(model);
                db.SaveChanges();
                #endregion

                foreach (var item in lstModel)
                {
                    // Lấy mã phiếu xuất
                    item.MAPX = model.MAPX;
                    // Tìm mã hoá chất

                    #region Trừ bớt số lượng tồn hoá chất có trong kho và tăng số lượng tồn hoá chất bên khoa
                    var hc = db.HOACHATs.SingleOrDefault(n => n.MAHC == item.MAHC);
                    //var slhoachat_cntp = db.HOACHAT_CNTP.SingleOrDefault(n => n.MAHC_CNTP == item.MAHC_CNTP);
                    if (hc.LUONGTON >= item.SOLUONGXUAT)
                    {
                        hc.LUONGTON -= item.SOLUONGXUAT;
                    }
                    //else if(slhoachat_cntp.LUONGTON >= item.SOLUONGXUAT)
                    //{
                    //    slhoachat_cntp.LUONGTON += item.SOLUONGXUAT;
                    //}
                    else
                    {
                        //Thoát và không lưu
                        return RedirectToAction("HienThiLoi500", "TrangChu");
                    }
                    #endregion
                    db.SaveChanges();
                    return RedirectToAction("Index", "HOACHATs");
                }
            }
            return View(model);
            //// Lấy mã phiếu xuất
            //foreach (var item in lstModel) // chi tiết phiếu xuất
            //{

            //    // tìm mã hoá chất
            //    var hoachat = db.HOACHATs.SingleOrDefault(n => n.MAHC == item.MAHC);
            //    if (hoachat.MALHC == 1) // mã nguyên hoá chất == 4
            //    {
            //        int SL;
            //        if (int.TryParse(item.SOLUONGXUAT.ToString(), out SL))
            //        {
            //            CHITIETPHIEUXUAT xhc = new CHITIETPHIEUXUAT();
            //            xhc.MAPX = model.MAPX;
            //            xhc.MAHC = item.MAHC;
            //            xhc.SOLUONGXUAT = item.SOLUONGXUAT;
            //            db.CHITIETPHIEUXUATs.Add(xhc);

            //            #region Trừ bớt số lượng trong kho
            //            var slhc = db.HOACHATs.SingleOrDefault(n => n.MAHC == item.MAHC);
            //            if (slhc.LUONGTON >= item.SOLUONGXUAT)
            //            {
            //                slhc.LUONGTON = slhc.LUONGTON - item.SOLUONGXUAT;
            //            }
            //            else
            //            {
            //                // Thoát và k lưu
            //                return RedirectToAction("HienThiLoi500", "TrangChu");
            //            }
            //            #endregion
            //        }
            //        else
            //        {

            //        }
            //    }
            //    else
            //    {
            //        CHITIETPHIEUXUAT xhc = new CHITIETPHIEUXUAT();
            //        xhc.MAPX = model.MAPX;
            //        xhc.MAHC = item.MAHC;
            //        xhc.SOLUONGXUAT = item.SOLUONGXUAT;
            //        db.CHITIETPHIEUXUATs.Add(xhc);

            //        #region Trừ bớt số lượng trong kho
            //        var slHC = db.HOACHATs.SingleOrDefault(n => n.MAHC == item.MAHC);
            //        if (slHC.LUONGTON >= item.SOLUONGXUAT)
            //        {
            //            slHC.LUONGTON = slHC.LUONGTON - item.SOLUONGXUAT;
            //        }
            //        else
            //        {
            //            // Thoát và k lưu
            //            return RedirectToAction("HienThiLoi500", "TrangChu");
            //        }

            //    }
            //    #endregion
            //}
            //db.SaveChanges();
            //return RedirectToAction("DanhSachPhieuXuat");
        }
        #endregion


        #region Thêm mới vào phiếu xuất thiêt bị
        [HttpGet]
        public ActionResult ThemPhieuXuat2()
        {
            ViewBag.PhongBan = db.PHONGBANs.ToList();
            ViewBag.ThietBi = db.THIETBIs.ToList().OrderBy(n => n.TENTB);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemPhieuXuatTB([Bind(Include = "MAPX,THOIGIANXUAT,NOIDUNG,NGAYXUAT,MAPHBAN,CHAPNHAN,NGUOICHAPNHAN,GHICHU")] PHIEUXUAT model, IEnumerable<CHITIETPHIEUXUAT> lstModel)
        {
            if (ModelState.IsValid)
            {
                #region Lưu phiếu xuất vào database
                PHIEUXUAT px = new PHIEUXUAT();
                if (model.NGAYXUAT == null)
                {
                    model.NGAYXUAT = DateTime.Now;
                }
                db.PHIEUXUATs.Add(model);
                db.SaveChanges();
                #endregion

                foreach (var item in lstModel)
                {
                    // Lấy mã phiếu xuất
                    item.MAPX = model.MAPX;
                    // Tìm mã hoá chất

                    #region Trừ bớt số lượng tồn thiết bị có trong kho và tăng số lượng tồn thiết bị bên khoa
                    var tb = db.THIETBIs.SingleOrDefault(n => n.MATB == item.MATB);
                    if (tb.SLTON >= item.SOLUONGXUAT)
                    {
                        tb.SLTON -= item.SOLUONGXUAT;
                    }
                    
                    else
                    {
                        //Thoát và không lưu
                        return RedirectToAction("HienThiLoi500", "TrangChu");
                    }
                    #endregion
                    db.SaveChanges();
                    return RedirectToAction("Index", "THIETBIs");
                }
            }
            return View(model);
        }
        #endregion

        #region Thêm mới vào phiếu xuất dụng cụ
        [HttpGet]
        public ActionResult ThemPhieuXuat3()
        {
            ViewBag.PhongBan = db.PHONGBANs.ToList();
            ViewBag.DungCu = db.DUNGCUs.ToList().OrderBy(n => n.TENDC);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemPhieuXuatDC([Bind(Include = "MAPX,THOIGIANXUAT,NOIDUNG,NGAYXUAT,MAPHBAN,CHAPNHAN,NGUOICHAPNHAN,GHICHU")] PHIEUXUAT model, IEnumerable<CHITIETPHIEUXUAT> lstModel)
        {
            if (ModelState.IsValid)
            {
                #region Lưu phiếu xuất vào database
                PHIEUXUAT px = new PHIEUXUAT();
                if (model.NGAYXUAT == null)
                {
                    model.NGAYXUAT = DateTime.Now;
                }
                db.PHIEUXUATs.Add(model);
                db.SaveChanges();
                #endregion

                foreach (var item in lstModel)
                {
                    // Lấy mã phiếu xuất
                    item.MAPX = model.MAPX;
                    // Tìm mã hoá chất

                    #region Trừ bớt số lượng tồn dụng cụ có trong kho và tăng số lượng tồn dụng cụ bên khoa
                    var dc = db.DUNGCUs.SingleOrDefault(n => n.MADC == item.MADC);
                    if (dc.LUONGTON >= item.SOLUONGXUAT)
                    {
                        dc.LUONGTON -= item.SOLUONGXUAT;
                    }

                    else
                    {
                        //Thoát và không lưu
                        return RedirectToAction("HienThiLoi500", "TrangChu");
                    }
                    #endregion
                    db.SaveChanges();
                    return RedirectToAction("Index", "DUNGCUs");
                }
            }
            return View(model);
        }
        #endregion
    }
}