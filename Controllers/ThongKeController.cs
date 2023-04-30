using QuanLyThuVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyThuVien.Controllers
{
    public class ThongKeController : Controller
    {
        private ThuVienEntities db = new ThuVienEntities();
        // GET: ThongKe
        public ActionResult Phieumuon(DateTime? start , DateTime? end)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (start == null || end == null)
            {
                end = DateTime.Now;
                start = DateTime.Now.AddDays(-7);
            }
            List<PhieuMuon> phieuMuons = db.PhieuMuons.Where(x => x.NgayMuon >= (DateTime)start && x.NgayMuon <= (DateTime)end).ToList();
            ThongKePhieuMuon thongKe = new ThongKePhieuMuon();
            thongKe.start = (DateTime)start;
            thongKe.end = (DateTime)end;
            thongKe.phieuMuonCollection = phieuMuons;
            return View(thongKe);
        }

        // POST: Sach/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Phieumuon([Bind(Include = "start , end")] ThongKePhieuMuon tk)
        {
            List<PhieuMuon> phieuMuons = null ;
            ThongKePhieuMuon thongKe = new ThongKePhieuMuon();
            if (ModelState.IsValid)
            {
                phieuMuons = db.PhieuMuons.Where(x => x.NgayMuon >= tk.start && x.NgayMuon <= tk.end).ToList();
                thongKe = new ThongKePhieuMuon();
                thongKe.start = tk.start;
                thongKe.end = tk.end;
                thongKe.phieuMuonCollection = phieuMuons;
                return View(thongKe);
            }

            DateTime start= DateTime.Now;
            DateTime end = DateTime.Now.AddDays(-7);

            phieuMuons = db.PhieuMuons.Where(x => x.NgayMuon >= (DateTime)start && x.NgayMuon <= (DateTime)end).ToList();
            thongKe = new ThongKePhieuMuon();
            thongKe.start = (DateTime)start;
            thongKe.end = (DateTime)end;
            thongKe.phieuMuonCollection = phieuMuons;
            return View(thongKe);

            
        }


        public ActionResult ViPham(DateTime? start, DateTime? end)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (start == null || end == null)
            {
                end = DateTime.Now;
                start = DateTime.Now.AddDays(-7);
            }
            List<PhieuMuon> phieuMuons = db.PhieuMuons.Where(x => x.PhieuPhats.Count > 0 && x.NgayMuon >= (DateTime)start && x.NgayMuon <= (DateTime)end).ToList();
            ThongKePhieuMuon thongKe = new ThongKePhieuMuon();
            thongKe.start = (DateTime)start;
            thongKe.end = (DateTime)end;
            thongKe.phieuMuonCollection = phieuMuons;
            return View(thongKe);
        }

        // POST: Sach/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ViPham([Bind(Include = "start , end")] ThongKePhieuMuon tk)
        {
            List<PhieuMuon> phieuMuons = null;
            ThongKePhieuMuon thongKe = new ThongKePhieuMuon();
            if (ModelState.IsValid)
            {
                phieuMuons = db.PhieuMuons.Where(x => x.PhieuPhats.Count > 0 && x.NgayMuon >= tk.start && x.NgayMuon <= tk.end).ToList();
                thongKe = new ThongKePhieuMuon();
                thongKe.start = tk.start;
                thongKe.end = tk.end;
                thongKe.phieuMuonCollection = phieuMuons;
                return View(thongKe);
            }

            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now.AddDays(-7);

            phieuMuons = db.PhieuMuons.Where(x => x.PhieuPhats.Count > 0 && x.NgayMuon >= (DateTime)start && x.NgayMuon <= (DateTime)end).ToList();
            thongKe = new ThongKePhieuMuon();
            thongKe.start = (DateTime)start;
            thongKe.end = (DateTime)end;
            thongKe.phieuMuonCollection = phieuMuons;
            return View(thongKe);


        }

        public ActionResult Sach()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var saches = db.Saches;
            return View(saches.ToList());
            
        }

        public ActionResult Docgia(DateTime? start, DateTime? end)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (start == null || end == null)
            {
                end = DateTime.Now;
                start = DateTime.Now.AddDays(-7);
            }
            List<DocGia>  docGias = db.DocGias.Where(x => x.TheThuVien.ThoiHan >= start && x.TheThuVien.ThoiHan <= end).ToList();
            ThongKeDocGia thongKe = new ThongKeDocGia();
            thongKe.start = (DateTime)start;
            thongKe.end = (DateTime)end;
            thongKe.DocGiaCollection = docGias;
            return View(thongKe);
        }

        // POST: Sach/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Docgia([Bind(Include = "start , end")] ThongKeDocGia tk)
        {
            List<DocGia> docGias = null;
            ThongKeDocGia thongKe = new ThongKeDocGia();
            if (ModelState.IsValid)
            {
                docGias = db.DocGias.Where(x => x.TheThuVien.ThoiHan >= tk.start && x.TheThuVien.ThoiHan <= tk.end).ToList();
                thongKe = new ThongKeDocGia();
                thongKe.start = tk.start;
                thongKe.end = tk.end;
                thongKe.DocGiaCollection = docGias;
                return View(thongKe);
            }

            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now.AddDays(-7);

            docGias = db.DocGias.Where(x => x.TheThuVien.ThoiHan >= start && x.TheThuVien.ThoiHan <= end).ToList();
            thongKe = new ThongKeDocGia();
            thongKe.start = start;
            thongKe.end = end;
            thongKe.DocGiaCollection = docGias;
            return View(thongKe);


        }
    }
}