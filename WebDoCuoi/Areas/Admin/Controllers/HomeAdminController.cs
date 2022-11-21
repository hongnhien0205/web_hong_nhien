using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDoCuoi.Models;


namespace WebDoCuoi.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        dbwebEntities db = new dbwebEntities();
        // GET: Admin/HomeAdmin
        public ActionResult Index()
        {

            if(Session["Admin"] == null || Session["Admin"].ToString() == "")
            {
                return Redirect("~/Admin/HomeAdmin/Login");
            }
            return View();
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
            ADMIN ad = db.ADMINs.SingleOrDefault(n => n.TenDNAD == sTenDN && n.MatKhauAD == sMatKhau);
            if (ad != null)
            {
                Session["Admin"] = ad;
                return RedirectToAction("Index", "HomeAdmin");
            }
            else
            {
                ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }
       

    }
}