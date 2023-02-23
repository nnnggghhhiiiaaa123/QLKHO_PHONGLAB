using QL_HDPHONGLAB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace QL_HDPHONGLAB.Controllers
{
    public class LoginController : Controller
    {
        QL_HDPHONGLABEntities db = new QL_HDPHONGLABEntities();
        // GET: Login
        public ActionResult DanhSach()
        {
            return View(db.NGUOIDUNGs.ToList());
        }

        [HttpGet]
        public ActionResult FormDangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NguoiDung(NGUOIDUNG nd)
        {
            var check = db.NGUOIDUNGs.Where(s => s.EMAIL.Equals(nd.EMAIL) && s.PASSWORD.Equals(nd.PASSWORD)).FirstOrDefault();
            if (check != null)
            {
                return RedirectToAction("Index", "Home");
                
            }
            else
            {
                return View("ShowLoi404", "Home");
            }
        }


        //Logout
        public ActionResult Logout()
        {
            Session["use"] = null;
            return RedirectToAction("FormDangNhap", "Login");
        }

        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Changepassword(DOIMATKHAU login)
        {
            using (QL_HDPHONGLABEntities db = new QL_HDPHONGLABEntities())
            {
                var detail = db.DOIMATKHAUs.Where(log => log.PASSWORD == login.PASSWORD
                && log.EMAIL == login.EMAIL && log.NEWPASSWORD == login.NEWPASSWORD).FirstOrDefault();
                if (detail != null)
                {
                    detail.PASSWORD = login.NEWPASSWORD;
                    detail.NEWPASSWORD = login.CONFIRMPASSWORD;

                    db.SaveChanges();
                    ViewBag.Message = "Cập nhật thành công!";
                    return RedirectToAction("FormDangNhapAfter");

                }
                else
                {
                    ViewBag.Message = "Mật khẩu chưa được cập nhật!";
                }


            }

            return View(login);
        }

        [HttpGet]
        public ActionResult FormDangNhapAfter()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AuthenAfter(DOIMATKHAU change)
        {
            var check = db.DOIMATKHAUs.Where(s => s.EMAIL.Equals(change.EMAIL) && s.PASSWORD.Equals(change.PASSWORD)).FirstOrDefault();
            if (check == null)
            {
                return RedirectToAction("ShowLoi404", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}