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
        private IMstBahanBakuBLL _mstBahanBakuBLL;
        private IMstKemasanBLL _mstKemasanBLL;
        private ITrnSpbBLL _trnSpbBLL;
        private ITrnDoBLL _trnDoBLL;
        public TrnSuratPengantarBongkarMuatController(IPageBLL pageBll, ITrnSpbBLL TrnSpbBLL, ITrnDoBLL TrnDoBLL, IMstBarangJadiBLL MstBarangJadiBLL, IMstKemasanBLL MstKemasanBLL, IMstBahanBakuBLL MstBahanBakuBLL, ITrnSuratPengantarBongkarMuatBLL TrnSuratPengantarBongkarMuatBLL) : base(pageBll, Enums.MenuList.TrnSuratPengantarBongkarMuat)
        {
            _trnSuratPengantarBongkarMuatBLL = TrnSuratPengantarBongkarMuatBLL;
            _mstBarangJadiBLL = MstBarangJadiBLL;
            _mstKemasanBLL = MstKemasanBLL;
            _trnSpbBLL = TrnSpbBLL;
            _trnDoBLL = TrnDoBLL;
            _mstBahanBakuBLL = MstBahanBakuBLL;
        }
        // GET: TrnSuratPengantarBongkarMuat
        public ActionResult Index()
        {
            var model = new TrnSuratPengantarBongkarViewModel();
            model.ListData = Mapper.Map<List<TrnSuratPengantarBongkarMuatModel>>(_trnSuratPengantarBongkarMuatBLL.GetActiveAll());

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
            model.ChangesHistory = GetChangesHistory((int)Enums.MenuList.TrnSuratPengantarBongkarMuat, model.ID);

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

                    if(!string.IsNullOrEmpty(model.NO_SPB))
                    {
                        model.NO_SPB = model.NO_SPB.ToUpper();
                        model.NO_SPB = model.NO_SPB.TrimEnd('\r', '\n', ' ');
                        model.NO_SPB = model.NO_SPB.TrimStart('\r', '\n', ' ');
                    }

                    model.TRNSPT_NO_POLISI = model.TRNSPT_NO_POLISI.Replace(" ", "");

                    var Dto = Mapper.Map<TrnSuratPengantarBongkarMuatDto>(model);
                    _trnSuratPengantarBongkarMuatBLL.Save(Dto, Mapper.Map<LoginDto>(CurrentUser));

                    AddMessageInfo("Sukses Create Surat Pengantar Bongkakr Muat", Enums.MessageInfoType.Success);
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

        #region --- Create ---
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return HttpNotFound();
            }

            try
            {
                var model = new TrnSuratPengantarBongkarMuatModel();

                model = Mapper.Map<TrnSuratPengantarBongkarMuatModel>(_trnSuratPengantarBongkarMuatBLL.GetById(id));

                model = Init(model);
                return View(model);
            }
            catch (Exception exp)
            {
                LogError.LogError.WriteError(exp);
                AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
                return RedirectToAction("Index", "TrnSuratPengantarBongkarMuat");
            }
        }

        [HttpPost]
        public ActionResult Edit(TrnSuratPengantarBongkarMuatModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.MODIFIED_BY = CurrentUser.USERNAME;
                    model.MODIFIED_DATE = DateTime.Now;

                    if (!string.IsNullOrEmpty(model.NO_SPB))
                    {
                        model.NO_SPB = model.NO_SPB.ToUpper();
                        model.NO_SPB = model.NO_SPB.TrimEnd('\r', '\n', ' ');
                        model.NO_SPB = model.NO_SPB.TrimStart('\r', '\n', ' ');
                    }

                    model.TRNSPT_NO_POLISI = model.TRNSPT_NO_POLISI.Replace(" ", "");

                    var Dto = Mapper.Map<TrnSuratPengantarBongkarMuatDto>(model);
                    _trnSuratPengantarBongkarMuatBLL.Save(Dto, Mapper.Map<LoginDto>(CurrentUser));

                    AddMessageInfo("Sukses Update Surat Pengantar Bongkar Muat", Enums.MessageInfoType.Success);
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

        #region --- Details ---
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return HttpNotFound();
            }

            try
            {
                var model = new TrnSuratPengantarBongkarMuatModel();

                model = Mapper.Map<TrnSuratPengantarBongkarMuatModel>(_trnSuratPengantarBongkarMuatBLL.GetById(id));

                model = Init(model);
                return View(model);
            }
            catch (Exception exp)
            {
                LogError.LogError.WriteError(exp);
                AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
                return RedirectToAction("Index", "TrnSuratPengantarBongkarMuat");
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
                var model = _trnSuratPengantarBongkarMuatBLL.GetById(id);
                if (model == null)
                {
                    return HttpNotFound();
                }

                _trnSuratPengantarBongkarMuatBLL.Delete(id.Value, Remarks);

                AddMessageInfo("Data sukses dihapus", Enums.MessageInfoType.Success);
                return RedirectToAction("Index", "TrnSuratPengantarBongkarMuat");
            }
            catch (Exception exp)
            {
                LogError.LogError.WriteError(exp);
                AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
                return RedirectToAction("Index", "TrnSuratPengantarBongkarMuat");
            }
        }
        #endregion
        
        #region --- Json ---
        [HttpPost]
        public JsonResult CheckDoExistBySpb(string NoSpb)
        {
            var data = _trnDoBLL.GetBySPB(NoSpb);
            if (data == null)
            {
                data = new List<TrnDoDto>();
            }
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

        //Get DO
        public JsonResult GetDoList(string No_SPB)
        {
            var model = _trnDoBLL
             .GetBySPB(No_SPB)
             .Select(x
                 => new
                 {
                     DATA = x.NO_DO.PadLeft(4,'0').ToUpper()
                 })
             .Distinct()
             .OrderBy(X => X.DATA)
             .ToList();

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProdukList(int JenisBarang)
        {
            if (JenisBarang == (int)Enums.JenisBarang.BarangJadi)
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
            else if (JenisBarang == (int)Enums.JenisBarang.BahanBaku)
            {
                var model = _mstBahanBakuBLL
                  .GetAll()
                  .Select(x
                      => new
                      {
                          DATA = x.NAMA_BARANG.ToUpper(),
                          DESKRIPSI = (x.SATUAN),
                      })
                  .Distinct()
                  .OrderBy(X => X.DATA)
                  .ToList();
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            return Json("Error", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetProduk(string Produk, int JenisBarang)
        {
            if (JenisBarang == (int)Enums.JenisBarang.BarangJadi)
            {
                var data = _mstBarangJadiBLL.GetByNama(Produk);
                return Json(data);
            }
            else if (JenisBarang == (int)Enums.JenisBarang.BarangJadi)
            {
                var data = _mstBahanBakuBLL.GetByNama(Produk);
                return Json(data);
            }
            return Json("Error");
        }
        #endregion
    }
}