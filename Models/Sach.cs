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

    public partial class Sach
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sach()
        {
            this.ChiTietPhieuMuons = new HashSet<ChiTietPhieuMuon>();
        }
        [DisplayName("Mã sách")]
        public int MaSach { get; set; }
        [DisplayName("Tên sách")]
        [Required(ErrorMessage = "Vui lòng nhập thông tin")]
        public string TenSach { get; set; }
        [DisplayName("Tác giả")]
        [Required(ErrorMessage = "Vui lòng nhập thông tin")]
        public string TenTG { get; set; }
        [DisplayName("Số lượng")]
        [Required(ErrorMessage = "Vui lòng nhập thông tin")]
        public int SoLuong { get; set; }
        [DisplayName("Số lượng thực tế")]
        [Required(ErrorMessage = "Vui lòng nhập thông tin")]
        public int SoLuongTT { get; set; }
        [DisplayName("Mã thể loại")]
        [Required(ErrorMessage = "Vui lòng nhập thông tin")]
        public int MaTL { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPhieuMuon> ChiTietPhieuMuons { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập thông tin")]
        public virtual TheLoai TheLoai { get; set; }
        public string ErMes { get; internal set; }
    }
}