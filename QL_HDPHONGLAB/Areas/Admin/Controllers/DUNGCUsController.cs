using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OfficeOpenXml;
using PagedList;
using QL_HDPHONGLAB.Models;

namespace QL_HDPHONGLAB.Areas.Admin.Controllers
{
    public class DUNGCUsController : Controller
    {
        private QL_HDPHONGLABEntities db = new QL_HDPHONGLABEntities();

        // GET: Admin/DUNGCUs
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
            var dUNGCU = db.DUNGCUs.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                dUNGCU = dUNGCU.Where(s => s.MADC.Contains(searchString)
                                       || s.TENDC.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "ma_desc":
                    dUNGCU = dUNGCU.OrderByDescending(s => s.MADC);
                    break;
                case "ten":
                    dUNGCU = dUNGCU.OrderBy(s => s.TENDC);
                    break;
                case "ten_desc":
                    dUNGCU = dUNGCU.OrderByDescending(s => s.TENDC);
                    break;
                default:
                    dUNGCU = dUNGCU.OrderBy(s => s.MADC);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            //var dUNGCU = db.DUNGCU.Include(d => d.CT_DUNGCU);
            ViewBag.total = (from DUNGCU in db.DUNGCUs select DUNGCU.LUONGTON).Sum();

            return View(dUNGCU.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/DUNGCUs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DUNGCU dUNGCU = db.DUNGCUs.Find(id);
            if (dUNGCU == null)
            {
                return HttpNotFound();
            }
            return View(dUNGCU);
        }

        // GET: Admin/DUNGCUs/Create
        public ActionResult Create()
        {

            ViewBag.MADC = new SelectList(db.CT_DUNGCU, "MADC", "XUATXU");
            

            return View();
        }

        public ActionResult XemChiTiet(string madc)
        {
            DUNGCU dc = db.DUNGCUs.Find(madc);
            if(dc == null)
            {
                return HttpNotFound();
            }
            return View(dc);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MADC,TENDC,LUONGNHAP,LUONGXUAT,LUONGTON,LUONGTHANHLY,DVT,NGAYNHAP,GIOSD,HINHANH")] DUNGCU dUNGCU)
        {
            if (ModelState.IsValid)
            {
                if(dUNGCU.NGAYNHAP == null)
                {
                    dUNGCU.NGAYNHAP = DateTime.Now;
                }    
                db.DUNGCUs.Add(dUNGCU);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MADC = new SelectList(db.CT_DUNGCU, "MADC", "XUATXU", dUNGCU.MADC);
            return View(dUNGCU);
        }

        // GET: Admin/DUNGCUs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DUNGCU dUNGCU = db.DUNGCUs.Find(id);
            if (dUNGCU == null)
            {
                return HttpNotFound();
            }
            ViewBag.MADC = new SelectList(db.CT_DUNGCU, "MADC", "XUATXU", dUNGCU.MADC);
            return View(dUNGCU);
        }

        // POST: Admin/DUNGCUs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MADC,TENDC,LUONGNHAP,LUONGXUAT,LUONGTON,LUONGTHANHLY,DVT,NGAYNHAP,GIOSD,HINHANH")] DUNGCU dUNGCU)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dUNGCU).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MADC = new SelectList(db.CT_DUNGCU, "MADC", "XUATXU", dUNGCU.MADC);
            return View(dUNGCU);
        }

        // GET: Admin/DUNGCUs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DUNGCU dUNGCU = db.DUNGCUs.Find(id);
            if (dUNGCU == null)
            {
                return HttpNotFound();
            }
            return View(dUNGCU);
        }

        // POST: Admin/DUNGCUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DUNGCU dUNGCU = db.DUNGCUs.Find(id);
            db.DUNGCUs.Remove(dUNGCU);
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

        public ActionResult XoaDungCu(string madc)
        {
            var lstDC = db.DUNGCUs.Where(n => n.MADC == madc).ToList();
            foreach (var item in lstDC)
            {
                db.DUNGCUs.Remove(item);
            }
            db.SaveChanges();
            return RedirectToAction("Index", "DUNGCUs");
        }

        public ActionResult ExportToExcel()
        {
            var dungcu = from DUNGCU in db.DUNGCUs
                          select DUNGCU;
            byte[] fileContents;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("ThongTinDungCu");

            //"MADC,TENDC,LUONGNHAP,LUONGXUAT,LUONGTON,LUONGTHANHLY,DVT,NGAYNHAP,GIOSD,HINHANH"
            Sheet.Cells["A1"].Value = "MADC";
            Sheet.Cells["B1"].Value = "TENDC";
            Sheet.Cells["C1"].Value = "LUONGTON";
            Sheet.Cells["D1"].Value = "LUONGTHANHLY";
            Sheet.Cells["E1"].Value = "DVT";
            Sheet.Cells["F1"].Value = "NGAYNHAP";
            Sheet.Cells["G1"].Value = "GIOSD";
            Sheet.Cells["H1"].Value = "HINHANH";

            int row = 2;
            foreach (var item in dungcu)
            {
                Sheet.Cells[string.Format("A{0}", row)].Value = item.MADC;
                Sheet.Cells[string.Format("B{0}", row)].Value = item.TENDC;
                Sheet.Cells[string.Format("C{0}", row)].Value = item.LUONGTON;
                Sheet.Cells[string.Format("D{0}", row)].Value = item.LUONGTHANHLY;
                Sheet.Cells[string.Format("E{0}", row)].Value = item.DVT;
                Sheet.Cells[string.Format("F{0}", row)].Value = item.NGAYNHAP;
                Sheet.Cells[string.Format("G{0}", row)].Value = item.GIOSD;
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
                fileDownloadName: "List_DungCu.xlsx"
                );
        }

