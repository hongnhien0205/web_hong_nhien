using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using WebDoCuoi.Models;

namespace WebDoCuoi.Controllers
{
    public class FilterController : Controller
    {
        dbwebEntities db = new dbwebEntities();
        // GET: Filter
        public ActionResult Index()
        {
            return View(); //trả về kết quả   
        }

        public ActionResult ByFilter(int tsp, int? page)
        {
            ViewBag.searchString = tsp;
            int iSize = 9;
            int iPageNum = (page ?? 1);
            var sanpham = from sp in db.SANPHAMs where sp.MaSP == tsp select sp;
            return View(sanpham.OrderBy(n => n.TenSP).ToPagedList(iPageNum, iSize));
        }
        public ActionResult Search(FormCollection f)
        {
            var links = from sp in db.SANPHAMs select sp;
            var sanpham = db.SANPHAMs.Where(sp => sp.TenSP.Contains(f["searchString"]));
            return View(sanpham);
        }
    }
}