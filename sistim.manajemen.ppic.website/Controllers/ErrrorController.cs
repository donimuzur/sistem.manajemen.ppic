using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sistem.manajemen.ppic.bll.IBLL;
using sistem.manajemen.ppic.core;

namespace sistem.manajemen.ppic.website.Controllers
{
    public class ErrrorController : BaseController
    {
        public ErrrorController(IPageBLL pageBll) : base(pageBll, Enums.MenuList.Error)
        {

        }
        
        public ActionResult Index()
        {
            return View();
        }
    }
}