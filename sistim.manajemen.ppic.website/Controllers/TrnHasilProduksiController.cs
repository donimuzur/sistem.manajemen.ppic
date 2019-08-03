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

            model.ListData = Mapper.Map<List<TrnHasilProduksiModel>>(_trnHasilProduksiBLL.GetActiveAll());

            model.CurrentUser = CurrentUser;

            model.MainMenu = Enums.MenuList.TrnHasilProduksi;
            model.Menu = Enums.GetEnumDescription(Enums.MenuList.Produksi);
            model.Tittle = "Hasil Produksi";


            return View(model);
        }

        public TrnHasilProduksiModel Init(TrnHasilProduksiModel model)
        {
            model.CurrentUser = CurrentUser;

            model.MainMenu = Enums.MenuList.TrnHasilProduksi;
            model.Menu = Enums.GetEnumDescription(Enums.MenuList.Produksi);
            model.Tittle = "Hasil Produksi";

            model.ChangesHistory = GetChangesHistory((int)Enums.MenuList.TrnHasilProduksi, model.ID);

            var ListShift = new Dictionary<int, string>();
            ListShift.Add(1, "Shift 1");
            ListShift.Add(2, "Shift 2");
            ListShift.Add(3, "Shift 3");
            model.ShiftList = new SelectList(ListShift, "Key", "Value");

            return model;
        }

        #region --- Create ---
        public ActionResult Create()
        {
            var model = new TrnHasilProduksiModel();
            model.TANGGAL_PRODUKSI = DateTime.Now;
            
            return View(Init(model));
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
                    model.STATUS = (int)Enums.StatusDocument.Open;

                    var GetBarang = _mstBarangJadiBLL.GetByNama(model.NAMA_BARANG);
                    if (GetBarang != null)
                    {
                        model.ID_BARANG = GetBarang.ID;
                        model.STOCK_AWAL = GetBarang.STOCK;
                        model.STOCK_AKHIR = model.STOCK_AWAL + model.KUANTUM;
                    }

                    var Dto = Mapper.Map<TrnHasilProduksiDto>(model);
                    _trnHasilProduksiBLL.Save(Dto, Mapper.Map<LoginDto>(CurrentUser));
                    _mstBarangJadiBLL.TambahSaldo(model.ID_BARANG, model.KUANTUM.Value);
                
                    AddMessageInfo("Sukses Create Form Hasil Produksi", Enums.MessageInfoType.Success);
                    return RedirectToAction("Index", "TrnHasilProduksi");
                }
                catch (Exception exp)
                {
                    LogError.LogError.WriteError(exp);
                    AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
                    return RedirectToAction("Index", "TrnHasilProduksi");
                }
            }
            AddMessageInfo("Gagal Create Form Hasil Produksi", Enums.MessageInfoType.Error);
            return View(Init(model));
        }
        #endregion

        #region --- Deatils ---
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
        public ActionResult Hapus(int? id, string Remarks)
        {
            if (!id.HasValue)
            {
                return HttpNotFound();
            }

            try
            {
                var model = _trnHasilProduksiBLL.GetById(id);
                if (model == null)
                {
                    return HttpNotFound();
                }

                _trnHasilProduksiBLL.Delete(id.Value, Remarks);
                _mstBarangJadiBLL.KurangSaldo(model.ID_BARANG, model.KUANTUM.Value);

                AddMessageInfo("Data sukses dihapus", Enums.MessageInfoType.Success);
                return RedirectToAction("Index", "TrnHasilProduksi");
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
        public JsonResult GetSPPList()
        {
            var model = new List<string>();
            model = _trnSpbBLL.GetAll()
                    .Select(x => x.NO_SPB)
                    .ToList();

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSpbList()
        {
            var model = new List<string>();
            model = _trnSpbBLL.GetAll()
                    .Select(x => x.NO_SPB)
                    .ToList();
                           
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProdukList()
        {
            try
            {
               var model = _mstBarangJadiBLL
              .GetAll()
              .Select(x
                  => new
                  {
                      DATA = x.NAMA_BARANG.ToUpper(),
                      DESKRIPSI = ((x.BENTUK == "Lain-Lain" ? x.BENTUK_LAIN.ToUpper() : x.BENTUK.ToUpper()) + " - " + x.KEMASAN.ToUpper()),
                  })
              .Distinct()
              .OrderBy(X => X.DATA)
              .ToList();
               return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult GetProduk(string Produk)
        {
            try
            {
                var data = _mstBarangJadiBLL.GetByNama(Produk);
                return Json(data);
            }
            catch (Exception)
            {
                return Json("Error");
            }
        }
        #endregion

    }
}