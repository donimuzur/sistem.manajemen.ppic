using AutoMapper;
using CrystalDecisions.CrystalReports.Engine;
using sistem.manajemen.ppic.bll;
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
    public class TrnSlipTimbanganController : BaseController
    {
        private ITrnSlipTimbanganBLL _trnSlipTimbanganBLL;
        private IMstKonsumenBLL _mstKonsumenBLL;
        public TrnSlipTimbanganController(IPageBLL pageBll, ITrnSlipTimbanganBLL TrnSlipTimbanganBLL, IMstKonsumenBLL MstKonsumenBLL) : base(pageBll, Enums.MenuList.TrnSlipTimbangan)
        {
            _trnSlipTimbanganBLL = TrnSlipTimbanganBLL;
            _mstKonsumenBLL = MstKonsumenBLL;
        }
        public TrnSlipTimbanganModel Init(TrnSlipTimbanganModel model)
        {
            model.MainMenu = Enums.MenuList.TrnSlipTimbangan;
            model.Menu = Enums.GetEnumDescription(Enums.MenuList.Ekspedisi);
            model.CurrentUser = CurrentUser;
            model.Tittle = "Slip Timbangan";

            model.ChangesHistory = GetChangesHistory((int)Enums.MenuList.TrnSlipTimbangan, model.ID);

            return model;
        }
        //GET: TrnSlipTimbangan
        public ActionResult Index()
        {
            try
            {
                var model = new TrnSlipTimbanganViewModel();
                var data = _trnSlipTimbanganBLL.GetAll();

                model.ListData = Mapper.Map<List<TrnSlipTimbanganModel>>(data);

                model.MainMenu = Enums.MenuList.TrnSlipTimbangan;
                model.Menu = Enums.GetEnumDescription(Enums.MenuList.Ekspedisi);
                model.CurrentUser = CurrentUser;
                model.Tittle = "Slip Timbangan";

                return View(model);
            }
            catch (Exception exp)
            {
                LogError.LogError.WriteError(exp);
                AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
                return RedirectToAction("Index", "TrnSlipTimbangan");
            }
        }

        #region --- Create ---
        public ActionResult Create()
        {
            var model = new TrnSlipTimbanganModel();

            model.TANGGAL = DateTime.Now;
            model.JAM_MASUK = DateTime.Now;

            model = Init(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TrnSlipTimbanganModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Dto = Mapper.Map<TrnSlipTimbanganDto>(model);
                    
                    Dto.CREATED_BY = CurrentUser.USERNAME;
                    Dto.CREATED_DATE = DateTime.Now;
                    
                    Dto = _trnSlipTimbanganBLL.Save(Dto, Mapper.Map<LoginDto>(CurrentUser));

                    MstKonsumenDto KonsumenDto = new MstKonsumenDto { ALAMAT_KONSUMEN = " ",  NAMA_KONSUMEN = model.PERUSAHAAN, STATUS = 1 };
                    _mstKonsumenBLL.InsertUpdateMstKonsumen(KonsumenDto, Mapper.Map<LoginDto>(CurrentUser));

                    KonsumenDto = new MstKonsumenDto { ALAMAT_KONSUMEN = " ", NAMA_KONSUMEN = model.SUPPLIER, STATUS = 1 };
                    _mstKonsumenBLL.InsertUpdateMstKonsumen(KonsumenDto, Mapper.Map<LoginDto>(CurrentUser));

                    AddMessageInfo("Sukses Buat Slip Timbangan", Enums.MessageInfoType.Success);
                    return RedirectToAction("Details", "TrnSlipTimbangan", new { id = Dto.ID });
                }
                catch (Exception exp)
                {
                    LogError.LogError.WriteError(exp);
                    AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
                    return RedirectToAction("Index", "TrnSlipTimbangan");
                }
            }
            else
            {
                AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
                model = Init(model);
                return View(model);
            }
        }
        #endregion

        #region --- Edit ---
        public ActionResult Edit(int? id)
        {
            try
            {
                if (!id.HasValue)
                {
                    return HttpNotFound();
                }

                var model = new TrnSlipTimbanganModel();

                var GetData = _trnSlipTimbanganBLL.GetById(id);
                if (GetData == null)
                {
                    return HttpNotFound();
                }

                model = Mapper.Map<TrnSlipTimbanganModel>(GetData);

                model = Init(model);
                return View(model);
            }
            catch (Exception exp)
            {
                LogError.LogError.WriteError(exp);
                AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
                return RedirectToAction("Index", "TrnSlipTimbangan");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TrnSlipTimbanganModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.MODIFIED_BY = CurrentUser.USER_ID;
                    model.MODIFIED_DATE = DateTime.Now;
                    var Dto = Mapper.Map<TrnSlipTimbanganDto>(model);
                    
                    Dto = _trnSlipTimbanganBLL.Save(Dto, Mapper.Map<LoginDto>(CurrentUser));

                    AddMessageInfo("Success Update", Enums.MessageInfoType.Success);
                    return RedirectToAction("Details", "TrnSlipTimbangan", new { id = model.ID });
                }
                catch (Exception exp)
                {
                    LogError.LogError.WriteError(exp);
                    AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
                    return RedirectToAction("Index", "TrnSlipTimbangan");
                }
            }
            AddMessageInfo("Gagal Update", Enums.MessageInfoType.Error);
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
                var model = new TrnSlipTimbanganModel();
                model = Mapper.Map<TrnSlipTimbanganModel>(_trnSlipTimbanganBLL.GetById(id));
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
                return RedirectToAction("Index", "TrnSlipTimbangan");
            }
        }
        #endregion
        
        #region --- Json ---
        public JsonResult GetKonsumenList()
        {
            var model = _mstKonsumenBLL
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
        #endregion

        #region --- PRINT ---
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
                var fileName = System.IO.Path.GetFileName("RptSlipTimbangan" + id.ToString().PadLeft(4, '0') + ".pdf");
                var fullPath = System.IO.Path.Combine(@FilesUploadPath + "\\Slip Timbangan\\", fileName);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
                var SystemPath = HostingEnvironment.ApplicationPhysicalPath;

                cryRpt.Load(SystemPath + "\\Reports\\ReportSlipTimbangan.rpt");
                cryRpt.SetDatabaseLogon(SqlConnection.UserID, SqlConnection.Password, SqlConnection.DataSource, SqlConnection.InitialCatalog);
                cryRpt.SetParameterValue("id", id);
                cryRpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, fullPath);

                return Json(WebrootUrl + "\\files_upload\\Reports\\Slip Timbangan\\" + fileName);
            }
            catch (Exception exp)
            {
                LogError.LogError.WriteError(exp);
                return Json("Error");
            }
        }
        //[HttpPost]
        //public JsonResult PrintPDF(int id)
        //{
        //    try
        //    {
        //        var connection = System.Configuration.ConfigurationManager.ConnectionStrings["PPICEntities"].ConnectionString;
        //        var connectString = ConfigurationManager.ConnectionStrings["PPICEntities"].ConnectionString;
        //        var entityStringBuilder = new EntityConnectionStringBuilder(connectString);
        //        SqlConnectionStringBuilder SqlConnection = new SqlConnectionStringBuilder(entityStringBuilder.ProviderConnectionString);

        //        ReportDocument cryRpt = new ReportDocument();
        //        var WebrootUrl = ConfigurationManager.AppSettings["Webrooturl"];
        //        var FilesUploadPath = ConfigurationManager.AppSettings["FilesReport"];
        //        var fileName = System.IO.Path.GetFileName("RptSlipTimbangan" + id.ToString().PadLeft(4, '0') + ".pdf");
        //        var fullPath = System.IO.Path.Combine(@FilesUploadPath + "\\Slip Timbangan\\", fileName);
        //        if (System.IO.File.Exists(fullPath))
        //        {
        //            System.IO.File.Delete(fullPath);
        //        }
        //        var SystemPath = HostingEnvironment.ApplicationPhysicalPath;
                
        //        cryRpt.Load(SystemPath + "\\Reports\\ReportSuratJalan.rpt");
        //        cryRpt.SetDatabaseLogon(SqlConnection.UserID, SqlConnection.Password, SqlConnection.DataSource, SqlConnection.InitialCatalog);
        //        cryRpt.SetParameterValue("id", 1013);

        //        cryRpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, fullPath);

        //        return Json(WebrootUrl + "\\files_upload\\Reports\\Slip Timbangan\\" + fileName);
        //    }
        //    catch (Exception exp)
        //    {
        //        LogError.LogError.WriteError(exp);
        //        return Json("Error");
        //    }
        //}
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
                
                cryRpt.Load(SystemPath + "\\Reports\\ReportSlipTimbangan.rpt");
                cryRpt.SetDatabaseLogon(SqlConnection.UserID, SqlConnection.Password, SqlConnection.DataSource, SqlConnection.InitialCatalog);
                cryRpt.SetParameterValue("id", id);

                //cryRpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.WordForWindows, fullPath);

                System.Drawing.Printing.PrinterSettings printersettings = new System.Drawing.Printing.PrinterSettings();
                System.Drawing.Printing.PageSettings pageSettings = new System.Drawing.Printing.PageSettings();
                var paper = new System.Drawing.Printing.PaperSize("Custom", 300, 600);

                printersettings.DefaultPageSettings.Landscape = false;
                printersettings.DefaultPageSettings.PaperSize = paper;

                pageSettings.PaperSize = paper;
                pageSettings.Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
                pageSettings.Landscape = false;

                var nama = System.Drawing.Printing.PrinterSettings.InstalledPrinters;

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