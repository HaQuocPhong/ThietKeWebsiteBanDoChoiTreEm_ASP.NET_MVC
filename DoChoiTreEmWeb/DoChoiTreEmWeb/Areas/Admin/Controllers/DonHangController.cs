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
    public class DonHangController : Controller
    {
        dbDoChoiTreEmDataContext data = new dbDoChoiTreEmDataContext();
        // GET: Admin/DonHang
        public ActionResult Index(int? page)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("DangNhap", "Home");
            }
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            return View(data.DONDATHANGs.ToList().OrderBy(n => n.MaDonHang).ToPagedList(iPageNum, iPageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
            //ViewBag.MaDM = new SelectList(data.DANHMUCs.ToList().OrderBy(n => n.TenDM), "MaDM", "TenDM");
            //ViewBag.MaTH = new SelectList(data.THUONGHIEUs.ToList().OrderBy(n => n.TenTH), "MaTH", "TenTH");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(FormCollection f)
        {
            if (ModelState.IsValid)
            {
                DONDATHANG dh = new DONDATHANG();
                if (f["sTinhTrangThanhToan"] == "Đã thanh toán")
                {
                    dh.DaThanhToan = true;
                }
                else
                {
                    dh.DaThanhToan = false;
                }
                if (f["sTinhTrangGiaoHang"] == "Đã giao hàng")
                {
                    dh.TinhTrangGiaoHang = 1;
                }
                else
                {
                    dh.TinhTrangGiaoHang = 0;
                }
                //sp.AnhBia = sFileName;
                dh.NgayDat = DateTime.Now;
                dh.NgayGiao = Convert.ToDateTime(f["dNgayGiao"]);
                dh.MaKH = int.Parse(f["sMaKH"]);
                data.DONDATHANGs.InsertOnSubmit(dh);
                data.SubmitChanges();
                // về lại trang quản lý sản phẩm
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Delete");
            }
        }
    }
}