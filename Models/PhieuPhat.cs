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

    public partial class PhieuPhat
    {
        [DisplayName("Mã phiếu phạt")]
        public int MaPP { get; set; }
        public Nullable<int> MaPM { get; set; }
        [DisplayName("Lý do")]
        [Required(ErrorMessage = "Vui lòng nhập thông tin")]
        public string LyDo { get; set; }
        public virtual PhieuMuon PhieuMuon { get; set; }
    }
}
