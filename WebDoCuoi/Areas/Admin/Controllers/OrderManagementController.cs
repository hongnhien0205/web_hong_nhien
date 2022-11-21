using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WebDoCuoi.Models;

namespace WebDoCuoi.Areas.Admin.Controllers
{
    public class OrderManagementController : Controller
    {
        dbwebEntities db = new dbwebEntities();
        // GET: Admin/OrderManagement
        public ActionResult Index(int?page)
        {
            if (Session["Admin"] == null || Session["Admin"].ToString() == "")
            {
                return Redirect("~/Admin/HomeAdmin/Login");
            }
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            return View(db.HOADONs.ToList().OrderBy(n => n.MaHD).ToPagedList(iPageNum, iPageSize));
        }
        public ActionResult Delete(int id)
        {
            var hd = db.HOADONs.SingleOrDefault(n => n.MaHD == id);
            if (hd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(hd);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id, FormCollection f)
        {
            var hd = db.HOADONs.SingleOrDefault(n => n.MaHD == id);
            if (hd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var cthd = db.CTHOADONs.Where(ct => ct.MaHD == id);
            if (cthd.Count() > 0)
            {
                ViewBag.ThongBao = "Đơn hàng này này đang có trong bảng Chi tiết hóa đơn <br>" + " Nếu muốn xóa thì phải xóa hết mã hóa đơn này trong bảng Chi tiết hóa đơn";

                return View(hd);
            }

            db.HOADONs.Remove(hd);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}