using QuanLyThuVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyThuVien.Controllers
{
    public class TrangChuController : Controller
    {
        // GET: TrangChu
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            using (ThuVienEntities db = new ThuVienEntities())
            {
                var saches = db.Saches.ToList() as List<Sach>;
                int i = 0;
                foreach (Sach item in saches)
                {
                    i = i + item.SoLuong;
                }
                this.ViewBag.Sach = i;
                this.ViewBag.Docgia = db.DocGias.ToList().Count;
                this.ViewBag.Dangmuon = db.PhieuMuons.Where(x => x.TrangThai == 0).ToList().Count;
                this.ViewBag.Quahan = db.PhieuMuons.Where(x => x.TrangThai == 2).ToList().Count;

            }
            return View();
        }

        [HttpPost]
        public ActionResult Logout()
        {
            Session["user"] = null;
            Session["staff"] = null;
            return RedirectToAction("Index", "Login");
        }

        public ActionResult Infor()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
    }
}