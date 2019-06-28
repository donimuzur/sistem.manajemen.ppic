using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sistem.manajemen.ppic.bll.IBLL;
using sistem.manajemen.ppic.core;
using sistem.manajemen.ppic.website.Models;

namespace sistem.manajemen.ppic.website.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IPageBLL pageBll) : base(pageBll, Enums.MenuList.Home)
        {

        }
        public ActionResult Index()
        {
            var model = new BaseModel();

            model.CurrentUser = CurrentUser;
            model.Menu = Enums.GetEnumDescription(Enums.MenuList.Home);

            return View(model);
        }
    }
}