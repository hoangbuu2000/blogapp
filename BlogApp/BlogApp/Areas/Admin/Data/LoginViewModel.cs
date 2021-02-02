using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogApp.Areas.Admin.Data
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Thông tin bắt buộc")]
        [Display(Name = "Tài khoản")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Thông tin bắt buộc")]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }
    }
}