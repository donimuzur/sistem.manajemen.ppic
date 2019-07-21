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
using CrystalDecisions.CrystalReports.Engine;
using System.Configuration;
using System.Web.Hosting;

namespace sistem.manajemen.ppic.website.Controllers
{
    public class TrnDoController : BaseController
    {
        private ITrnDoBLL _trnDoBLL;
        private ITrnSpbBLL _trnSpbBLL;
        private IMstBarangJadiBLL _mstBarangJadiBLL;
        public TrnDoController(IPageBLL pageBll, ITrnDoBLL TrnDoBLL, ITrnSpbBLL TrnSpbBLL, IMstBarangJadiBLL MstBarangJadiBLL) : base(pageBll, Enums.MenuList.TrnDo)
        {
            _trnDoBLL = TrnDoBLL;
            _trnSpbBLL = TrnSpbBLL;
            _mstBarangJadiBLL = MstBarangJadiBLL;
        }
        public TrnDoModel Init(TrnDoModel model)
        {
            model.CurrentUser = CurrentUser;

            model.Menu = Enums.GetEnumDescription(Enums.MenuList.Transaction);
            model.Tittle = "Delivery Order (DO)";

            model.ChangesHistory = GetChangesHistory((int)Enums.MenuList.TrnDo, model.ID);

            return model;
        }
        public ActionResult Index()
        {
            var model = new TrnDoViewModel();

            model.ListData = Mapper.Map<List<TrnDoModel>>(_trnDoBLL.GetAll());

            model.CurrentUser = CurrentUser;
            model.MainMenu = Enums.MenuList.TrnDo;
            model.Menu = Enums.GetEnumDescription(Enums.MenuList.Transaction);
            model.Tittle = "Delivery Order (DO)";

            return View(model);
        }

        #region --- Create ---
        public ActionResult Create()
        {
            var model = new TrnDoModel();
            
            model = Init(model);
            return View(model);
        }
        [HttpPost]
        public ActionResult Create(TrnDoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.CREATED_BY = CurrentUser.USERNAME;
                    model.CREATED_DATE = DateTime.Now;
                    model.TANGGAL = DateTime.Now;
                    model.STATUS = Enums.StatusDocument.Open;

                    model.NO_SPB = model.NO_SPB.ToUpper();
                    model.NO_SPB = model.NO_SPB.TrimEnd('\r', '\n', ' ');
                    model.NO_SPB = model.NO_SPB.TrimStart('\r', '\n', ' ');

                    var CheckDataExist = _trnSpbBLL.GetBySPB(model.NO_SPB);
                    if (CheckDataExist == null)
                    {
                        AddMessageInfo("No SPB tersebut tidak ada", Enums.MessageInfoType.Error);
                        return View(Init(model));
                    }

                    var Dto = _trnDoBLL.Save(Mapper.Map<TrnDoDto>(model), Mapper.Map<LoginDto>(CurrentUser));

                    AddMessageInfo("Sukses Create DO",Enums.MessageInfoType.Success);
                    return RedirectToAction("Details", "TrnDo",new {id= Dto.ID });
                }
                catch (Exception exp)
                {
                    LogError.LogError.WriteError(exp);
                    AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
                    return RedirectToAction("Index", "TrnDo");
                }
            }
            AddMessageInfo("Gagal Create DO", Enums.MessageInfoType.Error);
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
                var model = new TrnDoModel();

                model = Mapper.Map<TrnDoModel>(_trnDoBLL.GetById(id));

                model = Init(model);
                return View(model);
            }
            catch (Exception exp)
            {
                LogError.LogError.WriteError(exp);
                AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
                return RedirectToAction("Index", "TrnDo");
            }
        }
        
        [HttpPost]
        public ActionResult Edit(TrnDoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.MODIFIED_BY = CurrentUser.USERNAME;
                    model.MODIFIED_DATE = DateTime.Now;

                    model.NO_SPB = model.NO_SPB.ToUpper();
                    model.NO_SPB = model.NO_SPB.TrimEnd('\r', '\n', ' ');
                    model.NO_SPB = model.NO_SPB.TrimStart('\r', '\n', ' ');
                    var CheckDataExist = _trnSpbBLL.GetBySPB(model.NO_SPB);

                    if (CheckDataExist == null)
                    {
                        AddMessageInfo("No SPB tersebut tidak ada", Enums.MessageInfoType.Error);
                        model = Init(model);
                        return View(model);
                    }

                    _trnDoBLL.Save(Mapper.Map<TrnDoDto>(model), Mapper.Map<LoginDto>(CurrentUser));

                    AddMessageInfo("Sukses Update DO", Enums.MessageInfoType.Success);
                    return RedirectToAction("Index", "TrnDo");
                }
                catch (Exception exp)
                {
                    LogError.LogError.WriteError(exp);
                    AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
                    return RedirectToAction("Index", "TrnDo");
                }
            }
            AddMessageInfo("Gagal Create DO", Enums.MessageInfoType.Error);
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
                var model = new TrnDoModel();

                model = Mapper.Map<TrnDoModel>(_trnDoBLL.GetById(id));

                model = Init(model);
                return View(model);
            }
            catch (Exception exp)
            {
                LogError.LogError.WriteError(exp);
                AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
                return RedirectToAction("Index", "TrnDo");
            }
        }
        #endregion

        #region --- Pdf ---

        #endregion

        #region --- Json ---
        [HttpPost]
        public JsonResult GetDataSpb(string NoSpb)
        {
            var data = _trnSpbBLL.GetBySPB(NoSpb);
            if(data== null)
            {
                data = new TrnSpbDto();
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

        [HttpPost]
        public JsonResult PrintPDF(int id)
        {
            try
            {
                ReportDocument cryRpt = new ReportDocument();
                var WebrootUrl = ConfigurationManager.AppSettings["Webrooturl"];
                var FilesUploadPath = ConfigurationManager.AppSettings["FilesReport"];
                var fileName = System.IO.Path.GetFileName("RptDO_" + id.ToString().PadLeft(4, '0') + ".pdf");
                var fullPath = System.IO.Path.Combine(@FilesUploadPath + "\\Do\\", fileName);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
                var SystemPath = HostingEnvironment.ApplicationPhysicalPath;

                cryRpt.Load(SystemPath + "\\Reports\\ReportDo.rpt");
                cryRpt.SetParameterValue("id", id);
                cryRpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, fullPath);
                
                return Json(WebrootUrl+"\\files_upload\\Reports\\Do\\" +fileName);
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