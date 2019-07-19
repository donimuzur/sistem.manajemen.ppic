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
        private IMstKemasanBLL _mstKemasanBLL;
        public MstBarangJadiController(IMstBarangJadiBLL MstBarangJadiBLL, IMstKemasanBLL MstKemasanBLL, IPageBLL pageBll) : base(pageBll, Enums.MenuList.MstBarangJadi)
        {
            _mstBarangJadiBLL = MstBarangJadiBLL;
            _mstKemasanBLL = MstKemasanBLL;
        }

        public MstBarangJadiModel Init(MstBarangJadiModel model)
        {
            var ListKemasan = new List<string>();
            ListKemasan = _mstKemasanBLL.GetAll().Select(x => x.KEMASAN).ToList();
            model.KemasanList = new SelectList(ListKemasan);

            model.MainMenu = Enums.MenuList.MstBarangJadi;
            model.Menu = Enums.GetEnumDescription(Enums.MenuList.Master);
            model.CurrentUser = CurrentUser;
            model.Tittle = "Master Barang Jadi";

            model.ChangesHistory = GetChangesHistory((int)Enums.MenuList.MstBarangJadi, model.ID);

            return model;
        }

        public ActionResult Index()
        {
            try
            {
                var model = new MstBarangJadiViewModel();
                var data = _mstBarangJadiBLL.GetAll();

                model.ListData = Mapper.Map<List<MstBarangJadiModel>>(data);

                model.MainMenu = Enums.MenuList.MstBarangJadi;
                model.Menu = Enums.GetEnumDescription(Enums.MenuList.Master);
                model.CurrentUser = CurrentUser;
                model.Tittle = "Master Barang Jadi";

                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Error");
            }
         
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
                    Dto.STOCK = model.STOCK_AWAL == null ? 0 : model.STOCK_AWAL.Value;
                    
                    Dto.STATUS = true;

                    var GetDataExist = _mstBarangJadiBLL.GetByNama(model.NAMA_BARANG);
                    if (GetDataExist != null)
                    {
                        AddMessageInfo("Barang dengan Nama Tersebut Sudah ada", Enums.MessageInfoType.Error);
                        return View(Init(model));
                    }
                    _mstBarangJadiBLL.Save(Dto, Mapper.Map<LoginDto>(CurrentUser));
                    AddMessageInfo("Sukses create Master Barang Jadi", Enums.MessageInfoType.Error);
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
            try
            {
                if (!id.HasValue)
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
            catch (Exception exp)
            {
                LogError.LogError.WriteError(exp);
                AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
                return RedirectToAction("Index", "MstBarangJadi");
            }
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
                    AddMessageInfo("Success Update Master Barang Jadi", Enums.MessageInfoType.Success);
                    return RedirectToAction("Index", "MstBarangJadi");
                }
                catch (Exception exp)
                {
                    LogError.LogError.WriteError(exp);
                    AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
                    return RedirectToAction("Index", "MstBarangJadi");
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
                var model = new MstBarangJadiModel();

                model = Mapper.Map<MstBarangJadiModel>(_mstBarangJadiBLL.GetById(id));

                model = Init(model);
                return View(model);
            }
            catch (Exception exp)
            {
                LogError.LogError.WriteError(exp);
                AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
                return RedirectToAction("Index", "MstBarangJadi");
            }
        }
        #endregion

        #region --- Json ---

        #endregion
    }
}