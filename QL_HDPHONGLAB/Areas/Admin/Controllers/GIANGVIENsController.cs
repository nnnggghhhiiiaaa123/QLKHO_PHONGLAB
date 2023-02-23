using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QL_HDPHONGLAB.Models;

namespace QL_HDPHONGLAB.Areas.Admin.Controllers
{
    public class GIANGVIENsController : Controller
    {
        private QL_HDPHONGLABEntities db = new QL_HDPHONGLABEntities();

        // GET: Admin/GIANGVIENs
        public ActionResult Index()
        {
            var gIANGVIEN = db.GIANGVIENs.Include(g => g.PHONGBAN);
            return View(gIANGVIEN.ToList());
        }

        // GET: Admin/GIANGVIENs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GIANGVIEN gIANGVIEN = db.GIANGVIENs.Find(id);
            if (gIANGVIEN == null)
            {
                return HttpNotFound();
            }
            return View(gIANGVIEN);
        }

        // GET: Admin/GIANGVIENs/Create
        public ActionResult Create()
        {
            //ViewBag.MADT = new SelectList(db.DUTRU, "MADT", "TENDT");
            ViewBag.MAPHBAN = new SelectList(db.PHONGBANs, "MAPHBAN", "TENPHBAN");
            //ViewBag.MAPHLAB = new SelectList(db.PHONGLABs, "MAPHLAB", "TENPHLAB");
            return View();
        }

        // POST: Admin/GIANGVIENs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MAGV,HOTEN,NGAYSINH,GIOITINH,SDT,DIACHI,MAPHBAN,VAITRO")] GIANGVIEN gIANGVIEN)
        {
            if (ModelState.IsValid)
            {
                db.GIANGVIENs.Add(gIANGVIEN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.MADT = new SelectList(db.DUTRU, "MADT", "TENDT", gIANGVIEN.MADT);
            ViewBag.MAPHBAN = new SelectList(db.PHONGBANs, "MAPHBAN", "TENPHBAN", gIANGVIEN.MAPHBAN);
            //ViewBag.MAPHLAB = new SelectList(db.PHONGLABs, "MAPHLAB", "TENPHLAB", gIANGVIEN.MAPHLAB);
            return View(gIANGVIEN);
        }

        // GET: Admin/GIANGVIENs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GIANGVIEN gIANGVIEN = db.GIANGVIENs.Find(id);
            if (gIANGVIEN == null)
            {
                return HttpNotFound();
            }
            //ViewBag.MADT = new SelectList(db.DUTRU, "MADT", "TENDT", gIANGVIEN.MADT);
            ViewBag.MAPHBAN = new SelectList(db.PHONGBANs, "MAPHBAN", "TENPHBAN", gIANGVIEN.MAPHBAN);
            //ViewBag.MAPHLAB = new SelectList(db.PHONGLABs, "MAPHLAB", "TENPHLAB", gIANGVIEN.MAPHLAB);
            return View(gIANGVIEN);
        }

        // POST: Admin/GIANGVIENs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MAGV,HOTEN,NGAYSINH,GIOITINH,SDT,DIACHI,MAPHBAN,VAITRO")] GIANGVIEN gIANGVIEN)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gIANGVIEN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.MADT = new SelectList(db.DUTRU, "MADT", "TENDT", gIANGVIEN.MADT);
            ViewBag.MAPHBAN = new SelectList(db.PHONGBANs, "MAPHBAN", "TENPHBAN", gIANGVIEN.MAPHBAN);
            //ViewBag.MAPHLAB = new SelectList(db.PHONGLABs, "MAPHLAB", "TENPHLAB", gIANGVIEN.MAPHLAB);
            return View(gIANGVIEN);
        }

        // GET: Admin/GIANGVIENs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GIANGVIEN gIANGVIEN = db.GIANGVIENs.Find(id);
            if (gIANGVIEN == null)
            {
                return HttpNotFound();
            }
            return View(gIANGVIEN);
        }

        // POST: Admin/GIANGVIENs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            GIANGVIEN gIANGVIEN = db.GIANGVIENs.Find(id);
            db.GIANGVIENs.Remove(gIANGVIEN);
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
