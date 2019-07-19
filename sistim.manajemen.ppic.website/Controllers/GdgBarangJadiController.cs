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
    public class GdgBarangJadiController : BaseController
    {
        // GET: GdgBarangJadi
        private IMstBarangJadiBLL _mstBarangJadiBLL;
        public GdgBarangJadiController(IMstBarangJadiBLL MstBarangJadiBLL, IPageBLL pageBll) : base(pageBll, Enums.MenuList.MstBarangJadi)
        {
            _mstBarangJadiBLL = MstBarangJadiBLL;
        }
        
        public ActionResult Index()
        {
            try
            {
                var model = new MstBarangJadiViewModel();
                var data = _mstBarangJadiBLL.GetAll();

                model.ListData = Mapper.Map<List<MstBarangJadiModel>>(data);

                model.MainMenu = Enums.MenuList.GdgBarangJadi;
                model.Menu = Enums.GetEnumDescription(Enums.MenuList.Gudang);
                model.CurrentUser = CurrentUser;
                model.Tittle = "Gudang Barang Jadi";

                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Error");
            }

        }
    }
}