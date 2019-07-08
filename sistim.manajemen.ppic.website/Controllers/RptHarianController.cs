using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sistem.manajemen.ppic.bll.IBLL;
using sistem.manajemen.ppic.core;

namespace sistem.manajemen.ppic.website.Controllers
{
    public class RptHarianController : BaseController
    {
        private IRptHarianEkspedisiBLL _rptHarianEkspedisi;
        public RptHarianController(IPageBLL pageBll, IRptHarianEkspedisiBLL RptHarianEkspedisi) : base(pageBll, Enums.MenuList.LaporanRealisasiHarian)
        {
            _rptHarianEkspedisi = RptHarianEkspedisi;
        }
        // GET: RptHarian
        public ActionResult Index()
        {
            return View();
        }
        //public List<> GetRpt()
        //{
        //    var model = _rptHarianEkspedisi.GetRpt();
        //}
    }
}