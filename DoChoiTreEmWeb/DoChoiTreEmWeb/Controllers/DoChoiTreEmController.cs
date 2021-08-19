using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using DoChoiTreEmWeb.Models;

namespace DoChoiTreEmWeb.Controllers
{
    public class DoChoiTreEmController : Controller
    {
        dbDoChoiTreEmDataContext data = new dbDoChoiTreEmDataContext();
        // GET: DoChoiTreEm
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NavPartial()
        {
            return PartialView();
        }

        public ActionResult SliderPartial()
        {
            return PartialView();
        }

        public ActionResult ThongTinCuaHang()
        {
            return View();
        }

        public ActionResult DanhMucPartial()
        {
            var listLoai = from loai in data.DANHMUCs select loai;
            return PartialView(listLoai);
        }

        public ActionResult ThuongHieuPartial()
        {
            var listTH = from loai in data.THUONGHIEUs select loai;
            return PartialView(listTH);
        }

        private List<SANPHAM> SPBanNhieu(int count)
        {
            return data.SANPHAMs.OrderByDescending(a => a.SoLuongBan).Take(count).ToList();

        }
        public ActionResult SPBanNhieu()
        {
            var listSPBanNhieu = SPBanNhieu(8);
            return PartialView(listSPBanNhieu);
        }

        public ActionResult SPBanNhieuPartial()
        {
            var listSPBanNhieu = SPBanNhieu(12);
            return PartialView(listSPBanNhieu);
        }

        private List<SANPHAM> SPMoi(int count)
        {
            return data.SANPHAMs.OrderByDescending(a => a.NgayCapNhat).Take(count).ToList();
        }

        public ActionResult SPMoi()
        {
            var listSPMoi = SPMoi(8);
            return PartialView(listSPMoi);
        }

        public ActionResult SPMoiPartial()
        {
            var listSPMoi = SPMoi(12);
            return PartialView(listSPMoi);

        }
        public ActionResult SanPhamTheoDanhMuc(int id, int? page)
        {
            ViewBag.MaDM = id;
            // tạo biến quy định số sản phẩm trên mỗi trang
            int iSize = 8;
            //Tạo biến số trang
            int iPageNum = (page ?? 1);
            var sp = from s in data.SANPHAMs
                       where s.MaDM == id
                       select s;
            return View(sp.ToPagedList(iPageNum, iSize));

        }

        public ActionResult SanPhamTheoThuongHieu(int id, int? page)
        {
            ViewBag.MaTH = id;
            int iSize = 8;
            int iPageNum = (page ?? 1);
            var sach = from s in data.SANPHAMs
                       where s.MaTH == id
                       select s;
            return View(sach.ToPagedList(iPageNum, iSize));
        }
        public ActionResult ChiTietSP(int id)
        {
            var sach = from s in data.SANPHAMs
                       where s.MaSP == id
                       select s;
            return View(sach.Single());
        }

        public ActionResult TinTuc()
        {
            return View();
        }
    }
}