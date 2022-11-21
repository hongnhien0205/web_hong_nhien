using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebDoCuoi.Models;
namespace WebDoCuoi.Models
{

    public class ShoppingCart
    {
        dbwebEntities db = new dbwebEntities();
        public int iMaSP { get; set; }
        public string sTenSP { get; set; }
        public string sAnhSP { get; set; }
        public double mGiaSP { get; set; }
        public int iSoLuong { get; set; }
        public double dThanhTien
        {
            get { return iSoLuong * mGiaSP; }
        }
        public ShoppingCart(int msp)
        {
            iMaSP = msp;
            SANPHAM sp = db.SANPHAMs.Single(n => n.MaSP == iMaSP);
            sTenSP = sp.TenSP;
            sAnhSP = sp.AnhSP;
            mGiaSP = double.Parse(sp.Gia.ToString());
            iSoLuong = 1;
        }
    }
}