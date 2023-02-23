using QL_HDPHONGLAB.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace QL_HDPHONGLAB.Areas.Admin.Controllers
{
    public class PhongLABController : Controller
    {
        private QL_HDPHONGLABEntities db;
        public ActionResult AddPhongLab()
        {
            db = new QL_HDPHONGLABEntities();
            var data = from PHONGLAB in db.PHONGLABs select PHONGLAB;
            return View(data);
        }

        [HttpPost]
        public ActionResult Insert_LAB(string sophong, int succhua, string diadiem, string ghichu)
        {
            db = new QL_HDPHONGLABEntities();
            PHONGLAB lab = new PHONGLAB();
            lab.SOPHONG = sophong;
            lab.SUCCHUA = succhua;
            lab.DIADIEM = diadiem;
            lab.GHICHU = ghichu;

            db.PHONGLABs.Add(lab);

            try
            {
                db.SaveChanges();
            }
            catch(DbEntityValidationException e)
            {
                Console.WriteLine(e);
            }
            if(db.SaveChanges() > 0)
            {
                ViewData["status"] = "Thêm thành công";
                ViewBag.status = "Thêm thành công";
                List<PHONGLAB> phlab = db.PHONGLABs.ToList();
                return View(phlab);
            }
            else
            {
                ViewData["fail_status"] = "Thêm thất bại";
            }
            return RedirectToAction("AddPhongLab");
        }

        [HttpPost]
        public ActionResult Delete_PHONGLAB(int? id)
        {
            db = new QL_HDPHONGLABEntities();
            if (id == null)
            {
                return HttpNotFound();
            }
            var check = db.PHONGLABs.Find(id);
            if (check == null)
            {
                return HttpNotFound();
            }
            return View(check);
        }

        [HttpGet]
        public ActionResult Delete_PHONGLAB_Confirmed(int id)
        {
            //PHONGLAB phlab = new PHONGLAB();
            var phonglab = db.PHONGLABs.Find(id);
            db.PHONGLABs.Remove(phonglab);
            db.SaveChanges();
            ViewBag.delete_status = "Dữ liệu đã được xoá";
            return RedirectToAction("AddPhongLab", "PhongLAB");
        }

        public ActionResult XoaPhongLab(int maphlab)
        {

            var lstLAB = db.PHONGLABs.Where(n => n.MAPHLAB == maphlab).ToList();
            foreach (var item in lstLAB)
            {
                db.PHONGLABs.Remove(item);
            }
            db.SaveChanges();
            return RedirectToAction("AddPhongLab", "PhongLAB");
        }

        //public ActionResult Delete_PHONGLAB(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    PHONGLAB pHONGLAB = db.PHONGLABs.Find(id);
        //    if (pHONGLAB == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(pHONGLAB);
        //}

        //// POST: PHONGLABs/Delete/5
        //[HttpPost, ActionName("Delete_PHONGLAB")]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete_PHONGLAB_Confirmed(int id)
        //{
        //    PHONGLAB pHONGLAB = db.PHONGLABs.Find(id);
        //    db.PHONGLABs.Remove(pHONGLAB);
        //    db.SaveChanges();
        //    return RedirectToAction("AddPhongLab");
        //}

        //public ActionResult Xoa_PhongLAB(int maphlab)
        //{
        //    var labs = db.PHONGLABs.Where(n => n.MAPHLAB == maphlab).ToList();
        //    foreach (var item in labs)
        //    {
        //        db.PHONGLABs.Remove(item);
        //    }
        //    db.SaveChanges();
        //    return RedirectToAction("AddPhongLab", "PhongLAB");
        //}
    }
}