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
    public class TrnPengirimanController : BaseController
    {
        private ITrnPengirimanBLL _trnPengirimanBLL;
        private ITrnSpbBLL _trnSpbBLL;
        private ITrnDoBLL _trnDoBLL;
        public TrnPengirimanController(IPageBLL pageBll, ITrnPengirimanBLL TrnPengirimanBLL, ITrnSpbBLL TrnSpbBLL, ITrnDoBLL TrnDoBLL) : base(pageBll, Enums.MenuList.TrnPengiriman)
        {
            _trnPengirimanBLL = TrnPengirimanBLL;
            _trnDoBLL = TrnDoBLL;
            _trnSpbBLL = TrnSpbBLL;
        }
        // GET: TrnPengiriman
        public ActionResult Index()
        {
            var model = new TrnPengirimanViewModel();
            
            model.CurrentUser = CurrentUser;
            model.Menu = "Pengiriman";
            model.MainMenu = Enums.MenuList.TrnPengiriman;

            model.ListData = Mapper.Map<List<TrnPengirimanModel>>(_trnPengirimanBLL.GetAll());
            
            return View(model);
        }
        public TrnPengirimanModel Init(TrnPengirimanModel model)
        {
            model.CurrentUser = CurrentUser;
            model.Menu = "Pengiriman";
            model.MainMenu = Enums.MenuList.TrnPengiriman;
            model.ChangesHistory = GetChangesHistory((int)Enums.MenuList.TrnPengiriman, model.ID);

            return model;
        }

        #region --- Create ---
        public ActionResult Create()
        {
            var model =new TrnPengirimanModel();
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
                    var CheckDataExist = _trnSpbBLL.GetBySPB(model.NO_SPB);
                    if (CheckDataExist == null)
                    {
                        AddMessageInfo("No SPB tersebut tidak ada", Enums.MessageInfoType.Error);
                        model = Init(model);
                        return View(model);
                    }
                    
                    var GetDataExist = _trnPengirimanBLL.GetBySPB(model.NO_SPB);
                    if (GetDataExist != null)
                    {
                        AddMessageInfo("Gagal Create Pengiriman no SPB sudah terdaftar", Enums.MessageInfoType.Error);
                        model = Init(model);
                        return View(model);
                    }

                    model.CREATED_BY = CurrentUser.USERNAME;
                    model.CREATED_DATE = DateTime.Now;
                    model.TANGGAL = DateTime.Now;
                    model.MODIFIED_BY = CurrentUser.USERNAME;

                    _trnPengirimanBLL.Save(Mapper.Map<TrnPengirimanDto>(model), Mapper.Map<LoginDto>(CurrentUser));
                    
                    AddMessageInfo("Sukses Create Pengiriman", Enums.MessageInfoType.Success);
                    return RedirectToAction("Index", "TrnPengiriman");
                }
                catch (Exception exp)
                {
                    LogError.LogError.WriteError(exp);
                    AddMessageInfo("Telah terjadi kesalahan", Enums.MessageInfoType.Error);
                    return RedirectToAction("Index", "TrnPengiriman");
                }
            }
            AddMessageInfo("Telah terjadi kesalahan", Enums.MessageInfoType.Error);
            model = Init(model);
            return View(model);
        }
        #endregion

        #region --- Edit ---
        public ActionResult Edit(int Id)
        {
            var model = new TrnPengirimanModel();

            model = Mapper.Map<TrnPengirimanModel>(_trnDoBLL.GetById(Id));

            model = Init(model);

            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(TrnPengirimanModel model)
        {
            return View();
        }
        #endregion

        #region --- Details ---
        public ActionResult Details(int Id)
        {
            var model = new TrnPengirimanModel();

            model = Mapper.Map<TrnPengirimanModel>(_trnDoBLL.GetById(Id));

            model = Init(model);

            return View(model);
        }
        #endregion

        #region --- Json ---
        public  JsonResult GetListSpb()
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
        [HttpPost]
        public JsonResult GetSpb(string NoSpb)
        {
            var data = _trnDoBLL.GetBySPB(NoSpb);
            if (data == null)
            {
                data = new TrnDoDto();
            }
            return Json(data);
        }
        #endregion
    }
}