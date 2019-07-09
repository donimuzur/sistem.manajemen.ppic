using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sistem.manajemen.ppic.bll.IBLL;
using sistem.manajemen.ppic.core;
using sistem.manajemen.ppic.website.Models;
using AutoMapper;

namespace sistem.manajemen.ppic.website.Controllers
{
    public class TrnMutasiBarangController : BaseController
    {
        private ITrnMutasiBarangBLL _trnMutasiBarangBLL;

        public TrnMutasiBarangController(IPageBLL pageBll, ITrnMutasiBarangBLL TrnMutasiBarangBLL) : base(pageBll, Enums.MenuList.TrnSuratMutasiBarang)
        {
            _trnMutasiBarangBLL = TrnMutasiBarangBLL;
        }

        // GET: TrnMutasiBarang
        public ActionResult Index()
        {
            var model = new TrnMutasiBarangViewModel();
            model.ListData = Mapper.Map<List<TrnMutasiBarangModel>>(_trnMutasiBarangBLL.GetAll());

            model.CurrentUser = CurrentUser;
            model.Menu = Enums.GetEnumDescription(Enums.MenuList.Ekspedisi);
            model.Tittle = "Mutasi Barang";

            return View(model);
        }
        public TrnMutasiBarangModel Init(TrnMutasiBarangModel model)
        {
            model.CurrentUser = CurrentUser;
            model.Menu = Enums.GetEnumDescription(Enums.MenuList.Ekspedisi);
            model.Tittle = "Surat Pengantar Bongkar Muat";

            //var ListBentuk = new List<string>();
            //ListBentuk.Add("Powder");
            //ListBentuk.Add("Granule");
            //ListBentuk.Add("Tablet");
            //ListBentuk.Add("Bricket");
            //ListBentuk.Add("Lain-Lain");
            //model.BentukList = new SelectList(ListBentuk);

            //var ListKemasan = new List<string>();
            //ListKemasan = _mstKemasanBLL.GetAll().Select(x => x.KEMASAN).ToList();
            //ListKemasan.Add("Lain-Lain");
            //model.KemasanList = new SelectList(ListKemasan);

            var ListJenisBarang = new Dictionary<int, string>();
            ListJenisBarang.Add(1, "Barang Jadi");
            ListJenisBarang.Add(2, "Bahan Baku");
            model.JenisbarangList = new SelectList(ListJenisBarang, "Key", "Value");

            return model;
        }
    }
}