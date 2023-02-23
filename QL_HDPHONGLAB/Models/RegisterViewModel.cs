using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_HDPHONGLAB.Models
{
    public class RegisterViewModel
    {
        // Đăng ký mượn phòng

        [Display(Name = "Họ tên giảng viên.")]
        [Required(ErrorMessage ="Bắt buộc nhập họ tên giảng viên.")]
        public string HOTEN_GV { get; set; }

        [Display(Name ="Địa chỉ giảng viên.")]
        [Required(ErrorMessage ="Bắt buộc nhập địa chỉ.")]
        public string DIACHI_GV { get; set; }

        [Display(Name ="Số điện thoại giảng viên.")]
        [Required(ErrorMessage ="Bắt buộc nhập SĐT.")]
        public string SDT_GV { get; set; }

        [Display(Name ="Ngày đăng ký mượn phòng.")]
        [Required(ErrorMessage ="Bắt buộc ngập ngày đăng ký phòng.")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime NGAY_DKPH { get; set; }

        [Display(Name ="Ngày trả phòng.")]
        [Required(ErrorMessage ="Bắt buộc nhập ngày trả phòng.")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime NGAY_TRAPH { get; set; }

        [Display(Name ="Phòng Chỉ Định.")]
        [Required(ErrorMessage ="Bắt buộc nhập phòng chỉ định.")]
        public int PHONGCHIDINH { get; set; }

        [Display(Name ="Số lượng người.")]
        [Required(ErrorMessage ="Bắt buộc nhập số lượng.")]
        public int SOLUONG { get; set; }

        public IEnumerable<SelectListItem> DSPhongLAB { get; set; }
    }
}