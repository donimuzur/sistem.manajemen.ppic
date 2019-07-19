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
    public class TrnMutasiBarangController : BaseController
    {
        private ITrnMutasiBarangBLL _trnMutasiBarangBLL;
        private IMstBarangJadiBLL _mstBarangJadiBLL;
        private IMstBahanBakuBLL _mstBahanBakuBLL;
        public TrnMutasiBarangController(IPageBLL pageBll, ITrnMutasiBarangBLL TrnMutasiBarangBLL, IMstBarangJadiBLL MstBarangJadiBLL, IMstBahanBakuBLL MstBahanBakuBLL) : base(pageBll, Enums.MenuList.TrnSuratMutasiBarang)
        {
            _trnMutasiBarangBLL = TrnMutasiBarangBLL;
            _mstBarangJadiBLL = MstBarangJadiBLL;
            _mstBahanBakuBLL= MstBahanBakuBLL;
        }

        // GET: TrnMutasiBarang
        public ActionResult Index()
        {
            var model = new TrnMutasiBarangViewModel();
            model.ListData = Mapper.Map<List<TrnMutasiBarangModel>>(_trnMutasiBarangBLL.GetActiveAll());

            model.CurrentUser = CurrentUser;
            model.Menu = Enums.GetEnumDescription(Enums.MenuList.Ekspedisi);
            model.Tittle = "Mutasi Barang";

            return View(model);
        }
        public TrnMutasiBarangModel Init(TrnMutasiBarangModel model)
        {
            model.CurrentUser = CurrentUser;
            model.Menu = Enums.GetEnumDescription(Enums.MenuList.Ekspedisi);
            model.Tittle = "Mutasi Barang";
            model.ChangesHistory = GetChangesHistory((int)Enums.MenuList.TrnSuratMutasiBarang, model.ID);

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

        #region --- Create ---
        public ActionResult Create()
        {
            var model = new TrnMutasiBarangModel();
            model.TANGGAL = DateTime.Now;
            return View(Init(model));
        }
        [HttpPost]
        public ActionResult Create(TrnMutasiBarangModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.TANGGAL = DateTime.Now;
                    model.CREATED_BY = CurrentUser.USERNAME;
                    model.CREATED_DATE = DateTime.Now;
                    model.STATUS = (int)Enums.StatusDocument.Open;
                    
                    if(model.JENIS_BARANG == (int)Enums.JenisBarang.BarangJadi)
                    {
                        var GetBarang = _mstBarangJadiBLL.GetByNama(model.NAMA_BARANG);
                        if (GetBarang != null)
                        {
                            model.ID_BARANG_JADI = GetBarang.ID;
                            model.STOCK_AWAL = GetBarang.STOCK;
                            model.STOCK_AKHIR = model.STOCK_AWAL - model.JUMLAH;
                            if (model.STOCK_AKHIR < 0)
                            {
                                AddMessageInfo("Stock barang tidak mencukupi", Enums.MessageInfoType.Error);
                                return View(Init(model));
                            }
                        }
                    }
                    else if(model.JENIS_BARANG == (int)Enums.JenisBarang.BahanBaku)
                    {
                        var GetBarang = _mstBahanBakuBLL.GetById(model.NAMA_BARANG);
                        if (GetBarang != null)
                        {
                            model.ID_BAHAN_BAKU = GetBarang.ID;
                            model.STOCK_AWAL = GetBarang.STOCK;
                            model.STOCK_AKHIR = model.STOCK_AWAL - model.JUMLAH;
                            if (model.STOCK_AKHIR < 0)
                            {
                                AddMessageInfo("Stock barang tidak mencukupi", Enums.MessageInfoType.Error);
                                return View(Init(model));
                            }
                        }
                    }
                    
                    var Dto = Mapper.Map<TrnMutasiBarangDto>(model);
                    _trnMutasiBarangBLL.Save(Dto, Mapper.Map<LoginDto>(CurrentUser));
                    if(model.JENIS_BARANG == (int)Enums.JenisBarang.BarangJadi)
                    {
                        _mstBarangJadiBLL.KurangSaldo(model.ID_BARANG_JADI.Value, model.JUMLAH.Value);
                    }
                    else if(model.JENIS_BARANG == (int)Enums.JenisBarang.BahanBaku)
                    {
                        _mstBahanBakuBLL.KurangSaldo(model.ID_BAHAN_BAKU.Value, model.JUMLAH.Value);
                    }

                    AddMessageInfo("Sukses Create Form Mutasi Barang", Enums.MessageInfoType.Success);
                    return RedirectToAction("Index", "TrnMutasiBarang");
                }
                catch (Exception exp)
                {
                    LogError.LogError.WriteError(exp);
                    AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
                    return RedirectToAction("Index", "TrnMutasiBarang");
                }
            }
            AddMessageInfo("Gagal Create Form Mutasi Barang", Enums.MessageInfoType.Error);
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
                var model = new TrnMutasiBarangModel();

                model = Mapper.Map<TrnMutasiBarangModel>(_trnMutasiBarangBLL.GetById(id));
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
                return RedirectToAction("Index", "TrnMutasiBarang");
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
                var model = _trnMutasiBarangBLL.GetById(id);
                if (model == null)
                {
                    return HttpNotFound();
                }

                _trnMutasiBarangBLL.Delete(id.Value, Remarks);
                if(model.JENIS_BARANG == (int)Enums.JenisBarang.BarangJadi)
                {
                    _mstBarangJadiBLL.TambahSaldo(model.ID_BARANG_JADI.Value, model.JUMLAH.Value);
                }
                else if(model.JENIS_BARANG == (int)Enums.JenisBarang.BahanBaku)
                {
                    _mstBahanBakuBLL.TambahSaldo(model.ID_BAHAN_BAKU.Value, model.JUMLAH.Value);
                }

                AddMessageInfo("Data sukses dihapus", Enums.MessageInfoType.Success);
                return RedirectToAction("Index", "TrnMutasiBarang");
            }
            catch (Exception exp)
            {
                LogError.LogError.WriteError(exp);
                AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
                return RedirectToAction("Index", "TrnMutasiBarang");
            }
        }
        #endregion

        #region --- Json ---
        public JsonResult GetProdukList(int JenisBarang)
        {
            if(JenisBarang == (int)Enums.JenisBarang.BarangJadi)
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
            else if(JenisBarang == (int)Enums.JenisBarang.BahanBaku)
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
            if(JenisBarang == (int)Enums.JenisBarang.BarangJadi)
            {
                var data = _mstBarangJadiBLL.GetByNama(Produk);
                return Json(data);
            }
            else if(JenisBarang == (int)Enums.JenisBarang.BarangJadi)
            {
                var data = _mstBahanBakuBLL.GetByNama(Produk);
                return Json(data);
            }
            return Json("Error");
        }
        #endregion
    }
}