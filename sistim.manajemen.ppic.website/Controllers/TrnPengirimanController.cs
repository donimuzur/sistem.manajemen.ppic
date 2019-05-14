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
        private IMstBarangJadiBLL _mstBarangJadiBLL;
        public TrnPengirimanController(IPageBLL pageBll, ITrnPengirimanBLL TrnPengirimanBLL, IMstBarangJadiBLL MstBarangJadiBLL
            , ITrnSpbBLL TrnSpbBLL, ITrnDoBLL TrnDoBLL) : base(pageBll, Enums.MenuList.TrnPengiriman)
        {
            _trnPengirimanBLL = TrnPengirimanBLL;
            _trnDoBLL = TrnDoBLL;
            _trnSpbBLL = TrnSpbBLL;
            _mstBarangJadiBLL = MstBarangJadiBLL;
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
                    
                    var GetDataExist = _trnPengirimanBLL.GetBySj(model.SURAT_JALAN);
                    if (GetDataExist != null)
                    {
                        AddMessageInfo("Gagal Create Nomor Surat Jalan sudah ada", Enums.MessageInfoType.Error);
                        model = Init(model);
                        return View(model);
                    }

                    if (model.PARTY < model.AKUMULASI + model.JUMLAH)
                    {
                        AddMessageInfo("Jumlah Pengiriman tidak boleh lebih dari Party", Enums.MessageInfoType.Error);
                        model = Init(model);
                        return View(model);
                    }
                    
                    var GetBarangExist = _mstBarangJadiBLL.GetByNama(model.NAMA_BARANG);
                    if (GetBarangExist == null)
                    {
                        AddMessageInfo("Gagal Create Nama Barang Tersebut Tidak ada di Gudang", Enums.MessageInfoType.Error);
                        model = Init(model);
                        return View(model);
                    }
                    else if(GetBarangExist.STOCK < model.JUMLAH)
                    {
                        AddMessageInfo("Stock di Gudang Kurang", Enums.MessageInfoType.Error);
                        model = Init(model);
                        return View(model);
                    }

                    model.CREATED_BY = CurrentUser.USERNAME;
                    model.CREATED_DATE = DateTime.Now;
                    model.TANGGAL = DateTime.Now;
                    model.ID_BARANG = GetBarangExist.ID;

                    _trnPengirimanBLL.Save(Mapper.Map<TrnPengirimanDto>(model), Mapper.Map<LoginDto>(CurrentUser));
                    _mstBarangJadiBLL.KurangSaldo(GetBarangExist.ID, model.JUMLAH.Value);

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

            model = Mapper.Map<TrnPengirimanModel>(_trnPengirimanBLL.GetById(Id));

            model = Init(model);

            model.AKUMULASI = _trnPengirimanBLL.GetAkumulasi(model.NO_SPB);
            model.SISA = model.PARTY - model.AKUMULASI;

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
            var data = new TrnDoModel();
            data = Mapper.Map<TrnDoModel>(_trnDoBLL.GetBySPB(NoSpb));
            data.AKUMULASI = _trnPengirimanBLL.GetAkumulasi(NoSpb);

            if (data == null)
            {
                data = new TrnDoModel();
            }
            return Json(data);
        }
        #endregion
    }
}