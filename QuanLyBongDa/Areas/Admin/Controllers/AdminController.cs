using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyBongDa.Models;

namespace QuanLyBongDa.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        DBGiaiDauDataContext db = new DBGiaiDauDataContext();
        // GET: Admin/Admin
        public ActionResult Index()
        {
            return View();
        }    

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(tTaiKhoan objUser)
        {
            if (ModelState.IsValid)
            {
                using (DBGiaiDauDataContext db = new DBGiaiDauDataContext())
                {
                    var obj = db.tTaiKhoans.Where(a => a.TaiKhoan.Equals(objUser.TaiKhoan) && a.MatKhau.Equals(objUser.MatKhau)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["TaiKhoan"] = obj.TaiKhoan.ToString();
                        Session["MatKhau"] = obj.MatKhau.ToString();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Message = "Mật Khẩu Hoặc Tài Khoản Sai";
                    }
                }
            }
            return View(objUser);
        }

        public ActionResult Logout()
        {
            DBGiaiDauDataContext db = new DBGiaiDauDataContext();
            Session["TaiKhoan"] = null;
            Session["MatKhau"] = null;
            return RedirectToAction("Index");
        }



        public PartialViewResult DanhSachGiaiDau()
        {
            return PartialView(db.GiaiDaus.ToList());
        }

        public ViewResult ChiTietGiaiDau(string MaGD = "G01")
        {
            GiaiDau gd = db.GiaiDaus.SingleOrDefault(n => n.MaGiaiDau == MaGD);
            if (gd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(gd);
        }

        [HttpGet]
        public ViewResult ThemDoiBong()
        {
            ViewBag.MaGiaiDau = new SelectList(db.GiaiDaus.ToList(), "MaGiaiDau", "TenGiaiDau");
            ViewBag.MaHLV = new SelectList(db.HuanLuyenViens.ToList(), "MaHLV", "TenHLV");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemDoiBong([Bind(Include ="MaDoiBong, TenDoi, MaGiaiDau, MaHLV")] DoiBong doib)
        {
            if (ModelState.IsValid)
            {
                db.DoiBongs.InsertOnSubmit(doib);
                db.SubmitChanges();
                return RedirectToAction("/");
            }
            return View(doib);
        }
        [HttpGet]
        public ActionResult Xoa(string MaGD)
        {
            GiaiDau gd = db.GiaiDaus.SingleOrDefault(n => n.MaGiaiDau == MaGD);
            if (gd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(gd);
        }

        [HttpPost, ActionName("Xoa")]
        public ActionResult XacNhanXoa(string MaGD)
        {
            GiaiDau gd = db.GiaiDaus.SingleOrDefault(n => n.MaGiaiDau == MaGD);
            
            if (gd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
           
            db.GiaiDaus.DeleteOnSubmit(gd);
            db.SubmitChanges();
            return RedirectToAction("/");
        }

        public ActionResult ThemMoiGiaiDau()
        {
            return View();
        }
    }
}