using QL_HDPHONGLAB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_HDPHONGLAB.Areas.Admin.Controllers
{
    public class PhieuHoanTraController : Controller
    {
        private QL_HDPHONGLABEntities db;

        public PhieuHoanTraController()
        {
            db = new QL_HDPHONGLABEntities();
        }

        public ActionResult DanhSach()
        {
            var list = db.HOANTRAs.OrderByDescending(n => n.MAHT).ToList();
            return View(list);
        }

        // Xem Chi Tiết Phiếu Hoàn Trả
        public ActionResult XemChiTiet(int maht)
        {
            var hoantra = db.HOANTRAs.SingleOrDefault(n => n.MAHT == maht);
            ViewBag.HoaChatTra = db.TTBTRAs.Where(n => n.MAHT_id == maht);
            return View(hoantra);
        }

        // Xoá phiếu trả
        public ActionResult XoaPhieuTra(int maht)
        {
            var listHoantra = db.TTBTRAs.Where(n => n.MAHT_id == maht);
            if (listHoantra != null)
            {
                foreach (var item in listHoantra)
                {
                    db.TTBTRAs.Remove(item);
                }
            }
            var phieutra = db.HOANTRAs.SingleOrDefault(n => n.MAHT == maht);
            db.HOANTRAs.Remove(phieutra);
            db.SaveChanges();
            return RedirectToAction("DanhSach", "PhieuHoanTra");
        }

        #region Thêm mới hoàn trả kho tttnth
        [HttpGet]
        public ActionResult PhieuHoanTraKho()
        {
            ViewBag.HoaChat = db.HOACHATs.OrderBy(n => n.TENHC).ToList();
            ViewBag.NguoiDuyet = db.NGUOIDUNGs.Where(n => n.MAQUYEN == 4).ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PhieuHoanTraKho([Bind(Include = "MAHT,NGAYTRA,NOIDUNG,NGUOITRA,TU,DEN,GHICHU")] HOANTRA model, IEnumerable<TTBTRA> listmodel)
        {
            if(ModelState.IsValid)
            {
                #region Lưu vào csdl phiếu hoàn trả kho
                HOANTRA ht = new HOANTRA();
                if (model.NGAYTRA == null)
                {
                    model.NGAYTRA = DateTime.Now;
                }
                else
                {
                    ht.NGAYTRA = model.NGAYTRA;
                }
                db.HOANTRAs.Add(model);
                db.SaveChanges();
                #endregion

                // Lấy mã hoàn trả để xuất kho
                var mahoantra = db.HOANTRAs.OrderByDescending(n => n.MAHT).FirstOrDefault();
                foreach (var item in listmodel)
                {
                    // Tìm mã hoá chất
                    var hoachat = db.HOACHATs.SingleOrDefault(n => n.MAHC == item.MAHC_id);
                    // Kiểm tra nếu nhập số lẽ với hoá chất
                    //TTBTRA tra = new TTBTRA();
                    //tra.MAHT_id = mahoantra.MAHT;
                    //tra.MAHC_id = item.MAHC_id;
                    //tra.MATB_id = item.MATB_id;
                    //tra.MADC_id = item.MADC_id;
                    //tra.SOLUONGTRA = item.SOLUONGTRA;
                    //db.TTBTRAs.Add(tra);
                    //db.SaveChanges();

                    #region Trừ số lượng số lượng trong kho
                    var slHoaChat = db.HOACHATs.SingleOrDefault(n => n.MAHC == item.MAHC_id);
                    slHoaChat.LUONGTON = slHoaChat.LUONGTON - item.SOLUONGTRA;
                    db.SaveChanges();
                    #endregion
                }
                //db.SaveChanges();
                return RedirectToAction("DanhSach", "PhieuHoanTra");
            }
            return View(model);
        }    
        #endregion
    }
}