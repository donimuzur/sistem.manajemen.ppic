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
    public class TrnSpbController : BaseController
    {
        private ITrnSpbBLL _trnSpbBLL;
        private IMstBarangJadiBLL _mstBarangJadiBLL;
        private IMstWilayahBLL _mstWilayahBLL;
        public TrnSpbController(ITrnSpbBLL TrnSpbBLL,IPageBLL pageBll, IMstBarangJadiBLL MstBarangJadiBLL, IMstWilayahBLL MstWilayahBLL) : base(pageBll, Enums.MenuList.TrnSpb)
        {
            _trnSpbBLL = TrnSpbBLL;
            _mstBarangJadiBLL = MstBarangJadiBLL;
            _mstWilayahBLL = MstWilayahBLL;
        }
        public ActionResult Index()
        {
            var model = new TrnSpbViewModel();
            model.ListData = Mapper.Map<List<TrnSpbModel>>(_trnSpbBLL.GetAll());

            model.CurrentUser = CurrentUser;
            model.Menu = "Surat Permintaan Barang (SPB)";

            return View(model);
        }
        public TrnSpbModel Init(TrnSpbModel model)
        {
            model.CurrentUser = CurrentUser;
            model.Menu = "Surat Permintaan Barang (SPB)";
            model.ChangesHistory = GetChangesHistory((int)Enums.MenuList.TrnSpb, model.ID);

            var ListBentuk = new List<string>();
            ListBentuk.Add("Powder");
            ListBentuk.Add("Granule");
            ListBentuk.Add("Tablet");
            ListBentuk.Add("Bricket");
            ListBentuk.Add("Lain-Lain");
            model.BentukList= new SelectList(ListBentuk);

            var ListJenisPenjualan = new List<string>();
            ListJenisPenjualan.Add("Loko");
            ListJenisPenjualan.Add("Site");
            ListJenisPenjualan.Add("FOB");
            ListJenisPenjualan.Add("FAS");
            ListJenisPenjualan.Add("CNF");
            ListJenisPenjualan.Add("CIF");
            ListJenisPenjualan.Add("Lain-Lain");
            model.JenisPenjualanList = new SelectList(ListJenisPenjualan);

            var ListSegmenPasar = new List<string>();
            ListSegmenPasar.Add("PTPN");
            ListSegmenPasar.Add("PBSN");
            ListSegmenPasar.Add("Plasma");
            ListSegmenPasar.Add("Tambak");
            ListSegmenPasar.Add("Tanpang");
            ListSegmenPasar.Add("Industri");
            ListSegmenPasar.Add("Proyek");
            model.SegmenPasarList = new SelectList(ListSegmenPasar);

            var ListPPN = new List<string>();
            ListPPN.Add("Incl PPN");
            ListPPN.Add("Excl PPN");
            ListPPN.Add("Non PPN");
            model.PPNList = new SelectList(ListPPN);

            var ListCaraPembayaran = new List<string>();
            ListCaraPembayaran.Add("Tunai");
            ListCaraPembayaran.Add("Kredit");
            model.CaraPembayaranList = new SelectList(ListCaraPembayaran);
            
            var ListKemasan = new List<string>();
            ListKemasan.Add("50 Kg");
            ListKemasan.Add("25 Kg");
            ListKemasan.Add("Lain-Lain");
            model.KemasanList = new SelectList(ListKemasan);

            return model;
        }

        #region --- Create ---
        public ActionResult Create()
        {
            var model = new TrnSpbModel();

            model = Init(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(TrnSpbModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    model.TANGGAL = DateTime.Now;
                    model.CREATED_BY = CurrentUser.USERNAME;
                    model.CREATED_DATE = DateTime.Now;
                    model.STATUS = Enums.StatusDocument.Open;

                    var CheckExist = _trnSpbBLL.GetBySPB(model.NO_SPB);
                    if(CheckExist != null)
                    {
                        AddMessageInfo("No. SPB sudah ada", Enums.MessageInfoType.Error);
                        return View(Init(model));
                    }

                    var Dto = Mapper.Map<TrnSpbDto>(model);
                    _trnSpbBLL.Save(Dto,Mapper.Map<LoginDto>(CurrentUser));

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

        #region --- Edit ---
        public ActionResult Edit(int? id)
        {
            if(!id.HasValue)
            {
                return HttpNotFound();
            }

            try
            {
                var model = new TrnSpbModel();

                model = Mapper.Map<TrnSpbModel>(_trnSpbBLL.GetById(id));

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
        public ActionResult Edit(TrnSpbModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.MODIFIED_BY = CurrentUser.USERNAME;
                    model.MODIFIED_DATE = DateTime.Now;
                    
                    var Dto = Mapper.Map<TrnSpbDto>(model);
                    _trnSpbBLL.Save(Dto,Mapper.Map<LoginDto>(CurrentUser));

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
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return HttpNotFound();
            }

            try
            {
                var model = new TrnSpbModel();

                model = Mapper.Map<TrnSpbModel>(_trnSpbBLL.GetById(id));

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
        public JsonResult GetProdukList()
        {
            var model = _mstBarangJadiBLL
             .GetAll()
             .Select(x
                 => new
                 {
                     DATA = (x.NAMA_BARANG + " - " + (x.BENTUK == "Lain-Lain" ? x.BENTUK_LAIN : x.BENTUK) + " - " + x.KEMASAN),
                     VALUE = x.ID
                 })
             .OrderBy(X => X.DATA)
             .ToList();

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetProduk(string NoSpb)
        {
            var data = _trnSpbBLL.GetBySPB(NoSpb);
            if (data == null)
            {
                data = new TrnSpbDto();
            }
            return Json(data);
        }
        public JsonResult GetCustomerList()
        {
            var model = _trnSpbBLL
             .GetAll()
             .Select(x
                 => new 
                 {
                     DATA = (x.NAMA_KONSUMEN)
                 })
             .GroupBy(X => X.DATA)
             .ToList();

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetWilayahList()
        {
            var model = _mstWilayahBLL
             .GetAll()
             .Select(x
                 => new
                 {
                     DATA = (x.WILAYAH)
                 })
             .GroupBy(X => new { DATA = X.DATA })
             .ToList();

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion
        
    }
}