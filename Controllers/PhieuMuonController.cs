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
    public class PhieuMuonController : Controller
    {
        private ThuVienEntities db = new ThuVienEntities();

        // GET: PhieuMuon
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var phieuMs = db.PhieuMuons.ToList();
            var phieuMuons = db.PhieuMuons.Include(p => p.LuaChon1).Include(p => p.NhanVien).Include(p => p.TheThuVien).Include(p => p.TrangThai1);
            foreach (var p in phieuMs)
            {
                if (p.NgayTra < DateTime.Now && p.TrangThai == 0)
                {
                    p.TrangThai = 2;
                    db.Entry(p).State = EntityState.Modified;
                    db.SaveChanges();
                } else if (p.NgayTra > DateTime.Now && p.TrangThai == 2)
                {
                    p.TrangThai = 0;
                    db.Entry(p).State = EntityState.Modified;
                    db.SaveChanges();
                }

                
            }
            return View(phieuMuons.ToList());
        }

        // GET: PhieuMuon/Details/5
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
            PhieuMuon phieuMuon = db.PhieuMuons.Include(x => x.ChiTietPhieuMuons).Include(x => x.PhieuPhats).Where(x => x.MaPM == id).FirstOrDefault();
            if (phieuMuon == null)
            {
                return HttpNotFound();
            }
            return View(phieuMuon);
        }

        // GET: PhieuMuon/Create
        public ActionResult Create()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.LuaChon = new SelectList(db.LuaChons, "id", "name");
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "Chucvu");
            ViewBag.MaDG = new SelectList(db.TheThuViens, "MaDG", "MaDG");
            
            return View();
        }

        // POST: PhieuMuon/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaPM,MaDG,MaNV,NgayMuon,NgayTra,LuaChon,TrangThai")] PhieuMuon phieuMuon)
        {
            TheThuVien the = db.TheThuViens.Where(x => x.MaDG == phieuMuon.MaDG).Include(x => x.DocGia).FirstOrDefault();
            NhanVien nhanVien = db.NhanViens.Where(x => x.MaNV == phieuMuon.MaNV).FirstOrDefault();
            List<PhieuMuon> dangmuon = db.PhieuMuons.Where(x => x.TrangThai == 0 && x.MaDG == phieuMuon.MaDG).Include(x => x.ChiTietPhieuMuons).ToList();
            int sl = 0;
            foreach (PhieuMuon pm in dangmuon)
            {
                foreach (ChiTietPhieuMuon ct in pm.ChiTietPhieuMuons)
                {
                    sl += ct.SoLuong;
                }
            }

            if (phieuMuon.LuaChon == 0)
            {
                phieuMuon.NgayMuon = DateTime.Now;
                phieuMuon.NgayTra = DateTime.Now;
            }

            if (the == null)
            {
                //The thu vien khong ton tai
                phieuMuon.ErMess = "Độc giả này không tồn tại";
                ViewBag.LuaChon = new SelectList(db.LuaChons, "id", "name", phieuMuon.LuaChon);

                return View(phieuMuon);
            }
            if (nhanVien == null)
            {
                //Nhan vien khong ton tai
                phieuMuon.ErMess = "Nhân viên này không tồn tại";
                ViewBag.LuaChon = new SelectList(db.LuaChons, "id", "name", phieuMuon.LuaChon);

                return View(phieuMuon);
            }
            else if (sl >= (the.DocGia.LoaiDG * 2 + 2))
            {
                //So luong cho moi cap do la 2 4 6 Trong TH nay qua so luong muon
                phieuMuon.ErMess = "Độc giả này đã mượn tối đa sách (Hiện tại đang mượn " + sl.ToString() +" cuốn)";
                
                ViewBag.LuaChon = new SelectList(db.LuaChons, "id", "name", phieuMuon.LuaChon);
                return View(phieuMuon);
            }
            else if (the.ThoiHan < DateTime.Now)
            {
                //The thu vien da het han
                phieuMuon.ErMess = "Thẻ thư viện của độc giả này đã hết hạn";
                ViewBag.LuaChon = new SelectList(db.LuaChons, "id", "name", phieuMuon.LuaChon);
                return View(phieuMuon);
            }
            else if (the.ThoiHan < phieuMuon.NgayTra)
            {
                //The thu vien khong du den ngay muon
                phieuMuon.ErMess = "Thẻ thư viện này hết hạn trươc ngày trả";
                ViewBag.LuaChon = new SelectList(db.LuaChons, "id", "name", phieuMuon.LuaChon);
                return View(phieuMuon);
            }
            else if (the.DocGia.LoaiDG == 0)
            {
                //Normal khong muon qua 7 ngay
                if (phieuMuon.NgayTra > DateTime.Now.AddDays(7))
                {
                    phieuMuon.ErMess = "Độc giả Học sinh không thể mượn sách quá 7 ngày";
                    ViewBag.LuaChon = new SelectList(db.LuaChons, "id", "name", phieuMuon.LuaChon);
                    return View(phieuMuon);
                }
            }
            else if (the.DocGia.LoaiDG == 1)
            {
                //Premium khong muon qua 14 ngay
                if (phieuMuon.NgayTra > DateTime.Now.AddDays(14))
                {
                    phieuMuon.ErMess = "Độc giả Giáo viên không thể mượn sách quá 14 ngày";
                    
                    ViewBag.LuaChon = new SelectList(db.LuaChons, "id", "name", phieuMuon.LuaChon);
                    return View(phieuMuon);
                }
            }
            else if (the.DocGia.LoaiDG == 2)
            {
                // Pro khong muon qua 30 ngay
                if (phieuMuon.NgayTra > DateTime.Now.AddDays(30))
                {
                    phieuMuon.ErMess = "Độc giả Pro không thể mượn sách quá 30 ngày";
                    
                    ViewBag.LuaChon = new SelectList(db.LuaChons, "id", "name", phieuMuon.LuaChon);
                    return View(phieuMuon);
                }
            } 
            
            if (phieuMuon.NgayTra < phieuMuon.NgayMuon)
            {
                phieuMuon.ErMess = "Ngày mượn không thể trước ngày trả";
                ViewBag.LuaChon = new SelectList(db.LuaChons, "id", "name", phieuMuon.LuaChon);
                return View(phieuMuon);
            }

            if (phieuMuon.NgayMuon < DateTime.Now.AddDays(-30))
            {
                phieuMuon.ErMess = "Không thể tạo phiếu mượn cách quá 1 tháng";
                ViewBag.LuaChon = new SelectList(db.LuaChons, "id", "name", phieuMuon.LuaChon);
                return View(phieuMuon);
            }

            if (phieuMuon.NgayMuon > DateTime.Now.AddDays(1))
            {
                phieuMuon.ErMess = "Không thể tạo phiếu mượn trước quá 1 ngày";
                ViewBag.LuaChon = new SelectList(db.LuaChons, "id", "name", phieuMuon.LuaChon);
                return View(phieuMuon);
            }

            if (ModelState.IsValid)
            {
                phieuMuon.TrangThai = 0;
                db.PhieuMuons.Add(phieuMuon);
                db.SaveChanges();

                PhieuMuon newPhieumuon = db.PhieuMuons.ToList().Last();
                return RedirectToAction("Index", "ChiTietPhieuMuon", new { id = newPhieumuon.MaPM });
            }
            
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "Chucvu", phieuMuon.MaNV);
            ViewBag.MaDG = new SelectList(db.TheThuViens, "MaDG", "MaDG", phieuMuon.MaDG);
            
            return View(phieuMuon);
        }

        // GET: PhieuMuon/Edit/5
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
            PhieuMuon phieuMuon = db.PhieuMuons.Find(id);
            if (phieuMuon == null)
            {
                return HttpNotFound();
            }
            ViewBag.LuaChon = new SelectList(db.LuaChons, "id", "name", phieuMuon.LuaChon);
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "Chucvu", phieuMuon.MaNV);
            ViewBag.MaDG = new SelectList(db.TheThuViens, "MaDG", "MaDG", phieuMuon.MaDG);
            ViewBag.TrangThai = new SelectList(db.TrangThais, "id", "name", phieuMuon.TrangThai);
            return View(phieuMuon);
        }

        // POST: PhieuMuon/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaPM,MaDG,MaNV,NgayMuon,NgayTra,LuaChon,TrangThai")] PhieuMuon phieuMuon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phieuMuon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LuaChon = new SelectList(db.LuaChons, "id", "name", phieuMuon.LuaChon);
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "Chucvu", phieuMuon.MaNV);
            ViewBag.MaDG = new SelectList(db.TheThuViens, "MaDG", "MaDG", phieuMuon.MaDG);
            ViewBag.TrangThai = new SelectList(db.TrangThais, "id", "name", phieuMuon.TrangThai);
            return View(phieuMuon);
        }

        // GET: PhieuMuon/Delete/5
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
            PhieuMuon phieuMuon = db.PhieuMuons.Find(id);
            if (phieuMuon == null)
            {
                return HttpNotFound();
            }
            return View(phieuMuon);
        }

        // POST: PhieuMuon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            List<ChiTietPhieuMuon> cts = db.ChiTietPhieuMuons.Where(x => x.MaPM == id).ToList();
            foreach (ChiTietPhieuMuon ct in cts)
            {
                db.ChiTietPhieuMuons.Remove(ct);
                db.SaveChanges();
            }

            List<PhieuPhat> pp = db.PhieuPhats.Where(x => x.MaPM == id).ToList();
            foreach (PhieuPhat ct in pp)
            {
                db.PhieuPhats.Remove(ct);
                db.SaveChanges();
            }


            PhieuMuon phieuMuon = db.PhieuMuons.Find(id);
            db.PhieuMuons.Remove(phieuMuon);
            db.SaveChanges();

            
            return RedirectToAction("Index");
        }


        // GET: PhieuMuon/Edit/5
        public ActionResult Update(int? id)
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuMuon phieuMuon = db.PhieuMuons.Include(x => x.ChiTietPhieuMuons).Where(x => x.MaPM == id).FirstOrDefault();
            if (phieuMuon == null)
            {
                return HttpNotFound();
            }

            ViewBag.Id = id;
            ViewBag.LuaChon = new SelectList(db.LuaChons, "id", "name", phieuMuon.LuaChon);
            ViewBag.MaNV = new SelectList(db.NhanViens, "MaNV", "Chucvu", phieuMuon.MaNV);
            ViewBag.MaDG = new SelectList(db.TheThuViens, "MaDG", "MaDG", phieuMuon.MaDG);
            ViewBag.TrangThai = new SelectList(db.TrangThais, "id", "name", phieuMuon.TrangThai);
            
            return View(phieuMuon);
        }

        // POST: PhieuMuon/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update([Bind(Include = "MaPM,MaDG,MaNV,NgayMuon,NgayTra,LuaChon,TrangThai,ChiTietPhieuMuons")] PhieuMuon phieuMuon)
        {
            if (ModelState.IsValid)
            {
                List<ChiTietPhieuMuon> cts = db.ChiTietPhieuMuons.Where(x =>x.MaPM == phieuMuon.MaPM).ToList();
                foreach (ChiTietPhieuMuon ct in cts) {
                    Sach sach = db.Saches.Find(ct.MaSach);
                    if (sach != null) {
                        sach.SoLuongTT = sach.SoLuongTT + ct.SoLuong;
                        db.Entry(sach).State= EntityState.Modified;
                        db.SaveChanges();
                        
                    }
                    

                }

                phieuMuon.TrangThai = 1;
                
                db.Entry(phieuMuon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(phieuMuon);
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
