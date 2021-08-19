using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoChoiTreEmWeb.Models;

namespace DoChoiTreEmWeb.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        dbDoChoiTreEmDataContext data = new dbDoChoiTreEmDataContext();// GET: Admin/Home
        public ActionResult Index()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("DangNhap", "Home");
            }
            return View();
        }

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection f)
        {
            // gán các giá trị người dùng nhập liệu cho các biến
            var sTenDN = f["UserName"];
            var sMatKhau = f["Password"];
            // gán giá trị cho đối tượng được tạo mới(ad)
            ADMIN ad = data.ADMINs.SingleOrDefault(n => n.TenDN == sTenDN && n.MatKhau == sMatKhau);
            if (f["remember"].Contains("true"))
            {
                Response.Cookies["UserName"].Value = sTenDN;
                Response.Cookies["Password"].Value = sMatKhau;
                Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(1);
                Response.Cookies["Password"].Expires = DateTime.Now.AddDays(1);
            }
            else
            {
                Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
                Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);
            }
            if (ad != null)
            {
                Session["Admin"] = ad;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }
    }
}