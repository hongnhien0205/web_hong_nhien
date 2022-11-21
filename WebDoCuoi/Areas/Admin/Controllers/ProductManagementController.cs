using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WebDoCuoi.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace WebDoCuoi.Areas.Admin.Controllers
{
    public class ProductManagementController : Controller
    {
        dbwebEntities db = new dbwebEntities();
        // GET: Admin/SanPham
        public ActionResult Index(int ? page)
        {
            if (Session["Admin"] == null || Session["Admin"].ToString() == "")
            {
                return Redirect("~/Admin/HomeAdmin/Login");
            }
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            return View(db.SANPHAMs.ToList().OrderBy(n => n.MaSP).ToPagedList(iPageNum, iPageSize));
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.MaDM = new SelectList(db.DANHMUCSPs.ToList().OrderBy(n => n.TenDM), "MaDM", "TenDM");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(SANPHAM sp, FormCollection f, HttpPostedFileBase fFileUpload)
        {
            ViewBag.MaDM = new SelectList(db.DANHMUCSPs.ToList().OrderBy(n => n.TenDM), "MaDM", "TenDM");
            if (fFileUpload == null)
            {
                ViewBag.MaSP = f["iMaSP"];
                
                ViewBag.TenSP = f["sTenSP"];
                ViewBag.AnhSP = f["sAnhSP"];
                ViewBag.MaNTK = f["sMaNTK"];
                ViewBag.MauSac = f["sMauSac"];
                ViewBag.ChatLieu = f["sChatLieu"];
                ViewBag.Mota = f["sMota"];
                ViewBag.Gia = decimal.Parse(f["iGia"]);
                ViewBag.SLton = int.Parse(f["iSLton"]);
                ViewBag.MaDM = new SelectList(db.DANHMUCSPs.ToList().OrderBy(n => n.TenDM), "MaDM", "TenDM");
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var sFileName = Path.GetFileName(fFileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/img/sanpham"), sFileName);
                    if (!System.IO.File.Exists(path))
                    {
                        fFileUpload.SaveAs(path);
                    }
                    sp.MaSP = int.Parse(f["iMaSP"]);
                    sp.MaDM = int.Parse(f["iMaDM"]);
                    sp.TenSP = f["sTenSP"];
                    sp.MaNTK = int.Parse(f["iMaNTK"]);
                    sp.AnhSP = sFileName;
                    sp.MauSac = f["sMauSac"];
                    sp.ChatLieu = f["sChatLieu"];
                    sp.Mota = f["sMota"].Replace("<p>", "").Replace("</p>", "\n");
                    sp.Gia = decimal.Parse(f["mGia"]);
                    sp.SLton = int.Parse(f["iSLton"]);
                    db.SANPHAMs.Add(sp);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View();   
            }
            
        }
        public ActionResult Details(int id)
        {
            var sp = db.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sp);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var sp = db.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sp);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id, FormCollection f)
        {
            var sp = db.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var cthd = db.CTHOADONs.Where(ct => ct.MaSP == id);
            if (cthd.Count() > 0)
            {
                ViewBag.ThongBao = "Sản phẩm này đang có trong bảng Chi tiết hóa đơn <br>" + " Nếu muốn xóa thì phải xóa hết mã sản phẩm này trong bảng Chi tiết hóa đơn";

                return View(sp);
            }
            
            db.SANPHAMs.Remove(sp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            var sp = db.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaDM = new SelectList(db.DANHMUCSPs.ToList().OrderBy(n => n.TenDM), "MaDM", "TenDM");
            return View(sp);
        }
     
    }
}