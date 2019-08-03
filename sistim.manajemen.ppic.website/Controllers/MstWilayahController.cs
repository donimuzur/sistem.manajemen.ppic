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
            model.MainMenu = Enums.MenuList.MasterWilayah;
            model.Menu = Enums.GetEnumDescription(Enums.MenuList.Master);
            model.CurrentUser = CurrentUser;
            model.Tittle = "Master Wilayah";

            model.ChangesHistory = GetChangesHistory((int)Enums.MenuList.MasterWilayah, model.ID);

            return model;
        }
        public ActionResult Index()
        {
            var model = new MstWilayahViewModel();
            var data = _mstWilayahBLL.GetAll();

            model.ListData = Mapper.Map<List<MstWilayahModel>>(data);

            model.MainMenu = Enums.MenuList.MasterWilayah;
            model.Menu = Enums.GetEnumDescription(Enums.MenuList.Master);
            model.CurrentUser = CurrentUser;
            model.Tittle = "Master Wilayah";


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

                    var GetDataExist = _mstWilayahBLL.GetByWilayah(model.WILAYAH);
                    if (GetDataExist != null)
                    {
                        AddMessageInfo("Data dengan wilayah Tersebut Sudah ada", Enums.MessageInfoType.Error);
                        return View(Init(model));
                    }

                    Dto = _mstWilayahBLL.Save(Dto, Mapper.Map<LoginDto>(CurrentUser));
                    AddMessageInfo("Sukses tambah data", Enums.MessageInfoType.Success);
                    return RedirectToAction("Details", "MstWilayah", new { id = Dto.ID });
                }
                catch (Exception exp)
                {
                    LogError.LogError.WriteError(exp);
                    AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
                    return RedirectToAction("Index", "MstWilayah");
                }
            }
            else
            {
                AddMessageInfo("Gagal update data", Enums.MessageInfoType.Error);
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
                    AddMessageInfo("Sukses update data", Enums.MessageInfoType.Success);
                    return RedirectToAction("Details", "MstWilayah", new {id=model.ID });
                }
                catch (Exception exp)
                {
                    LogError.LogError.WriteError(exp);
                    AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
                    return RedirectToAction("Index", "MstWilayah");
                }
            }
            else
            {
                model = Init(model);
                return View(model);
            }
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
                var model = new MstWilayahModel();

                model = Mapper.Map<MstWilayahModel>(_mstWilayahBLL.GetById(id));

                model = Init(model);
                return View(model);
            }
            catch (Exception exp)
            {
                LogError.LogError.WriteError(exp);
                AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
                return RedirectToAction("Index", "MstWilayah");
            }
        }
        #endregion


        #region --- Json ---

        #endregion
    }
}