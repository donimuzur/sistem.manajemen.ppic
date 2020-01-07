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
    public class TrnSuratPermintaanBahanBakuController : BaseController
    {
        private ITrnSuratPermintaanBahanBakuBLL _trnSuratPermintaanBahanBakuBLL;
        private ITrnSuratPerintahProduksiBLL _trnSuratPerintahProduksiBLL;
        private IMstBahanBakuBLL _mstBahanBakuBLL;
        public TrnSuratPermintaanBahanBakuController(IPageBLL pageBll, IMstBahanBakuBLL MstBahanBakuBLL, ITrnSuratPerintahProduksiBLL TrnSuratPerintahProduksiBLL, ITrnSuratPermintaanBahanBakuBLL TrnSuratPermintaanBahanBakuBLL) : base(pageBll, Enums.MenuList.TrnPermintaanBahanBaku)
        {
            _trnSuratPermintaanBahanBakuBLL = TrnSuratPermintaanBahanBakuBLL;
            _trnSuratPerintahProduksiBLL = TrnSuratPerintahProduksiBLL;
            _mstBahanBakuBLL = MstBahanBakuBLL;
        }
        public TrnSuratPermintaanBahanBakuModel Init(TrnSuratPermintaanBahanBakuModel model)
        {
            model.MainMenu = Enums.MenuList.TrnPermintaanBahanBaku;
            model.Menu = Enums.GetEnumDescription(Enums.MenuList.Produksi);
            model.CurrentUser = CurrentUser;
            model.Tittle = "Surat Permintaan Bahan Baku";

            var ListShift = new Dictionary<int, string>();
            ListShift.Add(1, "Shift 1");
            ListShift.Add(2, "Shift 2");
            ListShift.Add(3, "Shift 3");
            model.ShiftList = new SelectList(ListShift, "Key", "Value");

            model.ChangesHistory = GetChangesHistory((int)Enums.MenuList.TrnPermintaanBahanBaku, model.ID);

            return model;
        }
        // GET: TrnSuratPermintaanBahanBaku
        public ActionResult Index()
        {
            try
            {
                var model = new TrnSuratPermintaanBahanBakuViewModel();
                var data = _trnSuratPermintaanBahanBakuBLL.GetAll();

                model.ListData = Mapper.Map<List<TrnSuratPermintaanBahanBakuModel>>(data);

                model.MainMenu = Enums.MenuList.TrnPermintaanBahanBaku;
                model.Menu = Enums.GetEnumDescription(Enums.MenuList.Produksi);
                model.CurrentUser = CurrentUser;
                model.Tittle = "Surat Permintaan Bahan Baku";

                return View(model);
            }
            catch (Exception exp)
            {
                LogError.LogError.WriteError(exp);
                AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
                return RedirectToAction("Index", "TrnSuratPermintaanBahanBaku");
            }
        }

        #region --- Create ---
        public ActionResult Create()
        {
            var model = new TrnSuratPermintaanBahanBakuModel();
            model.TANGGAL = DateTime.Now;

            return View(Init(model));
        }
        [HttpPost]
        public ActionResult Create(TrnSuratPermintaanBahanBakuModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.TANGGAL = DateTime.Now;
                    model.CREATED_BY = CurrentUser.USERNAME;
                    model.CREATED_DATE = DateTime.Now;
                    model.STATUS = (int)Enums.StatusDocument.Open;

                    var Dto = Mapper.Map<TrnSuratPermintaanBahanBakuDto>(model);

                    Dto = _trnSuratPermintaanBahanBakuBLL.Save(Dto, Mapper.Map<LoginDto>(CurrentUser));

                    foreach(var item in Dto.TRN_SURAT_PERMINTAAN_BAHAN_BAKU_DETAILS)
                    {
                        _mstBahanBakuBLL.KurangSaldo(item.ID_MST_BAHAN_BAKU,item.KUANTUM.Value);
                    }

                    AddMessageInfo("Sukses Create Form Hasil Produksi", Enums.MessageInfoType.Success);
                    return RedirectToAction("Details", "TrnSuratPermintaanBahanBaku", new { id= Dto.ID});
                }
                catch (Exception exp)
                {
                    LogError.LogError.WriteError(exp);
                    AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
                    return RedirectToAction("Index", "TrnSuratPermintaanBahanBaku");
                }
            }
            AddMessageInfo("Gagal Create Form Hasil Produksi", Enums.MessageInfoType.Error);
            return View(Init(model));
        }
        #endregion

        #region --- Edit ---
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return HttpNotFound();
            }

            try
            {
                var model = new TrnSuratPermintaanBahanBakuModel();

                model = Mapper.Map<TrnSuratPermintaanBahanBakuModel>(_trnSuratPermintaanBahanBakuBLL.GetById(id.Value));
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
                return RedirectToAction("Index", "TrnSuratPermintaanBahanBaku");
            }
        }
        [HttpPost]
        public ActionResult Edit(TrnSuratPermintaanBahanBakuModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.MODIFIED_BY = CurrentUser.USERNAME;
                    model.MODIFIED_DATE = DateTime.Now;

                    var Dto = Mapper.Map<TrnSuratPermintaanBahanBakuModel>(model);

                    _trnSuratPermintaanBahanBakuBLL.Save(Mapper.Map<TrnSuratPermintaanBahanBakuDto>(Dto), Mapper.Map<LoginDto>(CurrentUser));

                    AddMessageInfo("Sukses update data", Enums.MessageInfoType.Success);
                    return RedirectToAction("Index", "TrnSuratPermintaanBahanBaku");
                }
                catch (Exception exp)
                {
                    LogError.LogError.WriteError(exp);
                    AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
                    return RedirectToAction("Index", "TrnSuratPermintaanBahanBaku");
                }
            }
            AddMessageInfo("Gagal update data", Enums.MessageInfoType.Error);
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
                var model = new TrnSuratPermintaanBahanBakuModel();

                model = Mapper.Map<TrnSuratPermintaanBahanBakuModel>(_trnSuratPermintaanBahanBakuBLL.GetById(id.Value));
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
                return RedirectToAction("Index", "TrnPengiriman");
            }
        }
        #endregion

        #region --- Json ---
        public JsonResult GetSppList()
        {
            try
            {
                var model = _trnSuratPerintahProduksiBLL
               .GetAll()
               .Select(x
                   => new
                   {
                       DATA = x.NO_SURAT.ToUpper(),
                       DESKRIPSI = ((x.NAMA_PRODUK.ToUpper() + (string.IsNullOrEmpty(x.KOMPOSISI) ? "":(" - " + x.KOMPOSISI.ToUpper())) + " - " + x.KEMASAN.ToUpper())),
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
        public JsonResult GetSPP(string NoSPP)
        {
            try
            {
                var data = _trnSuratPerintahProduksiBLL.GetByNoSurat(NoSPP);
                return Json(data);
            }
            catch (Exception)
            {
                return Json("Error");
            }
        }
        public JsonResult MstBahanBakuList()
        {
            try
            {
                var model = _mstBahanBakuBLL
               .GetAll()
               .Select(x
                   => new
                   {
                       DATA = x.NAMA_BARANG.ToUpper()
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
        public JsonResult GetMstBahanBaku(string BahanBaku)
        {
            try
            {
                var data = _mstBahanBakuBLL.GetByNama(BahanBaku);
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