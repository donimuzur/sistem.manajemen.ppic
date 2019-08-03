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
    public class TrnSuratPerintahProduksiController : BaseController
    {
        private ITrnSuratPerintahProduksiBLL _trnSuratPerintahProduksiBLL;
        private IMstBarangJadiBLL _mstBarangJadiBLL;
        private IMstKemasanBLL _mstKemasanBLL;
        private ITrnSpbBLL _trnSpbBLL;
        private ITrnDoBLL _trnDoBLL;
        public TrnSuratPerintahProduksiController(IPageBLL pageBll, ITrnSuratPerintahProduksiBLL TrnSuratPerintahProduksiBLL, ITrnSpbBLL TrnSpbBLL,
            ITrnDoBLL TrnDoBLL, IMstBarangJadiBLL MstBarangJadiBLL, IMstKemasanBLL MstKemasanBLL) : base(pageBll, Enums.MenuList.TrnSuratPerintahProduksi)
        {
            _trnSuratPerintahProduksiBLL = TrnSuratPerintahProduksiBLL;
            _mstBarangJadiBLL = MstBarangJadiBLL;
            _mstKemasanBLL = MstKemasanBLL;
            _trnSpbBLL = TrnSpbBLL;
            _trnDoBLL = TrnDoBLL;
        }

        // GET: TrnSuratPerintahProduksi
        public ActionResult Index()
        {
            var model = new TrnSuratPerintahProduksiViewModel();
            model.ListData = Mapper.Map<List<TrnSuratPerintahProduksiModel>>(_trnSuratPerintahProduksiBLL.GetActiveAll());

            model.CurrentUser = CurrentUser;
            model.Menu = Enums.GetEnumDescription(Enums.MenuList.Produksi);
            model.Tittle = "Surat Perintah Produksi";

            return View(model);
        }
        public TrnSuratPerintahProduksiModel Init(TrnSuratPerintahProduksiModel model)
        {
            model.CurrentUser = CurrentUser;
            model.Menu = Enums.GetEnumDescription(Enums.MenuList.Produksi);
            model.Tittle = "Surat Perintah Produksi";
            model.ChangesHistory = GetChangesHistory((int)Enums.MenuList.TrnSuratPerintahProduksi, model.ID);

            var ListTujuanProduksi = new Dictionary<int, string>();
            ListTujuanProduksi.Add(1,"Stock");
            ListTujuanProduksi.Add(2,"Konsumen");
            model.TujuanProduksiList = new SelectList(ListTujuanProduksi, "Key", "Value");

            var ListBentuk = new List<string>();
            ListBentuk.Add("Powder");
            ListBentuk.Add("Granule");
            ListBentuk.Add("Tablet");
            ListBentuk.Add("Bricket");
            ListBentuk.Add("Lain-Lain");
            model.BentukList = new SelectList(ListBentuk);
            return model;
        }
        #region --- Create ---
        public ActionResult Create()
        {
            var model = new TrnSuratPerintahProduksiModel();
            model.TANGGAL = DateTime.Now;

            model = Init(model);

            return View(model);
        }
        [HttpPost]
        public ActionResult Create(TrnSuratPerintahProduksiModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.TANGGAL = DateTime.Now;
                    model.CREATED_BY = CurrentUser.USERNAME;
                    model.CREATED_DATE = DateTime.Now;
                    model.STATUS = (int)Enums.StatusDocument.Open;

                    if (!string.IsNullOrEmpty(model.NO_SPB))
                    {
                        model.NO_SPB = model.NO_SPB.ToUpper();
                        model.NO_SPB = model.NO_SPB.TrimEnd('\r', '\n', ' ');
                        model.NO_SPB = model.NO_SPB.TrimStart('\r', '\n', ' ');
                    }
                    
                    var Dto = Mapper.Map<TrnSuratPerintahProduksiDto>(model);
                    _trnSuratPerintahProduksiBLL.Save(Dto, Mapper.Map<LoginDto>(CurrentUser));

                    AddMessageInfo("Sukses Create Surat Perintah Produksi", Enums.MessageInfoType.Success);
                    return RedirectToAction("Details", "TrnSuratPerintahProduksi");
                }
                catch (Exception exp)
                {
                    LogError.LogError.WriteError(exp);
                    AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
                    return RedirectToAction("Index", "TrnSuratPerintahProduksi");
                }
            }
            AddMessageInfo("Gagal Create Surat Perintah Produksi", Enums.MessageInfoType.Error);
            return View(Init(model));
        }
        #endregion

        #region --- Edit ---

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
                var model = new TrnSuratPerintahProduksiModel();

                model = Mapper.Map<TrnSuratPerintahProduksiModel>(_trnSuratPerintahProduksiBLL.GetById(id));

                model = Init(model);
                return View(model);
            }
            catch (Exception exp)
            {
                LogError.LogError.WriteError(exp);
                AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
                return RedirectToAction("Index", "TrnSuratPerintahProduksi");
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
                     DATA = x.NAMA_BARANG.ToUpper(),
                     DESKRIPSI = ((x.BENTUK == "Lain-Lain" ? x.BENTUK_LAIN.ToUpper() : x.BENTUK.ToUpper()) + " - " + x.KEMASAN.ToUpper()),
                 })
             .Distinct()
             .OrderBy(X => X.DATA)
             .ToList();

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetProduk(string Produk)
        {
            var data = _mstBarangJadiBLL.GetByNama(Produk);

            return Json(data);
        }
        public JsonResult GetSpbList()
        {
            var model = _trnSpbBLL
                .GetAll()
                .Select(x
                    => new
                    {
                        DATA = x.NO_SPB
                    })
                    .OrderBy(X => X.DATA)
                .ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}