        // KHOA CNTP
        // GET: Admin/DUNGCUs/CNTP
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
            var dUNGCU = db.DUNGCU_CNTP.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                dUNGCU = dUNGCU.Where(s => s.MADC.Contains(searchString)
                                       || s.TENDC.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "ma_desc":
                    dUNGCU = dUNGCU.OrderByDescending(s => s.MADC);
                    break;
                case "ten":
                    dUNGCU = dUNGCU.OrderBy(s => s.TENDC);
                    break;
                case "ten_desc":
                    dUNGCU = dUNGCU.OrderByDescending(s => s.TENDC);
                    break;
                default:
                    dUNGCU = dUNGCU.OrderBy(s => s.MADC);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            //var dUNGCU = db.DUNGCU.Include(d => d.CT_DUNGCU);
            ViewBag.total = (from DUNGCU_CNTP in db.DUNGCU_CNTP select DUNGCU_CNTP.LUONGTON).Sum();
            return View(dUNGCU.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult XemChiTiet_CNTP(string madc_cntp)
        {
            DUNGCU_CNTP dc_cntp = db.DUNGCU_CNTP.Find(madc_cntp);
            if (dc_cntp == null)
            {
                return HttpNotFound();
            }
            return View(dc_cntp);
        }

        // GET: Admin/DUNGCUs/Details/5/CNTP
        public ActionResult Details_CNTP(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DUNGCU_CNTP dUNGCU = db.DUNGCU_CNTP.Find(id);
            if (dUNGCU == null)
            {
                return HttpNotFound();
            }
            return View(dUNGCU);
        }

        // GET: Admin/DUNGCUs/Create
        public ActionResult Create_CNTP()
        {
            ViewBag.MADC = new SelectList(db.CT_DUNGCU, "MADC", "XUATXU");
            return View();
        }

        // POST: Admin/DUNGCUs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_CNTP([Bind(Include = "MADC,TENDC,LUONGTON,LUONGTHANHLY,DVT,GIOSD")] DUNGCU_CNTP dUNGCU)
        {
            if (ModelState.IsValid)
            {
                db.DUNGCU_CNTP.Add(dUNGCU);
                db.SaveChanges();
                return RedirectToAction("Index_CNTP");
            }

            ViewBag.MADC = new SelectList(db.CT_DUNGCU, "MADC", "XUATXU", dUNGCU.MADC);
            return View(dUNGCU);
        }

        // GET: Admin/DUNGCUs/Edit/5
        public ActionResult Edit_CNTP(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DUNGCU_CNTP dUNGCU = db.DUNGCU_CNTP.Find(id);
            if (dUNGCU == null)
            {
                return HttpNotFound();
            }
            
            ViewBag.MADC = new SelectList(db.CT_DUNGCU, "MADC", "XUATXU", dUNGCU.MADC);
            return View(dUNGCU);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_CNTP([Bind(Include = "MADC,TENDC,LUONGTON,LUONGTHANHLY,DVT,GIOSD")] DUNGCU_CNTP dUNGCU)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dUNGCU).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index_CNTP");
            }

            ViewBag.MADC = new SelectList(db.CT_DUNGCU, "MADC", "XUATXU", dUNGCU.MADC);
            return View(dUNGCU);
        }

        // GET: Admin/DUNGCUs/Delete/5
        public ActionResult Delete_CNTP(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DUNGCU_CNTP dUNGCU = db.DUNGCU_CNTP.Find(id);
            if (dUNGCU == null)
            {
                return HttpNotFound();
            }
            return View(dUNGCU);
        }

        // POST: Admin/DUNGCUs/Delete/5
        [HttpPost, ActionName("Delete_CNTP")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed_CNTP(string id)
        {
            DUNGCU_CNTP dUNGCU = db.DUNGCU_CNTP.Find(id);
            db.DUNGCU_CNTP.Remove(dUNGCU);
            db.SaveChanges();
            return RedirectToAction("Index_CNTP");
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

                foreach (var item in lstModel)
                {
                    // Lấy mã thanh lý
                    item.MATL = model.MATL;

                    // Tìm mã hoá chất
                    #region Trừ lượng tồn tăng lượng thanh lý
                    var luongton = db.DUNGCUs.SingleOrDefault(n => n.MADC == item.MADC);
                    luongton.LUONGTON -= item.SLTON;

                    var luongthanhly = db.DUNGCUs.SingleOrDefault(n => n.MADC == item.MADC);
                    luongthanhly.LUONGTHANHLY += item.SLTHANHLY;
                    #endregion
                }
                db.SaveChanges();
                return RedirectToAction("Index", "DUNGCUs");
            }
            return View(model);
        }

        public ActionResult XoaDungCu_CNTP(string madc_cntp)
        {
            var lstDC_CNTP = db.DUNGCU_CNTP.Where(n => n.MADC == madc_cntp).ToList();
            foreach (var item in lstDC_CNTP)
            {
                db.DUNGCU_CNTP.Remove(item);
            }
            db.SaveChanges();
            return RedirectToAction("Index_CNTP", "DUNGCUs");
        }
    }
}
