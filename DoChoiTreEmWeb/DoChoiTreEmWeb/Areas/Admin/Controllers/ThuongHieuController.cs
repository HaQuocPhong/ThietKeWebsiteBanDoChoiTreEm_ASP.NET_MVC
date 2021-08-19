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
    public class ThuongHieuController : Controller
    {
        dbDoChoiTreEmDataContext data = new dbDoChoiTreEmDataContext();
        // GET: Admin/DanhMuc
        public ActionResult Index(int? page)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("DangNhap", "Home");
            }
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            return View(data.THUONGHIEUs.ToList().OrderBy(n => n.MaTH).ToPagedList(iPageNum, iPageSize));
        }

        public THUONGHIEU GetTH(int id)
        {
            return data.THUONGHIEUs.Where(nxb => nxb.MaTH == id).SingleOrDefault();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            THUONGHIEU th= data.THUONGHIEUs.Where(n => n.MaTH == id).SingleOrDefault();
            return View(th);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit()
        {
            if (ModelState.IsValid)
            {
                //dùng đối tượng request.form[" "] để lấy
                // giá trị của các đối tượng truyền từ form
                var th = GetTH(int.Parse(Request.Form["MaTH"]));
                th.TenTH = Request.Form["TenTH"];
                th.XuatXu = Request.Form["XuatXu"];
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

            return View(GetTH(id));
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection f)
        {
            var th = GetTH(id);
            if (th == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.THUONGHIEUs.DeleteOnSubmit(th);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(FormCollection f)
        {
            if (ModelState.IsValid)
            {
               THUONGHIEU th = new THUONGHIEU();

                th.TenTH = f["TenTH"];
                th.XuatXu = f["XuatXu"];
                data.THUONGHIEUs.InsertOnSubmit(th);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Delete");
            }
        }
    }
}