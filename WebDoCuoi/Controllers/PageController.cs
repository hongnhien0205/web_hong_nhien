using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDoCuoi.Models;
using PagedList;
using PagedList.Mvc;
using Microsoft.Ajax.Utilities;
using System.Web.UI;

namespace WebDoCuoi.Controllers
{
    public class PageController : Controller
    {
        dbwebEntities db = new dbwebEntities();
        // GET: Product

        public ActionResult Index(int id, int? page)
        {
            ViewBag.MaSP = id;
            int iSize = 9;
            int iPageNum = (page ?? 1);
            var sanpham = from sp in db.SANPHAMs where sp.MaDM == id select sp;
            return View(sanpham.OrderBy(n => n.MaDM).ToPagedList(iPageNum, iSize));
        }
        private List<SANPHAM> New(int count)
        {
            return db.SANPHAMs.OrderByDescending(a => a.NgayCapNhat).Take(count).ToList();

        }
        public ActionResult NewProduct()
        {
            var newP = New(4);
            return View(newP);
        }
        private List<SANPHAM> Mermaid(int count)
        {
            return db.SANPHAMs.OrderByDescending(a => a.MaDM).Take(count).ToList();

        }
        public ActionResult MermaidCollection()
        {
            var mermaid = Mermaid(8);
            return View(mermaid);
        }
            // GET: Feedback
            public ActionResult Feedback()
            {
                return View();
            }
            public string ProcessUpload(HttpPostedFileBase file)
            {
                //validate( tự validate)

                //xử lý upload
                file.SaveAs(Server.MapPath("~/Content/img/ảnh cưới đẹp/" + file.FileName));

                return "/Content/img/ảnh cưới đẹp/" + file.FileName;
            }
        }
}