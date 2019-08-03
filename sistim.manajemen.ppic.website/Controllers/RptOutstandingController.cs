using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sistem.manajemen.ppic.bll.IBLL;
using sistem.manajemen.ppic.core;
using sistem.manajemen.ppic.website.Models;
using sistem.manajemen.ppic.dto.Input;
using AutoMapper;
using System.Configuration;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.Entity.Core.EntityClient;
using System.Web.Hosting;

namespace sistem.manajemen.ppic.website.Controllers
{
    public class RptOutstandingController : BaseController
    {
        private IRptOutstandingBLL _rptOutstandingBLL;
        public RptOutstandingController(IPageBLL pageBll, IRptOutstandingBLL RptOutstandingBLL) : base(pageBll, Enums.MenuList.LaporanOutstanding)
        {
            _rptOutstandingBLL = RptOutstandingBLL;
        }

        // GET: RptOutstanding
        public ActionResult Index()
        {
            var model = new RptOutstandingViewModel();

            model.ListData = new List<RptOutstandingModel>();

            model.MainMenu = Enums.MenuList.LaporanOutstanding;
            model.Menu = Enums.GetEnumDescription(Enums.MenuList.Report);
            model.CurrentUser = CurrentUser;
            model.Tittle = "Laporan Outstanding";

            return View(model);
        }

        #region --- Json ---
        [HttpPost]
        public JsonResult GetDataList(string Tanggal)
        {
            try
            {
                var data = _rptOutstandingBLL.GetRpt();
                return Json(data);
            }
            catch (Exception exp)
            {
                LogError.LogError.WriteError(exp);
                return Json("Error");
            }
        }
        [HttpPost]
        public JsonResult PrintPDF(string Tanggal)
        {
            try
            {
                var Date = Convert.ToDateTime(Tanggal);
                var connection = System.Configuration.ConfigurationManager.ConnectionStrings["PPICEntities"].ConnectionString;
                var connectString = ConfigurationManager.ConnectionStrings["PPICEntities"].ConnectionString;
                var entityStringBuilder = new EntityConnectionStringBuilder(connectString);
                SqlConnectionStringBuilder SqlConnection = new SqlConnectionStringBuilder(entityStringBuilder.ProviderConnectionString);

                ReportDocument cryRpt = new ReportDocument();
                var WebrootUrl = ConfigurationManager.AppSettings["Webrooturl"];
                var FilesUploadPath = ConfigurationManager.AppSettings["FilesReport"];
                var fileName = System.IO.Path.GetFileName("RptOutstanding_" + DateTime.Now.ToString("yyyyMMdd") + ".pdf");
                var fullPath = System.IO.Path.Combine(@FilesUploadPath + "\\Laporan\\", fileName);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
                var SystemPath = HostingEnvironment.ApplicationPhysicalPath;

                cryRpt.Load(SystemPath + "\\Reports\\ReportOutstanding.rpt");
                cryRpt.SetDatabaseLogon(SqlConnection.UserID, SqlConnection.Password, SqlConnection.DataSource, SqlConnection.InitialCatalog);
                cryRpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, fullPath);

                return Json(WebrootUrl + "\\files_upload\\Reports\\Laporan\\" + fileName);
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