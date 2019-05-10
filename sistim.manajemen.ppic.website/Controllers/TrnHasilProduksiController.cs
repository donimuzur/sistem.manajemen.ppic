using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sistem.manajemen.ppic.bll.IBLL;
using sistem.manajemen.ppic.core;
using sistem.manajemen.ppic.dal;
using sistem.manajemen.ppic.website.Models;
using AutoMapper;
using sistem.manajemen.ppic.dto;

namespace sistem.manajemen.ppic.website.Controllers
{
    public class TrnHasilProduksiController : BaseController
    {
        private ITrnHasilProduksiBLL _trnHasilProduksiBLL;
        private ITrnSpbBLL _trnSpbBLL;
        private IMstBarangJadiBLL _mstBarangJadiBLL;
        public TrnHasilProduksiController(IPageBLL pageBll , ITrnHasilProduksiBLL TrnHasilProduksiBLL, IMstBarangJadiBLL MstBarangJadiBLL, ITrnSpbBLL TrnSpbBLL) : base(pageBll, Enums.MenuList.TrnHasilProduksi)
        {
            _trnHasilProduksiBLL = TrnHasilProduksiBLL;
            _mstBarangJadiBLL = MstBarangJadiBLL;
            _trnSpbBLL = TrnSpbBLL;
        }

        // GET: TrnHasilProduksi
        public ActionResult Index()
        {
            var model = new TrnHasilProduksiViewModel();

            model.ListData = Mapper.Map<List<TrnHasilProduksiModel>>(_trnHasilProduksiBLL.GetAll());

            model.MainMenu = Enums.MenuList.TrnHasilProduksi;
            model.CurrentUser = CurrentUser;
            model.Menu = "Hasil Produksi";

            return View(model);
        }

        public TrnHasilProduksiModel Init(TrnHasilProduksiModel model)
        {
            model.CurrentUser = CurrentUser;
            model.ChangesHistory = GetChangesHistory((int)Enums.MenuList.TrnHasilProduksi, model.ID);
            model.Menu = "Hasil Produksi";


            var ListBarang = new List<MstBarangJadiDto>();
            ListBarang = _mstBarangJadiBLL.GetAll().Where(x => x.STATUS == true).ToList();          
            model.BarangList = new SelectList(ListBarang);

            return model;
        }

        #region --- Create ---
        public ActionResult Create()
        {
            var model = new TrnHasilProduksiModel();
            model = Init(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(TrnHasilProduksiModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.TANGGAL = DateTime.Now;
                    model.CREATED_BY = CurrentUser.USERNAME;
                    model.CREATED_DATE = DateTime.Now;
                    
                    var Dto = Mapper.Map<TrnHasilProduksiDto>(model);
                    _trnHasilProduksiBLL.Save(Dto, Mapper.Map<LoginDto>(CurrentUser));

                    AddMessageInfo("Data Telah Tersimpan", Enums.MessageInfoType.Success);
                    return RedirectToAction("Index", "TrnSpb");
                }
                catch (Exception exp)
                {
                    model = Init(model);
                    LogError.LogError.WriteError(exp);
                    AddMessageInfo("Gagal Simpan Data", Enums.MessageInfoType.Error);
                    return RedirectToAction("Index", "TrnSpb");
                }
            }
            model = Init(model);
            AddMessageInfo("Gagal Simpan Data", Enums.MessageInfoType.Error);
            return View(Init(model));
        }
        #endregion
                
        #region --- Edit ---
        public ActionResult Edit(int? Id)
        {
            if (!Id.HasValue)
            {
                return HttpNotFound();
            }

            try
            {
                var model = new TrnHasilProduksiModel();

                model = Mapper.Map<TrnHasilProduksiModel>(_trnHasilProduksiBLL.GetById(Id));

                model = Init(model);
                return View(model);
            }
            catch (Exception exp)
            {
                LogError.LogError.WriteError(exp);
                AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
                return RedirectToAction("Index", "TrnSpb");
            }
        }

        [HttpPost]
        public ActionResult Edit(TrnHasilProduksiModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.MODIFIED_BY = CurrentUser.USERNAME;
                    model.MODIFIED_DATE = DateTime.Now;

                    var Dto = Mapper.Map<TrnHasilProduksiDto>(model);
                    _trnHasilProduksiBLL.Save(Dto, Mapper.Map<LoginDto>(CurrentUser));

                    AddMessageInfo("Success Create SPB", Enums.MessageInfoType.Success);
                    return RedirectToAction("Index", "TrnSpb");
                }
                catch (Exception exp)
                {
                    LogError.LogError.WriteError(exp);
                    AddMessageInfo("Gagal Create SPB", Enums.MessageInfoType.Error);
                    return RedirectToAction("Index", "TrnSpb");
                }
            }
            AddMessageInfo("Gagal Create SPB", Enums.MessageInfoType.Error);
            return View(Init(model));
        }
        #endregion

        #region --- Details ---
        public ActionResult Details(int? Id)
        {
            if (!Id.HasValue)
            {
                return HttpNotFound();
            }

            try
            {
                var model = new TrnHasilProduksiModel();

                model = Mapper.Map<TrnHasilProduksiModel>(_trnHasilProduksiBLL.GetById(Id));

                model = Init(model);
                return View(model);
            }
            catch (Exception exp)
            {
                LogError.LogError.WriteError(exp);
                AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
                return RedirectToAction("Index", "TrnSpb");
            }
        }
        #endregion

        #region --- Json ---
        public JsonResult GetSpbList()
        {
            var model = new List<string>();
            model = _trnSpbBLL.GetAll()
                    .Select(x => x.NO_SPB)
                    .ToList();
                           
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBarangJadi()
        {
            var data = new List<string>();
                data = _mstBarangJadiBLL.GetAll()
                       .Where(x => x.STATUS == true)
                       .Select(j=>j.NAMA_BARANG).ToList();

            return Json(data,JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}