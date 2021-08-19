using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using PagedList;
using DoChoiTreEmWeb.Models;


namespace DoChoiTreEmWeb.Areas.Admin.Controllers
{
    public class KhachHangController : Controller
    {
        dbDoChoiTreEmDataContext data = new dbDoChoiTreEmDataContext();
        // GET: Admin/KhachHang
        public ActionResult Index(int? page)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("DangNhap", "Home");
            }
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            return View(data.KHACHHANGs.ToList().OrderBy(n => n.MaKH).ToPagedList(iPageNum, iPageSize));
        }

        public KHACHHANG GetKH(int id)
        {
            return data.KHACHHANGs.Where(nxb => nxb.MaKH == id).SingleOrDefault();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            KHACHHANG kh = data.KHACHHANGs.Where(n => n.MaKH == id).SingleOrDefault();
            return View(kh);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f)
        {
            if (ModelState.IsValid)
            {
                //dùng đối tượng request.form[" "] để lấy
                // giá trị của các đối tượng truyền từ form
                var kh = GetKH(int.Parse(Request.Form["MaKH"]));
                kh.HoTen = Request.Form["HoTen"];
                kh.TaiKhoan = Request.Form["TaiKhoan"];
                kh.MatKhau = Request.Form["MatKhau"];
                kh.Email = Request.Form["Email"];
                kh.DiaChi = Request.Form["DiaChi"];
                kh.DienThoai = Request.Form["DienThoai"];
                kh.NgaySinh = Convert.ToDateTime(f["NgaySinh"]);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Edit");
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {

            return View(GetKH(id));
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection f)
        {
            var kh = GetKH(id);
            if (kh == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.KHACHHANGs.DeleteOnSubmit(kh);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}