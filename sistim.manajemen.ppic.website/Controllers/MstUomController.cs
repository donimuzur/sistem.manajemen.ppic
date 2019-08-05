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
    public class MstUomController : BaseController
    {
        private IMstUomBLL _mstUomBLL;
        public MstUomController(IMstUomBLL MstUomBLL, IPageBLL pageBll) : base(pageBll, Enums.MenuList.MasterUom)
        {
            _mstUomBLL = MstUomBLL;
        }
        public MstUomModel Init(MstUomModel model)
        {
            model.MainMenu = Enums.MenuList.MasterUom;
            model.Menu = Enums.GetEnumDescription(Enums.MenuList.Master);
            model.CurrentUser = CurrentUser;
            model.Tittle = "Master Uom";

            model.ChangesHistory = GetChangesHistory((int)Enums.MenuList.MasterUom, model.ID);

            return model;
        }
        public ActionResult Index()
        {
            var model = new MstUomViewModel();
            var data = _mstUomBLL.GetAll();

            model.ListData = Mapper.Map<List<MstUomModel>>(data);

            model.MainMenu = Enums.MenuList.MasterUom;
            model.Menu = Enums.GetEnumDescription(Enums.MenuList.Master);
            model.CurrentUser = CurrentUser;
            model.Tittle = "Master Uom";


            return View(model);
        }
        #region --- Create ---
        public ActionResult Create()
        {
            var model = new MstUomModel();

            model = Init(model);

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MstUomModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Dto = Mapper.Map<MstUomDto>(model);

                    Dto.CREATED_BY = CurrentUser.USERNAME;
                    Dto.CREATED_DATE = DateTime.Now;

                    var GetDataExist = _mstUomBLL.GetByUom(model.SATUAN);
                    if (GetDataExist != null)
                    {
                        AddMessageInfo("Data dengan satuan tersebut sudah ada", Enums.MessageInfoType.Error);
                        return View(Init(model));
                    }

                    Dto = _mstUomBLL.Save(Dto, Mapper.Map<LoginDto>(CurrentUser));
                    AddMessageInfo("Sukses tambah data", Enums.MessageInfoType.Success);
                    return RedirectToAction("Details", "MstUom", new { id = Dto.ID });
                }
                catch (Exception exp)
                {
                    LogError.LogError.WriteError(exp);
                    AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
                    return RedirectToAction("Index", "MstUom");
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

            var model = new MstUomModel();

            var GetData = _mstUomBLL.GetById(id);
            if (GetData == null)
            {
                return HttpNotFound();
            }

            model = Mapper.Map<MstUomModel>(GetData);
            model = Init(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MstUomModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Dto = Mapper.Map<MstUomDto>(model);

                    Dto.MODIFIED_BY = CurrentUser.USERNAME;
                    Dto.MODIFIED_DATE = DateTime.Now;

                    _mstUomBLL.Save(Dto, Mapper.Map<LoginDto>(CurrentUser));
                    AddMessageInfo("Sukses update data", Enums.MessageInfoType.Success);
                    return RedirectToAction("Details", "MstUom", new { id = model.ID });
                }
                catch (Exception exp)
                {
                    LogError.LogError.WriteError(exp);
                    AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
                    return RedirectToAction("Index", "MstUom");
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

        #region --- Details ---
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return HttpNotFound();
            }
            try
            {
                var model = new MstUomModel();

                model = Mapper.Map<MstUomModel>(_mstUomBLL.GetById(id));

                model = Init(model);
                return View(model);
            }
            catch (Exception exp)
            {
                LogError.LogError.WriteError(exp);
                AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
                return RedirectToAction("Index", "MstUom");
            }
        }
        #endregion


        #region --- Json ---

        #endregion
    }
}