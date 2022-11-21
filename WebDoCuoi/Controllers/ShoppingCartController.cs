using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDoCuoi.Models;

namespace WebDoCuoi.Controllers
{
    public class ShoppingCartController : Controller
    {
        dbwebEntities db = new dbwebEntities();
        // GET: ShoppingCart
        public List<ShoppingCart> TakeCart()
        {
            List<ShoppingCart> lstCart = Session["ShoppingCart"] as List<ShoppingCart>;
            if (lstCart == null)
            {
                lstCart = new List<ShoppingCart>();
                Session["ShoppingCart"] = lstCart;
            }
            return lstCart;
        }
        public ActionResult AddCart(int msp, string url)
        {
            List<ShoppingCart> lstCart = TakeCart();
            ShoppingCart sp = lstCart.Find(n => n.iMaSP.Equals(msp));
            if (sp == null)
            {
                sp = new ShoppingCart(msp);
                lstCart.Add(sp);
            }
            else
            {
                sp.iSoLuong++;
            }
            return Redirect(url);
        }
        private int Quantity()
        {
            int iQuantity = 0;
            List<ShoppingCart> lstCart = Session["ShoppingCart"] as List<ShoppingCart>;
            if (lstCart!= null)
            {
                iQuantity = lstCart.Sum(n => n.iSoLuong);
            }
            return iQuantity;
        }
        private double TotalMoney()
        {
            double dTotalMoney = 0;
            List<ShoppingCart> lstCart = Session["ShoppingCart"] as List<ShoppingCart>;
            if (lstCart != null)
            {
                dTotalMoney = lstCart.Sum(n => n.dThanhTien);
            }
            return dTotalMoney;
        }
        public ActionResult ShoppingCart()
        {
            List<ShoppingCart> lstCart = TakeCart();
            if (lstCart.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Quantity = Quantity();
            ViewBag.TotalMoney = TotalMoney();
            return View(lstCart);
        }
        public ActionResult CartPartial()
        {
            ViewBag.Quantity = Quantity();
            ViewBag.TotalMoney = TotalMoney();
            return PartialView();
        }
        public ActionResult DeleteProduct(int id)
        {
            List<ShoppingCart> lstCart = TakeCart();
            ShoppingCart sp = lstCart.SingleOrDefault(n => n.iMaSP == id);
            if (sp != null)
            {
                lstCart.RemoveAll(n => n.iMaSP == id);
                if (lstCart.Count == 0)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("ShoppingCart");
        }
        public ActionResult UpdateCart(int id, FormCollection f)
        {
            List<ShoppingCart> lstCart = TakeCart();
            ShoppingCart sp = lstCart.SingleOrDefault(n => n.iMaSP == id);
            if (sp != null)
            {
                sp.iSoLuong = int.Parse(f["txtSoLuong"].ToString());
            }
            return RedirectToAction("ShoppingCart");
        }
        public ActionResult DeleteCart()
        {
            List<ShoppingCart> lstCart = TakeCart();
            lstCart.Clear();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult Order()
        {
            if (Session["TenDN"] == null || Session["TenDN"].ToString() == "")
            {
                return Redirect("~/Home/Login?id=2");
            }
            if (Session["ShoppingCart"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<ShoppingCart> lstCart = TakeCart();
            ViewBag.Quantity = Quantity();
            ViewBag.TotalMoney = TotalMoney();
            return View(lstCart);
        }
        [HttpPost]
        public ActionResult Order(FormCollection f)
        {
            HOADON hd = new HOADON();
            KHACHHANG kh = (KHACHHANG)Session["TenDN"];
            List<ShoppingCart> lstCart = TakeCart();
            hd.MaKH = kh.MaKH;
            hd.NgayDat = DateTime.Now;
            var NgayGiao = String.Format("{0:MM/dd/yyyy}", f["NgayGiao"]);
            hd.NgayGiao = DateTime.Parse(NgayGiao);
            hd.TrangThai = false;
            hd.ThanhToan = false;
            hd.TenNguoiNhan = kh.TenKH;
            hd.DiaChiNhan = kh.Diachi;
            hd.SĐT = kh.SDT;
            db.HOADONs.Add(hd);
            db.SaveChanges();
            foreach (var item in lstCart)
            {
                CTHOADON cthd = new CTHOADON();
                cthd.MaHD = hd.MaHD;
                cthd.MaSP = item.iMaSP;
                cthd.SoLuong = item.iSoLuong;
                cthd.GiaSP = (decimal)item.mGiaSP;
                hd.GiaTri = (decimal)item.dThanhTien;
                db.CTHOADONs.Add(cthd);
            }
            db.SaveChanges();
            Session["ShoppingCart"] = null;
            return RedirectToAction("Confirmation", "ShoppingCart");
        }
        public ActionResult Confirmation()
        {
            return View();
        }
    }
}