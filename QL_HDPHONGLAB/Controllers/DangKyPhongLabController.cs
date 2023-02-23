using QL_HDPHONGLAB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_HDPHONGLAB.Controllers
{
    public class DangKyPhongLabController : Controller
    {
        private QL_HDPHONGLABEntities db;
        public DangKyPhongLabController()
        {
            db = new QL_HDPHONGLABEntities();
        }

        public ActionResult DangKyPhong()
        {
            RegisterViewModel dbRegister = new RegisterViewModel();
            dbRegister.DSPhongLAB = (from objLAB in db.PHONGLABs
                                     where objLAB.TRANGTHAI == 0
                                     select new SelectListItem()
                                     {
                                         Text = objLAB.SOPHONG,
                                         Value = objLAB.MAPHLAB.ToString()
                                     }).ToList();

            dbRegister.NGAY_DKPH = DateTime.Now;
            dbRegister.NGAY_TRAPH = DateTime.Now.AddDays(1);
            return View(dbRegister);
        }

        [HttpPost]
        public ActionResult DangKyPhong(RegisterViewModel dbRegister)
        {
            int songay = Convert.ToInt32(dbRegister.NGAY_TRAPH - dbRegister.NGAY_DKPH);
            PHONGLAB dbPhLab = db.PHONGLABs.Single(model => model.MAPHLAB == dbRegister.PHONGCHIDINH);

            DANGKYPHONG dkphong = new DANGKYPHONG()
            {
                HOTEN_GV = dbRegister.HOTEN_GV,
                DIACHI_GV = dbRegister.DIACHI_GV,
                SDT_GV = dbRegister.SDT_GV,
                NGAY_DKPH = dbRegister.NGAY_DKPH,
                NGAY_TRAPH = dbRegister.NGAY_TRAPH,
                //PHONGCHIDINH = dbRegister.PHONGCHIDINH,
                SOLUONG = dbRegister.SOLUONG
            };

            db.DANGKYPHONGs.Add(dkphong);

            db.SaveChanges();

            dbPhLab.TRANGTHAI = 1;
            db.SaveChanges();
            return Json(new { message = "Đã Đăng Ký Phòng.", success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public PartialViewResult LichSuDKPhong()
        {
            List<LABRegisterViewModel> lstLABRegister = new List<LABRegisterViewModel>();
            lstLABRegister = (from objLABRegister in db.DANGKYPHONGs
                              //join objPhLAB in db.PHONGLABs on objLABRegister.PHONGCHIDINH equals objPhLAB.MAPHLAB
                              select new LABRegisterViewModel()
                              {
                                  MADK = objLABRegister.MADK,
                                  //SOPHONG = objPhLAB.SOPHONG,
                                  HOTEN_GV = objLABRegister.HOTEN_GV,
                                  DIACHI_GV = objLABRegister.DIACHI_GV,
                                  SDT_GV = objLABRegister.SDT_GV,
                                  SOLUONG = (int)objLABRegister.SOLUONG,
                                  NGAY_DKPH = (DateTime)objLABRegister.NGAY_DKPH,
                                  NGAY_TRAPH = (DateTime)objLABRegister.NGAY_TRAPH,
                                  SONGAY = System.Data.Entity.DbFunctions.DiffDays(objLABRegister.NGAY_DKPH, objLABRegister.NGAY_TRAPH).Value
                              }).ToList();

            return PartialView("_LichSuDKPhongPartial", lstLABRegister);
        }
    }
}