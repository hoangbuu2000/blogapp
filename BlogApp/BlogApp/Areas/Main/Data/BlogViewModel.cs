using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlogApp.Areas.Admin.Data;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Areas.Main.Data
{
    public class BlogViewModel
    {
        public Post Blog { get; set; }
        public Topic Topic { get; set; }
        public Post Prev_Blog { get; set; }
        public Post Next_Blog { get; set; }
        public IEnumerable<Post> RelatedPost { get; set; }
        public IEnumerable<Response> Responses { get; set; }

        // For reply
        public String CurrentBlogID
        {
            get { return Blog.ID.Trim(); }
        }
        [Required(ErrorMessage = "Vui lòng nhập tên")]
        public String Username { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập Email")]
        [EmailAddress(ErrorMessage = "Địa chỉ Email không hợp lệ")]
        public String Email { get; set; }
        public String Website { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập nội dung phản hồi")]
        public String Content { get; set; }
    }
}