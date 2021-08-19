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
    public class SanPhamController : Controller
    {
        dbDoChoiTreEmDataContext data = new dbDoChoiTreEmDataContext();
        // GET: Admin/SanPham
        public ActionResult Index(int? page)
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("DangNhap", "Home");
            }
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            return View(data.SANPHAMs.ToList().OrderBy(n => n.MaSP).ToPagedList(iPageNum, iPageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.MaDM = new SelectList(data.DANHMUCs.ToList().OrderBy(n => n.TenDM), "MaDM", "TenDM");
            ViewBag.MaTH = new SelectList(data.THUONGHIEUs.ToList().OrderBy(n => n.TenTH), "MaTH", "TenTH");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(SANPHAM sp, FormCollection f, HttpPostedFileBase fFileUpload)
        {
            //đưa dữ liệu vào DropDown
            ViewBag.MaDM = new SelectList(data.DANHMUCs.ToList().OrderBy(n => n.TenDM), "MaDM", "TenDM");
            ViewBag.MaTH = new SelectList(data.THUONGHIEUs.ToList().OrderBy(n => n.TenTH), "MaTH", "TenTH");

            if (fFileUpload == null)
            {
                // nội dung thông báo yêu cầu chọn ảnh bìa
                ViewBag.ThongBao = "Hãy chọn ảnh bìa.";
                //lưu thông tin để khi load lại trang do yêu cầu
                // chọn ảnh bìa sẽ hiển thị các thông tin này lên trang
                ViewBag.TenSach = f["sTenSP"];
                ViewBag.MoTa = f["sMoTa"];
                ViewBag.SoLuong = int.Parse(f["iSoLuong"]);
                ViewBag.GiaBan = decimal.Parse(f["mGiaBan"]);
                ViewBag.MaDM = new SelectList(data.DANHMUCs.ToList().OrderBy(n => n.TenDM), "MaDM", "TenDM", int.Parse(f["MaDM"]));
                ViewBag.MaTH = new SelectList(data.THUONGHIEUs.ToList().OrderBy(n => n.TenTH), "MaTH", "TenTH", int.Parse(f["MaTH"]));
                return View();

            }
            else
            {
                if (ModelState.IsValid)
                {
                    // lấy tên file (khai báo thư viện :System.Io
                    var sFileName = Path.GetFileName(fFileUpload.FileName);
                    // lấy đường dẫn lưu file
                    var path = Path.Combine(Server.MapPath("~/Images"), sFileName);
                    // kiểm tra ảnh bìa đã tồn tại chưa để lưu lên thư mục
                    if (!System.IO.File.Exists(path))
                    {
                        fFileUpload.SaveAs(path);
                    }
                    // lưu Sách vào csdl
                    sp.TenSP = f["sTenSP"];
                    sp.MoTa = f["sMoTa"].Replace("<p>", "").Replace("<p>", "");
                    sp.AnhBia = sFileName;
                    sp.NgayCapNhat = Convert.ToDateTime(f["dNgayCapNhat"]);
                    sp.SoLuongBan = int.Parse(f["iSoLuong"]);
                    sp.GiaBan = decimal.Parse(f["mGiaBan"]);
                    sp.MaDM = int.Parse(f["MaDM"]);
                    sp.MaTH = int.Parse(f["MaTH"]);
                    data.SANPHAMs.InsertOnSubmit(sp);
                    data.SubmitChanges();
                    // về lại trang quản lý sản phẩm
                    return RedirectToAction("Index");
                }
                return View();
            }
        }

        public ActionResult Details(int id)
        {
            var sp = data.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sp);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            var sp = data.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sp);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int? id, FormCollection f)
        {
            var sp = data.SANPHAMs.SingleOrDefault(n => n.MaSP == id);

            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var ctdh = data.CHITIETDATHANGs.Where(ct => ct.MaSP == id);
            if (ctdh.Count() > 0)
            {
                //Nội dung sẽ hiển thị khi sách cần xóa đã có trong table ChiTietDonHang
                ViewBag.ThongBao = "Sản phẩm này đang được đặt hàng!!!";
                return View(sp);
            }
            
            //Xóa sách
            data.SANPHAMs.DeleteOnSubmit(sp);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            var sp = data.SANPHAMs.SingleOrDefault(n => n.MaSP == id);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaDM = new SelectList(data.DANHMUCs.ToList().OrderBy(n => n.TenDM), "MaDM", "TenDM");
            ViewBag.MaTH = new SelectList(data.THUONGHIEUs.ToList().OrderBy(n => n.TenTH), "MaTH", "TenTH");
            return View(sp);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FormCollection f, HttpPostedFileBase fFileUpload)
        {
            var sp = data.SANPHAMs.SingleOrDefault(n => n.MaSP == int.Parse(f["iMaSP"]));
            ViewBag.MaCD = new SelectList(data.DANHMUCs.ToList().OrderBy(n => n.TenDM), "MaDM", "TenDM", sp.MaDM);
            ViewBag.MaNXB = new SelectList(data.THUONGHIEUs.ToList().OrderBy(n => n.TenTH), "MaTH", "TenTH", sp.MaTH);
            if (ModelState.IsValid)
            {
                if (fFileUpload != null) //Kiểm tra để xác nhận cho thay đổi ảnh bìa
                {
                    //Lấy tên file (Khai báo thư viện: System.IO)
                    var sFileName = Path.GetFileName(fFileUpload.FileName);
                    //Lấy đường dẫn lưu file
                    var path = Path.Combine(Server.MapPath("~/Images"), sFileName);
                    //Kiểm tra file đã tồn tại chưa
                    if (!System.IO.File.Exists(path))
                    {
                        fFileUpload.SaveAs(path);
                    }
                    sp.AnhBia = sFileName;
                }
                //Lưu Sach vào CSDL
                sp.TenSP = f["sTenSP"];
                sp.MoTa = f["sMoTa"].Replace("<p>", "").Replace("</p>", "\n");

                sp.NgayCapNhat = Convert.ToDateTime(f["dNgayCapNhat"]);
                sp.SoLuongBan = int.Parse(f["iSoLuong"]);
                sp.GiaBan = decimal.Parse(f["mGiaBan"]);
                sp.MaDM = int.Parse(f["MaDM"]);
                sp.MaTH = int.Parse(f["MaTH"]);
                data.SubmitChanges();
                //Về lại trang Quản lý sản phẩm
                return RedirectToAction("Index");
            }
            return View(sp);
        }

        public ActionResult Thongke()
        {
            var kq = from s in data.SANPHAMs
                     group s by s.MaTH into g
                     select new ReporInfo
                     {
                         Id = g.Key.ToString(),
                         Count = g.Count(),
                         Sum = g.Sum(n => n.SoLuongBan),
                         Max = g.Max(n => n.SoLuongBan),
                         Min = g.Min(n => n.SoLuongBan),
                         Avg = Convert.ToDecimal(g.Average(n => n.SoLuongBan))
                     };
            return View(kq);
        }
    }
}