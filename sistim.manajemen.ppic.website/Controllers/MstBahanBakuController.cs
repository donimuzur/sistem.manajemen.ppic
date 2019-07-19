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
    public class MstBahanBakuController : BaseController
    {
        private IMstBahanBakuBLL _mstBahanBakuBLL;
        private IMstUomBLL _mstUomBLL;
        public MstBahanBakuController(IMstBahanBakuBLL MstBahanBakuBLL, IMstUomBLL MstUomBLL, IPageBLL pageBll) : base(pageBll, Enums.MenuList.MstBahanBaku)
        {
            _mstBahanBakuBLL = MstBahanBakuBLL;
            _mstUomBLL = MstUomBLL;
        }

        public MstBahanBakuModel Init(MstBahanBakuModel model)
        {
            var ListUom = new List<string>();
            ListUom = _mstUomBLL.GetAll().Select(x => x.SATUAN).ToList();
            model.UomList = new SelectList(ListUom);

            model.MainMenu = Enums.MenuList.MstBahanBaku;
            model.Menu = Enums.GetEnumDescription(Enums.MenuList.Master);
            model.CurrentUser = CurrentUser;
            model.Tittle = "Master Bahan Baku";

            model.ChangesHistory = GetChangesHistory((int)Enums.MenuList.MstBahanBaku, model.ID);

            return model;
        }

        public ActionResult Index()
        {
            try
            {
                var model = new MstBahanBakuViewModel();
                var data = _mstBahanBakuBLL.GetAll();

                model.ListData = Mapper.Map<List<MstBahanBakuModel>>(data);

                model.MainMenu = Enums.MenuList.MstBahanBaku;
                model.Menu = Enums.GetEnumDescription(Enums.MenuList.Master);
                model.CurrentUser = CurrentUser;
                model.Tittle = "Master Bahan Baku";

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
            var model = new MstBahanBakuModel();

            model = Init(model);
            model.STATUS = true;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MstBahanBakuModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Dto = Mapper.Map<MstBahanBakuDto>(model);

                    Dto.CREATED_BY = CurrentUser.USERNAME;
                    Dto.CREATED_DATE = DateTime.Now;
                    Dto.STOCK = model.STOCK_AWAL == null ? 0 : model.STOCK_AWAL.Value;

                    Dto.STATUS = true;

                    var GetDataExist = _mstBahanBakuBLL.GetByNama(model.NAMA_BARANG);
                    if(GetDataExist != null)
                    {
                        AddMessageInfo("Bahan baku dengan Nama Tersebut Sudah ada", Enums.MessageInfoType.Error);
                        return View(Init(model));
                    }

                    _mstBahanBakuBLL.Save(Dto, Mapper.Map<LoginDto>(CurrentUser));
                    AddMessageInfo("Success Create Master Bahan Baku", Enums.MessageInfoType.Success);
                    return RedirectToAction("Index", "MstBahanBaku");
                }
                catch (Exception exp)
                {
                    LogError.LogError.WriteError(exp);
                    AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
                    return RedirectToAction("Index", "MstBahanBaku");
                }
            }
            AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
            return View(Init(model));
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

                var model = new MstBahanBakuModel();

                var GetData = _mstBahanBakuBLL.GetById(id);
                if (GetData == null)
                {
                    return HttpNotFound();
                }

                model = Mapper.Map<MstBahanBakuModel>(GetData);

                model = Init(model);
                return View(model);
            }
            catch (Exception exp)
            {
                LogError.LogError.WriteError(exp);
                AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
                return RedirectToAction("Index", "MstBahanBaku");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MstBahanBakuModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var GetData = _mstBahanBakuBLL.GetById(model.ID);

                    GetData.STATUS = model.STATUS;
                    GetData.MODIFIED_BY = CurrentUser.USERNAME;
                    GetData.MODIFIED_DATE = DateTime.Now;

                    _mstBahanBakuBLL.Save(GetData, Mapper.Map<LoginDto>(CurrentUser));
                    AddMessageInfo("Sukses Update Master Bahan Baku", Enums.MessageInfoType.Success);
                    return RedirectToAction("Index", "MstBahanBaku");
                }
                catch (Exception exp)
                {
                    LogError.LogError.WriteError(exp);
                    AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
                    return RedirectToAction("Index", "MstBahanBaku");
                }
            }
            AddMessageInfo("Gagal Update Master Bahan Baku", Enums.MessageInfoType.Error);
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
                var model = new MstBahanBakuModel();

                model = Mapper.Map<MstBahanBakuModel>(_mstBahanBakuBLL.GetById(id));

                model = Init(model);
                return View(model);
            }
            catch (Exception exp)
            {
                LogError.LogError.WriteError(exp);
                AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
                return RedirectToAction("Index", "MstBahanBaku");
            }
        }
        #endregion

        #region --- Json ---
        public JsonResult GetSatuanList()
        {
            var model = _mstUomBLL
                .GetAll()
                .Select(x
                    => new
                    {
                        DATA = x.SATUAN
                    })
                    .OrderBy(X => X.DATA)
                .ToList();
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}