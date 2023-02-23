using QL_HDPHONGLAB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_HDPHONGLAB.Areas.Admin.Controllers
{
    public class KhoController : Controller
    {
        private QL_HDPHONGLABEntities db = new QL_HDPHONGLABEntities();
        // GET: Admin/Kho
        #region Nhập Kho Hoá Chất
        public ActionResult NhapKho()
        {
            ViewBag.ListLoaiHoaChat = db.LOAIHOACHATs.ToList();
            ViewBag.All = db.LOAIHOACHATs.Count();
            ViewBag.HoaChat = db.HOACHATs.ToList().OrderBy(n => n.TENHC);
            ViewBag.NhanVien = db.NGUOIDUNGs.Where(n => n.MAQUYEN == 4).ToList();
            ViewBag.NhaCungCap = db.NCCs.ToList();
            return View();
        }

        //[HttpPost]
        //public ActionResult NhapKho(PHIEUNHAP model, IEnumerable<CHITIETPHIEUNHAP> lstModel)
        //{
        //    //double? TongTien = 0;
        //    if (model.NGAYNHAP == null)
        //    {
        //        model.NGAYNHAP = DateTime.Now;

        //    }
        //    db.PHIEUNHAPs.Add(model);
        //    db.SaveChanges();

        //    // Lấy mã phiếu nhập lưu dữ liệu để gán cho list chi tiết phiếu nhập
        //    foreach (var item in lstModel) // chi tiết phiếu nhập
        //    {
        //        // Gán mã phiếu nhập cho cả chi tiết phiếu nhập
        //        item.MAPN = model.MAPN;
        //        //item.THANHTIEN = (item.SOLUONGNHAP * item.GIANHAP);
        //        //TongTien = TongTien + item.THANHTIEN;

        //        #region Cộng số lượng hiện có của hoá chất
        //        var slhc = db.HOACHATs.SingleOrDefault(n => n.MAHC == item.MAHC);

        //        slhc.LUONGTON += item.SOLUONGNHAP;
        //        //slhc.GIANHAP = item.GIANHAP;
        //        #endregion
        //    }
        //    db.CHITIETPHIEUNHAPs.AddRange(lstModel);
        //    PHIEUNHAP pn = db.PHIEUNHAPs.SingleOrDefault(n => n.MAPN == model.MAPN);
        //    //pn.TONGTIEN = (double)TongTien;
        //    db.SaveChanges();
        //    return RedirectToAction("DanhSachPhieuNhap", "PhieuNhap");
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NhapHoaChat([Bind(Include = "MAPN,NGAYNHAP,NOIDUNG,MANCC,NGUOINHAN,MAPHLAB,TONGTIEN,GHICHU")] PHIEUNHAP model, IEnumerable<CHITIETPHIEUNHAP> lstmodel)
        {
            
            if (ModelState.IsValid)
            {
                if (model.NGAYNHAP == null)
                {
                    model.NGAYNHAP = DateTime.Now;
                }
                db.PHIEUNHAPs.Add(model);
                db.SaveChanges();

                foreach (var item in lstmodel)
                {
                    // Gán mã phiếu nhập cho cả chi tiết phiếu nhập
                    item.MAPN = model.MAPN;

                    #region Cộng số lướng hiện có của hoá chất
                    var slhoachat = db.HOACHATs.SingleOrDefault(n => n.MAHC == item.MAHC);
                    slhoachat.LUONGTON += item.SOLUONGNHAP;
                    #endregion
                }
                //db.CHITIETPHIEUNHAPs.AddRange(lstmodel);
                db.SaveChanges();
                return RedirectToAction("Index", "HOACHATs");
            }
            return View(model);
        }
        #endregion

        #region Nhập kho thiết bị
        public ActionResult NhapKho2()
        {
            //ViewBag.All = db.LOAIHOACHATs.Count();
            ViewBag.ThietBi = db.THIETBIs.ToList().OrderBy(n => n.TENTB);
            ViewBag.NhanVien = db.NGUOIDUNGs.Where(n => n.MAQUYEN == 4).ToList();
            ViewBag.NhaCungCap = db.NCCs.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NhapThietBi([Bind(Include = "MAPN,NGAYNHAP,NOIDUNG,MANCC,NGUOINHAN,MAPHLAB,TONGTIEN,GHICHU")] PHIEUNHAP model, IEnumerable<CHITIETPHIEUNHAP> lstmodel)
        {

            if (ModelState.IsValid)
            {
                if (model.NGAYNHAP == null)
                {
                    model.NGAYNHAP = DateTime.Now;
                }
                db.PHIEUNHAPs.Add(model);
                db.SaveChanges();

                foreach (var item in lstmodel)
                {
                    // Gán mã phiếu nhập cho cả chi tiết phiếu nhập
                    item.MAPN = model.MAPN;

                    #region Cộng số lướng hiện có của thiết bị
                    var slthietbi = db.THIETBIs.SingleOrDefault(n => n.MATB == item.MATB);
                    slthietbi.SLTON += item.SOLUONGNHAP;
                    #endregion
                }
                //db.CHITIETPHIEUNHAPs.AddRange(lstmodel);
                db.SaveChanges();
                return RedirectToAction("Index", "THIETBIs");
            }
            return View(model);
        }
        #endregion

        #region Nhập kho dụng cụ
        public ActionResult NhapKho3()
        {
            ViewBag.DungCu = db.DUNGCUs.ToList().OrderBy(n => n.TENDC);
            ViewBag.NhanVien = db.NGUOIDUNGs.Where(n => n.MAQUYEN == 4).ToList();
            ViewBag.NhaCungCap = db.NCCs.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NhapDungCu([Bind(Include = "MAPN,NGAYNHAP,NOIDUNG,MANCC,NGUOINHAN,MAPHLAB,TONGTIEN,GHICHU")] PHIEUNHAP model, IEnumerable<CHITIETPHIEUNHAP> lstmodel)
        {
            if(ModelState.IsValid)
            {
                if(model.NGAYNHAP == null)
                {
                    model.NGAYNHAP = DateTime.Now;
                }
                db.PHIEUNHAPs.Add(model);
                db.SaveChanges();

                foreach (var item in lstmodel)
                {
                    // Gán mã phiếu nhập cho cả chi tiết phiếu nhập
                    item.MAPN = model.MAPN;

                    #region Cộng số lướng hiện có của thiết bị
                    var sldungcu = db.DUNGCUs.SingleOrDefault(n => n.MADC == item.MADC);
                    sldungcu.LUONGTON += item.SOLUONGNHAP;
                    #endregion
                }
                //db.CHITIETPHIEUNHAPs.AddRange(lstmodel);
                db.SaveChanges();
                return RedirectToAction("Index", "DUNGCUs");
            }
            return View(model);
        }    
        #endregion
    }
}