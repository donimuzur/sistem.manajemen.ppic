using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sistem.manajemen.ppic.bll.IBLL;
using sistem.manajemen.ppic.core;

namespace sistem.manajemen.ppic.website.Controllers
{
    public class TrnPengirimanController : BaseController
    {
        public TrnPengirimanController(IPageBLL pageBll) : base(pageBll, Enums.MenuList.TrnPengiriman)
        {
        }
        // GET: TrnPengiriman
        public ActionResult Index()
        {
            return View();
        }
    }
}