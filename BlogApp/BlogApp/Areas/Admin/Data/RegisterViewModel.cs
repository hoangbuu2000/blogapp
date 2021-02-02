using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using BlogApp.Areas.Admin.Validation;

namespace BlogApp.Areas.Admin.Data
{
    public class RegisterViewModel
    {
        public string ID
        {
            get { return Guid.NewGuid().ToString().Substring(0, 10); }
        }
        [Required(ErrorMessage = "Thông tin bắt buộc")]
        [Display(Name = "Tài khoản")]
        [Validation.MaxLength(16)]
        [NoSpace()]
        public string Username { get; set; }
        [Required(ErrorMessage = "Thông tin bắt buộc")]
        [Display(Name = "Mật khẩu")]
        [Validation.MaxLength(16)]
        [NoSpace()]
        public string Password { get; set; }
        [Required(ErrorMessage = "Thông tin bắt buộc")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Thông tin bắt buộc")]
        [Display(Name = "Họ tên")]
        public string Fullname { get; set; }
        public byte Active
        {
            get { return 1; }
        }
        public DateTime AccessDate
        {
            get { return DateTime.Now; }
        }
        public int Role_ID
        {
            get { return 2; }
        }
    }
}