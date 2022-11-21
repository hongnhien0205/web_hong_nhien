using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDoCuoi.Models;

namespace WebDoCuoi.Controllers
{
    public class AccountController : Controller
    {
        dbwebEntities db = new dbwebEntities();
        // GET: Account
        public ActionResult Index(int id)
        {
            var acc = db.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
            if (acc == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(acc);
        }
        public ActionResult EditAcc(int id)
        {
            var acc = db.KHACHHANGs.SingleOrDefault(n => n.MaKH == id);
            if (acc == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaKH = new SelectList(db.KHACHHANGs.ToList().OrderBy(n => n.TenKH), "MaKH", "TenKH");
            return View(acc);
        }

    }
}