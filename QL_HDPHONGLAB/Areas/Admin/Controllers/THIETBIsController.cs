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
    public class THIETBIsController : Controller
    {
        private QL_HDPHONGLABEntities db = new QL_HDPHONGLABEntities();

        // GET: Admin/THIETBIs
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
            var tHIETBI = db.THIETBIs.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                tHIETBI = tHIETBI.Where(s => s.MATB.Contains(searchString)
                                       || s.TENTB.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "ma_desc":
                    tHIETBI = tHIETBI.OrderByDescending(s => s.MATB);
                    break;
                case "ten":
                    tHIETBI = tHIETBI.OrderBy(s => s.TENTB);
                    break;
                case "ten_desc":
                    tHIETBI = tHIETBI.OrderByDescending(s => s.TENTB);
                    break;
                default:
                    tHIETBI = tHIETBI.OrderBy(s => s.MATB);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            //var tHIETBI = db.THIETBI.Include(t => t.CT_THIETBI);
            ViewBag.total = (from THIETBI in db.THIETBIs select THIETBI.SLTON).Sum();
            return View(tHIETBI.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/THIETBIs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THIETBI tHIETBI = db.THIETBIs.Find(id);
            if (tHIETBI == null)
            {
                return HttpNotFound();
            }
            return View(tHIETBI);
        }

        // GET: Admin/THIETBIs/Create
        public ActionResult Create()
        {
            ViewBag.MATB = new SelectList(db.CT_THIETBI, "MATB", "SERIAL");
            return View();
        }

        // POST: Admin/THIETBIs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MATB,TENTB,QUICACH,SLBANDAU,SLXUAT,SLTON,SLTHANHLY,TAPHUAN,SERIAL,HINHANH")] THIETBI tHIETBI)
        {
            if (ModelState.IsValid)
            {
                db.THIETBIs.Add(tHIETBI);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MATB = new SelectList(db.CT_THIETBI, "MATB", "SERIAL", tHIETBI.MATB);
            return View(tHIETBI);
        }

        // GET: Admin/THIETBIs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THIETBI tHIETBI = db.THIETBIs.Find(id);
            if (tHIETBI == null)
            {
                return HttpNotFound();
            }
            ViewBag.MATB = new SelectList(db.CT_THIETBI, "MATB", "SERIAL", tHIETBI.MATB);
            return View(tHIETBI);
        }

        public ActionResult XemChiTiet(string matb)
        {
            THIETBI tb = db.THIETBIs.Find(matb);
            if (tb == null)
            {
                return HttpNotFound();
            }
            return View(tb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MATB,TENTB,QUICACH,SLBANDAU,SLXUAT,SLTON,SLTHANHLY,TAPHUAN,SERIAL,HINHANH")] THIETBI tHIETBI)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tHIETBI).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MATB = new SelectList(db.CT_THIETBI, "MATB", "SERIAL", tHIETBI.MATB);
            return View(tHIETBI);
        }

        // GET: Admin/THIETBIs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THIETBI tHIETBI = db.THIETBIs.Find(id);
            if (tHIETBI == null)
            {
                return HttpNotFound();
            }
            return View(tHIETBI);
        }

        // POST: Admin/THIETBIs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            THIETBI tHIETBI = db.THIETBIs.Find(id);
            db.THIETBIs.Remove(tHIETBI);
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

        public ActionResult XoaThietBi(string matb)
        {
            var lstTB = db.THIETBIs.Where(n => n.MATB == matb).ToList();
            foreach (var item in lstTB)
            {
                db.THIETBIs.Remove(item);
            }
            db.SaveChanges();
            return RedirectToAction("Index", "THIETBIs");
        }

        public ActionResult ExportToExcel()
        {
            var thietbi = from THIETBI in db.THIETBIs
                          select THIETBI;
            byte[] fileContents;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("ThongTinThietBi");

            //"MATB,TENTB,QUICACH,SLBANDAU,SLXUAT,SLTON,SLTHANHLY,TAPHUAN,SERIAL,HINHANH"
            Sheet.Cells["A1"].Value = "MATB";
            Sheet.Cells["B1"].Value = "TENTB";
            Sheet.Cells["C1"].Value = "QUICACH";
            Sheet.Cells["D1"].Value = "SLTON";
            Sheet.Cells["E1"].Value = "SLTHANHLY";
            Sheet.Cells["F1"].Value = "TAPHUAN";
            Sheet.Cells["G1"].Value = "SERIAL";
            Sheet.Cells["H1"].Value = "HINHANH";

            int row = 2;
            foreach (var item in thietbi)
            {
                Sheet.Cells[string.Format("A{0}", row)].Value = item.MATB;
                Sheet.Cells[string.Format("B{0}", row)].Value = item.TENTB;
                Sheet.Cells[string.Format("C{0}", row)].Value = item.QUICACH;
                Sheet.Cells[string.Format("D{0}", row)].Value = item.SLTON;
                Sheet.Cells[string.Format("E{0}", row)].Value = item.SLTHANHLY;
                Sheet.Cells[string.Format("F{0}", row)].Value = item.TAPHUAN;
                Sheet.Cells[string.Format("G{0}", row)].Value = item.SERIAL;
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
                fileDownloadName: "List_ThietBi.xlsx"
                );
        }

        //KHOA CNTP
        // GET: Admin/THIETBIs/CNTP
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
            var tHIETBI = db.THIETBI_CNTP.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                tHIETBI = tHIETBI.Where(s => s.MATB.Contains(searchString)
                                       || s.TENTB.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "ma_desc":
                    tHIETBI = tHIETBI.OrderByDescending(s => s.MATB);
                    break;
                case "ten":
                    tHIETBI = tHIETBI.OrderBy(s => s.TENTB);
                    break;
                case "ten_desc":
                    tHIETBI = tHIETBI.OrderByDescending(s => s.TENTB);
                    break;
                default:
                    tHIETBI = tHIETBI.OrderBy(s => s.MATB);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            //var tHIETBI = db.THIETBI.Include(t => t.CT_THIETBI);
            ViewBag.total = (from THIETBI_CNTP in db.THIETBI_CNTP select THIETBI_CNTP.SLTON).Sum();

            return View(tHIETBI.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/THIETBIs/Details/5/CNTP
        public ActionResult Details_CNTP(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THIETBI_CNTP tHIETBI = db.THIETBI_CNTP.Find(id);
            if (tHIETBI == null)
            {
                return HttpNotFound();
            }
            return View(tHIETBI);
        }

        // GET: Admin/THIETBIs/Create/CNTP
        public ActionResult Create_CNTP()
        {
            ViewBag.MATB = new SelectList(db.CT_THIETBI, "MATB", "SERIAL");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create_CNTP([Bind(Include = "MATB,TENTB,QUICACH,SLTON,SLTHANHLY,SERIAL")] THIETBI_CNTP tHIETBI)
        {
            if (ModelState.IsValid)
            {
                db.THIETBI_CNTP.Add(tHIETBI);
                db.SaveChanges();
                return RedirectToAction("Index_CNTP");
            }

            ViewBag.MATB = new SelectList(db.CT_THIETBI, "MATB", "SERIAL", tHIETBI.MATB);
            return View(tHIETBI);
        }

        // GET: Admin/THIETBIs/Edit/5/CNTP
        public ActionResult Edit_CNTP(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THIETBI_CNTP tHIETBI = db.THIETBI_CNTP.Find(id);
            if (tHIETBI == null)
            {
                return HttpNotFound();
            }
            ViewBag.MATB = new SelectList(db.CT_THIETBI, "MATB", "SERIAL", tHIETBI.MATB);
            return View(tHIETBI);
        }

        // POST: Admin/THIETBIs/Edit/5/CNTP
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_CNTP([Bind(Include = "MATB,TENTB,QUICACH,SLTON,SLTHANHLY,SERIAL")] THIETBI_CNTP tHIETBI)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tHIETBI).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index_CNTP");
            }
            ViewBag.MATB = new SelectList(db.CT_THIETBI, "MATB", "SERIAL", tHIETBI.MATB);
            return View(tHIETBI);
        }

        // GET: Admin/THIETBIs/Delete/5/CNTP
        public ActionResult Delete_CNTP(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THIETBI_CNTP tHIETBI = db.THIETBI_CNTP.Find(id);
            if (tHIETBI == null)
            {
                return HttpNotFound();
            }
            return View(tHIETBI);
        }

        // POST: Admin/THIETBIs/Delete/5/CNTP
        [HttpPost, ActionName("Delete_CNTP")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed_CNTP(string id)
        {
            THIETBI_CNTP tHIETBI = db.THIETBI_CNTP.Find(id);
            db.THIETBI_CNTP.Remove(tHIETBI);
            db.SaveChanges();
            return RedirectToAction("Index_CNTP");
        }

        public ActionResult XemChiTiet_CNTP(string matb_cntp)
        {
            THIETBI_CNTP tb_cntp = db.THIETBI_CNTP.Find(matb_cntp);
            if (tb_cntp == null)
            {
                return HttpNotFound();
            }
            return View(tb_cntp);
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
                    var luongton = db.THIETBIs.SingleOrDefault(n => n.MATB == item.MATB);
                    luongton.SLTON -= item.SLTON;

                    var luongthanhly = db.THIETBIs.SingleOrDefault(n => n.MATB == item.MATB);
                    luongthanhly.SLTHANHLY += item.SLTHANHLY;
                    #endregion
                }
                db.SaveChanges();
                return RedirectToAction("Index", "THIETBIs");
            }
            return View(model);
        }


        public ActionResult XoaThietBi_CNTP(string matb_cntp)
        {
            var lstTB = db.THIETBI_CNTP.Where(n => n.MATB == matb_cntp).ToList();
            foreach (var item in lstTB)
            {
                db.THIETBI_CNTP.Remove(item);
            }
            db.SaveChanges();
            return RedirectToAction("Index_CNTP", "THIETBIs");
        }
    }
}
