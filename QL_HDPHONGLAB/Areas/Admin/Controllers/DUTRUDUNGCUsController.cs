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
    public class DUTRUDUNGCUsController : Controller
    {
        private QL_HDPHONGLABEntities db = new QL_HDPHONGLABEntities();

        // GET: Admin/DUTRUDUNGCUs
        public ActionResult Index()
        {
            var dUTRUDUNGCU = db.DUTRUDUNGCUs.Include(d => d.DUNGCU);
            return View(dUTRUDUNGCU.ToList());
        }

        // GET: Admin/DUTRUDUNGCUs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DUTRUDUNGCU dUTRUDUNGCU = db.DUTRUDUNGCUs.Find(id);
            if (dUTRUDUNGCU == null)
            {
                return HttpNotFound();
            }
            return View(dUTRUDUNGCU);
        }

        // GET: Admin/DUTRUDUNGCUs/Create
        public ActionResult Create()
        {
            ViewBag.MADC = new SelectList(db.DUTRUDUNGCUs, "MADC", "MADC");
            return View();
        }

        // POST: Admin/DUTRUDUNGCUs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MADC,TENDC,DACTA,SOLUONG")] DUTRUDUNGCU dUTRUDUNGCU)
        {
            if (ModelState.IsValid)
            {
                db.DUTRUDUNGCUs.Add(dUTRUDUNGCU);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MADC = new SelectList(db.DUNGCUs, "MADC", "MADC", dUTRUDUNGCU.MADC);
            return View(dUTRUDUNGCU);
        }

        // GET: Admin/DUTRUDUNGCUs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DUTRUDUNGCU dUTRUDUNGCU = db.DUTRUDUNGCUs.Find(id);
            if (dUTRUDUNGCU == null)
            {
                return HttpNotFound();
            }
            ViewBag.MADC = new SelectList(db.DUNGCUs, "MADC", "MADC", dUTRUDUNGCU.MADC);
            return View(dUTRUDUNGCU);
        }

        // POST: Admin/DUTRUDUNGCUs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MADC,TENDC,DACTA,SOLUONG")] DUTRUDUNGCU dUTRUDUNGCU)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dUTRUDUNGCU).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MADC = new SelectList(db.DUNGCUs, "MADC", "MADC", dUTRUDUNGCU.MADC);
            return View(dUTRUDUNGCU);
        }

        // GET: Admin/DUTRUDUNGCUs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DUTRUDUNGCU dUTRUDUNGCU = db.DUTRUDUNGCUs.Find(id);
            if (dUTRUDUNGCU == null)
            {
                return HttpNotFound();
            }
            return View(dUTRUDUNGCU);
        }

        // POST: Admin/DUTRUDUNGCUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DUTRUDUNGCU dUTRUDUNGCU = db.DUTRUDUNGCUs.Find(id);
            db.DUTRUDUNGCUs.Remove(dUTRUDUNGCU);
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
