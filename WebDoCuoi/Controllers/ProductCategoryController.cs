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
    public class ProductCategoryController : Controller
    {
        dbwebEntities db = new dbwebEntities();
        // GET: ProductCategory
        public ActionResult Index()
        {
            return View();
        }
       
        public ActionResult ByCategory(int id, int? page)
        {
            ViewBag.MaDM = id;
            int iSize = 9;
            int iPageNum = (page ?? 1);
            var sanpham = from sp in db.SANPHAMs where sp.MaDM == id select sp;
            return View(sanpham.OrderBy(n => n.MaDM).ToPagedList(iPageNum, iSize));
        }
        public ActionResult ByDesigner(int id, int? page)
        {
            ViewBag.MaNTK = id;
            int iSize = 9;
            int iPageNum = (page ?? 1);
            var sanpham = from sp in db.SANPHAMs where sp.MaNTK == id select sp;
            return View(sanpham.OrderBy(n => n.MaNTK).ToPagedList(iPageNum, iSize));
        }
        public ActionResult ProductDetails(int id)
        {
            ViewBag.MaSP = id;
            var hinh = from ha in db.HINHANHs where ha.MaSP == id select ha;
            return View(hinh);
        }
    }
}