using Facebook;
using QL_HDPHONGLAB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace QL_HDPHONGLAB.Controllers
{
    public class HomeController : Controller
    {
        private QL_HDPHONGLABEntities db;
        public HomeController()
        {
            db = new QL_HDPHONGLABEntities();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ThongBao()
        {
            return View();
        }
        public ActionResult PhanLoai(string maloai)
        {
            var listTTB = db.LOAITTBs.Take(3).OrderBy(l => l.MALOAI == maloai).ToList();
            return View(listTTB);
        }

        public ActionResult Gioithieu()
        {
            return View();
        }
        public ActionResult ShowLoi404()
        {
            return View();
        }

        public ActionResult ShowLoi500()
        {
            return View();
        }

        public ActionResult KhongChoMuonHC()
        {
            return View();
        }

        public ActionResult KhongChoMuonTB()
        {
            return View();
        }

        public ActionResult KhongChoMuonDC()
        {
            return View();
        }

        public ActionResult KhongTraHC()
        {
            return View();
        }

        public ActionResult KhongTraTB()
        {
            return View();
        }

        public ActionResult KhongTraDC()
        {
            return View();
        }
        private Uri RediredtUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }

        [AllowAnonymous]
        public ActionResult Facebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = "Your App ID",
                client_secret = "Your App Secret key",
                redirect_uri = RediredtUri.AbsoluteUri,
                response_type = "code",
                scope = "email"
            });
            return Redirect(loginUrl.AbsoluteUri);
        }

        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = "Your Facebook api Client id here",
                client_secret = "Your Facebook api Secret key here",
                redirect_uri = RediredtUri.AbsoluteUri,
                code = code
            });
            var accessToken = result.access_token;
            Session["AccessToken"] = accessToken;
            fb.AccessToken = accessToken;
            dynamic me = fb.Get("me?fields=link,first_name,currency,last_name,email,gender,locale,timezone,verified,picture,age_range");
            string email = me.email;
            string lastname = me.lastname;
            string picture = me.picture.data.url;
            FormsAuthentication.SetAuthCookie(email, false);
            return RedirectToAction("Index", "Home");
        }
    }
}