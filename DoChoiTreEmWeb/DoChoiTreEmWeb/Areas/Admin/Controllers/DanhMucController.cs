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
    public class DanhMucController : Controller
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
            return View(data.DANHMUCs.ToList().OrderBy(n => n.MaDM).ToPagedList(iPageNum, iPageSize));
        }

        public DANHMUC GetDM(int id)
        {
            return data.DANHMUCs.Where(nxb => nxb.MaDM == id).SingleOrDefault();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            DANHMUC dm = data.DANHMUCs.Where(n => n.MaDM == id).SingleOrDefault();
            return View(dm);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit()
        {
            if (ModelState.IsValid)
            {
                //dùng đối tượng request.form[" "] để lấy
                // giá trị của các đối tượng truyền từ form
                var dm = GetDM(int.Parse(Request.Form["MaDM"]));
                dm.TenDM = Request.Form["TenDM"];

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

            return View(GetDM(id));
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection f)
        {
            var dm = GetDM(id);
            if (dm == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            data.DANHMUCs.DeleteOnSubmit(dm);
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
                DANHMUC dm = new DANHMUC();

                dm.TenDM = f["TenDM"];
                data.DANHMUCs.InsertOnSubmit(dm);
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