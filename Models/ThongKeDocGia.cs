using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyThuVien.Models
{
    public class ThongKeDocGia
    {
        public DateTime start { get; set; }
        public DateTime end { get; set; }

        public ICollection<DocGia> DocGiaCollection { get; set; }

        public ThongKeDocGia() { }
    }
}