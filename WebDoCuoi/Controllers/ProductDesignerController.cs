using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using WebDoCuoi.Models;

namespace WebDoCuoi.Controllers
{
    public class ProductDesignerController : Controller
    {
        dbwebEntities db = new dbwebEntities();
        // GET: DesignerCategory
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ByDesigner(int id, int? page)
        {
            ViewBag.MaNTK = id;
            int iSize = 9;
            int iPageNum = (page ?? 1);
            var sanpham = from sp in db.SANPHAMs where sp.MaNTK == id select sp;
            return View(sanpham.OrderBy(n => n.MaNTK).ToPagedList(iPageNum, iSize));
        }
    }
}