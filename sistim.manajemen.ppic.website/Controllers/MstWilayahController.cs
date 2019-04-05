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
    public class MstWilayahController : BaseController
    {
        private IMstWilayahBLL _mstWilayahBLL;
        public MstWilayahController(IMstWilayahBLL MstWilayahBLL, IPageBLL pageBll) : base(pageBll, Enums.MenuList.MasterWilayah)
        {
            _mstWilayahBLL = MstWilayahBLL; 
        }
        public MstWilayahModel Init(MstWilayahModel model)
        {
            model.CurrentUser = CurrentUser;
            model.Menu = "Wilayah";
            model.ChangesHistory = GetChangesHistory((int)Enums.MenuList.MasterWilayah, model.ID);

            return model;
        }
        public ActionResult Index()
        {
            var model = new MstWilayahViewModel();
            var data = _mstWilayahBLL.GetAll();

            model.ListData = Mapper.Map<List<MstWilayahModel>>(data);

            model.CurrentUser = CurrentUser;
            model.Menu = "Wilayah";

            return View(model);
        }
        #region --- Create ---
        public ActionResult Create()
        {
            var model = new MstWilayahModel();

            model = Init(model);
            model.STATUS = true;

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MstWilayahModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Dto = Mapper.Map<MstWilayahDto>(model);

                    Dto.CREATED_BY = CurrentUser.USERNAME;
                    Dto.CREATED_DATE = DateTime.Now;
                    Dto.STATUS = true;

                    _mstWilayahBLL.Save(Dto,Mapper.Map<LoginDto>(CurrentUser));
                    return RedirectToAction("Index", "MstWilayah");
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                model = Init(model);
                return View(model);
            }
        }
        #endregion

        #region --- Edit ---
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return HttpNotFound();
            }

            var model = new MstWilayahModel();

            var GetData = _mstWilayahBLL.GetById(id);
            if (GetData == null)
            {
                return HttpNotFound();
            }

            model = Mapper.Map<MstWilayahModel>(GetData);
            model = Init(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MstWilayahModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {                                                      
                    var Dto = Mapper.Map<MstWilayahDto>(model);

                    Dto.MODIFIED_BY = CurrentUser.USERNAME;
                    Dto.MODIFIED_DATE = DateTime.Now;
                    
                    _mstWilayahBLL.Save(Dto, Mapper.Map<LoginDto>(CurrentUser));
                    return RedirectToAction("Index", "MstWilayah");
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                model = Init(model);
                return View(model);
            }
        }
        #endregion

        #region --- Json ---

        #endregion
    }
}