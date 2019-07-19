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
using sistem.manajemen.ppic.website.Controllers.Utility;

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
            model.ListData = Mapper.Map<List<TrnSpbModel>>(_trnSpbBLL.GetActiveAll());

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
                    AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
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
                    AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
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

        #region --- Import ---
        
        public ActionResult Import()
        {
            var model = new TrnSpbModel();

            model = Init(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Import(HttpPostedFileBase Fileupload)
        {
            try
            {
                var model = new TrnSpbModel();

                var TrnSpbList = _trnSpbBLL.GetAll();
                var MstBarangJadiList = _mstBarangJadiBLL.GetAll();
                var MstKemasanList = _mstKemasanBLL.GetAll();
                
                var data = (new ExcelReader()).ReadExcel(Fileupload);
                var ItemToUpload = new List<TrnSpbModel>();
                if (data != null)
                {
                    foreach (var dataRow in data.DataRows)
                    {
                        if (dataRow.Count <= 0)
                        {
                            continue;
                        }
                        if (dataRow[0] == "")
                        {
                            continue;
                        }
                        if ((dataRow[0] == null ? "" : dataRow[0].ToUpper()) == "NO SPB")
                        {
                            continue;
                        }

                        var item = new TrnSpbModel();
                        try
                        {
                            //NoSpb
                            if (dataRow[0] == "")
                            {
                                item.NO_SPB = "";
                            }
                            else
                            {
                                item.NO_SPB = dataRow[0];
                                if (TrnSpbList.Where(x => x.NO_SPB == item.NO_SPB).ToList().Count > 0)
                                {
                                    item.NO_SPB = "";
                                }
                            }

                            //Sales
                            if (string.IsNullOrEmpty(dataRow[1]))
                            {
                                item.SALES = "";
                            }
                            else
                            {
                                item.SALES = dataRow[1];
                            }

                            //Wilayah
                            if (string.IsNullOrEmpty(dataRow[2]))
                            {
                                item.WILAYAH = "";
                            }
                            else
                            {
                                item.WILAYAH = dataRow[2];
                            }

                            //Nama Pupuk
                            if (string.IsNullOrEmpty(dataRow[3]))
                            {
                                item.NAMA_PRODUK = "";
                            }
                            else
                            {
                                item.NAMA_PRODUK = dataRow[3];
                            }

                            //Komposisi
                            if (string.IsNullOrEmpty(dataRow[4]))
                            {
                                item.KOMPOSISI = "";
                            }
                            else
                            {
                                item.KOMPOSISI = dataRow[4];
                            }

                            //Bentuk
                            var ListBentuk = new List<string>();
                            ListBentuk.Add("Powder");
                            ListBentuk.Add("Granule");
                            ListBentuk.Add("Tablet");
                            ListBentuk.Add("Bricket");
                            ListBentuk.Add("Lain-Lain");
                            if (string.IsNullOrEmpty(dataRow[5]))
                            {
                                item.BENTUK = "";
                            }
                            else
                            {
                                item.BENTUK = dataRow[5];
                                var GetData = ListBentuk.Where(x => (x == null ? "" : x.ToUpper()) == item.BENTUK.ToUpper()).FirstOrDefault();
                                if (GetData == null)
                                {
                                    item.BENTUK_DESC = item.BENTUK;
                                    item.BENTUK = "Lain-Lain";
                                }
                                else
                                {
                                    item.BENTUK = GetData;
                                }
                            }

                            //Kemasan
                            var ListKemasan = new List<string>();
                            ListKemasan = MstKemasanList.Select(x => x.KEMASAN).ToList();
                            ListKemasan.Add("Lain-Lain");
                            if (string.IsNullOrEmpty(dataRow[6]))
                            {
                                item.KEMASAN = "";
                            }
                            else
                            {
                                item.KEMASAN = dataRow[6];
                                var GetData = ListKemasan.Where(x => (x == null ? "" : x.ToUpper()) == item.KEMASAN.ToUpper()).FirstOrDefault();
                                if (GetData == null)
                                {
                                    item.KEMASAN_DESC = item.KEMASAN;
                                    item.KEMASAN = "Lain-Lain";
                                }
                                else
                                {
                                    item.KEMASAN = GetData;
                                }
                            }

                            //Konsumen
                            if (string.IsNullOrEmpty(dataRow[7]))
                            {
                                item.NAMA_KONSUMEN = "";
                            }
                            else
                            {
                                item.NAMA_KONSUMEN = dataRow[7];
                            }

                            //Alamat Konsumen
                            if (string.IsNullOrEmpty(dataRow[8]))
                            {
                                item.ALAMAT_KONSUMEN = "";
                            }
                            else
                            {
                                item.ALAMAT_KONSUMEN = dataRow[8];
                            }

                            //Segmen Pasar
                            var ListSegmenPasar = new List<string>();
                            ListSegmenPasar.Add("PTPN");
                            ListSegmenPasar.Add("PBSN");
                            ListSegmenPasar.Add("Plasma");
                            ListSegmenPasar.Add("Tambak");
                            ListSegmenPasar.Add("Tanpang");
                            ListSegmenPasar.Add("Industri");
                            ListSegmenPasar.Add("Proyek");
                            if (string.IsNullOrEmpty(dataRow[9]))
                            {
                                item.SEGMEN_PASAR = "";
                            }
                            else
                            {
                                item.SEGMEN_PASAR = dataRow[9];
                                if (ListSegmenPasar.Where(x => (x == null ? "" : x.ToUpper()) == item.SEGMEN_PASAR.ToUpper()).ToList().Count == 0)
                                {
                                    item.SEGMEN_PASAR = "";
                                }
                            }

                            //CP
                            item.CONTACT_PERSON = "";
                            if (!string.IsNullOrEmpty(dataRow[10]))
                            {
                                item.CONTACT_PERSON = dataRow[10];
                            }

                            //Telepon
                            item.NO_TELP = "";
                            if (!string.IsNullOrEmpty(dataRow[11]))
                            {
                                item.NO_TELP = dataRow[11];
                            }

                            //Jenis Penjualan
                            var ListJenisPenjualan = new List<string>();
                            ListJenisPenjualan.Add("Loko");
                            ListJenisPenjualan.Add("Site");
                            ListJenisPenjualan.Add("FOB");
                            ListJenisPenjualan.Add("FAS");
                            ListJenisPenjualan.Add("CNF");
                            ListJenisPenjualan.Add("CIF");
                            ListJenisPenjualan.Add("Lain-Lain");
                            if (string.IsNullOrEmpty(dataRow[12]))
                            {
                                item.JENIS_PENJUALAN = "";
                            }
                            else
                            {
                                item.JENIS_PENJUALAN = dataRow[12];
                                var GetData = ListJenisPenjualan.Where(x => (x == null ? "" : x.ToUpper()) == item.JENIS_PENJUALAN.ToUpper()).FirstOrDefault();
                                if (GetData == null)
                                {
                                    item.JENIS_PENJUALAN_DESC = item.JENIS_PENJUALAN;
                                    item.JENIS_PENJUALAN = "Lain-Lain";
                                }
                                else
                                {
                                    item.JENIS_PENJUALAN = GetData;
                                }
                            }

                            //Tujuan Kirim
                            if (string.IsNullOrEmpty(dataRow[13]))
                            {
                                item.LOKASI_KIRIM = "";
                            }
                            else
                            {
                                item.LOKASI_KIRIM = dataRow[13];
                            }

                            //Kuantum
                            if (string.IsNullOrEmpty(dataRow[14]))
                            {
                            }
                            else
                            {
                                try
                                {
                                    item.KUANTUM = decimal.Round(decimal.Parse(dataRow[14]), 2);
                                }
                                catch (Exception)
                                {

                                }
                            }

                            //Batas Akhir Kirim
                            if (string.IsNullOrEmpty(dataRow[15]))
                            {

                            }
                            else
                            {
                                try
                                {
                                    item.BATAS_KIRIM = DateTime.FromOADate(double.Parse(dataRow[15]));
                                }
                                catch (Exception)
                                {

                                }
                            }

                            //Cara Pembayaran
                            var ListCaraPembayaran = new List<string>();
                            ListCaraPembayaran.Add("Tunai");
                            ListCaraPembayaran.Add("Kredit");
                            if (string.IsNullOrEmpty(dataRow[16]))
                            {
                            }
                            else
                            {
                                item.CARA_PEMBAYARAN = dataRow[16].ToLower();
                                var GetData = ListCaraPembayaran.Where(x => (x == null ? "" : x.ToUpper()) == item.CARA_PEMBAYARAN.ToUpper()).FirstOrDefault();
                                if (GetData == null)
                                {
                                }
                                else
                                {
                                    item.CARA_PEMBAYARAN = GetData;
                                }
                            }

                            //PPN
                            var ListPPN = new List<string>();
                            ListPPN.Add("Incl PPN");
                            ListPPN.Add("Excl PPN");
                            ListPPN.Add("Non PPN");
                            if (string.IsNullOrEmpty(dataRow[17]))
                            {
                            }
                            else
                            {
                                item.PPN = dataRow[17];
                                var GetData = ListPPN.Where(x => (x == null ? "" : x.ToUpper()) == item.PPN.ToUpper()).FirstOrDefault();
                                if (GetData == null)
                                {
                                    item.PPN = "";
                                }
                                else
                                {
                                    item.PPN = GetData;
                                }
                            }

                            //Harga Jual
                            if (string.IsNullOrEmpty(dataRow[18]))
                            {
                            }
                            else
                            {
                                try
                                {
                                    item.HARGA_JUAL = decimal.Round(decimal.Parse(dataRow[18]), 2);
                                }
                                catch (Exception)
                                {

                                }
                            }

                            //Harga Loko
                            if (string.IsNullOrEmpty(dataRow[19]))
                            {
                            }
                            else
                            {
                                try
                                {
                                    item.HARGA_LOKO = decimal.Round(decimal.Parse(dataRow[19]), 2); 
                                }
                                catch (Exception)
                                {
                                }
                            }

                            //Ongkos Kirim
                            if (string.IsNullOrEmpty(dataRow[20]))
                            {
                            }
                            else
                            {
                                try
                                {
                                    item.ONGKOS_KIRIM = decimal.Round(decimal.Parse(dataRow[20]), 2);
                                }
                                catch (Exception)
                                {
                                }
                            }

                            //Jenis Dokumen
                            var DokumenList = new List<string>();
                            DokumenList.Add("MOU");
                            DokumenList.Add("SPK");
                            DokumenList.Add("Kontrak");
                            DokumenList.Add("PO");
                            DokumenList.Add("Lain-Lain");
                            if (!string.IsNullOrEmpty(dataRow[21]))
                            {
                                item.DOKUMEN_PENDUKUNG = dataRow[21];
                                var GetData = DokumenList.Where(x => (x == null ? "" : x.ToUpper()) == item.DOKUMEN_PENDUKUNG.ToUpper()).FirstOrDefault();
                                if (GetData == null)
                                {
                                    item.DOKUMEN_PENDUKUNG_DESC = item.DOKUMEN_PENDUKUNG;
                                    item.DOKUMEN_PENDUKUNG = "Lain-Lain";
                                }
                                else
                                {
                                    item.DOKUMEN_PENDUKUNG = GetData;
                                }
                            }

                            //Nomor Dokumen
                            if (!string.IsNullOrEmpty(dataRow[22]))
                            {
                                item.DOKUMEN_NOMOR = dataRow[22].ToUpper();
                            }

                            //Keterangan
                            if (!string.IsNullOrEmpty(dataRow[23]))
                            {
                                item.CATATAN = dataRow[23];
                            }

                            item.TANGGAL = DateTime.Now;
                            item.CREATED_BY = CurrentUser.USERNAME;
                            item.CREATED_DATE = DateTime.Now;
                            item.STATUS = Enums.StatusDocument.Open;

                            ItemToUpload.Add(item);
                        }
                        catch (Exception exp)
                        {
                            LogError.LogError.WriteError(exp);
                            AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
                            return RedirectToAction("Index", "TrnSpb");
                        }
                    }
                    _trnSpbBLL.Savelist(Mapper.Map<List<TrnSpbDto>>(ItemToUpload), Mapper.Map<LoginDto>(CurrentUser));
                    AddMessageInfo("Sukses Import SPB", Enums.MessageInfoType.Success);
                    return RedirectToAction("Index", "TrnSpb");
                }
                AddMessageInfo("Gagal Import SPB", Enums.MessageInfoType.Error);
                return View(Init(new TrnSpbModel()));
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


        [HttpPost]
        public JsonResult UploadFile(HttpPostedFileBase FileUpload)
        {
            var TrnSpbList = _trnSpbBLL.GetAll();
            var MstBarangJadiList = _mstBarangJadiBLL.GetAll();
            var MstKemasanList = _mstKemasanBLL.GetAll();
            
            var data = (new ExcelReader()).ReadExcel(FileUpload);
            var model = new List<TrnSpbUploadModel>();
            if (data != null)
            {
                foreach (var dataRow in data.DataRows)
                {
                    if (dataRow.Count <= 0)
                    {
                        continue;
                    }
                    if (dataRow[0] == "")
                    {
                        continue;
                    }
                    if ((dataRow[0]==null?"":dataRow[0].ToUpper()) == "NO SPB")
                    {
                        continue;
                    }

                    var item = new TrnSpbUploadModel();
                    item.MessageError = "";
                    string MessageError = null;
                    try
                    {
                        //NoSpb
                        if (dataRow[0] == "")
                        {
                            item.MessageError += "No SPB tidak boleh kosong, ";
                            item.NO_SPB = "";
                        }
                        else
                        {
                            item.NO_SPB = dataRow[0];
                            if (TrnSpbList.Where(x => x.NO_SPB == item.NO_SPB).ToList().Count > 0)
                            {
                                item.MessageError += "No SPB sudah ada, ";
                            }
                        }

                        //Sales
                        if (string.IsNullOrEmpty(dataRow[1]))
                        {
                            item.MessageError += "Sales Representatif tidak boleh kosong, ";
                            item.SALES = "";
                        }
                        else
                        {
                            item.SALES = dataRow[1];
                        }

                        //Wilayah
                        if (string.IsNullOrEmpty(dataRow[2]))
                        {
                            item.MessageError += "Wilayah tidak boleh kosong, ";
                            item.WILAYAH = "";
                        }
                        else
                        {
                            item.WILAYAH = dataRow[2];
                        }

                        //Nama Pupuk
                        if (string.IsNullOrEmpty(dataRow[3]))
                        {
                            item.MessageError += "Nama Pupuk tidak boleh kosong, ";
                            item.NAMA_PRODUK = "";
                        }
                        else
                        {
                            item.NAMA_PRODUK = dataRow[3];
                        }

                        //Komposisi
                        if (string.IsNullOrEmpty(dataRow[4]))
                        {
                            item.MessageError += "Komposisi tidak boleh kosong, ";
                            item.KOMPOSISI = "";
                        }
                        else
                        {
                            item.KOMPOSISI = dataRow[4];
                        }

                        //Bentuk
                        var ListBentuk = new List<string>();
                        ListBentuk.Add("Powder");
                        ListBentuk.Add("Granule");
                        ListBentuk.Add("Tablet");
                        ListBentuk.Add("Bricket");
                        ListBentuk.Add("Lain-Lain");
                        if (string.IsNullOrEmpty(dataRow[5]))
                        {
                            item.MessageError += "Bentuk tidak boleh kosong, ";
                            item.BENTUK = "";
                        }
                        else
                        {
                            item.BENTUK = dataRow[5];
                            var GetData = ListBentuk.Where(x => (x == null ? "" : x.ToUpper()) == item.BENTUK.ToUpper()).FirstOrDefault();
                            if (GetData == null)
                            {
                                item.BENTUK_DESC = item.BENTUK;
                                item.BENTUK = "Lain-Lain";
                            }
                            else
                            {
                                item.BENTUK = GetData;
                            }
                        }

                        //Kemasan
                        var ListKemasan = new List<string>();
                        ListKemasan =MstKemasanList.Select(x => x.KEMASAN).ToList();
                        ListKemasan.Add("Lain-Lain");
                        if (string.IsNullOrEmpty(dataRow[6]))
                        {
                            item.MessageError += "Kemasan tidak boleh kosong, ";
                            item.KEMASAN = "";
                        }
                        else
                        {
                            item.KEMASAN = dataRow[6];
                            var GetData = ListKemasan.Where(x => (x == null ? "" : x.ToUpper()) == item.KEMASAN.ToUpper()).FirstOrDefault();
                            if (GetData == null)
                            {
                                item.KEMASAN_DESC = item.KEMASAN;
                                item.KEMASAN = "Lain-Lain";
                            }
                            else
                            {
                                item.KEMASAN = GetData;
                            }
                        }

                        //Konsumen
                        if (string.IsNullOrEmpty(dataRow[7]))
                        {
                            item.MessageError += "konsumen tidak boleh kosong, ";
                            item.NAMA_KONSUMEN = "";
                        }
                        else
                        {
                            item.NAMA_KONSUMEN = dataRow[7];
                        }

                        //Alamat Konsumen
                        if (string.IsNullOrEmpty(dataRow[8]))
                        {
                            item.MessageError += "Alamat konsumen tidak boleh kosong, ";
                            item.ALAMAT_KONSUMEN = "";
                        }
                        else
                        {
                            item.ALAMAT_KONSUMEN = dataRow[8];
                        }

                        //Segmen Pasar
                        var ListSegmenPasar = new List<string>();
                        ListSegmenPasar.Add("PTPN");
                        ListSegmenPasar.Add("PBSN");
                        ListSegmenPasar.Add("Plasma");
                        ListSegmenPasar.Add("Tambak");
                        ListSegmenPasar.Add("Tanpang");
                        ListSegmenPasar.Add("Industri");
                        ListSegmenPasar.Add("Proyek");
                        if (string.IsNullOrEmpty(dataRow[9]))
                        {
                            item.MessageError += "Segmen Pasar tidak boleh kosong, ";
                            item.SEGMEN_PASAR = "";
                        }
                        else
                        {
                            item.SEGMEN_PASAR = dataRow[9];
                            if(ListSegmenPasar.Where(x => (x == null ? "" : x.ToUpper()) == item.SEGMEN_PASAR.ToUpper()).ToList().Count == 0)
                            {
                                item.MessageError += "Segmen Pasar tidak ada dalam daftar, ";
                                item.SEGMEN_PASAR = "";
                            }
                        }

                        //CP
                        item.CONTACT_PERSON = "";
                        if (!string.IsNullOrEmpty(dataRow[10]))
                        {
                            item.CONTACT_PERSON = dataRow[10];
                        }

                        //Telepon
                        item.NO_TELP = "";
                        if (!string.IsNullOrEmpty(dataRow[11]))
                        {
                            item.NO_TELP = dataRow[11];
                        }

                        //Jenis Penjualan
                        var ListJenisPenjualan = new List<string>();
                        ListJenisPenjualan.Add("Loko");
                        ListJenisPenjualan.Add("Site");
                        ListJenisPenjualan.Add("FOB");
                        ListJenisPenjualan.Add("FAS");
                        ListJenisPenjualan.Add("CNF");
                        ListJenisPenjualan.Add("CIF");
                        ListJenisPenjualan.Add("Lain-Lain");
                        if (string.IsNullOrEmpty(dataRow[12]))
                        {
                            item.MessageError += "Jenis Penjualan tidak boleh kosong, ";
                            item.JENIS_PENJUALAN = "";
                        }
                        else
                        {
                            item.JENIS_PENJUALAN = dataRow[12];
                            var GetData = ListJenisPenjualan.Where(x => (x == null ? "" : x.ToUpper()) == item.JENIS_PENJUALAN.ToUpper()).FirstOrDefault();
                            if (GetData == null)
                            {
                                item.JENIS_PENJUALAN_DESC = item.JENIS_PENJUALAN;
                                item.JENIS_PENJUALAN = "Lain-Lain";
                            }
                            else
                            {
                                item.JENIS_PENJUALAN = GetData;
                            }
                        }

                        //Tujuan Kirim
                        if (string.IsNullOrEmpty(dataRow[13]))
                        {
                            item.MessageError += "Tujuan Kirim tidak boleh kosong, ";
                            item.LOKASI_KIRIM = "";
                        }
                        else
                        {
                            item.LOKASI_KIRIM = dataRow[13];
                        }

                        //Kuantum
                        if (string.IsNullOrEmpty(dataRow[14]))
                        {
                            item.MessageError += "Kuantum tidak boleh kosong, ";
                        }
                        else
                        {
                            try
                            {
                                item.KUANTUM = decimal.Round(decimal.Parse(dataRow[14]),2);
                            }
                            catch (Exception)
                            {
                                item.MessageError += "harus diisi dengan angka, ";
                                
                            }
                        }

                        //Batas Akhir Kirim
                        if (string.IsNullOrEmpty(dataRow[15]))
                        {
                            item.MessageError += "batas Akhir Kirim tidak boleh kosong, ";

                        }
                        else
                        {
                            try
                            {
                                item.BATAS_KIRIM = DateTime.FromOADate(double.Parse(dataRow[15]));
                            }
                            catch (Exception)
                            {
                                item.MessageError += "Tangal harus sesuai format, ";

                            }
                        }

                        //Cara Pembayaran
                        var ListCaraPembayaran = new List<string>();
                        ListCaraPembayaran.Add("Tunai");
                        ListCaraPembayaran.Add("Kredit");
                        if (string.IsNullOrEmpty(dataRow[16]))
                        {
                            item.MessageError += "Cara Pembayaran tidak boleh kosong, ";
                        }
                        else
                        {
                            item.CARA_PEMBAYARAN = dataRow[16].ToLower();
                            var GetData = ListCaraPembayaran.Where(x => (x == null ? "" : x.ToUpper()) == item.CARA_PEMBAYARAN.ToUpper()).FirstOrDefault();
                            if (GetData == null)
                            {
                                item.MessageError += "Cara pembayaran tidak ada di daftar, ";
                            }
                            else
                            {
                                item.CARA_PEMBAYARAN = GetData;
                            }
                        }

                        //PPN
                        var ListPPN = new List<string>();
                        ListPPN.Add("Incl PPN");
                        ListPPN.Add("Excl PPN");
                        ListPPN.Add("Non PPN");
                        if (string.IsNullOrEmpty(dataRow[17]))
                        {
                            item.MessageError += "PPN tidak boleh kosong, ";
                        }
                        else
                        {
                            item.PPN = dataRow[17];
                            var GetData = ListPPN.Where(x => (x == null ? "" : x.ToUpper()) == item.PPN.ToUpper()).FirstOrDefault();
                            if (GetData== null )
                            {
                                item.MessageError += "PPN tidak ada di dalam daftar, ";
                                item.PPN = "";
                            }
                            else
                            {
                                item.PPN = GetData;
                            }
                        }

                        //Harga Jual
                        if (string.IsNullOrEmpty(dataRow[18]))
                        {
                            MessageError += "Harga Jual tidak boleh kosong, ";
                        }
                        else
                        {
                            try
                            {
                                item.HARGA_JUAL = decimal.Round(decimal.Parse(dataRow[18]), 2);
                            }
                            catch (Exception)
                            {
                                item.MessageError += "harus diisi dengan angka, ";

                            }
                        }

                        //Harga Loko
                        if (string.IsNullOrEmpty(dataRow[19]))
                        {
                            item.MessageError += "Harga Loko tidak boleh kosong, ";
                        }
                        else
                        {
                            try
                            {
                                item.HARGA_LOKO = decimal.Round(decimal.Parse(dataRow[19]), 2);
                            }
                            catch (Exception)
                            {
                                item.MessageError += "harus diisi dengan angka, ";
                            }
                        }

                        //Ongkos Kirim
                        if (string.IsNullOrEmpty(dataRow[20]))
                        {
                            item.MessageError += "Ongkos Kirim tidak boleh kosong, ";
                        }
                        else
                        {
                            try
                            {
                                item.ONGKOS_KIRIM = decimal.Round(decimal.Parse(dataRow[20]), 2);
                            }
                            catch (Exception)
                            {
                                item.MessageError += "harus diisi dengan angka, ";
                            }
                        }

                        //Jenis Dokumen
                        var DokumenList = new List<string>();
                        DokumenList.Add("MOU");
                        DokumenList.Add("SPK");
                        DokumenList.Add("Kontrak");
                        DokumenList.Add("PO");
                        DokumenList.Add("Lain-Lain");
                        if (!string.IsNullOrEmpty(dataRow[21]))
                        {
                            item.DOKUMEN_PENDUKUNG = dataRow[21];
                            var GetData = DokumenList.Where(x => (x == null ? "" : x.ToUpper()) == item.DOKUMEN_PENDUKUNG.ToUpper()).FirstOrDefault();
                            if (GetData == null)
                            {
                                item.DOKUMEN_PENDUKUNG_DESC = item.DOKUMEN_PENDUKUNG;
                                item.DOKUMEN_PENDUKUNG = "Lain-Lain";
                            }
                            else
                            {
                                item.DOKUMEN_PENDUKUNG = GetData;
                            }
                        }

                        //Nomor Dokumen
                        if (!string.IsNullOrEmpty(dataRow[22]))
                        {
                            item.DOKUMEN_NOMOR = dataRow[22].ToUpper();
                        }

                        //Keterangan
                        if (!string.IsNullOrEmpty(dataRow[23]))
                        {
                            item.CATATAN = dataRow[23];
                        }

                        item.MessageError = item.MessageError.TrimEnd(' ');
                        item.MessageError = item.MessageError.TrimEnd(',');

                        model.Add(item);
                    }
                    catch (Exception exp)
                    {
                        LogError.LogError.WriteError(exp);
                        throw;
                    }
                }
            }
            return Json(model);
        }
        #endregion
    }
}
