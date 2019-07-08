using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sistem.manajemen.ppic.bll.IBLL;
using sistem.manajemen.ppic.core;
using sistem.manajemen.ppic.website.Models;
using AutoMapper;
using sistem.manajemen.ppic.dto;

namespace sistem.manajemen.ppic.website.Controllers
{
    public class TrnSuratPengantarBongkarMuatController : BaseController
    {
        private ITrnSuratPengantarBongkarMuatBLL _trnSuratPengantarBongkarMuatBLL;
        private IMstBarangJadiBLL _mstBarangJadiBLL;
        private IMstKemasanBLL _mstKemasanBLL;
        public TrnSuratPengantarBongkarMuatController(IPageBLL pageBll, IMstBarangJadiBLL MstBarangJadiBLL, IMstKemasanBLL MstKemasanBLL, ITrnSuratPengantarBongkarMuatBLL TrnSuratPengantarBongkarMuatBLL) : base(pageBll, Enums.MenuList.TrnSuratPengantarBongkarMuat)
        {
            _trnSuratPengantarBongkarMuatBLL = TrnSuratPengantarBongkarMuatBLL;
            _mstBarangJadiBLL = MstBarangJadiBLL;
            _mstKemasanBLL = MstKemasanBLL;
        }
        // GET: TrnSuratPengantarBongkarMuat
        public ActionResult Index()
        {
            var model = new TrnSuratPengantarBongkarViewModel();
            model.ListData = Mapper.Map<List<TrnSuratPengantarBongkarMuatModel>>(_trnSuratPengantarBongkarMuatBLL.GetAll());

            model.CurrentUser = CurrentUser;
            model.Menu = Enums.GetEnumDescription(Enums.MenuList.Ekspedisi);
            model.Tittle = "Surat Pengantar Bongkar Muat";

            return View(model);
        }

        public TrnSuratPengantarBongkarMuatModel Init(TrnSuratPengantarBongkarMuatModel model)
        {
            model.CurrentUser = CurrentUser;
            model.Menu = Enums.GetEnumDescription(Enums.MenuList.Ekspedisi);
            model.Tittle = "Surat Pengantar Bongkar Muat";
            
            var ListBentuk = new List<string>();
            ListBentuk.Add("Powder");
            ListBentuk.Add("Granule");
            ListBentuk.Add("Tablet");
            ListBentuk.Add("Bricket");
            ListBentuk.Add("Lain-Lain");
            model.BentukList = new SelectList(ListBentuk);

            var ListKemasan = new List<string>();
            ListKemasan = _mstKemasanBLL.GetAll().Select(x => x.KEMASAN).ToList();
            ListKemasan.Add("Lain-Lain");
            model.KemasanList = new SelectList(ListKemasan);

            var ListJenisBarang = new Dictionary<int, string>();
            ListJenisBarang.Add(1, "Barang Jadi");
            ListJenisBarang.Add(2, "Bahan Baku");
            model.JenisbarangList = new SelectList(ListJenisBarang, "Key","Value");

            return model;
        }

        #region --- Create ---
        public ActionResult Create()
        {
            var model = new TrnSuratPengantarBongkarMuatModel();
            return View(Init(model));
        }
        [HttpPost]
        public ActionResult Create(TrnSuratPengantarBongkarMuatModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.TANGGAL = DateTime.Now;
                    model.CREATED_BY = CurrentUser.USERNAME;
                    model.CREATED_DATE = DateTime.Now;
                    model.STATUS = (int)Enums.StatusDocument.Open;

                    model.NO_SPB = model.NO_SPB.ToUpper();
                    model.NO_SPB = model.NO_SPB.TrimEnd('\r', '\n', ' ');
                    model.NO_SPB = model.NO_SPB.TrimStart('\r', '\n', ' ');

                    var Dto = Mapper.Map<TrnSuratPengantarBongkarMuatDto>(model);
                    _trnSuratPengantarBongkarMuatBLL.Save(Dto, Mapper.Map<LoginDto>(CurrentUser));

                    AddMessageInfo("Sukses Create SPB", Enums.MessageInfoType.Success);
                    return RedirectToAction("Index", "TrnSuratPengantarBongkarMuat");
                }
                catch (Exception exp)
                {
                    LogError.LogError.WriteError(exp);
                    AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
                    return RedirectToAction("Index", "TrnSuratPengantarBongkarMuat");
                }
            }
            AddMessageInfo("Gagal Create Surat Pengantar Bongkakr Muat", Enums.MessageInfoType.Error);
            return View(Init(model));
        }
        #endregion
    }
}