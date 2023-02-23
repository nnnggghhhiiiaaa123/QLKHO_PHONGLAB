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
    public class DUTRUHOACHATsController : Controller
    {
        private QL_HDPHONGLABEntities db = new QL_HDPHONGLABEntities();

        // GET: Admin/DUTRUHOACHATs
        public ActionResult Index()
        {
            var dUTRUHOACHAT = db.DUTRUHOACHATs.Include(d => d.HOACHAT);
            return View(dUTRUHOACHAT.ToList());
        }

        // GET: Admin/DUTRUHOACHATs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DUTRUHOACHAT dUTRUHOACHAT = db.DUTRUHOACHATs.Find(id);
            if (dUTRUHOACHAT == null)
            {
                return HttpNotFound();
            }
            return View(dUTRUHOACHAT);
        }

        // GET: Admin/DUTRUHOACHATs/Create
        public ActionResult Create()
        {
            ViewBag.MAHC = new SelectList(db.HOACHATs, "MAHC", "MAHC");
            return View();
        }

        // POST: Admin/DUTRUHOACHATs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MAHC,TENHC,DACTA,SOLUONG")] DUTRUHOACHAT dUTRUHOACHAT)
        {
            if (ModelState.IsValid)
            {
                db.DUTRUHOACHATs.Add(dUTRUHOACHAT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MAHC = new SelectList(db.HOACHATs, "MAHC", "MAHC", dUTRUHOACHAT.MAHC);
            return View(dUTRUHOACHAT);
        }

        // GET: Admin/DUTRUHOACHATs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DUTRUHOACHAT dUTRUHOACHAT = db.DUTRUHOACHATs.Find(id);
            if (dUTRUHOACHAT == null)
            {
                return HttpNotFound();
            }
            ViewBag.MAHC = new SelectList(db.HOACHATs, "MAHC", "MAHC", dUTRUHOACHAT.MAHC);
            return View(dUTRUHOACHAT);
        }

        // POST: Admin/DUTRUHOACHATs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MAHC,TENHC,DACTA,SOLUONG")] DUTRUHOACHAT dUTRUHOACHAT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dUTRUHOACHAT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MAHC = new SelectList(db.HOACHATs, "MAHC", "MAHC", dUTRUHOACHAT.MAHC);
            return View(dUTRUHOACHAT);
        }

        // GET: Admin/DUTRUHOACHATs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DUTRUHOACHAT dUTRUHOACHAT = db.DUTRUHOACHATs.Find(id);
            if (dUTRUHOACHAT == null)
            {
                return HttpNotFound();
            }
            return View(dUTRUHOACHAT);
        }

        // POST: Admin/DUTRUHOACHATs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DUTRUHOACHAT dUTRUHOACHAT = db.DUTRUHOACHATs.Find(id);
            db.DUTRUHOACHATs.Remove(dUTRUHOACHAT);
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
