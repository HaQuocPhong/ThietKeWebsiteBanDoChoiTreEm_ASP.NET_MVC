using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DoChoiTreEmWeb.Models;

namespace DoChoiTreEmWeb.Models
{
    public class GioHang
    {
        dbDoChoiTreEmDataContext db = new dbDoChoiTreEmDataContext();
        public int iMaSP { get; set; }
        public string sTenSP { get; set; }
        public string sAnhBia { get; set; }
        public double dDonGia { get; set; }
        public int iSoLuong { get; set; }
        public double dThanhTien
        {
            get { return iSoLuong * dDonGia; }
        }
        ///Khởi tạo giỏ hàng theo MaSach được truyền vào với SoLuong mặc định là 1
        public GioHang(int ms)
        {
            iMaSP = ms;
            SANPHAM s = db.SANPHAMs.Single(n => n.MaSP == iMaSP);
            sTenSP = s.TenSP;
            sAnhBia = s.AnhBia;
            dDonGia = double.Parse(s.GiaBan.ToString());
            iSoLuong = 1;
        }
    }
}