﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyThuVien.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class DocGia
    {
        [DisplayName("Mã độc giả")]
        public int MaDG { get; set; }
        [DisplayName("Nghề nghiệp")]
        [Required(ErrorMessage = "Vui lòng nhập thông tin")]
        public string NgheNghiep { get; set; }
        public int LoaiDG { get; set; }
        public Nullable<int> MaNguoi { get; set; }
      
        public virtual LoaiDG LoaiDG1 { get; set; }
        public virtual TheThuVien TheThuVien { get; set; }
        public virtual Nguoi Nguoi { get; set; }
    }
}
