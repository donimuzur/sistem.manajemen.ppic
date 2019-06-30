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
using System.Configuration;


namespace sistem.manajemen.ppic.website.Controllers
{
    public class TrnSpbController : BaseController
    {
        private ITrnSpbBLL _trnSpbBLL;
        private IMstBarangJadiBLL _mstBarangJadiBLL;
        private IMstWilayahBLL _mstWilayahBLL;
        private IMstKemasanBLL _mstKemasanBLL;
        public TrnSpbController(ITrnSpbBLL TrnSpbBLL,IPageBLL pageBll, IMstBarangJadiBLL MstBarangJadiBLL, IMstWilayahBLL MstWilayahBLL,
            IMstKemasanBLL MstKemasanBLL) : base(pageBll, Enums.MenuList.TrnSpb)
        {
            _trnSpbBLL = TrnSpbBLL;
            _mstBarangJadiBLL = MstBarangJadiBLL;
            _mstWilayahBLL = MstWilayahBLL;
            _mstKemasanBLL = MstKemasanBLL;
        }
        public ActionResult Index()
        {
            var model = new TrnSpbViewModel();
            model.ListData = Mapper.Map<List<TrnSpbModel>>(_trnSpbBLL.GetAll());

            model.CurrentUser = CurrentUser;
            model.Menu = Enums.GetEnumDescription(Enums.MenuList.Transaction);
            model.Tittle = "Surat Permintaan Barang (SPB)";

            return View(model);
        }
        public TrnSpbModel Init(TrnSpbModel model)
        {
            model.CurrentUser = CurrentUser;
            model.Menu = Enums.GetEnumDescription(Enums.MenuList.Transaction);
            model.Tittle = "Surat Permintaan Barang (SPB)";
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
            ListKemasan = _mstKemasanBLL.GetAll().Select(x => x.KEMASAN).ToList();
            ListKemasan.Add("Lain-Lain");
            model.KemasanList = new SelectList(ListKemasan);

            var DokumenList = new List<string>();
            DokumenList.Add("MOU");
            DokumenList.Add("SPK");
            DokumenList.Add("Kontrak");
            DokumenList.Add("PO");
            DokumenList.Add("Lain-Lain");
            model.DokumenList = new SelectList(DokumenList);

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

                    model.NO_SPB = model.NO_SPB.ToUpper();
                    model.NO_SPB = model.NO_SPB.TrimEnd('\r', '\n', ' ');
                    model.NO_SPB = model.NO_SPB.TrimStart('\r', '\n', ' ');

                    if (model.Fileupload !=null)
                    {
                        var FilesUploadPath = ConfigurationManager.AppSettings["FilesUpload"];
                        var fileName = System.IO.Path.GetFileName(model.Fileupload.FileName);
                        if (System.IO.File.Exists(System.IO.Path.Combine(@FilesUploadPath, fileName)))
                        {
                            var a = fileName.Split('.').ToList();
                            a[0] = a[0] + DateTime.Now.ToString("yyyyMMddHHmmss");
                            fileName = string.Join(".", a);
                        }
                        model.DOKUMEN_PENDUKUNG_FILE = fileName;
                        model.Fileupload.SaveAs(System.IO.Path.Combine(@FilesUploadPath, fileName));
                    }

                    var CheckExist = _trnSpbBLL.GetBySPB(model.NO_SPB);
                    if(CheckExist != null)
                    {
                        AddMessageInfo("No. SPB sudah ada", Enums.MessageInfoType.Error);
                        return View(Init(model));
                    }

                    var Dto = Mapper.Map<TrnSpbDto>(model);
                    _trnSpbBLL.Save(Dto,Mapper.Map<LoginDto>(CurrentUser));

                    AddMessageInfo("Sukses Create SPB", Enums.MessageInfoType.Success);
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

                    if (model.Fileupload != null)
                    {
                        var FilesUploadPath = ConfigurationManager.AppSettings["FilesUpload"];
                        var fileName = System.IO.Path.GetFileName(model.Fileupload.FileName);
                        if (System.IO.File.Exists(System.IO.Path.Combine(@FilesUploadPath, fileName)))
                        {
                            var a = fileName.Split('.').ToList();
                            a[0] = a[0] + DateTime.Now.ToString("yyyyMMddHHmmss");
                            fileName = string.Join(".", a);
                        }
                        model.DOKUMEN_PENDUKUNG_FILE = fileName;
                        model.Fileupload.SaveAs(System.IO.Path.Combine(@FilesUploadPath, fileName));
                    }

                    var Dto = Mapper.Map<TrnSpbDto>(model);
                    _trnSpbBLL.Save(Dto,Mapper.Map<LoginDto>(CurrentUser));

                    AddMessageInfo("Success Update SPB", Enums.MessageInfoType.Success);
                    return RedirectToAction("Index", "TrnSpb");
                }
                catch (Exception exp)
                {
                    LogError.LogError.WriteError(exp);
                    AddMessageInfo("Gagal Update SPB", Enums.MessageInfoType.Error);
                    return RedirectToAction("Index", "TrnSpb");
                }
            }
            AddMessageInfo("Gagal Update SPB", Enums.MessageInfoType.Error);
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
        public JsonResult GetCustomerList()
        {
            var model = _trnSpbBLL
             .GetAll()
             .Select(x
                => new 
                {
                    DATA = (x.NAMA_KONSUMEN.ToUpper())
                })
             .Distinct()
             .OrderBy(X => X.DATA)
             .ToList();

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetWilayahList()
        {
            var model = _mstWilayahBLL
             .GetAll()
             .Where(x => x.STATUS)
             .Select(x
                => new
                {
                    DATA = (x.WILAYAH.ToUpper())
                })
                .OrderBy(X => X.DATA)
             .ToList();
            return Json(model,JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSalesRef()
        {
            var model = _trnSpbBLL
            .GetAll()
            .Select(x
               => new
               {
                   DATA = (x.SALES.ToUpper())
               })
            .Distinct()
            .OrderBy(X => X.DATA)
            .ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult DeleteFileDokumen(string id)
        {
            return Json("");
        }
        #endregion

    }
}