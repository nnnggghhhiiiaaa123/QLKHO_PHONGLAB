using QL_HDPHONGLAB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_HDPHONGLAB.Controllers
{
    public class TraTTBController : Controller
    {
        private QL_HDPHONGLABEntities db = new QL_HDPHONGLABEntities();
        #region Trả Hoá Chất
        // Lấy danh sách trả hoá chất
        public List<DsTraHC> LayHoaChat()
        {
            List<DsTraHC> lstHC = Session["DsTraHC"] as List<DsTraHC>;
            if (lstHC == null)
            {
                // Nếu danh sách hoá chất chưa trả thì khởi tạo list trả
                lstHC = new List<DsTraHC>();
                Session["DsTraHC"] = lstHC;
            }
            return lstHC;
        }

        // Thêm mục danh sách mượn
        public ActionResult ThemDSTraHC(string iMahc, string strURL)
        {
            HOACHAT hc = db.HOACHATs.SingleOrDefault(n => n.MAHC == iMahc);
            if (hc == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Lấy ra session danh sách
            List<DsTraHC> lstGioHang = LayHoaChat();
            //Kiểm tra sp này đã tồn tại trong session[hoachat] chưa
            DsTraHC hoachat = lstGioHang.Find(n => n.MAHC == iMahc);
            if (hoachat == null)
            {
                hoachat = new DsTraHC(iMahc);
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

        // Cập nhật danh sách trả
        public ActionResult CapNhatDSTraHC(string iMahc, FormCollection f)
        {
            // Kiểm tra hoá chất
            HOACHAT hc = db.HOACHATs.SingleOrDefault(n => n.MAHC == iMahc);

            // Nếu lấy sai mã hoá chất thì sẽ trả về lỗi 404
            if (hc == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            // Lấy danh sách trả ra từ session
            List<DsTraHC> listTra = LayHoaChat();
            // Kiểm tra hoá chất đã tồn tại trong session chưa
            DsTraHC hoachat = listTra.SingleOrDefault(n => n.MAHC == iMahc);

            // Nếu mà tồn tại thì sửa số lượng
            if (hoachat != null)
            {
                hoachat.SOLUONG = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("DanhSachHC");
        }

        // Xoá danh sách mượn
        public ActionResult XoaDSTraHC(string iMahc)
        {
            // Kiểm tra hoá chất
            HOACHAT hc = db.HOACHATs.SingleOrDefault(n => n.MAHC == iMahc);

            // Nếu lấy sai mã hoá chất thì sẽ trả về lỗi 404
            if (hc == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            // Lấy danh sách trả ra từ session
            List<DsTraHC> listTra = LayHoaChat();

            DsTraHC hoachat = listTra.SingleOrDefault(n => n.MAHC == iMahc);

            // Nếu mà tồn tại thì xoá danh sách
            if (hoachat != null)
            {
                listTra.RemoveAll(n => n.MAHC == iMahc);
            }
            if (listTra.Count == 0)
            {
                return RedirectToAction("PhanLoai", "Home");
            }
            return RedirectToAction("DanhSachHC");
        }

        // Xây dựng trang danh sách trả hoá chất
        public ActionResult DanhSachHC()
        {
            if (Session["DsTraHC"] == null)
            {
                return RedirectToAction("PhanLoai", "Home");
            }
            List<DsTraHC> lstGioHang = LayHoaChat();
            return View(lstGioHang);
        }

        // Tính tổng số lượng mượn
        private int TongSoLuongHC()
        {
            int iTongSoLuong = 0;
            List<DsTraHC> listTra = Session["DsTraHC"] as List<DsTraHC>;
            if (listTra != null)
            {
                iTongSoLuong = listTra.Sum(n => n.SOLUONG);
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

        // Xây dựng 1 view cho người dùng chỉnh sửa danh sách trả hoá chất
        public ActionResult SuaDSHC()
        {
            if (Session["DsTraHC"] == null)
            {
                return RedirectToAction("DanhSachHC", "TraTTB");
            }
            List<DsTraHC> listTra = LayHoaChat();
            return View(listTra);

        }

        public List<DsMuonHC> LayDSHoaChatMuon()
        {
            List<DsMuonHC> lstHCMuon = Session["DsMuonHC"] as List<DsMuonHC>;
            if (lstHCMuon == null)
            {
                // Nếu danh sách hoá chất chưa trả thì khởi tạo list trả
                lstHCMuon = new List<DsMuonHC>();
                Session["DsMuonHC"] = lstHCMuon;
            }
            return lstHCMuon;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChapNhanHC([Bind(Include = "MAPT,NGAYTRA,NOIDUNG,CHAPNHAN,TINHTRANG,MAND")] PHIEUTRA model, IEnumerable<DsTraHC> lstModel)
        {

            #region Accept
            // Kiểm tra danh sách trả
            if (Session["DsTraHC"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            // Thêm phiếu trả
            PHIEUTRA pt = new PHIEUTRA();
            NGUOIDUNG nd = (NGUOIDUNG)Session["use"];
            List<DsTraHC> list = LayHoaChat();
            List<DsMuonHC> listMuon = LayDSHoaChatMuon();
            //pm.MAND = nd.MAND;
            pt.NGAYTRA = DateTime.Now;
            Console.WriteLine(pt);
            db.PHIEUTRAs.Add(pt);
            db.SaveChanges();

            // Thêm chi tiết phiếu trả
            foreach (var item in list)
            {
                CT_PHIEUTRA ctpt = new CT_PHIEUTRA();
                ctpt.MAPT = pt.MAPT;
                ctpt.MAHC = item.MAHC;
                ctpt.SOLUONG = item.SOLUONG;
                db.CT_PHIEUTRA.Add(ctpt);
                var hc = db.HOACHATs.SingleOrDefault(n => n.MAHC == item.MAHC);
                foreach(var view in listMuon)
                {
                    if(item.SOLUONG > view.SOLUONG)
                    {
                        return RedirectToAction("KhongTraHC", "Home");
                    }    
                    else if(Session["DsMuonHC"] == null)
                    {
                        ViewBag.Error = "Không có trong danh sách mượn nên không trả được";
                    }
                    else
                    {
                        hc.LUONGTON += item.SOLUONG;
                    }
                }    
            }
            db.SaveChanges();
            return RedirectToAction("ThongBao", "Home");
            #endregion
        }

        #endregion

        #region Trả Thiết Bị
        // Lấy danh sách mượn thiết bị
        public List<DsTraTB> LayThietBi()
        {
            List<DsTraTB> lstTB = Session["DsTraTB"] as List<DsTraTB>;
            if (lstTB == null)
            {
                // Nếu danh sách thiết bị chưa trả thì khởi tạo list mượn
                lstTB = new List<DsTraTB>();
                Session["DsTraTB"] = lstTB;
            }
            return lstTB;
        }

        // Thêm mục danh sách mượn
        public ActionResult ThemDSTraTB(string iMatb, string strUrl)
        {
            THIETBI tb = db.THIETBIs.SingleOrDefault(n => n.MATB == iMatb);
            if (tb == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            // Lấy session mượn thiết bị
            List<DsTraTB> listTra = LayThietBi();
            // Kiểm tra thiết bị đã tồn tại trong session chưa
            DsTraTB thietbi = listTra.Find(n => n.MATB == iMatb);
            if (thietbi == null)
            {
                thietbi = new DsTraTB(iMatb);
                // Thêm thiết bị mới vào list
                listTra.Add(thietbi);
                return Redirect(strUrl);
            }
            else
            {
                thietbi.SOLUONG++;
                return Redirect(strUrl);
            }
        }

        // Cập nhật danh sách mượn
        public ActionResult CapNhatDSTraTB(string iMatb, FormCollection f)
        {
            // Kiểm tra thiết bị
            THIETBI tb = db.THIETBIs.SingleOrDefault(n => n.MATB == iMatb);

            // Nếu lấy sai mã thiết bị thì sẽ trả về lỗi 404
            if (tb == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            // Lấy danh sách trả ra từ session
            List<DsTraTB> listTra = LayThietBi();
            // Kiểm tra thiết bị đã tồn tại trong session chưa
            DsTraTB hoachat = listTra.SingleOrDefault(n => n.MATB == iMatb);

            // Nếu mà tồn tại thì sửa số lượng
            if (hoachat != null)
            {
                hoachat.SOLUONG = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("DanhSachHC");
        }

        // Xoá danh sách trả
        public ActionResult XoaDSTraTB(string iMatb)
        {
            // Kiểm tra thiết bị
            THIETBI tb = db.THIETBIs.SingleOrDefault(n => n.MATB == iMatb);

            // Nếu lấy sai mã thiết bị thì sẽ trả về lỗi 404
            if (tb == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            // Lấy danh sách trả ra từ session
            List<DsTraTB> listTra = LayThietBi();

            DsTraTB thietbi = listTra.SingleOrDefault(n => n.MATB == iMatb);

            // Nếu mà tồn tại thì xoá danh sách
            if (thietbi != null)
            {
                listTra.RemoveAll(n => n.MATB == iMatb);
            }
            if (listTra.Count == 0)
            {
                return RedirectToAction("PhanLoai", "Home");
            }
            return RedirectToAction("DanhSachHC");
        }

        // Xây dựng trang danh sách trả thiết bị
        public ActionResult DanhSachTB()
        {
            if (Session["DsTraTB"] == null)
            {
                return RedirectToAction("PhanLoai", "Home");
            }
            List<DsTraTB> listTra = LayThietBi();
            return View(listTra);
        }

        // Tính tổng số lượng trả
        private int TongSoLuongTB()
        {
            int iTongSoLuong = 0;
            List<DsTraTB> listTra = Session["DsTraTB"] as List<DsTraTB>;
            if (listTra != null)
            {
                iTongSoLuong = listTra.Sum(n => n.SOLUONG);
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
        
        // Xây dựng 1 view cho người dùng chỉnh sửa danh sách trả thiết bị
        public ActionResult SuaDSTB()
        {
            if (Session["DsTraTB"] == null)
            {
                return RedirectToAction("DanhSachTB", "TraTTB");
            }
            List<DsTraTB> listTra = LayThietBi();
            return View(listTra);

        }

        public List<DsMuonTB> LayDSThietBiMuon()
        {
            List<DsMuonTB> lstTB = Session["DsMuonTB"] as List<DsMuonTB>;
            if (lstTB == null)
            {
                // Nếu danh sách thiết bị chưa trả thì khởi tạo list mượn
                lstTB = new List<DsMuonTB>();
                Session["DsMuonTB"] = lstTB;
            }
            return lstTB;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChapNhanTB([Bind(Include = "MAPT,NGAYTRA,NOIDUNG,CHAPNHAN,TINHTRANG,MAND")] PHIEUTRA model, IEnumerable<DsTraTB> lstModel)
        {

            #region Accept
            // Kiểm tra danh sách trả
            if (Session["DsTraTB"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            // Thêm phiếu trả
            PHIEUTRA pt = new PHIEUTRA();
            NGUOIDUNG nd = (NGUOIDUNG)Session["use"];
            List<DsTraTB> list = LayThietBi();
            List<DsMuonTB> listMuon = LayDSThietBiMuon();
            //pm.MAND = nd.MAND;
            pt.NGAYTRA = DateTime.Now;
            Console.WriteLine(pt);
            db.PHIEUTRAs.Add(pt);
            db.SaveChanges();

            // Thêm chi tiết phiếu trả
            foreach (var item in list)
            {
                CT_PHIEUTRA ctpt = new CT_PHIEUTRA();
                ctpt.MAPT = pt.MAPT;
                ctpt.MATB = item.MATB;
                ctpt.SOLUONG = item.SOLUONG;
                db.CT_PHIEUTRA.Add(ctpt);
                var tb = db.THIETBIs.SingleOrDefault(n => n.MATB == item.MATB);
                foreach (var view in listMuon)
                {
                    if (item.SOLUONG > view.SOLUONG)
                    {
                        return RedirectToAction("KhongTraTB", "Home");
                    }
                    else if (Session["DsMuonTB"] == null)
                    {
                        ViewBag.Error = "Không có trong danh sách mượn nên không trả được";
                    }
                    else
                    {
                        tb.SLTON += item.SOLUONG;
                    }
                }
            }
            db.SaveChanges();
            return RedirectToAction("ThongBao", "Home");
            #endregion
        }


        #endregion

        #region Trả Dụng Cụ
        // Lấy danh sách mượn hoá chất
        public List<DsTraDC> LayDungCu()
        {
            List<DsTraDC> lstDC = Session["DsTraDC"] as List<DsTraDC>;
            if (lstDC == null)
            {
                // Nếu danh sách dụng cụ chưa trả thì khởi tạo list trả
                lstDC = new List<DsTraDC>();
                Session["DsTraDC"] = lstDC;
            }
            return lstDC;
        }

        // Thêm mục danh sách trả
        public ActionResult ThemDSTraDC(string iMadc, string strUrl)
        {
            DUNGCU dc = db.DUNGCUs.SingleOrDefault(n => n.MADC == iMadc);
            if (dc == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            // Lấy session trả dụng cụ
            List<DsTraDC> listTra = LayDungCu();
            // Kiểm tra dụng cụ đã tồn tại trong session chưa
            DsTraDC dungcu = listTra.Find(n => n.MADC == iMadc);
            if (dungcu == null)
            {
                dungcu = new DsTraDC(iMadc);
                // Thêm hoá chất mới vào list
                listTra.Add(dungcu);
                return Redirect(strUrl);
            }
            else
            {
                dungcu.SOLUONG++;
                return Redirect(strUrl);
            }
        }

        // Cập nhật danh sách trả
        public ActionResult CapNhatDSTraDC(string iMadc, FormCollection f)
        {
            // Kiểm tra dụng cụ
            DUNGCU dc = db.DUNGCUs.SingleOrDefault(n => n.MADC == iMadc);

            // Nếu lấy sai mã dụng cụ thì sẽ trả về lỗi 404
            if (dc == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            // Lấy danh sách trả ra từ session
            List<DsTraDC> listTra = LayDungCu();
            // Kiểm tra dụng cụ đã tồn tại trong session chưa
            DsTraDC dungcu = listTra.SingleOrDefault(n => n.MADC == iMadc);

            // Nếu mà tồn tại thì sửa số lượng
            if (dungcu != null)
            {
                dungcu.SOLUONG = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("DanhSachDC");
        }

        // Xoá danh sách trả
        public ActionResult XoaDSTraDC(string iMadc)
        {
            // Kiểm tra dụng cụ
            DUNGCU hc = db.DUNGCUs.SingleOrDefault(n => n.MADC == iMadc);

            // Nếu lấy sai mã dụng cụ thì sẽ trả về lỗi 404
            if (hc == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            // Lấy danh sách trả ra từ session
            List<DsTraDC> listTra = LayDungCu();

            DsTraDC dungcu = listTra.SingleOrDefault(n => n.MADC == iMadc);

            // Nếu mà tồn tại thì xoá danh sách
            if (dungcu != null)
            {
                listTra.RemoveAll(n => n.MADC == iMadc);
            }
            if (listTra.Count == 0)
            {
                return RedirectToAction("PhanLoai", "Home");
            }
            return RedirectToAction("DanhSachDC");
        }

        // Xây dựng trang danh sách trả hoá chất
        public ActionResult DanhSachDC()
        {
            if (Session["DsTraDC"] == null)
            {
                return RedirectToAction("PhanLoai", "Home");
            }
            List<DsTraDC> listTra = LayDungCu();
            return View(listTra);
        }

        // Tính tổng số lượng trả
        private int TongSoLuongDC()
        {
            int iTongSoLuong = 0;
            List<DsTraDC> listTra = Session["DsTraDC"] as List<DsTraDC>;
            if (listTra != null)
            {
                iTongSoLuong = listTra.Sum(n => n.SOLUONG);
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

        // Xây dựng 1 view cho người dùng chỉnh sửa danh sách trả dụng cụ
        public ActionResult SuaDSDC()
        {
            if (Session["DsTraDC"] == null)
            {
                return RedirectToAction("DanhSachDC", "TraTTB");
            }
            List<DsTraDC> listTra = LayDungCu();
            return View(listTra);

        }


        public List<DsMuonDC> LayDSDungCuMuon()
        {
            List<DsMuonDC> lstDC = Session["DsMuonDC"] as List<DsMuonDC>;
            if (lstDC == null)
            {
                // Nếu danh sách dụng cụ chưa trả thì khởi tạo list trả
                lstDC = new List<DsMuonDC>();
                Session["DsMuonDC"] = lstDC;
            }
            return lstDC;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChapNhanDC([Bind(Include = "MAPT,NGAYTRA,NOIDUNG,CHAPNHAN,TINHTRANG,MAND")] PHIEUTRA model, IEnumerable<DsTraDC> lstModel)
        {

            #region Accept
            // Kiểm tra danh sách trả
            if (Session["DsTraDC"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            // Thêm phiếu trả
            PHIEUTRA pt = new PHIEUTRA();
            NGUOIDUNG nd = (NGUOIDUNG)Session["use"];
            List<DsTraDC> list = LayDungCu();
            List<DsMuonDC> listMuon = LayDSDungCuMuon();
            //pm.MAND = nd.MAND;
            pt.NGAYTRA = DateTime.Now;
            Console.WriteLine(pt);
            db.PHIEUTRAs.Add(pt);
            db.SaveChanges();

            // Thêm chi tiết phiếu trả
            foreach (var item in list)
            {
                CT_PHIEUTRA ctpt = new CT_PHIEUTRA();
                ctpt.MAPT = pt.MAPT;
                ctpt.MADC = item.MADC;
                ctpt.SOLUONG = item.SOLUONG;
                db.CT_PHIEUTRA.Add(ctpt);
                var dc = db.DUNGCUs.SingleOrDefault(n => n.MADC == item.MADC);
                foreach (var view in listMuon)
                {
                    if (item.SOLUONG > view.SOLUONG)
                    {
                        return RedirectToAction("KhongTraHC", "Home");
                    }
                    else if (Session["DsMuonDC"] == null)
                    {
                        ViewBag.Error = "Không có trong danh sách mượn nên không trả được";
                    }
                    else
                    {
                        dc.LUONGTON += item.SOLUONG;
                    }
                }
            }
            db.SaveChanges();
            return RedirectToAction("ThongBao", "Home");
            #endregion
        }
        #endregion
    }
}