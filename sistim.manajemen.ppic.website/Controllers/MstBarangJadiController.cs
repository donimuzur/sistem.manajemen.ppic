using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sistem.manajemen.ppic.core;
using sistem.manajemen.ppic.website.Models;
using sistem.manajemen.ppic.bll.IBLL;
using sistem.manajemen.ppic.dal;
using AutoMapper;
using sistem.manajemen.ppic.dto;

namespace sistem.manajemen.ppic.website.Controllers
{
    public class MstBarangJadiController : BaseController
    {
        private IMstBarangJadiBLL _mstBarangJadiBLL;
        public MstBarangJadiController(IMstBarangJadiBLL MstBarangJadiBLL, IPageBLL pageBll) : base(pageBll, Enums.MenuList.GdgBarangJadi)
        {
            _mstBarangJadiBLL = MstBarangJadiBLL;
        }
        public MstBarangJadiModel Init(MstBarangJadiModel model)
        {
            model.CurrentUser = CurrentUser;
            model.Menu = "Barang Jadi";
            model.ChangesHistory = GetChangesHistory((int)Enums.MenuList.GdgBarangJadi, model.ID);

            return model;
        }
        public ActionResult Index()
        {
            var model = new MstBarangJadiViewModel();
            var data = _mstBarangJadiBLL.GetAll();

            model.ListData = Mapper.Map <List<MstBarangJadiModel>>(data);

            model.CurrentUser = CurrentUser;
            model.Menu = "Barang Jadi";

            return View(model);
        }
        #region --- Create ---
        public ActionResult Create()
        {
            var model = new MstBarangJadiModel();

            model = Init(model);
            model.STATUS = true;

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MstBarangJadiModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Dto = Mapper.Map<MstBarangJadiDto>(model);

                    Dto.CREATED_BY = CurrentUser.USERNAME;
                    Dto.CREATED_DATE = DateTime.Now;
                    Dto.STATUS = true;
                    
                    _mstBarangJadiBLL.Save(Dto, Mapper.Map<LoginDto>(CurrentUser));
                    return RedirectToAction("Index", "MstBarangJadi");
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
            if(!id.HasValue)
            {
                return HttpNotFound();
            }

            var model = new MstBarangJadiModel();

            var GetData = _mstBarangJadiBLL.GetById(id);
            if (GetData == null)
            {
                return HttpNotFound();
            }

            model = Mapper.Map<MstBarangJadiModel>(GetData);

            model = Init(model);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MstBarangJadiModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var GetData = _mstBarangJadiBLL.GetById(model.ID);

                    GetData.STATUS = model.STATUS;
                    GetData.MODIFIED_BY = CurrentUser.USERNAME;
                    GetData.MODIFIED_DATE = DateTime.Now;

                    _mstBarangJadiBLL.Save(GetData,Mapper.Map<LoginDto>(CurrentUser));

                    return RedirectToAction("Index", "MstBarangJadi");
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