using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Controllers
{
    public class TheLoaiController : Controller
    {
        private ThuVienEntities db = new ThuVienEntities();

        // GET: TheLoai
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View(db.TheLoais.ToList());
        }

        // GET: TheLoai/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TheLoai theLoai = db.TheLoais.Find(id);
            if (theLoai == null)
            {
                return HttpNotFound();
            }
            return View(theLoai);
        }

        // GET: TheLoai/Create
        public ActionResult Create()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        // POST: TheLoai/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTL,TenTL")] TheLoai theLoai)
        {
            if (ModelState.IsValid)
            {
                db.TheLoais.Add(theLoai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(theLoai);
        }

        // GET: TheLoai/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TheLoai theLoai = db.TheLoais.Find(id);
            if (theLoai == null)
            {
                return HttpNotFound();
            }
            return View(theLoai);
        }

        // POST: TheLoai/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaTL,TenTL")] TheLoai theLoai)
        {
            if (ModelState.IsValid)
            {
                db.Entry(theLoai).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(theLoai);
        }

        // GET: TheLoai/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TheLoai theLoai = db.TheLoais.Find(id);
            if (theLoai == null)
            {
                return HttpNotFound();
            }
            return View(theLoai);
        }

        // POST: TheLoai/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TheLoai theLoai = db.TheLoais.Find(id);
            var sachs = db.Saches.Where(x => x.MaTL == id).ToList();
            List<ChiTietPhieuMuon> chiTietPhieuMuons = new List<ChiTietPhieuMuon> { };
            foreach ( var sach in sachs )
            {
                var phieuMuons = db.PhieuMuons.Include(x => x.ChiTietPhieuMuons);
                

                foreach (PhieuMuon item in phieuMuons)
                {
                    foreach (ChiTietPhieuMuon itemct in item.ChiTietPhieuMuons)
                    {

                        if (itemct.MaSach == sach.MaSach)
                        {
                            chiTietPhieuMuons.Add(itemct);
                        }
                    }


                }

                

            }
            foreach (ChiTietPhieuMuon item in chiTietPhieuMuons)
            {
                db.ChiTietPhieuMuons.Remove(item);
                db.SaveChanges();
            }

            sachs = db.Saches.Where(x => x.MaTL == id).ToList();
            foreach (Sach sach in sachs)
            {
                db.Saches.Remove(sach);
                db.SaveChanges();
            }

            db.TheLoais.Remove(theLoai);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
