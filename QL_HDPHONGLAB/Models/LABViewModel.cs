using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QL_HDPHONGLAB.Models
{
    public class LABViewModel
    {
        // Phòng LAB
        public int MAPHLAB { get; set; }

        [Display(Name = "Số Phòng.")]
        [Required(ErrorMessage = "Bắt buộc phải nhập số phòng.")]
        public string SOPHONG { get; set; }

        [Display(Name = "Hình ảnh phòng lab.")]
        public HttpPostedFileBase HINHANH { get; set; }

        [Display(Name = "Trạng thái đăng ký phòng.")]
        [Required(ErrorMessage = "Bắt buộc phải nhập trạng thái.")]
        public int TRANGTHAI { get; set; }

        [Display(Name = "Loại phòng lab.")]
        [Required(ErrorMessage = "Bắt buộc phải nhập loại phòng.")]
        public int LOAIPHONG { get; set; }

        [Display(Name = "Sức chứa của phòng lab.")]
        [Required(ErrorMessage = "Bắt buộc phải nhập sức chứa của phòng lab.")]
        [Range(1,100, ErrorMessage = "Sức chứa của phòng phải lớn hơn hoặc bằng {0}")]
        public int SUCCHUA { get; set; }

        [Display(Name ="Địa điểm phòng lab.")]
        public string DIADIEM { get; set; }

        [Display(Name ="Ghi Chú.")]
        public string GHICHU { get; set; }
        public List<SelectListItem> DSDangKy { get; set; }
        public List<SelectListItem> DSLoaiPhong { get; set; }
    }
}