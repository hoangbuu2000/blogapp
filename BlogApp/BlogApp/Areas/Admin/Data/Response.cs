﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BlogApp.Areas.Admin.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class Response
    {
        public string ID { get; set; }
        [Required(ErrorMessage = "Thông tin bắt buộc")]
        public string Content { get; set; }
        public System.DateTime PubDate { get; set; }
        [Required(ErrorMessage = "Thông tin bắt buộc")]
        public string Post_ID { get; set; }
        [Required(ErrorMessage = "Thông tin bắt buộc")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Thông tin bắt buộc")]
        public string Email { get; set; }
        public string Website { get; set; }
    
        public virtual Post Post { get; set; }
    }
}
