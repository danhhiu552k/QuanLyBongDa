using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using QuanLyBongDa.Models;
using System.Net;
using System.Data.Entity;
using PagedList.Mvc;
using PagedList;

namespace QuanLyBongDa.Controllers
{
    public class HomeController : Controller
    {
        DBGiaiDauDataContext db = new DBGiaiDauDataContext();
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        /*public PartialViewResult DoiBongPartial()
        {
            return PartialView(db.DoiBongs.ToList());
        }*/


        public ActionResult DanhSachGiaiDau()
        {
            
            GiaiDau GD = db.GiaiDaus.FirstOrDefault();
            return View(GD);
        }
        public ActionResult ChiTietGiaiDau(string MaGiaiDau)
        {
            DoiBong doiBong = db.DoiBongs.FirstOrDefault(a => a.MaGiaiDau == MaGiaiDau);
            if (doiBong == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(doiBong);

            //return View();
        }
        public ViewResult ChiTietDoi(string MaDoiBong)
        {
            CauThu cauthu = db.CauThus.FirstOrDefault(a => a.MaDoiBong == MaDoiBong);
            
            return View(cauthu);
            //return View();
        }

        public ActionResult ThemMoiGiaiDau()
        {
            return View();
        }

        public ActionResult SuaGiaiDau(string MaGiaiDau)
        {
            GiaiDau GD = db.GiaiDaus.FirstOrDefault(x=>x.MaGiaiDau == MaGiaiDau);
            return View(GD);
        }

        public ActionResult ThemDanhGia()
        {
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemDanhGia([Bind(Include = "MaDG, HoTen, SDT, Email, NoiDung")] tDanhGia danhgia)
        {
            if (ModelState.IsValid)
            {
                db.tDanhGias.InsertOnSubmit(danhgia);
                db.SubmitChanges();
                return RedirectToAction("DanhGias12");
            }
            return View(danhgia);
        }
        public PartialViewResult BangDanhGia()
        {
            return PartialView(db.tDanhGias.ToList());
        }
        public ViewResult DanhGias12()
        {
            return View();
        }

        
    }
}
