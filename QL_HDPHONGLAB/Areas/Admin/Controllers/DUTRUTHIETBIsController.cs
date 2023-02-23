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
    public class DUTRUTHIETBIsController : Controller
    {
        private QL_HDPHONGLABEntities db = new QL_HDPHONGLABEntities();

        // GET: Admin/DUTRUTHIETBIs
        public ActionResult Index()
        {
            var dUTRUTHIETBI = db.DUTRUTHIETBIs.Include(d => d.THIETBI);
            return View(dUTRUTHIETBI.ToList());
        }

        // GET: Admin/DUTRUTHIETBIs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DUTRUTHIETBI dUTRUTHIETBI = db.DUTRUTHIETBIs.Find(id);
            if (dUTRUTHIETBI == null)
            {
                return HttpNotFound();
            }
            return View(dUTRUTHIETBI);
        }

        // GET: Admin/DUTRUTHIETBIs/Create
        public ActionResult Create()
        {
            ViewBag.MATB = new SelectList(db.THIETBIs, "MATB", "MATB");
            return View();
        }

        // POST: Admin/DUTRUTHIETBIs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MATB,TENTB,DACTA,SOLUONG")] DUTRUTHIETBI dUTRUTHIETBI)
        {
            if (ModelState.IsValid)
            {
                db.DUTRUTHIETBIs.Add(dUTRUTHIETBI);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MATB = new SelectList(db.THIETBIs, "MATB", "MATB", dUTRUTHIETBI.MATB);
            return View(dUTRUTHIETBI);
        }

        // GET: Admin/DUTRUTHIETBIs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DUTRUTHIETBI dUTRUTHIETBI = db.DUTRUTHIETBIs.Find(id);
            if (dUTRUTHIETBI == null)
            {
                return HttpNotFound();
            }
            ViewBag.MATB = new SelectList(db.THIETBIs, "MATB", "MATB", dUTRUTHIETBI.MATB);
            return View(dUTRUTHIETBI);
        }

        // POST: Admin/DUTRUTHIETBIs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MATB,TENTB,DACTA,SOLUONG")] DUTRUTHIETBI dUTRUTHIETBI)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dUTRUTHIETBI).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MATB = new SelectList(db.THIETBIs, "MATB", "MATB", dUTRUTHIETBI.MATB);
            return View(dUTRUTHIETBI);
        }

        // GET: Admin/DUTRUTHIETBIs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DUTRUTHIETBI dUTRUTHIETBI = db.DUTRUTHIETBIs.Find(id);
            if (dUTRUTHIETBI == null)
            {
                return HttpNotFound();
            }
            return View(dUTRUTHIETBI);
        }

        // POST: Admin/DUTRUTHIETBIs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DUTRUTHIETBI dUTRUTHIETBI = db.DUTRUTHIETBIs.Find(id);
            db.DUTRUTHIETBIs.Remove(dUTRUTHIETBI);
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
