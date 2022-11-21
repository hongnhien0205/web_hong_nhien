using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebDoCuoi.Models;

namespace WebDoCuoi.Controllers
{
    
    public class HomeController : Controller
    {
        dbwebEntities db = new dbwebEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Footer()
        {
            return View();
        }
        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(FormCollection collection, KHACHHANG kh)
        {
            var sTenKH = collection["Name"];
            var STenDN = collection["UserName"];
            var sMatKhau = collection["PassWord"];
            if (string.IsNullOrEmpty(sTenKH))
            {
                ViewData["err1"] = "Họ tên không được rỗng";
            }
            else if (string.IsNullOrEmpty(STenDN))
            {
                ViewData["err2"] = "Tên đăng nhập không được rỗng";
            }
            else if(string.IsNullOrEmpty(sMatKhau))
            {
                ViewData["err3"] = "Phải nhập mật khẩu";
            }
            else
            {
                kh.TenKH = sTenKH;
                kh.TenDN = STenDN;
                kh.MatKhau = sMatKhau;
                db.KHACHHANGs.Add(kh);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            return this.SignUp();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection f)
        {
            var sTenDN = f["UserName"];
            var sMatKhau = f["PassWord"];
            KHACHHANG ad = db.KHACHHANGs.SingleOrDefault(n => n.TenDN == sTenDN && n.MatKhau == sMatKhau);
            if (ad != null)
            {
                Session["KH"] = ad;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }

        public ActionResult Category()
        {
            return PartialView(db.DANHMUCSPs);
        }

        public ActionResult DesignerCategory()
        {
            return PartialView(db.NTKs);
        }
        
    }
}