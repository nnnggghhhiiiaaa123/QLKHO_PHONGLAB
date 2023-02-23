using QL_HDPHONGLAB.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_HDPHONGLAB.Controllers
{
    public class LABsController : Controller
    {
        private QL_HDPHONGLABEntities db;
        public LABsController()
        {
            db = new QL_HDPHONGLABEntities();
        }
        public ActionResult PhongLab()
        {
            LABViewModel dbLab = new LABViewModel();
            dbLab.DSDangKy = (from obj in db.TTDANGKies
                              select new SelectListItem()
                              {
                                  Text = obj.TRANGTHAI,
                                  Value = obj.MATT.ToString()
                              }).ToList();

            dbLab.DSLoaiPhong = (from obj in db.LOAIPHONGLABs
                                 select new SelectListItem()
                                 {
                                     Text = obj.TENLOAI,
                                     Value = obj.MALOAI.ToString()
                                 }).ToList();
            return View(dbLab);
        }

        [HttpPost]
        public ActionResult PhongLab(LABViewModel dbLabViewModel)
        {
            string message = String.Empty;
            string ImageUniqueName = String.Empty;
            string ActualImageName = String.Empty;

            if(dbLabViewModel.MAPHLAB == 0)
            {
                ImageUniqueName = Guid.NewGuid().ToString();
                ActualImageName = ImageUniqueName + Path.GetExtension(dbLabViewModel.HINHANH.FileName);
                dbLabViewModel.HINHANH.SaveAs(Server.MapPath("~/LABImages/" + ActualImageName));

                PHONGLAB dbPhLab = new PHONGLAB()
                {
                    SOPHONG = dbLabViewModel.SOPHONG,
                    HINHANH = ActualImageName,
                    TRANGTHAI = dbLabViewModel.TRANGTHAI,
                    LOAIPHONG = dbLabViewModel.LOAIPHONG,
                    SUCCHUA = dbLabViewModel.SUCCHUA,
                    DIADIEM = dbLabViewModel.DIADIEM,
                    GHICHU = dbLabViewModel.GHICHU,
                    LABActive = true,
                };

                db.PHONGLABs.Add(dbPhLab);
                message = "Đã thêm mục phòng.";
            }

            else
            {
                PHONGLAB dbPhLab = db.PHONGLABs.Single(model => model.MAPHLAB == dbLabViewModel.MAPHLAB);
                if(dbLabViewModel.HINHANH != null)
                {
                    ImageUniqueName = Guid.NewGuid().ToString();
                    ActualImageName = ImageUniqueName + Path.GetExtension(dbLabViewModel.HINHANH.FileName);
                    dbLabViewModel.HINHANH.SaveAs(Server.MapPath("~/LABImages/" + ActualImageName));
                    dbPhLab.HINHANH = ActualImageName;
                }

                dbPhLab.SOPHONG = dbLabViewModel.SOPHONG;
                dbPhLab.TRANGTHAI = dbLabViewModel.TRANGTHAI;
                dbPhLab.LOAIPHONG = dbLabViewModel.LOAIPHONG;
                dbPhLab.SUCCHUA = dbLabViewModel.SUCCHUA;
                dbPhLab.DIADIEM = dbLabViewModel.DIADIEM;
                dbPhLab.GHICHU = dbLabViewModel.GHICHU;
                dbPhLab.LABActive = true;
                message = "Đã Cập Nhật Mục Phòng.";
            }

            db.SaveChanges();
            return Json(new { message = "Tạo Phòng Thành Công " + message, success = true }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetAllLabs()
        {
            IEnumerable<LABDetailsViewModel> lstLABDetails = (from obj in db.PHONGLABs
                                                              join objDK in db.TTDANGKies on
                                                              // So Sánh
                                                              obj.TRANGTHAI equals objDK.MATT

                                                              join objLoaiPhong in db.LOAIPHONGLABs on

                                                              // So Sánh
                                                              obj.LOAIPHONG equals objLoaiPhong.MALOAI
                                                              where obj.LABActive == true

                                                              select new LABDetailsViewModel()
                                                              {
                                                                  MAPHLAB = obj.MAPHLAB,
                                                                  SOPHONG = obj.SOPHONG,
                                                                  HINHANH = obj.HINHANH,
                                                                  TRANGTHAI = (int)obj.TRANGTHAI,
                                                                  LOAIPHONG = (int)obj.LOAIPHONG,
                                                                  SUCCHUA = (int)obj.SUCCHUA,
                                                                  DIADIEM = obj.DIADIEM,
                                                                  GHICHU = obj.GHICHU,

                                                              }).ToList();
            return PartialView("PhongLab", lstLABDetails);
        }

        [HttpGet]
        public JsonResult SuaPhongLab(int maphlab)
        {
            var result = db.PHONGLABs.Single(model => model.MAPHLAB == maphlab);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult XoaPhongLab(int maphlab)
        {
            PHONGLAB dbPhLab = db.PHONGLABs.Single(model => model.MAPHLAB == maphlab);
            dbPhLab.LABActive = false;
            db.SaveChanges();
            return Json(new { message = "Phòng Lab Đã Được Xoá.", success = true}, JsonRequestBehavior.AllowGet);
        }
    }
}