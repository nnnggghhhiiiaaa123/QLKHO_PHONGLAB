using System.Web.Mvc;
using System.Web.Routing;

namespace QL_HDPHONGLAB.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Dangnhap", controller = "LoginAdmin", id = UrlParameter.Optional }
            );
        }
    }
}