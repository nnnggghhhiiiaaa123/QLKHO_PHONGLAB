using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OfficeOpenXml;
using PagedList;
using QL_HDPHONGLAB.Models;

namespace QL_HDPHONGLAB.Areas.Admin.Controllers
{
    public class HOACHATsController : Controller
    {
        private QL_HDPHONGLABEntities db = new QL_HDPHONGLABEntities();

        // GET: Admin/HOACHATs
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.MaSortParm = String.IsNullOrEmpty(sortOrder) ? "ma_desc" : "";
            ViewBag.TenSortParm = sortOrder == "ten" ? "ten_desc" : "ten";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            var hOACHAT = db.HOACHATs.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                hOACHAT = hOACHAT.Where(s => s.MAHC.Contains(searchString)
                                       || s.TENHC.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "ma_desc":
                    hOACHAT = hOACHAT.OrderByDescending(s => s.MAHC);
                    break;
                case "ten":
                    hOACHAT = hOACHAT.OrderBy(s => s.TENHC);
                    break;
                case "ten_desc":
                    hOACHAT = hOACHAT.OrderByDescending(s => s.TENHC);
                    break;
                default:
                    hOACHAT = hOACHAT.OrderBy(s => s.MAHC);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            //var hOACHAT = db.HOACHAT.Include(h => h.CT_HOACHAT);
            ViewBag.total = (from HOACHAT in db.HOACHATs select HOACHAT.LUONGTON).Sum();
            //ViewBag.ListHoaChat = db.HOACHATs.OrderBy(n => n.MAHC).ToList();
            return View(hOACHAT.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DanhSach([Bind(Include = "MATL,NGAYTL,NGUOIDUYET,NOIDUNG,GHICHU")] PHIEUTHANHLY model, IEnumerable<CHITIETPHIEUTHANHLY> lstModel)
        {
            if (ModelState.IsValid)
            {
                #region Lưu ngày thanh lý vào phiếu
                if (model.NGAYTL == null)
                {
                    model.NGAYTL = DateTime.Now;
                }
                db.PHIEUTHANHLies.Add(model);
                db.SaveChanges();
                #endregion

                foreach(var item in lstModel)
                {
                    // Lấy mã thanh lý
                    item.MATL = model.MATL;

                    // Tìm mã hoá chất
                    #region Trừ lượng tồn tăng lượng thanh lý
                    var luongton = db.HOACHATs.SingleOrDefault(n => n.MAHC == item.MAHC);
                    luongton.LUONGTON -= item.SLTON;

                    var luongthanhly = db.HOACHATs.SingleOrDefault(n => n.MAHC == item.MAHC);
                    luongthanhly.LUONGTHANHLY += item.SLTHANHLY;
                    #endregion
                }
                db.SaveChanges();
                return RedirectToAction("Index", "HOACHATs");
            }
            return View(model);
        }

        // GET: Admin/HOACHATs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOACHAT hOACHAT = db.HOACHATs.Find(id);
            if (hOACHAT == null)
            {
                return HttpNotFound();
            }
            return View(hOACHAT);
        }

        public ActionResult XemChiTiet(string mahc)
        {
            HOACHAT hc = db.HOACHATs.Find(mahc);
            if(hc == null)
            {
                return HttpNotFound();
            }
            return View(hc);
        }
        // GET: Admin/HOACHATs/Create
        public ActionResult Create()
        {
            HOACHAT hoachat = new HOACHAT();
            return View(hoachat);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "MAHC,TENHC,THONGSO,CASNO,DONVI,LUONGNHAP,LUONGXUAT,LUONGTON,LUONGTHANHLY,HINHANH")] HOACHAT hOACHAT)
        //{
        //    try
        //    {
        //        if(ModelState.IsValid)
        //        {
        //            if (hOACHAT.ImageUpload != null)
        //            {
        //                string fileName = Path.GetFileNameWithoutExtension(hOACHAT.ImageUpload.FileName);
        //                string extension = Path.GetExtension(hOACHAT.ImageUpload.FileName);
        //                fileName = fileName + extension;
        //                hOACHAT.HINHANH = "~/images/hoachat/" + fileName;
        //                hOACHAT.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/images/hoachat/"), fileName));
        //            }
                    
        //        }
        //        db.HOACHATs.Add(hOACHAT);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
            
        //}

