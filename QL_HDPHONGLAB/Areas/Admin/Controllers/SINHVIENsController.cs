using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QL_HDPHONGLAB.Models;
using PagedList;

namespace QL_HDPHONGLAB.Areas.Admin.Controllers
{
    public class SINHVIENsController : Controller
    {
        private QL_HDPHONGLABEntities db = new QL_HDPHONGLABEntities();

        // GET: Admin/SINHVIENs
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
            var sINHVIEN = db.SINHVIENs.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                sINHVIEN = sINHVIEN.Where(s => s.MASV.Contains(searchString)
                                       || s.TENSV.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "ma_desc":
                    sINHVIEN = sINHVIEN.OrderByDescending(s => s.MASV);
                    break;
                case "ten":
                    sINHVIEN = sINHVIEN.OrderBy(s => s.TENSV);
                    break;
                case "ten_desc":
                    sINHVIEN = sINHVIEN.OrderByDescending(s => s.TENSV);
                    break;
                default:
                    sINHVIEN = sINHVIEN.OrderBy(s => s.MASV);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            //var sINHVIEN = db.SINHVIEN.Include(s => s.PHIEUXUATPHONGLAB);
            return View(sINHVIEN.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/SINHVIENs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SINHVIEN sINHVIEN = db.SINHVIENs.Find(id);
            if (sINHVIEN == null)
            {
                return HttpNotFound();
            }
            return View(sINHVIEN);
        }

        // GET: Admin/SINHVIENs/Create
        public ActionResult Create()
        {
            //ViewBag.MAPXPHLAB = new SelectList(db.PHIEUXUATPHONGLABs, "MAPXPHLAB", "NOIDUNG");
            return View();
        }

        // POST: Admin/SINHVIENs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MASV,TENSV,NGAYSINH,GIOITINH")] SINHVIEN sINHVIEN)
        {
            if (ModelState.IsValid)
            {
                db.SINHVIENs.Add(sINHVIEN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.MAPXPHLAB = new SelectList(db.PHIEUXUATPHONGLABs, "MAPXPHLAB", "NOIDUNG", sINHVIEN.MAPXPHLAB);
            return View(sINHVIEN);
        }

        // GET: Admin/SINHVIENs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SINHVIEN sINHVIEN = db.SINHVIENs.Find(id);
            if (sINHVIEN == null)
            {
                return HttpNotFound();
            }
            //ViewBag.MAPXPHLAB = new SelectList(db.PHIEUXUATPHONGLABs, "MAPXPHLAB", "NOIDUNG", sINHVIEN.MAPXPHLAB);
            return View(sINHVIEN);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MASV,TENSV,NGAYSINH,GIOITINH")] SINHVIEN sINHVIEN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sINHVIEN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.MAPXPHLAB = new SelectList(db.PHIEUXUATPHONGLABs, "MAPXPHLAB", "NOIDUNG", sINHVIEN.MAPXPHLAB);
            return View(sINHVIEN);
        }

        // GET: Admin/SINHVIENs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SINHVIEN sINHVIEN = db.SINHVIENs.Find(id);
            if (sINHVIEN == null)
            {
                return HttpNotFound();
            }
            return View(sINHVIEN);
        }

        // POST: Admin/SINHVIENs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SINHVIEN sINHVIEN = db.SINHVIENs.Find(id);
            db.SINHVIENs.Remove(sINHVIEN);
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
    }
}
