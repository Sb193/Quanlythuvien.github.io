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

    public partial class NhanVien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NhanVien()
        {
            this.PhieuMuons = new HashSet<PhieuMuon>();
            this.TaiKhoans = new HashSet<TaiKhoan>();
        }

        [DisplayName("Mã nhân viên")]
        [Required(ErrorMessage = "Vui lòng nhập thông tin")]
        public string MaNV { get; set; }
        [DisplayName("Chức vụ")]
        [Required(ErrorMessage = "Vui lòng nhập thông tin")]
        public string Chucvu { get; set; }

        public Nullable<int> MaNguoi { get; set; }
        
        public virtual Nguoi Nguoi { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhieuMuon> PhieuMuons { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TaiKhoan> TaiKhoans { get; set; }
        public string ErMess { get; internal set; }
    }
}