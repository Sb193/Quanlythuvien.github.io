using QuanLyThuVien.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyThuVien.Controllers
{
    public class LoginController : Controller
    {

        // GET: Login
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Autherize(TaiKhoan account)
        {
            using (ThuVienEntities db = new ThuVienEntities())
            {
                var userDetails = db.TaiKhoans.Where(x => x.TaiKhoan1 == account.TaiKhoan1 && x.MatKhau == account.MatKhau).FirstOrDefault();
                if (userDetails == null)
                {
                    account.LoginErrorMessage = "Tài khoản hoặc mật khẩu không đúng";
                    return View("Index", account);
                }

                if (ModelState.IsValid || userDetails != null)
                {

                    
                    
                        var staff = db.NhanViens.Where(x => x.MaNV == userDetails.MaNV).Include(x => x.Nguoi).FirstOrDefault();
                        
                        Session["user"] = userDetails;
                        Session["staff"] = staff;
                        
                        return RedirectToAction("Index", "TrangChu");

                    
                }

                else
                {
                    return View("Index", account);
                }
            }
        }

    }
}