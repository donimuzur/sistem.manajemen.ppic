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

            var ListBarang =  _mstBarangJadiBLL.GetAll().Where(x => x.STATUS == true).Select(x => new { VALUE = x.ID, TEXT = x.NAMA_BARANG + " - "+ x.KEMASAN + " - "+ (x.BENTUK == "Lain-Lain"?x.BENTUK_LAIN:x.BENTUK)}).ToList();          
            model.BarangList = new SelectList(ListBarang,"VALUE","TEXT");

            return model;
        }

        #region --- Create ---
        public ActionResult Create()
        {
            var model = new TrnHasilProduksiModel();
            model = Init(model);

            model.TANGGAL = DateTime.Now;
            
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
                    _mstBarangJadiBLL.TambahSaldo(model.ID_BARANG, model.JUMLAH.Value);

                    AddMessageInfo("Data Telah Tersimpan", Enums.MessageInfoType.Success);
                    return RedirectToAction("Index", "TrnHasilProduksi");
                }
                catch (Exception exp)
                {
                    model = Init(model);
                    LogError.LogError.WriteError(exp);
                    AddMessageInfo("Gagal Simpan Data", Enums.MessageInfoType.Error);
                    return RedirectToAction("Index", "TrnHasilProduksi");
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
                return RedirectToAction("Index", "TrnHasilProduksi");
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

                    AddMessageInfo("Data Berhasil disimpan", Enums.MessageInfoType.Success);
                    return RedirectToAction("Index", "TrnHasilProduksi");
                }
                catch (Exception exp)
                {
                    LogError.LogError.WriteError(exp);
                    AddMessageInfo("Data Gagal disimpan", Enums.MessageInfoType.Error);
                    return RedirectToAction("Index", "TrnHasilProduksi");
                }
            }
            AddMessageInfo("Data Gagal disimpan", Enums.MessageInfoType.Error);
            return View(Init(model));
        }
        #endregion

        #region --- Hapus ---
        public ActionResult Hapus(int Id)
        {
            AddMessageInfo("Gagal Simpan Data", Enums.MessageInfoType.Error);
            return RedirectToAction("Index", "TrnHasilProduksi");
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
                return RedirectToAction("Index", "TrnHasilProduksi");
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

        public JsonResult GetBarangJadi(int Id)
        {
            var data = new MstBarangJadiDto();
            data = _mstBarangJadiBLL
                    .GetById(Id);
            
            return Json(data,JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBahanBakuList()
        {
            var data = new List<string>();

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion
    
    }
}