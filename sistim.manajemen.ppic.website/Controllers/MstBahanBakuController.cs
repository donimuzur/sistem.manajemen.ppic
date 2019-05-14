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
    public class MstBahanBakuController : BaseController
    {
        private IMstBahanBakuBLL _mstBahanBakuBLL;
        public MstBahanBakuController(IPageBLL pageBll, IMstBahanBakuBLL MstBahanBakuBLL) : base(pageBll, Enums.MenuList.GdgBarangBB)
        {
            _mstBahanBakuBLL = MstBahanBakuBLL;
        }

        // GET: MstBahanBaku
        public ActionResult Index()
        {
            var model = new MstBahanBakuViewModel();
            model.ListData = Mapper.Map<List<MstBahanBakuModel>>(_mstBahanBakuBLL.GetAll());
            
            model.CurrentUser = CurrentUser;
            model.Menu = "Master Bahan Baku";
            model.MainMenu = Enums.MenuList.GdgBarangBB;

            return View(model);
        }

        public MstBahanBakuModel Init(MstBahanBakuModel model)
        {
            model.MainMenu = Enums.MenuList.GdgBarangBB;
            model.Menu = "Master Bahan Baku";
            model.CurrentUser = CurrentUser;

            return model;
        }

        #region --- Create ---
        public ActionResult Create()
        {
            var model = new MstBahanBakuModel();
            model = Init(model);

            return View(model);
        }

        [HttpPost]
        public ActionResult Create(MstBahanBakuModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Dto = Mapper.Map<MstBahanBakuDto>(model);

                    Dto.CREATED_BY = CurrentUser.USERNAME;
                    Dto.CREATED_DATE = DateTime.Now;
                    Dto.STATUS = true;

                    _mstBahanBakuBLL.Save(Dto, Mapper.Map<LoginDto>(CurrentUser));
                    return RedirectToAction("Index", "MstBahanBaku");
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
        public ActionResult Edit(int Id)
        {
            var model = new MstBahanBakuModel();

            model = Mapper.Map<MstBahanBakuModel>(_mstBahanBakuBLL.GetById(Id));
            model = Init(model);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(MstBahanBakuModel model)
        {
            return View(model);
        }
        #endregion

        #region --- Details ---
        public ActionResult Details(int Id)
        {
            var model = new MstBahanBakuModel();

            model = Mapper.Map<MstBahanBakuModel>(_mstBahanBakuBLL.GetById(Id));
            model = Init(model);

            return View(model);
        }
        #endregion

        #region --- Json ---
        public JsonResult GetBahanBakuList()
        {
            var model = _mstBahanBakuBLL
               .GetAll()
               .Select(x
                   => new
                   {
                       DATA = x.NAMA_BARANG
                   })
                   .OrderBy(X => X.DATA)
                   .Distinct()
               .ToList();
            return Json(new object());
        }

        public JsonResult NamaBarangList()
        {
            var model = _mstBahanBakuBLL
               .GetAll()
               .Select(x
                   => new
                   {
                       DATA = x.NAMA_BARANG
                   })
                   .OrderBy(X => X.DATA)
                   .Distinct()
               .ToList();
            return Json(new object());
        }
        #endregion
    }
}