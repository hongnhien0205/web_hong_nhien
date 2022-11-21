using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDoCuoi.Models;
using PagedList.Mvc;
using PagedList;
using System.IO;
using System.Web.UI;

namespace WebDoCuoi.Areas.Admin.Controllers
{
    public class CategoryManagementController : Controller
    {
        dbwebEntities db = new dbwebEntities();
        // GET: Admin/DanhMuc
        public ActionResult Index(int? page)
        {
            if (Session["Admin"] == null || Session["Admin"].ToString() == "")
            {
                return Redirect("~/Admin/HomeAdmin/Login");
            }
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            return View(db.DANHMUCSPs.ToList().OrderBy(n => n.MaDM).ToPagedList(iPageNum, iPageSize));
        }
        public ActionResult Create(int ? page)
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(DANHMUCSP dm, FormCollection f)
        {
            if (dm == null)
            {
                ViewBag.MaDM = f["iMaDM"];
                ViewBag.TenDM = f["sTenDM"];
                return View();
            }
            else
            {
                dm.MaDM = int.Parse(f["iMaDM"]);
                dm.TenDM = f["sTenDM"];
                db.DANHMUCSPs.Add(dm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var dm = db.DANHMUCSPs.SingleOrDefault(n => n.MaDM == id);
            if (dm == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(dm);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id, FormCollection f)
        {
            var dm = db.DANHMUCSPs.SingleOrDefault(n => n.MaDM == id);
            if (dm == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.DANHMUCSPs.Remove(dm);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var dm = db.DANHMUCSPs.SingleOrDefault(n => n.MaDM == id);
            if (dm == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(dm);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int id, FormCollection f)
        {
            var dm = db.DANHMUCSPs.SingleOrDefault(n => n.MaDM == id);
            if (dm == null)
            {
                ViewBag.MaDM = f["iMaDM"];
                ViewBag.TenDM = f["sTenDM"];
                return View();
            }
            else
            {
                dm.MaDM = int.Parse(f["iMaDM"]);
                dm.TenDM = f["sTenDM"];
                db.DANHMUCSPs.Add(dm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        public ActionResult Details(int id)
        {
            var dm = db.DANHMUCSPs.SingleOrDefault(n => n.MaDM == id);
            if (dm == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(dm);
        }
    }

}