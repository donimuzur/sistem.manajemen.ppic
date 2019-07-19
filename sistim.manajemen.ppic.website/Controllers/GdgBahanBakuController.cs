using AutoMapper;
using sistem.manajemen.ppic.bll.IBLL;
using sistem.manajemen.ppic.core;
using sistem.manajemen.ppic.website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sistem.manajemen.ppic.website.Controllers
{
    public class GdgBahanBakuController : BaseController
    {
        private IMstBahanBakuBLL _mstBahanBakuBLL;
        public GdgBahanBakuController(IMstBahanBakuBLL MstBahanBakuBLL, IPageBLL pageBll) : base(pageBll, Enums.MenuList.MstBahanBaku)
        {
            _mstBahanBakuBLL = MstBahanBakuBLL;
        }

      
        public ActionResult Index()
        {
            try
            {
                var model = new MstBahanBakuViewModel();
                var data = _mstBahanBakuBLL.GetAll();

                model.ListData = Mapper.Map<List<MstBahanBakuModel>>(data);

                model.MainMenu = Enums.MenuList.GdgBarangBB;
                model.Menu = Enums.GetEnumDescription(Enums.MenuList.Gudang);
                model.CurrentUser = CurrentUser;
                model.Tittle = "Gudang Bahan Baku";

                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Error");
            }

        }
    }
}