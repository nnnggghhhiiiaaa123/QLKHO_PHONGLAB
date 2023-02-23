using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QL_HDPHONGLAB.Models
{
    public class ChangPassword
    {
        QL_HDPHONGLABEntities db = new QL_HDPHONGLABEntities();
        //[Display(Name = "Tên tài khoản")]
        //[Required(ErrorMessage = "Tên tài khoản là bắt buộc")]
        //public string USERNAME { get; set; }

        [Display(Name = "Mật khẩu hiện tại")]
        [Required(ErrorMessage = "Mật khẩu hiện tại là bắt buộc")]
        [DataType(DataType.Password)]
        public string CURRENTPASSWORD { get; set; }

        [Display(Name = "Mật khẩu mới")]
        [Required(ErrorMessage = "Mật khẩu mới là bắt buộc")]
        [DataType(DataType.Password)]
        public string NEWPASSWORD { get; set; }

        [Display(Name = "Xác nhận mật khẩu")]
        [Required(ErrorMessage = "Xác nhận mật khẩu là bắt buộc")]
        [Compare(otherProperty: "NEWPASSWORD", ErrorMessage = "Mật khẩu mới không khớp.")]
        [DataType(DataType.Password)]
        public string CONFIRMPASSWORD { get; set; }
    }
}