        public ActionResult Create([Bind(Include = "MAHC,TENHC,THONGSO,CASNO,DONVI,LUONGNHAP,LUONGXUAT,LUONGTON,LUONGTHANHLY,HINHANH")] HOACHAT hOACHAT)
        {
            if (ModelState.IsValid)
            {
                //if (hOACHAT.ImageUpload != null)
                //{
                //    string fileName = Path.GetFileNameWithoutExtension(hOACHAT.ImageUpload.FileName);
                //    string extension = Path.GetExtension(hOACHAT.ImageUpload.FileName);
                //    fileName = fileName + extension;
                //    hOACHAT.HINHANH = "~/images/hoachat/" + fileName;
                //    hOACHAT.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/images/hoachat/"), fileName));
                //}
                db.HOACHATs.Add(hOACHAT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hOACHAT);
        }

        // GET: Admin/HOACHATs/Edit/5
        public ActionResult Edit(string id)
        {
            HOACHAT hOACHAT = db.HOACHATs.Find(id);
            ViewBag.MALHC = new SelectList(db.LOAIHOACHATs, "MALHC", "TENLHC", hOACHAT.MAHC);
            return View(hOACHAT);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MAHC,TENHC,THONGSO,CASNO,DONVI,LUONGNHAP,LUONGXUAT,LUONGTON,LUONGTHANHLY,HINHANH")] HOACHAT hOACHAT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hOACHAT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MAHC = new SelectList(db.CT_HOACHAT, "MAHC", "XUATXU", hOACHAT.MAHC);
            return View(hOACHAT);
        }

        // GET: Admin/HOACHATs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOACHAT hOACHAT = db.HOACHATs.Find(id);
            if (hOACHAT == null)
            {
                return HttpNotFound();
            }
            return View(hOACHAT);
        }

        public ActionResult XoaHoaChat(string mahc)
        {
            var lstHC = db.HOACHATs.Where(n => n.MAHC == mahc).ToList();
            foreach(var item in lstHC)
            {
                db.HOACHATs.Remove(item);
            }
            db.SaveChanges();
            return RedirectToAction("Index", "HOACHATs");
        }

        // POST: Admin/HOACHATs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            HOACHAT hOACHAT = db.HOACHATs.Find(id);
            db.HOACHATs.Remove(hOACHAT);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult ExportToExcel()
        {
            var hoachat = from HOACHAT in db.HOACHATs
                          select HOACHAT;
            byte[] fileContents;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("ThongTinHoaChat");

            // "MAHC,TENHC,THONGSO,CASNO,DONVI,LUONGNHAP,LUONGXUAT,LUONGTON,LUONGTHANHLY,HINHANH"
            Sheet.Cells["A1"].Value = "MAHC";
            Sheet.Cells["B1"].Value = "TENHC";
            Sheet.Cells["C1"].Value = "THONGSO";
            Sheet.Cells["D1"].Value = "CASNO";
            Sheet.Cells["E1"].Value = "DONVI";
            Sheet.Cells["F1"].Value = "LUONGTON";
            Sheet.Cells["G1"].Value = "LUONGTHANHLY";
            Sheet.Cells["H1"].Value = "HINHANH";

            int row = 2;
            foreach(var item in hoachat)
            {
                Sheet.Cells[string.Format("A{0}", row)].Value = item.MAHC;
                Sheet.Cells[string.Format("B{0}", row)].Value = item.TENHC;
                Sheet.Cells[string.Format("C{0}", row)].Value = item.THONGSO;
                Sheet.Cells[string.Format("D{0}", row)].Value = item.CASNO;
                Sheet.Cells[string.Format("E{0}", row)].Value = item.DONVI;
                Sheet.Cells[string.Format("F{0}", row)].Value = item.LUONGTON;
                Sheet.Cells[string.Format("G{0}", row)].Value = item.LUONGTHANHLY;
                Sheet.Cells[string.Format("H{0}", row)].Value = item.HINHANH;
                row++;
            }

            Sheet.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            fileContents = Ep.GetAsByteArray();

            if (fileContents == null || fileContents.Length == 0)
            {
                Response.StatusCode = 404;
                return null;
            }
            return File(
                fileContents: fileContents,
                contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileDownloadName: "List_HoaChat.xlsx"
                );
        }

        //KHOA CÔNG NGHIỆP THỰC PHẨM
        //GET: Admin/HOACHATs/CNTP/Index

