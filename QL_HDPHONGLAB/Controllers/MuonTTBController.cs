using QL_HDPHONGLAB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_HDPHONGLAB.Controllers
{
    public class MuonTTBController : Controller
    {
        private QL_HDPHONGLABEntities db = new QL_HDPHONGLABEntities();

        #region Mượn Hoá Chất
        // Lấy danh sách mượn hoá chất
        public List<DsMuonHC> LayHoaChat()
        {
            List<DsMuonHC> lstHC = Session["DsMuonHC"] as List<DsMuonHC>;
            if(lstHC == null)
            {
                // Nếu danh sách hoá chất chưa mượn thì khởi tạo list mượn
                lstHC = new List<DsMuonHC>();
                Session["DsMuonHC"] = lstHC;
            }
            return lstHC;
        }

        // Thêm mục danh sách mượn
        public ActionResult ThemDSMuonHC(string iMahc, string strURL)
        {
            HOACHAT hc = db.HOACHATs.SingleOrDefault(n => n.MAHC == iMahc);
            if (hc == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Lấy ra session giỏ hàng
            List<DsMuonHC> lstGioHang = LayHoaChat();
            //Kiểm tra sp này đã tồn tại trong session[hoachat] chưa
            DsMuonHC hoachat = lstGioHang.Find(n => n.MAHC == iMahc);
            if (hoachat == null)
            {
                hoachat = new DsMuonHC(iMahc);
                //Add sản phẩm mới thêm vào list
                lstGioHang.Add(hoachat);
                return Redirect(strURL);
            }
            else
            {
                hoachat.SOLUONG++;
                return Redirect(strURL);
            }
        }

        // Cập nhật danh sách mượn
        public ActionResult CapNhatDSMuonHC(string iMahc, FormCollection f)
        {
            // Kiểm tra hoá chất
            HOACHAT hc = db.HOACHATs.SingleOrDefault(n => n.MAHC == iMahc);

            // Nếu lấy sai mã hoá chất thì sẽ trả về lỗi 404
            if(hc == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            // Lấy danh sách mượn ra từ session
            List<DsMuonHC> listMuon = LayHoaChat();
            // Kiểm tra hoá chất đã tồn tại trong session chưa
            DsMuonHC hoachat = listMuon.SingleOrDefault(n => n.MAHC == iMahc);

            // Nếu mà tồn tại thì sửa số lượng
            if(hoachat != null)
            {
                hoachat.SOLUONG = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("DanhSachHC");
        }

        // Xoá danh sách mượn
        public ActionResult XoaDSMuonHC(string iMahc)
        {
            // Kiểm tra hoá chất
            HOACHAT hc = db.HOACHATs.SingleOrDefault(n => n.MAHC == iMahc);

            // Nếu lấy sai mã hoá chất thì sẽ trả về lỗi 404
            if (hc == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            // Lấy danh sách mượn ra từ session
            List<DsMuonHC> listMuon = LayHoaChat();

            DsMuonHC hoachat = listMuon.SingleOrDefault(n => n.MAHC == iMahc);

            // Nếu mà tồn tại thì xoá danh sách
            if(hoachat != null)
            {
                listMuon.RemoveAll(n => n.MAHC == iMahc);
            }    
            if(listMuon.Count == 0)
            {
                return RedirectToAction("PhanLoai", "Home");
            }
            return RedirectToAction("DanhSachHC");
        }

        // Xây dựng trang danh sách mượn hoá chất
        public ActionResult DanhSachHC()
        {
            if (Session["DsMuonHC"] == null)
            {
                return RedirectToAction("PhanLoai", "Home");
            }
            List<DsMuonHC> lstGioHang = LayHoaChat();
            return View(lstGioHang);
        }

        // Tính tổng số lượng mượn
        private int TongSoLuongHC()
        {
            int iTongSoLuong = 0;
            List<DsMuonHC> listMuon = Session["DsMuonHC"] as List<DsMuonHC>;
            if (listMuon != null)
            {
                iTongSoLuong = listMuon.Sum(n => n.SOLUONG);
            }
            return iTongSoLuong;
        }

        // Tạo partial Danh Sach hoá chất
        public ActionResult DanhSachHCPartial()
        {
            if (TongSoLuongHC() == 0)
            {
                return PartialView();
            }
            ViewBag.TongSoLuongHC = TongSoLuongHC();
            return PartialView();
        }

        // Xây dựng 1 view cho người dùng chỉnh sửa danh sách mượn hoá chất
        public ActionResult SuaDSHC()
        {
            if (Session["DsMuonHC"] == null)
            {
                return RedirectToAction("DanhSachHC", "MuonTTB");
            }
            List<DsMuonHC> listMuon = LayHoaChat();
            return View(listMuon);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChapNhanHC([Bind(Include = "MAPM,NGAYMUON,NGAYTRA,NOIDUNG,CHAPNHAN,TINHTRANG,MAND")] PHIEUMUON model, IEnumerable<DsMuonHC> lstModel)
        {

            #region Accept
            // Kiểm tra danh sách mượn
            if(Session["DsMuonHC"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            // Thêm phiếu mượn
            PHIEUMUON pm = new PHIEUMUON();
            NGUOIDUNG nd = (NGUOIDUNG)Session["use"];
            List<DsMuonHC> list = LayHoaChat();
            //pm.MAND = nd.MAND;
            pm.NGAYMUON = DateTime.Now;
            pm.NGAYTRA = DateTime.Now;
            Console.WriteLine(pm);
            db.PHIEUMUONs.Add(pm);
            db.SaveChanges();

            // Thêm chi tiết phiếu mượn
            foreach(var item in list)
            {
                CT_PHIEUMUON ctpm = new CT_PHIEUMUON();
                ctpm.MAPM = pm.MAPM;
                ctpm.MAHC = item.MAHC;
                ctpm.SOLUONG = item.SOLUONG;
                db.CT_PHIEUMUON.Add(ctpm);
                var hc = db.HOACHATs.SingleOrDefault(n => n.MAHC == item.MAHC);
                if (item.SOLUONG > hc.LUONGTON)
                {
                    return RedirectToAction("KhongChoMuonHC", "Home");
                   
                }
                else
                {
                    hc.LUONGTON -= item.SOLUONG;
                }
            }
            db.SaveChanges();
            return RedirectToAction("ThongBao", "Home");
            #endregion
        }

        #endregion

        #region Mượn Thiết Bị
        // Lấy danh sách mượn thiết bị
        public List<DsMuonTB> LayThietBi()
        {
            List<DsMuonTB> lstTB = Session["DsMuonTB"] as List<DsMuonTB>;
            if (lstTB == null)
            {
                // Nếu danh sách thiết bị chưa mượn thì khởi tạo list mượn
                lstTB = new List<DsMuonTB>();
                Session["DsMuonTB"] = lstTB;
            }
            return lstTB;
        }

        // Thêm mục danh sách mượn
        public ActionResult ThemDSMuonTB(string iMatb, string strUrl)
        {
            THIETBI tb = db.THIETBIs.SingleOrDefault(n => n.MATB == iMatb);
            if (tb == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            // Lấy session mượn thiết bị
            List<DsMuonTB> listMuon = LayThietBi();
            // Kiểm tra thiết bị đã tồn tại trong session chưa
            DsMuonTB thietbi = listMuon.Find(n => n.MATB == iMatb);
            if (thietbi == null)
            {
                thietbi = new DsMuonTB(iMatb);
                // Thêm thiết bị mới vào list
                listMuon.Add(thietbi);
                return Redirect(strUrl);
            }
            else
            {
                thietbi.SOLUONG++;
                return Redirect(strUrl);
            }
        }

        // Cập nhật danh sách mượn
        public ActionResult CapNhatDSMuonTB(string iMatb, FormCollection f)
        {
            // Kiểm tra thiết bị
            THIETBI tb = db.THIETBIs.SingleOrDefault(n => n.MATB == iMatb);

            // Nếu lấy sai mã thiết bị thì sẽ trả về lỗi 404
            if (tb == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            // Lấy danh sách mượn ra từ session
            List<DsMuonTB> listMuon = LayThietBi();
            // Kiểm tra thiết bị đã tồn tại trong session chưa
            DsMuonTB hoachat = listMuon.SingleOrDefault(n => n.MATB == iMatb);

            // Nếu mà tồn tại thì sửa số lượng
            if (hoachat != null)
            {
                hoachat.SOLUONG = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("DanhSachTB");
        }

        // Xoá danh sách mượn
        public ActionResult XoaDSMuonTB(string iMatb)
        {
            // Kiểm tra thiết bị
            THIETBI tb = db.THIETBIs.SingleOrDefault(n => n.MATB == iMatb);

            // Nếu lấy sai mã thiết bị thì sẽ trả về lỗi 404
            if (tb == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            // Lấy danh sách mượn ra từ session
            List<DsMuonTB> listMuon = LayThietBi();

            DsMuonTB thietbi = listMuon.SingleOrDefault(n => n.MATB == iMatb);

            // Nếu mà tồn tại thì xoá danh sách
            if (thietbi != null)
            {
                listMuon.RemoveAll(n => n.MATB == iMatb);
            }
            if (listMuon.Count == 0)
            {
                return RedirectToAction("PhanLoai", "Home");
            }
            return RedirectToAction("DanhSachTB");
        }

        // Xây dựng trang danh sách mượn thiết bị
        public ActionResult DanhSachTB()
        {
            if (Session["DsMuonTB"] == null)
            {
                return RedirectToAction("PhanLoai", "Home");
            }
            List<DsMuonTB> listMuon = LayThietBi();
            return View(listMuon);
        }

        // Tính tổng số lượng mượn
        private int TongSoLuongTB()
        {
            int iTongSoLuong = 0;
            List<DsMuonTB> listMuon = Session["DsMuonTB"] as List<DsMuonTB>;
            if (listMuon != null)
            {
                iTongSoLuong = listMuon.Sum(n => n.SOLUONG);
            }
            return iTongSoLuong;
        }

        // Tạo partial Danh Sach dụng cụ
        public ActionResult DanhSachTBPartial()
        {
            if (TongSoLuongTB() == 0)
            {
                return PartialView();
            }
            ViewBag.TongSoLuongTB = TongSoLuongTB();
            return PartialView();
        }

        // Xây dựng 1 view cho người dùng chỉnh sửa danh sách mượn thiết bị
        public ActionResult SuaDSTB()
        {
            if (Session["DsMuonTB"] == null)
            {
                return RedirectToAction("DanhSachTB", "MuonTTB");
            }
            List<DsMuonTB> listMuon = LayThietBi();
            return View(listMuon);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChapNhanTB([Bind(Include = "MAPM,NGAYMUON,NGAYTRA,NOIDUNG,CHAPNHAN,TINHTRANG,MAND")] PHIEUMUON model, IEnumerable<DsMuonTB> lstModel)
        {
            #region Accept
            // Kiểm tra danh sách mượn
            if (Session["DsMuonTB"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            // Thêm phiếu mượn
            PHIEUMUON pm = new PHIEUMUON();
            NGUOIDUNG nd = (NGUOIDUNG)Session["use"];
            List<DsMuonTB> list = LayThietBi();
            //pm.MAND = nd.MAND;
            pm.NGAYMUON = DateTime.Now;
            pm.NGAYTRA = DateTime.Now;
            Console.WriteLine(pm);
            db.PHIEUMUONs.Add(pm);
            db.SaveChanges();

            // Thêm chi tiết phiếu mượn
            foreach (var item in list)
            {
                CT_PHIEUMUON ctpm = new CT_PHIEUMUON();
                ctpm.MAPM = pm.MAPM;
                ctpm.MATB = item.MATB;
                ctpm.SOLUONG = item.SOLUONG;
                db.CT_PHIEUMUON.Add(ctpm);
                var tb = db.THIETBIs.SingleOrDefault(n => n.MATB == item.MATB);
                if(item.SOLUONG > tb.SLTON)
                {
                    return RedirectToAction("KhongChoMuonTB", "Home");
                }
                else
                {
                    tb.SLTON -= item.SOLUONG;
                }
            }
            db.SaveChanges();
            return RedirectToAction("ThongBao", "Home");
            #endregion
        }
        #endregion

        #region Mượn Dụng Cụ
        // Lấy danh sách mượn hoá chất
        public List<DsMuonDC> LayDungCu()
        {
            List<DsMuonDC> lstDC = Session["DsMuonDC"] as List<DsMuonDC>;
            if (lstDC == null)
            {
                // Nếu danh sách dụng cụ chưa mượn thì khởi tạo list mượn
                lstDC = new List<DsMuonDC>();
                Session["DsMuonDC"] = lstDC;
            }
            return lstDC;
        }

        // Thêm mục danh sách mượn
        public ActionResult ThemDSMuonDC(string iMadc, string strUrl)
        {
            DUNGCU dc = db.DUNGCUs.SingleOrDefault(n => n.MADC == iMadc);
            if (dc == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            // Lấy session mượn dụng cụ
            List<DsMuonDC> listMuon = LayDungCu();
            // Kiểm tra dụng cụ đã tồn tại trong session chưa
            DsMuonDC dungcu = listMuon.Find(n => n.MADC == iMadc);
            if (dungcu == null)
            {
                dungcu = new DsMuonDC(iMadc);
                // Thêm hoá chất mới vào list
                listMuon.Add(dungcu);
                return Redirect(strUrl);
            }
            else
            {
                dungcu.SOLUONG++;
                return Redirect(strUrl);
            }
        }

        // Cập nhật danh sách mượn
        public ActionResult CapNhatDSMuonDC(string iMadc, FormCollection f)
        {
            // Kiểm tra dụng cụ
            DUNGCU dc = db.DUNGCUs.SingleOrDefault(n => n.MADC == iMadc);

            // Nếu lấy sai mã dụng cụ thì sẽ trả về lỗi 404
            if (dc == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            // Lấy danh sách mượn ra từ session
            List<DsMuonDC> listMuon = LayDungCu();
            // Kiểm tra dụng cụ đã tồn tại trong session chưa
            DsMuonDC dungcu = listMuon.SingleOrDefault(n => n.MADC == iMadc);

            // Nếu mà tồn tại thì sửa số lượng
            if (dungcu != null)
            {
                dungcu.SOLUONG = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("DanhSachDC");
        }

        // Xoá danh sách mượn
        public ActionResult XoaDSMuonDC(string iMadc)
        {
            // Kiểm tra dụng cụ
            DUNGCU hc = db.DUNGCUs.SingleOrDefault(n => n.MADC == iMadc);

            // Nếu lấy sai mã dụng cụ thì sẽ trả về lỗi 404
            if (hc == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            // Lấy danh sách mượn ra từ session
            List<DsMuonDC> listMuon = LayDungCu();

            DsMuonDC dungcu = listMuon.SingleOrDefault(n => n.MADC == iMadc);

            // Nếu mà tồn tại thì xoá danh sách
            if (dungcu != null)
            {
                listMuon.RemoveAll(n => n.MADC == iMadc);
            }
            if (listMuon.Count == 0)
            {
                return RedirectToAction("PhanLoai", "Home");
            }
            return RedirectToAction("DanhSachDC");
        }

        // Xây dựng trang danh sách mượn hoá chất
        public ActionResult DanhSachDC()
        {
            if (Session["DsMuonDC"] == null)
            {
                return RedirectToAction("PhanLoai", "Home");
            }
            List<DsMuonDC> listMuon = LayDungCu();
            return View(listMuon);
        }

        // Tính tổng số lượng mượn
        private int TongSoLuongDC()
        {
            int iTongSoLuong = 0;
            List<DsMuonDC> listMuon = Session["DsMuonDC"] as List<DsMuonDC>;
            if (listMuon != null)
            {
                iTongSoLuong = listMuon.Sum(n => n.SOLUONG);
            }
            return iTongSoLuong;
        }

        // Tạo partial Danh Sach hoá chất
        public ActionResult DanhSachDCPartial()
        {
            if (TongSoLuongDC() == 0)
            {
                return PartialView();
            }
            ViewBag.TongSoLuongDC = TongSoLuongDC();
            return PartialView();
        }

        // Xây dựng 1 view cho người dùng chỉnh sửa danh sách mượn dụng cụ
        public ActionResult SuaDSDC()
        {
            if (Session["DsMuonDC"] == null)
            {
                return RedirectToAction("DanhSachDC", "MuonTTB");
            }
            List<DsMuonDC> listMuon = LayDungCu();
            return View(listMuon);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChapNhanDC([Bind(Include = "MAPM,NGAYMUON,NGAYTRA,NOIDUNG,CHAPNHAN,TINHTRANG,MAND")] PHIEUMUON model, IEnumerable<DsMuonDC> lstModel)
        {
            #region Accept
            // Kiểm tra danh sách mượn
            if (Session["DsMuonDC"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            // Thêm phiếu mượn
            PHIEUMUON pm = new PHIEUMUON();
            NGUOIDUNG nd = (NGUOIDUNG)Session["use"];
            List<DsMuonDC> list = LayDungCu();
            //pm.MAND = nd.MAND;
            pm.NGAYMUON = DateTime.Now;
            pm.NGAYTRA = DateTime.Now;
            Console.WriteLine(pm);
            db.PHIEUMUONs.Add(pm);
            db.SaveChanges();

            // Thêm chi tiết phiếu mượn
            foreach (var item in list)
            {
                CT_PHIEUMUON ctpm = new CT_PHIEUMUON();
                ctpm.MAPM = pm.MAPM;
                ctpm.MADC = item.MADC;
                ctpm.SOLUONG = item.SOLUONG;
                db.CT_PHIEUMUON.Add(ctpm);
                var dc = db.DUNGCUs.SingleOrDefault(n => n.MADC == item.MADC);
                if(item.SOLUONG > dc.LUONGTON)
                {
                    return RedirectToAction("KhongChoMuonDC", "Home");
                }
                else { dc.LUONGTON -= item.SOLUONG; }
            }
            db.SaveChanges();
            return RedirectToAction("ThongBao", "Home");
            #endregion
        }
        #endregion

    }
}