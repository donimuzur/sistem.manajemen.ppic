using AutoMapper;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using sistem.manajemen.ppic.bll.IBLL;
using sistem.manajemen.ppic.core;
using sistem.manajemen.ppic.dto;
using sistem.manajemen.ppic.website.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace sistem.manajemen.ppic.website.Controllers
{
    public class TrnPengirimanController : BaseController
    {
        private ITrnDoBLL _trnDoBLL;
        private ITrnSpbBLL _trnSpbBLL;
        private ITrnPengirimanBLL _trnPengirimanBLL;
        private IMstBarangJadiBLL _mstBarangJadiBLL;
        private ITrnSuratPengantarBongkarMuatBLL _suratPengantarBongkarMuatBLL;
        private IMstKemasanBLL _mstKemasanBLL;

        public TrnPengirimanController(IPageBLL pageBll, ITrnDoBLL TrnDoBLL, ITrnSpbBLL TrnSpbBLL, IMstKemasanBLL MstKemasanBLL,
            ITrnPengirimanBLL TrnPengirimanBLL,IMstBarangJadiBLL MstBarangJadiBLL, ITrnSuratPengantarBongkarMuatBLL SuratPengantarBongkarMuatBLL) : base(pageBll, Enums.MenuList.TrnPengiriman)
        {
            _trnDoBLL = TrnDoBLL;
            _trnSpbBLL = TrnSpbBLL;
            _mstBarangJadiBLL = MstBarangJadiBLL;
            _trnPengirimanBLL = TrnPengirimanBLL;
            _suratPengantarBongkarMuatBLL = SuratPengantarBongkarMuatBLL;
            _mstKemasanBLL = MstKemasanBLL;
        }
        public TrnPengirimanModel Init(TrnPengirimanModel model)
        {
            model.CurrentUser = CurrentUser;

            model.MainMenu = Enums.MenuList.TrnPengiriman;
            model.Menu = Enums.GetEnumDescription(Enums.MenuList.Ekspedisi);
            model.Tittle = "Form Pengiriman";

            model.ChangesHistory = GetChangesHistory((int)Enums.MenuList.TrnPengiriman, model.ID);

            return model;
        }
        public ActionResult Index()
        {
            var model = new TrnPengirimanViewModel();

            model.ListData = Mapper.Map<List<TrnPengirimanModel>>(_trnPengirimanBLL.GetActiveAll());

            model.CurrentUser = CurrentUser;
            model.MainMenu = Enums.MenuList.TrnPengiriman;
            model.Menu = Enums.GetEnumDescription(Enums.MenuList.Ekspedisi);
            model.Tittle = "Form Pengiriman";

            return View(model);
        }

        #region --- Create ---
        public ActionResult Create()
        {
            var model = new TrnPengirimanModel();

            model.TANGGAL = DateTime.Now;

            model = Init(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(TrnPengirimanModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CREATED_BY = CurrentUser.USERNAME;
                    model.CREATED_DATE = DateTime.Now;
                    model.STATUS = (int)Enums.StatusDocument.Open;

                    model.NO_SPB = model.NO_SPB.ToUpper();
                    model.NO_SPB = model.NO_SPB.TrimEnd('\r', '\n', ' ');
                    model.NO_SPB = model.NO_SPB.TrimStart('\r', '\n', ' ');

                    var GetBarang = _mstBarangJadiBLL.GetByNama(model.NAMA_BARANG);
                    if (GetBarang != null)
                    {
                        model.ID_BARANG = GetBarang.ID;
                        //model.STOCK_AWAL = GetBarang.STOCK;
                        //model.STOCK_AKHIR = model.STOCK_AWAL - model.KUANTUM;
                        //if(model.STOCK_AKHIR < 0)
                        //{
                        //    AddMessageInfo("Stock barang tidak mencukupi", Enums.MessageInfoType.Error);
                        //    return View(Init(model));
                        //}
                    }

                    //if (model.KUANTUM > model.SISA_KIRIM )
                    //{
                    //    AddMessageInfo("Kuantum tidak boleh melebihi sisa Kirim", Enums.MessageInfoType.Error);
                    //    return View(Init(model));
                    //}

                    //if (model.SISA_KIRIM <= 0)
                    //{
                    //    AddMessageInfo("Jumlah barang yg dikirim sudah sesuai", Enums.MessageInfoType.Error);
                    //    return View(Init(model));
                    //}

                    var CheckDataSPBExist = _trnDoBLL.GetBySPB(model.NO_SPB).FirstOrDefault();
                    if (CheckDataSPBExist == null)
                    {
                        AddMessageInfo("No SPB tersebut tidak ada", Enums.MessageInfoType.Error);
                        return View(Init(model));
                    }

                    var CheckDataDOExist = _trnDoBLL.GetBySpbAndDo(model.NO_SPB,model.NO_DO.ToString());
                    if (CheckDataDOExist == null)
                    {
                        AddMessageInfo("No DO tersebut tidak ada", Enums.MessageInfoType.Error);
                        return View(Init(model));
                    }

                    var Dto = _trnPengirimanBLL.Save(Mapper.Map<TrnPengirimanDto>(model), Mapper.Map<LoginDto>(CurrentUser));
                    //_mstBarangJadiBLL.KurangSaldo(model.ID_BARANG, model.KUANTUM.Value);

                    AddMessageInfo("Sukses Create Form Pengiriman", Enums.MessageInfoType.Success);
                    return RedirectToAction("Details", "TrnPengiriman", new { id = Dto.ID});
                }
                catch (Exception exp)
                {
                    LogError.LogError.WriteError(exp);
                    AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
                    return RedirectToAction("Index", "TrnPengiriman");
                }
            }
            AddMessageInfo("Gagal Create Form Pengiriman", Enums.MessageInfoType.Error);
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
                var model = new TrnPengirimanModel();
                model =Mapper.Map<TrnPengirimanModel>(_trnPengirimanBLL.GetById(id));
                if (model == null)
                {
                    return HttpNotFound();
                }

                model = Init(model);

                var GetAkumulasi = _trnPengirimanBLL.GetAkumulasi(model.NO_DO.Value, model.NO_SPB);

                model.TOTAL_KIRIM = GetAkumulasi;
                model.SISA_KIRIM = model.PARTAI - model.TOTAL_KIRIM;

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

        #region --- Hapus ---
        public ActionResult Hapus(int? id, string Remarks)
        {
            if (!id.HasValue)
            {
                return HttpNotFound();
            }

            try
            {
                var model = _trnPengirimanBLL.GetById(id);
                if (model == null)
                {
                    return HttpNotFound();
                }

                _trnPengirimanBLL.Delete(id.Value, Remarks);
                //_mstBarangJadiBLL.TambahSaldo(model.ID_BARANG, model.KUANTUM.Value);

                AddMessageInfo("Data sukses dihapus", Enums.MessageInfoType.Success);
                return RedirectToAction("Index", "TrnPengiriman");
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
        [HttpPost]
        public JsonResult GetDataSpb(string NoSpb)
        {
            var data = _trnSpbBLL.GetBySPB(NoSpb);
            if (data == null)
            {
                data = new TrnSpbDto();
            }
            return Json(data);
        }
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
            var model = _trnDoBLL
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
        [HttpPost]
        public JsonResult GetDo(string No_Spb, string No_Do)
        {
            var data = new TrnPengirimanDto();
            if(!string.IsNullOrEmpty(No_Spb) && !string.IsNullOrEmpty(No_Do))
            {
                var GetDo = _trnDoBLL.GetBySpbAndDo(No_Spb, int.Parse(No_Do).ToString());
                
                var GetAkumulasi = _trnPengirimanBLL.GetAkumulasi(int.Parse(No_Do), No_Spb);

                if(GetDo != null)
                {
                    data.ALAMAT_KONSUMEN = GetDo.ALAMAT_KONSUMEN;
                    data.NAMA_KONSUMEN = GetDo.NAMA_KONSUMEN;
                    data.NAMA_BARANG = GetDo.NAMA_BARANG;
                    data.KEMASAN = GetDo.KEMASAN;
                    data.PARTAI = GetDo.JUMLAH;
                    data.TOTAL_KIRIM = GetAkumulasi;
                    data.SISA_KIRIM = data.PARTAI - data.TOTAL_KIRIM;
                }
            }
            return Json(data);
        }
        [HttpPost]
        public JsonResult HitungKuantum(string Kemasan, string Zak)
        {
            try
            {
                decimal HitungKuantum = 0;
                var data = new MstKemasanDto();
                if (!string.IsNullOrEmpty(Kemasan) && !string.IsNullOrEmpty(Zak))
                {

                    data = _mstKemasanBLL.GetByNama(Kemasan);
                    HitungKuantum = data.CONVERTION.Value * Convert.ToInt32(Zak);
                    
                    return Json(HitungKuantum);
                }

                    return Json("Error");
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Get Data Transportir
        public JsonResult GetNopolList()
        {
            var model = _suratPengantarBongkarMuatBLL
             .GetAll()
             .Select(x
                 => new
                 {
                     DATA = x.TRNSPT_NO_POLISI
                 })
             .Distinct()
             .OrderBy(X => X.DATA)
             .ToList();

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetDataTransportir(string No_Polisi)
        {
            try
            {
                var data = new TrnSuratPengantarBongkarMuatDto();
                if (!string.IsNullOrEmpty(No_Polisi))
                {

                    data = _suratPengantarBongkarMuatBLL.GetBy_Nopol(No_Polisi);
                    return Json(data);
                }
                //if (!string.IsNullOrEmpty(No_Polisi) && !string.IsNullOrEmpty(SPB) && !string.IsNullOrEmpty(DO))
                //{

                //    data =_suratPengantarBongkarMuatBLL.GetBy_SPB_DO_Nopol(SPB,Convert.ToInt32(DO),No_Polisi);
                //    return Json(data);
                //}
                return Json("Error");
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public JsonResult PrintPDF(int id)
        {
            try
            {
                var connection = System.Configuration.ConfigurationManager.ConnectionStrings["PPICEntities"].ConnectionString;
                var connectString = ConfigurationManager.ConnectionStrings["PPICEntities"].ConnectionString;
                var entityStringBuilder = new EntityConnectionStringBuilder(connectString);
                SqlConnectionStringBuilder SqlConnection = new SqlConnectionStringBuilder(entityStringBuilder.ProviderConnectionString);

                ReportDocument cryRpt = new ReportDocument();
                var WebrootUrl = ConfigurationManager.AppSettings["Webrooturl"];
                var FilesUploadPath = ConfigurationManager.AppSettings["FilesReport"];
                var fileName = System.IO.Path.GetFileName("RptSuratJalan_" + id.ToString().PadLeft(4, '0') + ".pdf");
                var fullPath = System.IO.Path.Combine(@FilesUploadPath+ "\\Surat Jalan\\", fileName);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
                var SystemPath = HostingEnvironment.ApplicationPhysicalPath;

                cryRpt.Load(SystemPath + "\\Reports\\ReportSuratJalan3-Layout.rpt");
                cryRpt.SetDatabaseLogon(SqlConnection.UserID, SqlConnection.Password, SqlConnection.DataSource, SqlConnection.InitialCatalog);
                cryRpt.SetParameterValue("id", id);
                
                cryRpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, fullPath);
                
                return Json(WebrootUrl + "\\files_upload\\Reports\\Surat Jalan\\" + fileName);
            }
            catch (Exception exp)
            {
                LogError.LogError.WriteError(exp);
                return Json("Error");
            }
        }
        [HttpPost]
        public JsonResult PrintToPrinter(int id)
        {
            try
            {
                var connection = System.Configuration.ConfigurationManager.ConnectionStrings["PPICEntities"].ConnectionString;
                var connectString = ConfigurationManager.ConnectionStrings["PPICEntities"].ConnectionString;
                var entityStringBuilder = new EntityConnectionStringBuilder(connectString);
                var PrinterName = ConfigurationManager.AppSettings["PrinterName"];
                SqlConnectionStringBuilder SqlConnection = new SqlConnectionStringBuilder(entityStringBuilder.ProviderConnectionString);

                ReportDocument cryRpt = new ReportDocument();
                var SystemPath = HostingEnvironment.ApplicationPhysicalPath;

                cryRpt.Load(SystemPath + "\\Reports\\ReportSuratJalan3.rpt");
                cryRpt.SetDatabaseLogon(SqlConnection.UserID, SqlConnection.Password, SqlConnection.DataSource, SqlConnection.InitialCatalog);
                cryRpt.SetParameterValue("id", id);

                //cryRpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.WordForWindows, fullPath);
                
                System.Drawing.Printing.PrinterSettings printersettings = new System.Drawing.Printing.PrinterSettings();
                System.Drawing.Printing.PageSettings pageSettings = new System.Drawing.Printing.PageSettings();
                var paper = new System.Drawing.Printing.PaperSize("Custom", 850,650);
                
                printersettings.DefaultPageSettings.Landscape = true;
                printersettings.DefaultPageSettings.PaperSize = paper;

                pageSettings.PaperSize = paper;
                pageSettings.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
                pageSettings.Landscape = true;

                printersettings.PrinterName = PrinterName;

                cryRpt.PrintToPrinter(printersettings, pageSettings, false);
                return Json(true);
            }
            catch (Exception exp)
            {
                LogError.LogError.WriteError(exp);
                return Json("Error");
            }
        }
        #endregion

    }
}