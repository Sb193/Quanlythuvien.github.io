using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyThuVien.Models
{
    public class ThongKePhieuMuon
    {
        public DateTime start { get; set; }
        public DateTime end { get; set; }

        public ICollection<PhieuMuon> phieuMuonCollection { get; set; }

        public ThongKePhieuMuon() { }
    }
}