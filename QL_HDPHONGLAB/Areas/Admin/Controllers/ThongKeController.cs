using QL_HDPHONGLAB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_HDPHONGLAB.Areas.Admin.Controllers
{
    public class ThongKeController : Controller
    {
        private QL_HDPHONGLABEntities db;
        public ThongKeController()
        {
            db = new QL_HDPHONGLABEntities();
        }

        public double TongTienDonHang()
        {
            // Doanh Thu
            double TongDoanhThu = (double)db.PHIEUNHAPs.Sum(n => n.TONGTIEN);
            return TongDoanhThu;
        }

        #region Tính tổng doanh thu hoá đơn và doanh thu nhập
        public double DoanhThuThangNam(int iThang, int iNam)
        {
            var listDT = db.PHIEUNHAPs.Where(n => n.NGAYNHAP.Value.Month == iThang && n.NGAYNHAP.Value.Year == iNam);
            double TongTien = 0;
            foreach(var item in listDT)
            {
                TongTien = TongTien + (double)item.CHITIETPHIEUNHAP.Sum(n => n.THANHTIEN);
            }
            return TongTien;
        }

        public double DoanhThuNgayThangNam(int iNgay, int iThang, int iNam)
        {
            var listDT = db.PHIEUNHAPs.Where(n => n.NGAYNHAP.Value.Day == iNgay && n.NGAYNHAP.Value.Month == iThang && n.NGAYNHAP.Value.Year == iNam);
            double TongTien = 0;
            foreach (var item in listDT)
            {
                TongTien += (double)item.CHITIETPHIEUNHAP.Sum(n => n.THANHTIEN);
            }
            return TongTien;
        }

        public double DoanhThuNam(int iNam)
        {
            var listDT = db.PHIEUNHAPs.Where(n => n.NGAYNHAP.Value.Year == iNam);
            double TongTien = 0;
            foreach (var item in listDT)
            {
                TongTien += (double)item.CHITIETPHIEUNHAP.Sum(n => n.THANHTIEN);
            }
            return TongTien;
        }

        public double? DoanhThuThangNam_Nhap(int iThang, int iNam)
        {
            var listPhieuNhap = db.PHIEUNHAPs.Where(n => n.NGAYNHAP.Value.Month == iThang && n.NGAYNHAP.Value.Year == iNam);
            double? TongTien = 0;
            foreach (var item in listPhieuNhap)
            {
                TongTien += item.TONGTIEN;
            }
            return TongTien;
        }

        public double? DoanhThuNgayThangNam_Nhap(int Ngay, int Thang, int Nam)
        {

            var listPhieuNhap = db.PHIEUNHAPs.Where(n => n.NGAYNHAP.Value.Day == Ngay && n.NGAYNHAP.Value.Month == Thang && n.NGAYNHAP.Value.Year == Nam);
            double? TongTien = 0;
            foreach (var item in listPhieuNhap)
            {
                TongTien += item.TONGTIEN;
            }
            return TongTien;
        }

        public double? DoanhThuNam_Nhap(int Nam)
        {
            var listPhieuNhap = db.PHIEUNHAPs.Where(n => n.NGAYNHAP.Value.Year == Nam);
            double? TongTien = 0;
            foreach (var item in listPhieuNhap)
            {
                TongTien += item.TONGTIEN;
            }
            return TongTien;
        }

        // Tính tổng tiền theo tháng phiếu nhập
        public double? DoanhThuTuan(int iNgayBatDau, int iNgayKetThuc, int iThang)
        {
            var listPhieuNhap = db.PHIEUNHAPs.Where(n => n.NGAYNHAP.Value.Day >= iNgayBatDau && n.NGAYNHAP.Value.Day <= iNgayKetThuc && n.NGAYNHAP.Value.Month == iThang);
            double? TongTien = 0;
            foreach (var item in listPhieuNhap)
            {
                TongTien += item.TONGTIEN;
            }
            return TongTien;
        }

        // Tính Tổng tiền theo tháng -> tháng của năm nào đó
        public double? DoanhThuThangDenThang(int iThangBatDau, int iNamBatDau, int iThangKetThuc, int iNamKetThuc)
        {
            var listPhieuNhap = db.PHIEUNHAPs.Where(n => n.NGAYNHAP.Value.Month >= iThangBatDau && n.NGAYNHAP.Value.Year >= iNamBatDau && n.NGAYNHAP.Value.Month <= iThangKetThuc && n.NGAYNHAP.Value.Year <= iNamKetThuc);
            double? TongTien = 0;
            foreach(var item in listPhieuNhap)
            {
                TongTien += item.TONGTIEN;
            }
            return TongTien;
        }
        #endregion

        public ActionResult DanhSachThongKe()
        {
            var list = db.PHIEUNHAPs.OrderByDescending(n => n.MAPN).ToList();
            return View(list);
        }
        public ActionResult ThongKe()
        {
            ViewBag.KiemDuyet = db.PHIEUNHAPs.Count();
            ViewBag.DoanhThu = TongTienDonHang();
            ViewBag.HoaChat = db.HOACHATs.Count();
            ViewBag.PhieuXuat = db.PHIEUXUATs.Count();
            ViewBag.HoanTra = db.HOANTRAs.Count();
            ViewBag.PhieuNhap = db.PHIEUNHAPs.Count();
            ViewBag.PhieuMuon = db.PHIEUMUONs.Count();
            ViewBag.PhieuTra = db.PHIEUTRAs.Count();

            var phieunhap = db.PHIEUNHAPs.OrderByDescending(n => n.MAPN).ToList();

            // Kết quả thống kê doanh thu
            ViewBag.ThongKe = 0;

            // Kết quả thống kê phiếu nhập
            ViewBag.ThongKePhieuNhap = 0;
            ViewBag.ListHoaDon = null;
            ViewBag.ListPhieuNhap = null;
            ViewBag.SUM = 0;
            ViewBag.Loi = null;
            return View(phieunhap);
        }

        [HttpPost]
        public ActionResult ThongKe(FormCollection f)
        {
            int iNgay = int.Parse(f["Ngay"].ToString());
            int iThang = int.Parse(f["Thang"].ToString());
            int iNam = int.Parse(f["Nam"].ToString());

            ViewBag.KiemDuyet = db.PHIEUNHAPs.Count();
            ViewBag.DoanhThu = TongTienDonHang();
            ViewBag.HoaChat = db.HOACHATs.Count();
            ViewBag.PhieuXuat = db.PHIEUXUATs.Count();
            ViewBag.HoanTra = db.HOANTRAs.Count();
            ViewBag.PhieuNhap = db.PHIEUNHAPs.Count();
            ViewBag.PhieuMuon = db.PHIEUMUONs.Count();
            ViewBag.PhieuTra = db.PHIEUTRAs.Count();
            ViewBag.ThanhLy = db.HOACHATs.OrderByDescending(n => n.LUONGTHANHLY).Count();

            // Thống Kê Theo Năm
            if (iNgay == 0 && iThang == 0)
            {
                ViewBag.ThongKe = DoanhThuNam(iNam);
                ViewBag.ThongKePhieuNhap = DoanhThuNam_Nhap(iNam);

                
                ViewBag.SUM = DoanhThuNam(iNam) - DoanhThuNam_Nhap(iNam);
            }
            // Thống kê tháng năm
            else if (iNgay == 0)
            {
                ViewBag.ThongKe = DoanhThuThangNam(iThang, iNam);
                ViewBag.ThongKePhieuNhap = DoanhThuThangNam_Nhap(iThang, iNam);

                
                ViewBag.SUM = DoanhThuThangNam(iThang, iNam) - DoanhThuThangNam_Nhap(iThang, iNam);
            }
            // Thống Kê Ngày Tháng Năm
            else
            {
                ViewBag.ThongKe = DoanhThuNgayThangNam(iNgay, iThang, iNam);
                ViewBag.ThongKePhieuNhap = DoanhThuNgayThangNam_Nhap(iNgay, iThang, iNam);

               
                ViewBag.SUM = DoanhThuNgayThangNam(iNgay, iThang, iNam) - DoanhThuNgayThangNam_Nhap(iNgay, iThang, iNam);
            }

            
            return View();

        }

        #region Thống kê ngày -> ngày
        [HttpPost]
        public ActionResult ThongKeTuan(FormCollection f)
        {
            int iNgayBatDau = int.Parse(f["NgayBatDau"].ToString());
            int iNgayKetThuc = int.Parse(f["NgayKetThuc"].ToString());
            int iThang = int.Parse(f["Thang"].ToString());

            ViewBag.KiemDuyet = db.PHIEUNHAPs.Count();
            ViewBag.DoanhThu = TongTienDonHang();
            ViewBag.HoaChat = db.HOACHATs.Count();
            ViewBag.PhieuXuat = db.PHIEUXUATs.Count();
            ViewBag.HoanTra = db.HOANTRAs.Count();
            ViewBag.PhieuNhap = db.PHIEUNHAPs.Count();
            ViewBag.PhieuMuon = db.PHIEUMUONs.Count();
            ViewBag.PhieuTra = db.PHIEUTRAs.Count();

            ViewBag.ThongKePhieuNhap = DoanhThuTuan(iNgayBatDau, iNgayKetThuc, iThang);

            ViewBag.listPhieuNhap = db.PHIEUNHAPs.Where(n => n.NGAYNHAP.Value.Day >= iNgayBatDau & n.NGAYNHAP.Value.Day <= iNgayKetThuc & n.NGAYNHAP.Value.Month == iThang).ToList();
            return View();
        }
        #endregion

        #region Thống kê quý
        [HttpPost]
        public ActionResult ThongKeQuy(FormCollection f)
        {
            int iThangBatDau = int.Parse(f["ThangBatDau"].ToString());
            int iNamBatDau = int.Parse(f["NamBatDau"].ToString());
            int iThangKetThuc = int.Parse(f["ThangKetThuc"].ToString());
            int iNamKetThuc = int.Parse(f["NamKetThuc"].ToString());

            ViewBag.KiemDuyet = db.PHIEUNHAPs.Count();
            ViewBag.DoanhThu = TongTienDonHang();
            ViewBag.HoaChat = db.HOACHATs.Count();
            ViewBag.PhieuXuat = db.PHIEUXUATs.Count();
            ViewBag.HoanTra = db.HOANTRAs.Count();
            ViewBag.PhieuNhap = db.PHIEUNHAPs.Count();
            ViewBag.PhieuMuon = db.PHIEUMUONs.Count();
            ViewBag.PhieuTra = db.PHIEUTRAs.Count();


            ViewBag.ThongKePhieuNhap = DoanhThuThangDenThang(iThangBatDau, iNamBatDau, iThangKetThuc, iNamKetThuc);
            var listPhieuNhap = db.PHIEUNHAPs.Where(n => n.NGAYNHAP.Value.Month >= iThangBatDau & n.NGAYNHAP.Value.Year >= iNamBatDau & n.NGAYNHAP.Value.Month <= iThangKetThuc & n.NGAYNHAP.Value.Year <= iNamKetThuc).ToList();

            return View(listPhieuNhap);
            #endregion
        }

        public ActionResult Statistical(DateTime Date_From, DateTime Date_to)
        {

            // Hiển thị danh sách phiếu mượn trong khoảng
            ViewBag.NguoiMuon = db.NGUOIDUNGs.Where(n => n.MAQUYEN == 1).ToList();
            ViewBag.lstPhieuMuon = db.PHIEUMUONs.Where(n => DateTime.Compare(n.NGAYMUON, Date_From) > 0 & DateTime.Compare(n.NGAYTRA, Date_to) < 0).ToList();
            ViewBag.total_PhieuMuon = db.PHIEUMUONs.Where(n => DateTime.Compare(n.NGAYMUON, Date_From) > 0 & DateTime.Compare(n.NGAYTRA, Date_to) < 0).ToList().Count();

            // Hiển thị danh sách phiếu trả trong khoảng
            ViewBag.NguoiTra = db.NGUOIDUNGs.Where(n => n.MAQUYEN == 1).ToList();
            ViewBag.lstPhieuTra = db.PHIEUTRAs.Where(n => DateTime.Compare(n.NGAYTRA, Date_From) > 0).ToList();
            ViewBag.total_PhieuTra = db.PHIEUTRAs.Where(n => DateTime.Compare(n.NGAYTRA, Date_From) > 0).ToList().Count();

            return View();
            
        }    

        public ActionResult Statistical2(DateTime Date_From, DateTime Date_to)
        {
            // Hiển thị danh sách phiếu nhập về khoa trong khoảng
            ViewBag.NguoiNhap = db.NGUOIDUNGs.Where(n => n.MAQUYEN == 1).ToList();
            ViewBag.lstPhieuNhap_Khoa = db.PHIEUNHAP_KHOA.Where(n => DateTime.Compare(n.NGAYNHAP, Date_From) > 0).ToList();
            ViewBag.total_PhieuNhap_Khoa = db.PHIEUNHAP_KHOA.Where(n => DateTime.Compare(n.NGAYNHAP, Date_From) > 0).ToList().Count();

            // Hiển thị danh sách phiếu thanh lý trang thiết bị trong khoảng
            ViewBag.NguoiTL = db.NGUOIDUNGs.Where(n => n.MAQUYEN ==1).ToList();
            ViewBag.lstPhieuTL = db.PHIEUTHANHLies.Where(n => DateTime.Compare(n.NGAYTL, Date_From) > 0).ToList();
            ViewBag.total_ThanhLy = db.PHIEUTHANHLies.Where(n => DateTime.Compare(n.NGAYTL, Date_From) > 0).ToList().Count();
            return View();
        }
    }
}