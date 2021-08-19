using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoChoiTreEmWeb.Models;

namespace DoChoiTreEmWeb.Controllers
{
    public class SearchController : Controller
    {
        dbDoChoiTreEmDataContext data = new dbDoChoiTreEmDataContext();
        // GET: Search
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search(string strSearch)
        {
            ViewBag.Search = strSearch;
            if (!string.IsNullOrEmpty(strSearch))
            {
                //var kq = from s in data.SACHes where s.MaCD == int.Parse(strSearch) select s;
                //var kq = from s in data.SACHes where s.TenSach.Contains(strSearch) select s;
                //var kq = from s in data.SACHes where s.SoLuongBan >= 5 && s.SoLuongBan <= 10 select s;
                var kq = from s in data.SANPHAMs where s.TenSP.Contains(strSearch) || s.MoTa.Contains(strSearch) select s;
                //var kq = from s in data.SACHes orderby s.SoLuongBan where s.SoLuongBan >= 5 && s.SoLuongBan <= 10 select s;
                //var kq = from s in data.SACHes orderby s.SoLuongBan descending where s.MaCD == int.Parse(strSearch) select s;

                //var kq = data.SACHes.Where(s => s.MaCD == int.Parse(strSearch));
                //var kq = data.SACHes.Where(s => s.TenSach.Contains(strSearch)).Select(s => s);
                //var kq = data.SACHes.Where(s => s.SoLuongBan >= 5 && s.SoLuongBan <= 10);
                //var kq = data.SACHes.Where(s => s.TenSach.Contains(strSearch) || s.MoTa.Contains(strSearch));
                //var kq = data.SACHes.Where(s => s.SoLuongBan >= 5 && s.SoLuongBan <= 10).OrderBy(s => s.SoLuongBan);
                //var kq = data.SACHes.Where(s => s.MaCD == int.Parse(strSearch)).OrderByDescending(s => s.SoLuongBan);
                return View(kq);
            }
            return View();
        }
    }
}