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
    public class MstKemasanController : BaseController
    {
        private IMstKemasanBLL _mstKemasanBLL;
        public MstKemasanController(IPageBLL pageBll, IMstKemasanBLL MstKemasanBLL) : base(pageBll, Enums.MenuList.MasterKemasan)
        {
            _mstKemasanBLL = MstKemasanBLL;
        }
        public MstKemasanModel Init(MstKemasanModel model)
        {
            model.MainMenu = Enums.MenuList.MasterKemasan;
            model.Menu = Enums.GetEnumDescription(Enums.MenuList.Master);
            model.CurrentUser = CurrentUser;
            model.Tittle = "Master Kemasan";

            model.ChangesHistory = GetChangesHistory((int)Enums.MenuList.MasterKemasan, model.ID);

            return model;
        }
        //GET: MstKemasan
        public ActionResult Index()
        {
            try
            {
                var model = new MstKemasanViewModel();
                var data = _mstKemasanBLL.GetAll();

                model.ListData = Mapper.Map<List<MstKemasanModel>>(data);

                model.MainMenu = Enums.MenuList.MasterKemasan;
                model.Menu = Enums.GetEnumDescription(Enums.MenuList.Master);
                model.CurrentUser = CurrentUser;
                model.Tittle = "Master Kemasan";

                return View(model);
            }
            catch (Exception exp)
            {
                LogError.LogError.WriteError(exp);
                AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
                return RedirectToAction("Index", "MstKemasan");
            }
        }

        #region --- Create ---
        public ActionResult Create()
        {
            var model = new MstKemasanModel();

            model = Init(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MstKemasanModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Dto = Mapper.Map<MstKemasanDto>(model);

                    Dto.CREATED_BY = CurrentUser.USERNAME;
                    Dto.CREATED_DATE = DateTime.Now;
                 
                    var GetDataExist = _mstKemasanBLL.GetByNama(model.KEMASAN);
                    if (GetDataExist != null)
                    {
                        AddMessageInfo("Kemasan dengan Nama Tersebut Sudah ada", Enums.MessageInfoType.Success);
                        return View(Init(model));
                    }
                    Dto = _mstKemasanBLL.Save(Dto, Mapper.Map<LoginDto>(CurrentUser));
                    AddMessageInfo("Sukses create Master Kemasan", Enums.MessageInfoType.Success);
                    return RedirectToAction("Details", "MstKemasan", new {id = Dto.ID });
                }
                catch (Exception exp)
                {
                    LogError.LogError.WriteError(exp);
                    AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
                    return RedirectToAction("Index", "MstKemasan");
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

                var model = new MstKemasanModel();

                var GetData = _mstKemasanBLL.GetById(id);
                if (GetData == null)
                {
                    return HttpNotFound();
                }

                model = Mapper.Map<MstKemasanModel>(GetData);

                model = Init(model);
                return View(model);
            }
            catch (Exception exp)
            {
                LogError.LogError.WriteError(exp);
                AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
                return RedirectToAction("Index", "MstKemasan");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MstKemasanModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Dto = Mapper.Map<MstKemasanDto>(model);

                    var GetExist = _mstKemasanBLL.GetByNama(model.KEMASAN);
                    if(GetExist != null && GetExist.ID != model.ID)
                    {
                        AddMessageInfo("Kemasan dengan nama tersebut sudah ada", Enums.MessageInfoType.Success);
                        return View(model);
                    }

                    Dto = _mstKemasanBLL.Save(Dto, Mapper.Map<LoginDto>(CurrentUser));

                    AddMessageInfo("Success Update Master Kemasan", Enums.MessageInfoType.Success);
                    return RedirectToAction("Details", "MstKemasan", new {id = model.ID});
                }
                catch (Exception exp)
                {
                    LogError.LogError.WriteError(exp);
                    AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
                    return RedirectToAction("Index", "MstKemasan");
                }
            }
            AddMessageInfo("Gagal Update Master Barang Jadi", Enums.MessageInfoType.Error);
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
                var model = new MstKemasanModel();

                model = Mapper.Map<MstKemasanModel>(_mstKemasanBLL.GetById(id));

                model = Init(model);
                return View(model);
            }
            catch (Exception exp)
            {
                LogError.LogError.WriteError(exp);
                AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
                return RedirectToAction("Index", "MstKemasan");
            }
        }
        #endregion

    }
}