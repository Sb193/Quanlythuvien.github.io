﻿using System;
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
    public class NhanVienController : Controller
    {
        private ThuVienEntities db = new ThuVienEntities();

        // GET: NhanVien
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var nhanViens = db.NhanViens.Include(n => n.Nguoi);
            return View(nhanViens.ToList());
        }

        // GET: NhanVien/Details/5
        public ActionResult Details(string id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // GET: NhanVien/Create
        public ActionResult Create()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        // POST: NhanVien/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaNV,Chucvu,Nguoi")] NhanVien nhanVien)
        {
            if (db.NhanViens.Find(nhanVien.MaNV) == null)
            {
                nhanVien.ErMess = "Mã nhân viên đã tồn tại";
                return View(nhanVien);
            }
            if (ModelState.IsValid && db.NhanViens.Find(nhanVien.MaNV) == null && nhanVien.Nguoi != null)
            {
                db.Nguois.Add(nhanVien.Nguoi);
                db.SaveChanges();
                Nguoi nguoi = db.Nguois.ToList().Last();
                nhanVien.MaNguoi = nguoi.MaNguoi;


                db.NhanViens.Add(nhanVien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            
            return View(nhanVien);
        }

        // GET: NhanVien/Edit/5
        public ActionResult Edit(string id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaNguoi = new SelectList(db.Nguois, "MaNguoi", "HoTen", nhanVien.MaNguoi);
            return View(nhanVien);
        }

        // POST: NhanVien/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaNV,Chucvu,MaNguoi")] NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhanVien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaNguoi = new SelectList(db.Nguois, "MaNguoi", "HoTen", nhanVien.MaNguoi);
            return View(nhanVien);
        }

        // GET: NhanVien/Delete/5
        public ActionResult Delete(string id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhanVien nhanVien = db.NhanViens.Find(id);
            if (nhanVien == null)
            {
                return HttpNotFound();
            }
            return View(nhanVien);
        }

        // POST: NhanVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            NhanVien nhanVien = db.NhanViens.Include(x => x.Nguoi).Where(x => x.MaNV == id).FirstOrDefault();
            Nguoi nguoi = nhanVien.Nguoi;
            List<PhieuMuon> phieuMuons= db.PhieuMuons.Where(x => x.MaNV == id).ToList();
            foreach (PhieuMuon phieuMuon in phieuMuons) {
                List<ChiTietPhieuMuon> cts = db.ChiTietPhieuMuons.Where(x => x.MaPM == phieuMuon.MaPM).ToList();
                foreach (ChiTietPhieuMuon ct in cts)
                {
                    db.ChiTietPhieuMuons.Remove(ct);
                    db.SaveChanges();
                }

                List<PhieuPhat> pp = db.PhieuPhats.Where(x => x.MaPM == phieuMuon.MaPM).ToList();
                foreach (PhieuPhat ct in pp)
                {
                    db.PhieuPhats.Remove(ct);
                    db.SaveChanges();
                }

                db.PhieuMuons.Remove(phieuMuon);
                db.SaveChanges();

            }

            List<TaiKhoan> taiKhoans= db.TaiKhoans.Where(x => x.MaNV == id).ToList() ;
            foreach (TaiKhoan taiKhoan in taiKhoans)
            {
                db.TaiKhoans.Remove(taiKhoan);
                db.SaveChanges();
            }


            db.NhanViens.Remove(nhanVien);
            db.SaveChanges();

            db.Nguois.Remove(nguoi);
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
