using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace DoChoiTreEmWeb.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        
        // GET: Admin/Admin
        public ActionResult Index()
        {
            if (Session["Admin"] == null)
            {
                return RedirectToAction("DangNhap", "Home");
            }
            return View();
        }

        
    }
}