using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DoChoiTreEmWeb.Models;

namespace DoChoiTreEmWeb.Controllers
{
    public class UserController : Controller
    {
        dbDoChoiTreEmDataContext data = new dbDoChoiTreEmDataContext();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {
            var sTenDN = collection["TenDN"];
            var sMatKhau = collection["MatKhau"];
            if (String.IsNullOrEmpty(sTenDN))
            {
                ViewData["Err1"] = "Bạn chưa nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(sMatKhau))
            {
                ViewData["Err2"] = "Phải nhập mật khẩu";
            }
            else
            {
                KHACHHANG kh = data.KHACHHANGs.SingleOrDefault(n => n.TaiKhoan == sTenDN && n.MatKhau == sMatKhau);//GetMD5(sMatKhau));
                Session["TaiKhoan"] = kh;
                if (collection["remember"].Contains("true"))
                {
                    Response.Cookies["TenDN"].Value = sTenDN;
                    Response.Cookies["MatKhau"].Value = sMatKhau;
                    Response.Cookies["TenDN"].Expires = DateTime.Now.AddDays(1);
                    Response.Cookies["MatKhau"].Expires = DateTime.Now.AddDays(1);
                }
                else
                {
                    Response.Cookies["TenDN"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["MatKhau"].Expires = DateTime.Now.AddDays(-1);
                }
                if (kh != null)
                {
                    ViewBag.ThongBao = "Chúc mừng bạn đăng nhập thành công";
                    Session["TaiKhoan"] = kh;
                    return RedirectToAction("Index", "DoChoiTreEm");
                }
                else
                {
                    ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không hợp lệ";
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(FormCollection collection, KHACHHANG kh)
        {
            var sHoTen = collection["HoTen"];
            var sTenDN = collection["TaiKhoan"];
            var sMatKhau = collection["MatKhau"];
            var sMatKhauNhapLai = collection["MatKhauNL"];
            var sDiaChi = collection["DiaChi"];
            var sEmail = collection["Email"];
            var sDienThoai = collection["DienThoai"];
            var dNgaySinh = String.Format("{0:MM/dd/yyyy}", collection["NgaySinh"]);

            if (String.IsNullOrEmpty(sMatKhauNhapLai))
            {
                ViewData["err4"] = "Phải nhập lại mật khẩu";
            }
            else if (sMatKhau != sMatKhauNhapLai)
            {
                ViewData["err4"] = "Mật khẩu không trùng khớp";
            }
            else if (data.KHACHHANGs.SingleOrDefault(n => n.TaiKhoan == sTenDN) != null)
            {
                ViewBag.ThongBao = " Tên đăng nhập đã tồn tại";
            }
            else if (data.KHACHHANGs.SingleOrDefault(n => n.Email == sEmail) != null)
            {
                ViewBag.ThongBao = " Email đã sử dụng";
            }
            else if (ModelState.IsValid)
            {
                kh.HoTen = sHoTen;
                kh.TaiKhoan = sTenDN;
                kh.MatKhau = sMatKhau;//GetMD5(sMatKhau);
                kh.Email = sEmail;
                kh.DiaChi = sDiaChi;
                kh.DienThoai = sDienThoai;
                kh.NgaySinh = DateTime.Parse(dNgaySinh);
                data.KHACHHANGs.InsertOnSubmit(kh);
                data.SubmitChanges();
                return RedirectToAction("DangNhap");
            }
            return this.DangKy();
        }
        public ActionResult LoginPartial()
        {
            return PartialView();
        }
        public ActionResult DangXuat()
        {
            Session.Clear();

            return RedirectToAction("DangNhap");
        }

        //Mã hóa mật khẩu MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromDATA = Encoding.UTF8.GetBytes(str);
            byte[] targetDATA = md5.ComputeHash(fromDATA);
            string byte2String = null;
            for (int i = 0; i < targetDATA.Length; i++)
            {
                byte2String += targetDATA[i].ToString("x2");
            }
            return byte2String;
        }
        
        
    }
}