        public ActionResult Index_CNTP(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.MaSortParm = String.IsNullOrEmpty(sortOrder) ? "ma_desc" : "";
            ViewBag.TenSortParm = sortOrder == "ten" ? "ten_desc" : "ten";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            var hOACHAT_CNTP = db.HOACHAT_CNTP.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                hOACHAT_CNTP = hOACHAT_CNTP.Where(s => s.MAHC_CNTP.Contains(searchString)
                                       || s.TENHC_CNTP.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "ma_desc":
                    hOACHAT_CNTP = hOACHAT_CNTP.OrderByDescending(s => s.MAHC_CNTP);
                    break;
                case "ten":
                    hOACHAT_CNTP = hOACHAT_CNTP.OrderBy(s => s.TENHC_CNTP);
                    break;
                case "ten_desc":
                    hOACHAT_CNTP = hOACHAT_CNTP.OrderByDescending(s => s.TENHC_CNTP);
                    break;
                default:
                    hOACHAT_CNTP = hOACHAT_CNTP.OrderBy(s => s.MAHC_CNTP);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            //var hOACHAT = db.HOACHAT.Include(h => h.CT_HOACHAT);
            ViewBag.total = (from HOACHAT_CNTP in db.HOACHAT_CNTP select HOACHAT_CNTP.LUONGTON).Sum();
            return View(hOACHAT_CNTP.ToPagedList(pageNumber, pageSize));
        }

        //GET: Admin/HOACHATs/Details/CNTP
        public ActionResult Detail_CNTP(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOACHAT hOACHAT = db.HOACHATs.Find(id);
            if (hOACHAT == null)
            {
                return HttpNotFound();
            }
            return View(hOACHAT);
        }

        //GET: Admin/HOACHATs/Create/CNTP
        public ActionResult Create_CNTP()
        {
            ViewBag.MAHC = new SelectList(db.CT_HOACHAT, "MAHC_CNTP", "XUATXU");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_CNTP([Bind(Include = "MAHC_CNTP,TENHC_CNTP,THONGSO,CASNO,DONVI,LUONGNHAP,LUONGTON,LUONGTHANHLY")] HOACHAT_CNTP hOACHAT_CNTP, CT_HOACHAT cT)
        {
            if (ModelState.IsValid)
            {
                db.HOACHAT_CNTP.Add(hOACHAT_CNTP);
                db.SaveChanges();
                return RedirectToAction("Index_CNTP");
            }

            ViewBag.MAHC = new SelectList(db.CT_HOACHAT, "MAHC_CNTP", "XUATXU", hOACHAT_CNTP.MAHC_CNTP);
            return View(hOACHAT_CNTP);
        }

        
        public ActionResult Edit_CNTP(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOACHAT_CNTP hOACHAT = db.HOACHAT_CNTP.Find(id);
            if (hOACHAT == null)
            {
                return HttpNotFound();
            }
            ViewBag.MAHC = new SelectList(db.CT_HOACHAT, "MAHC_CNTP", "XUATXU", hOACHAT.MAHC_CNTP);
            return View(hOACHAT);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_CNTP([Bind(Include = "MAHC_CNTP,TENHC_CNTP,MALHC,THONGSO,CASNO,DONVI,LUONGNHAP,LUONGTON,LUONGTHANHLY")] HOACHAT_CNTP hOACHAT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hOACHAT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index_CNTP");
            }

            ViewBag.MAHC = new SelectList(db.CT_HOACHAT, "MAHC", "XUATXU", hOACHAT.MAHC_CNTP);
            return View(hOACHAT);
        }

        public ActionResult XemChiTiet_CNTP(string mahc_cntp)
        {
            HOACHAT_CNTP hc_cntp = db.HOACHAT_CNTP.Find(mahc_cntp);
            if (hc_cntp == null)
            {
                return HttpNotFound();
            }
            return View(hc_cntp);
        }

            // GET: Admin/HOACHATs/Delete/5
            public ActionResult Delete_CNTP(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HOACHAT_CNTP hOACHAT_CNTP = db.HOACHAT_CNTP.Find(id);
            if (hOACHAT_CNTP == null)
            {
                return HttpNotFound();
            }
            return View(hOACHAT_CNTP);
        }

        // POST: Admin/HOACHATs/Delete/5/CNTP
        [HttpPost, ActionName("Delete_CNTP")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed_CNTP(string id)
        {
            HOACHAT_CNTP hOACHAT_CNTP = db.HOACHAT_CNTP.Find(id);
            db.HOACHAT_CNTP.Remove(hOACHAT_CNTP);
            db.SaveChanges();
            return RedirectToAction("Index_CNTP");
        }

        public ActionResult XoaHoaChat_CNTP(string mahc_cntp)
        {
            var lstHC_cntp = db.HOACHAT_CNTP.Where(n => n.MAHC_CNTP == mahc_cntp).ToList();
            foreach (var item in lstHC_cntp)
            {
                db.HOACHAT_CNTP.Remove(item);
            }
            db.SaveChanges();
            return RedirectToAction("Index_CNTP", "HOACHATs");
        }

    }
}
