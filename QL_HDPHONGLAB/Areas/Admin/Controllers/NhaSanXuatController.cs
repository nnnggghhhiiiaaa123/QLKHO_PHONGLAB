using QL_HDPHONGLAB.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace QL_HDPHONGLAB.Areas.Admin.Controllers
{
    public class NhaSanXuatController : Controller
    {
        // GET: Admin/NhaSanXuat
        private QL_HDPHONGLABEntities db = new QL_HDPHONGLABEntities();

        // GET: NSXes
        public ActionResult Index()
        {
            return View(db.NSXes.ToList());
        }

        // GET: NSXes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NSX nSX = db.NSXes.Find(id);
            if (nSX == null)
            {
                return HttpNotFound();
            }
            return View(nSX);
        }

        // GET: NSXes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NSXes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MANSX,TENNSX,SDT,DIACHI")] NSX nSX)
        {
            if (ModelState.IsValid)
            {
                db.NSXes.Add(nSX);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nSX);
        }

        // GET: NSXes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NSX nSX = db.NSXes.Find(id);
            if (nSX == null)
            {
                return HttpNotFound();
            }
            return View(nSX);
        }

        // POST: NSXes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MANSX,TENNSX,SDT,DIACHI")] NSX nSX)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nSX).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nSX);
        }

        // GET: NSXes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NSX nSX = db.NSXes.Find(id);
            if (nSX == null)
            {
                return HttpNotFound();
            }
            return View(nSX);
        }

        // POST: NSXes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NSX nSX = db.NSXes.Find(id);
            db.NSXes.Remove(nSX);
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