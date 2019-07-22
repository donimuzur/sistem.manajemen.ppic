using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sistem.manajemen.ppic.bll.IBLL;
using sistem.manajemen.ppic.core;
using sistem.manajemen.ppic.website.Models;
using AutoMapper;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using System.Web.Hosting;

namespace sistem.manajemen.ppic.website.Controllers
{
    public class RptHarianController : BaseController
    {
        private IRptHarianEkspedisiBLL _rptHarianEkspedisi;
        public RptHarianController(IPageBLL pageBll, IRptHarianEkspedisiBLL RptHarianEkspedisi) : base(pageBll, Enums.MenuList.LaporanRealisasiHarian)
        {
            _rptHarianEkspedisi = RptHarianEkspedisi;
        }

        // GET: RptHarian
        public ActionResult Index()
        {
            var model = new RptEkspedisiHarianViewModel();

            model.ListData = new List<RptEkspedisiHarianModel>();

            model.MainMenu = Enums.MenuList.LaporanRealisasiHarian;
            model.Menu = Enums.GetEnumDescription(Enums.MenuList.Report);
            model.CurrentUser = CurrentUser;
            model.Tittle = "Laporan harian";

            return View(model);
        }

        #region --- Json ---
        [HttpPost]
        public JsonResult GetRptEkspedisiList(string Tanggal)
        {
            try
            {
                var data = _rptHarianEkspedisi.GetRpt(Tanggal);
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
                var fileName = System.IO.Path.GetFileName("RptRealisasiHarian_" + Tanggal + ".pdf");
                var fullPath = System.IO.Path.Combine(@FilesUploadPath + "\\Laporan\\", fileName);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
                var SystemPath = HostingEnvironment.ApplicationPhysicalPath;

                cryRpt.Load(SystemPath + "\\Reports\\ReportRealisasiHarian.rpt");
                cryRpt.SetDatabaseLogon(SqlConnection.UserID, SqlConnection.Password, SqlConnection.DataSource, SqlConnection.InitialCatalog);
                cryRpt.SetParameterValue("@Date", Date.ToString("yyyy-MM-dd"));
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