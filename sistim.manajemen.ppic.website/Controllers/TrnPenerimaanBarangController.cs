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
    public class TrnPenerimaanBarangController : BaseController
    {
        private ITrnPenerimaanBarangBLL _trnPenerimaanBarang;
        private IMstBahanBakuBLL _mstBahanBakuBLL;
        public TrnPenerimaanBarangController(IPageBLL pageBll, ITrnPenerimaanBarangBLL TrnPenerimaanBarangBLL, IMstBahanBakuBLL MstBahanBakuBLL) : base(pageBll, Enums.MenuList.TrnPenerimaanBarang)
        {
            _trnPenerimaanBarang = TrnPenerimaanBarangBLL;
            _mstBahanBakuBLL = MstBahanBakuBLL;
        }

        // GET: TrnPenerimaanBarang
        public ActionResult Index()
        {
            var model = new TrnPenerimaanBarangViewModel();
            model.ListData = Mapper.Map<List<TrnPenerimaanBarangModel>>(_trnPenerimaanBarang.GetActiveAll());

            model.CurrentUser = CurrentUser;
            model.Menu = Enums.GetEnumDescription(Enums.MenuList.Ekspedisi);
            model.Tittle = "Form Penerimaan Barang";

            return View(model);
        }
        public TrnPenerimaanBarangModel Init(TrnPenerimaanBarangModel model)
        {
            model.CurrentUser = CurrentUser;
            model.Menu = Enums.GetEnumDescription(Enums.MenuList.Ekspedisi);
            model.Tittle = "Form Penerimaan Barang";
            model.ChangesHistory = GetChangesHistory((int)Enums.MenuList.TrnPenerimaanBarang, model.ID);


            return model;
        }

        #region --- Create ---
        public ActionResult Create()
        {
            var model = new TrnPenerimaanBarangModel();
            model.TANGGAL = DateTime.Now;
            return View(Init(model));
        }
        [HttpPost]
        public ActionResult Create(TrnPenerimaanBarangModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.TANGGAL = DateTime.Now;
                    model.CREATED_BY = CurrentUser.USERNAME;
                    model.CREATED_DATE = DateTime.Now;
                    model.STATUS = (int)Enums.StatusDocument.Open;

                    var GetBarang = _mstBahanBakuBLL.GetByNama(model.NAMA_BARANG);
                    if (GetBarang != null)
                    {
                        model.ID_BAHAN_BAKU = GetBarang.ID;
                        model.STOCK_AWAL = GetBarang.STOCK;
                        model.STOCK_AKHIR = model.STOCK_AWAL + model.KUANTUM;
                    }
                    else
                    {
                        AddMessageInfo("Nama bahan baku tidak ditemukan di master", Enums.MessageInfoType.Error);
                        return View(Init(model));
                    }

                    var Dto = Mapper.Map<TrnPenerimaanBarangDto>(model);
                    _trnPenerimaanBarang.Save(Dto, Mapper.Map<LoginDto>(CurrentUser));
                    _mstBahanBakuBLL.KurangSaldo(model.ID_BAHAN_BAKU, model.KUANTUM.Value);

                    AddMessageInfo("Sukses Create Form Penerimaan Barang", Enums.MessageInfoType.Success);
                    return RedirectToAction("Index", "TrnPenerimaanBarang");
                }
                catch (Exception exp)
                {
                    LogError.LogError.WriteError(exp);
                    AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
                    return RedirectToAction("Index", "TrnPenerimaanBarang");
                }
            }
            AddMessageInfo("Gagal Create Form Penerimaan Barang", Enums.MessageInfoType.Error);
            return View(Init(model));
        }
        #endregion

        #region --- Details ---
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return HttpNotFound();
            }

            try
            {
                var model = new TrnPenerimaanBarangModel();

                model = Mapper.Map<TrnPenerimaanBarangModel>(_trnPenerimaanBarang.GetById(id));
                if (model == null)
                {
                    return HttpNotFound();
                }

                model = Init(model);
                return View(model);
            }
            catch (Exception exp)
            {
                LogError.LogError.WriteError(exp);
                AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
                return RedirectToAction("Index", "TrnPenerimaanBarang");
            }
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
                var model = _trnPenerimaanBarang.GetById(id);
                if (model == null)
                {
                    return HttpNotFound();
                }

                _trnPenerimaanBarang.Delete(id.Value, Remarks);
                _mstBahanBakuBLL.KurangSaldo(model.ID_BAHAN_BAKU, model.JUMLAH);

                AddMessageInfo("Data sukses dihapus", Enums.MessageInfoType.Success);
                return RedirectToAction("Index", "TrnPenerimaanBarang");
            }
            catch (Exception exp)
            {
                LogError.LogError.WriteError(exp);
                AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
                return RedirectToAction("Index", "TrnPenerimaanBarang");
            }
        }
        #endregion

        #region --- Json ---
        public JsonResult GetProdukList()
        {
            try
            {
                var model = _mstBahanBakuBLL
                .GetAll()
                .Select(x
                    => new
                    {
                        DATA = x.NAMA_BARANG.ToUpper(),
                        DESKRIPSI = (" - " + x.SATUAN.ToUpper()),
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
        public JsonResult GetBahanBaku(string Produk)
        {
            try
            {
                var data = _mstBahanBakuBLL.GetByNama(Produk);
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