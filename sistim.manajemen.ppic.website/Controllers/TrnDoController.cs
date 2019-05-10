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
    public class TrnDoController : BaseController
    {
        private ITrnDoBLL _trnDoBLL;
        private ITrnSpbBLL _trnSpbBLL;
        public TrnDoController(IPageBLL pageBll, ITrnDoBLL TrnDoBLL, ITrnSpbBLL TrnSpbBLL) : base(pageBll, Enums.MenuList.TrnDo)
        {
            _trnDoBLL = TrnDoBLL;
            _trnSpbBLL = TrnSpbBLL;
        }
        public TrnDoModel Init(TrnDoModel model)
        {
            model.CurrentUser = CurrentUser;
            model.MainMenu = Enums.MenuList.TrnDo;
            model.Menu = "Delivery Order (DO)";
            model.ChangesHistory = GetChangesHistory((int)Enums.MenuList.TrnDo, model.ID);

            return model;
        }
        public ActionResult Index()
        {
            var model = new TrnDoViewModel();

            model.ListData = Mapper.Map<List<TrnDoModel>>(_trnDoBLL.GetAll());

            model.CurrentUser = CurrentUser;
            model.MainMenu = Enums.MenuList.TrnDo;
            model.Menu = "Delivery Order (DO)";

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
                    var CheckDataExist = _trnSpbBLL.GetBySPB(model.NO_SPB);
                    if(CheckDataExist == null)
                    {
                        AddMessageInfo("No SPB tersebut tidak ada", Enums.MessageInfoType.Error);
                        model = Init(model);
                        return View(model);
                    }
                    var GetDataExist = _trnDoBLL.GetBySPB(model.NO_SPB);
                    if (GetDataExist != null)
                    {
                        AddMessageInfo("Gagal Create Do, no SPB sudah terdaftar", Enums.MessageInfoType.Error);
                        model = Init(model);
                        return View(model);
                    }

                    model.CREATED_BY = CurrentUser.USERNAME;
                    model.CREATED_DATE = DateTime.Now;
                    model.TANGGAL = DateTime.Now;

                    _trnDoBLL.Save(Mapper.Map<TrnDoDto>(model), Mapper.Map<LoginDto>(CurrentUser));
                    _trnSpbBLL.CloseSpb(model.NO_SPB);

                    AddMessageInfo("Sukses Create Do",Enums.MessageInfoType.Success);
                    return RedirectToAction("Index", "TrnDo");
                }
                catch (Exception exp)
                {
                    LogError.LogError.WriteError(exp);
                    AddMessageInfo("Telah terjadi kesalahan", Enums.MessageInfoType.Error);
                    return RedirectToAction("Index", "TrnDo");
                }
            }
            model = Init(model);
            return View(model);
        }
        #endregion

        #region --- Edit ---k
        public ActionResult Edit(int Id)
        {
            var model = new TrnDoModel();

            model = Mapper.Map<TrnDoModel>(_trnDoBLL.GetById(Id));

            model = Init(model);
            return View(model);
        }
        
        [HttpPost]
        public ActionResult Edit(TrnDoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var CheckDataExist = _trnSpbBLL.GetBySPB(model.NO_SPB);

                    if (CheckDataExist == null)
                    {
                        AddMessageInfo("No SPB tersebut tidak ada", Enums.MessageInfoType.Error);
                        model = Init(model);
                        return View(model);
                    }

                    var GetDataExist = _trnDoBLL.GetBySPB(model.NO_SPB);
                    if (GetDataExist != null && GetDataExist.ID != model.ID)
                    {
                        AddMessageInfo("Gagal Create Do, No SPB sudah terdaftar", Enums.MessageInfoType.Error);
                        model = Init(model);
                        return View(model);
                    }

                    model.MODIFIED_BY = CurrentUser.USERNAME;
                    model.MODIFIED_DATE = DateTime.Now;
                    model.TANGGAL = DateTime.Now;
                    
                    _trnDoBLL.Save(Mapper.Map<TrnDoDto>(model), Mapper.Map<LoginDto>(CurrentUser));
                    _trnSpbBLL.CloseSpb(model.NO_SPB);

                    AddMessageInfo("Sukses Update DO", Enums.MessageInfoType.Success);
                    return RedirectToAction("Index", "TrnDo");
                }
                catch (Exception exp)
                {
                    LogError.LogError.WriteError(exp);
                    AddMessageInfo("Telah terjadi kesalahan", Enums.MessageInfoType.Error);
                    return RedirectToAction("Index", "TrnDo");
                }
            }
            model = Init(model);
            return View(model);
        }
        #endregion

        #region --- Details ---
        public ActionResult Details(int Id)
        {
            var model = new TrnDoModel();

            model = Mapper.Map<TrnDoModel>(_trnDoBLL.GetById(Id));

            model = Init(model);
            return View(model);
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
        #endregion
    }
}