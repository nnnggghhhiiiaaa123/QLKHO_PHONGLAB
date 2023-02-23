using QL_HDPHONGLAB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_HDPHONGLAB.Areas.Admin.Controllers
{
    public class KhuVucThanhLyController : Controller
    {
        private QL_HDPHONGLABEntities db;
        public KhuVucThanhLyController()
        {
            db = new QL_HDPHONGLABEntities();
        }

        public ActionResult KVTL()
        {
            var list = db.PHIEUTHANHLies.OrderByDescending(n => n.MATL).ToList();
            return View(list);
        }

        // Hiển thị thông tin chi tiết thanh lý
        public ActionResult ChiTiet(int iMaTL)
        {
            var thanhly = db.PHIEUTHANHLies.SingleOrDefault(n => n.MATL == iMaTL);
            ViewBag.ChiTiet = db.CHITIETPHIEUTHANHLies.Where(n => n.MATL == iMaTL).ToList();
            ViewBag.PhieuTL = db.PHIEUTHANHLies.SingleOrDefault(n => n.MATL == iMaTL);
            ViewBag.NguoiDuyet = db.NGUOIDUNGs.Where(n => n.MAQUYEN == 4).ToList();
            return View(thanhly);
        }

        // Xoá phiếu thanh lý
        public ActionResult XuLy(int iMaTL)
        {
            var thanhly = db.PHIEUTHANHLies.SingleOrDefault(n => n.MATL == iMaTL);
            var listthanhly = db.CHITIETPHIEUTHANHLies.Where(n => n.MATL == iMaTL).ToList();
            foreach(var item in listthanhly)
            {
                db.CHITIETPHIEUTHANHLies.Remove(item);
            }
            db.PHIEUTHANHLies.Remove(thanhly);
            db.SaveChanges();
            return RedirectToAction("KVTL", "KhuVucThanhLy");
        }

        // Cập nhật
        [HttpGet]
        public ActionResult CapNhat(int iMaTL)
        {
            var thanhly = db.PHIEUNHAPs.SingleOrDefault(n => n.MAPN == iMaTL);
            ViewBag.NguoiDuyet = db.NGUOIDUNGs.Where(n => n.MAQUYEN == 4).ToList();
            ViewBag.ChiTiet = db.CHITIETPHIEUTHANHLies.Where(n => n.MATL == iMaTL).ToList();
            return View(thanhly);
        }

        [HttpPost]
        public ActionResult CapNhat(int iMaTL, string iMaHoaChat, string iMaThietBi, string iMaDungCu, string strURL, FormCollection f)
        {
            int soluongthanhly = int.Parse(f["txtSoLuongThanhLy"].ToString());

            // Lấy hoá chất thanh lý tương ứng
            var chitietthanhly = db.CHITIETPHIEUTHANHLies.SingleOrDefault(n => n.MATL == iMaTL && n.MAHC == iMaHoaChat && n.MATB == iMaThietBi && n.MADC == iMaDungCu);

            // Truy xuất trang thiết bị
            var hoachat = db.HOACHATs.SingleOrDefault(n => n.MAHC == iMaHoaChat);
            var thietbi = db.THIETBIs.SingleOrDefault(n => n.MATB == iMaThietBi);
            var dungcu = db.DUNGCUs.SingleOrDefault(n => n.MADC == iMaDungCu);

            if(soluongthanhly < chitietthanhly.SLTHANHLY) // Giảm số lượng tồn trong kho tăng lượng thanh lý
            {
                if (hoachat.MAHC == "HC04" || hoachat.MAHC == "HC01") // Hoá chất có mã là hc04 hoặc 01
                {
                    if (soluongthanhly == (float)soluongthanhly) // kiểm tra có nguyên hay không
                    {
                        #region Giảm bớt số lượng hiện còn hoá chất
                        hoachat.LUONGTON = hoachat.LUONGTON - (chitietthanhly.SLTHANHLY - soluongthanhly);
                        //hoachat.GIANHAP = GiaNhap;
                        #endregion
                        chitietthanhly.SLTHANHLY = soluongthanhly;
                        //chiTietNhap.GIANHAP = GiaNhap;
                        db.SaveChanges();
                    }
                    else
                    {
                    }
                }
                else if(thietbi.MATB == "TB04" || thietbi.MATB == "TB01") // Thiết bị có mã là tb04 hoặc tb01
                {
                    if (soluongthanhly == (float)soluongthanhly) // kiểm tra có nguyên hay không
                    {
                        #region Giảm bớt số lượng hiện còn của thiết bị
                        thietbi.SLTON = thietbi.SLTON - (chitietthanhly.SLTHANHLY - soluongthanhly);
                        //hoachat.GIANHAP = GiaNhap;
                        #endregion
                        chitietthanhly.SLTHANHLY = soluongthanhly;
                        //chiTietNhap.GIANHAP = GiaNhap;
                        db.SaveChanges();
                    }
                    else
                    {
                    }
                }  
                else if(dungcu.MADC == "DC04" || dungcu.MADC == "DC01") // Dụng cụ có mã là dc04 hoặc dc01
                {
                    if (soluongthanhly == (float)soluongthanhly) // kiểm tra có nguyên hay không
                    {
                        #region Giảm bớt số lượng hiện còn của thiết bị
                        dungcu.LUONGTON = dungcu.LUONGTON - (chitietthanhly.SLTHANHLY - soluongthanhly);
                        //hoachat.GIANHAP = GiaNhap;
                        #endregion
                        chitietthanhly.SLTHANHLY = soluongthanhly;
                        //chiTietNhap.GIANHAP = GiaNhap;
                        db.SaveChanges();
                    }
                    else
                    {
                    }
                }    
                else
                {
                    #region Giảm bớt số lượng hiện còn của hoá chất
                    hoachat.LUONGTON = hoachat.LUONGTON - (chitietthanhly.SLTHANHLY - soluongthanhly);
                    thietbi.SLTON = thietbi.SLTON - (chitietthanhly.SLTHANHLY - soluongthanhly);
                    dungcu.LUONGTON = dungcu.LUONGTON - (chitietthanhly.SLTHANHLY - soluongthanhly);
                    //hoachat.GIANHAP = GiaNhap;
                    #endregion
                    chitietthanhly.SLTHANHLY = soluongthanhly;
                    //chiTietNhap.GIANHAP = GiaNhap;
                    db.SaveChanges();
                }    
            }
            else if(soluongthanhly > chitietthanhly.SLTHANHLY) // cập nhật số lượng thanh lý tăng lên so với ban đầu
            {
                if (hoachat.MAHC == "HC04" || hoachat.MAHC == "HC01") // Hoá chất có mã là hc04 hoặc 01
                {
                    if (soluongthanhly == (float)soluongthanhly) // kiểm tra có nguyên hay không
                    {
                        #region Giảm bớt số lượng hiện còn hoá chất
                        hoachat.LUONGTON = hoachat.LUONGTON + (chitietthanhly.SLTHANHLY - soluongthanhly);
                        //hoachat.GIANHAP = GiaNhap;
                        #endregion
                        chitietthanhly.SLTHANHLY = soluongthanhly;
                        //chiTietNhap.GIANHAP = GiaNhap;
                        db.SaveChanges();
                    }
                    else
                    {
                    }
                }
                else if (thietbi.MATB == "TB04" || thietbi.MATB == "TB01") // Thiết bị có mã là tb04 hoặc tb01
                {
                    if (soluongthanhly == (float)soluongthanhly) // kiểm tra có nguyên hay không
                    {
                        #region Giảm bớt số lượng hiện còn của thiết bị
                        thietbi.SLTON = thietbi.SLTON + (chitietthanhly.SLTHANHLY - soluongthanhly);
                        //hoachat.GIANHAP = GiaNhap;
                        #endregion
                        chitietthanhly.SLTHANHLY = soluongthanhly;
                        //chiTietNhap.GIANHAP = GiaNhap;
                        db.SaveChanges();
                    }
                    else
                    {
                    }
                }
                else if (dungcu.MADC == "DC04" || dungcu.MADC == "DC01") // Dụng cụ có mã là dc04 hoặc dc01
                {
                    if (soluongthanhly == (float)soluongthanhly) // kiểm tra có nguyên hay không
                    {
                        #region Giảm bớt số lượng hiện còn của thiết bị
                        dungcu.LUONGTON = dungcu.LUONGTON + (chitietthanhly.SLTHANHLY - soluongthanhly);
                        //hoachat.GIANHAP = GiaNhap;
                        #endregion
                        chitietthanhly.SLTHANHLY = soluongthanhly;
                        //chiTietNhap.GIANHAP = GiaNhap;
                        db.SaveChanges();
                    }
                    else
                    {
                    }
                }
                else
                {
                    #region Giảm bớt số lượng hiện còn của hoá chất
                    hoachat.LUONGTON = hoachat.LUONGTON + (chitietthanhly.SLTHANHLY - soluongthanhly);
                    thietbi.SLTON = thietbi.SLTON + (chitietthanhly.SLTHANHLY - soluongthanhly);
                    dungcu.LUONGTON = dungcu.LUONGTON + (chitietthanhly.SLTHANHLY - soluongthanhly);
                    //hoachat.GIANHAP = GiaNhap;
                    #endregion
                    chitietthanhly.SLTHANHLY = soluongthanhly;
                    //chiTietNhap.GIANHAP = GiaNhap;
                    db.SaveChanges();
                }    
            }
            else 
            {
                
            }
            return Redirect(strURL);
        }
    }
}