using AutoMapper;
using sistem.manajemen.ppic.bll.IBLL;
using sistem.manajemen.ppic.core;
using sistem.manajemen.ppic.dto;
using sistem.manajemen.ppic.website.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sistem.manajemen.ppic.website.Controllers
{
    public class TrnPengirimanController : BaseController
    {
        private ITrnDoBLL _trnDoBLL;
        private ITrnSpbBLL _trnSpbBLL;
        private ITrnPengirimanBLL _trnPengirimanBLL;
        private IMstBarangJadiBLL _mstBarangJadiBLL;
        public TrnPengirimanController(IPageBLL pageBll, ITrnDoBLL TrnDoBLL, ITrnSpbBLL TrnSpbBLL, 
            ITrnPengirimanBLL TrnPengirimanBLL,IMstBarangJadiBLL MstBarangJadiBLL) : base(pageBll, Enums.MenuList.TrnPengiriman)
        {
            _trnDoBLL = TrnDoBLL;
            _trnSpbBLL = TrnSpbBLL;
            _mstBarangJadiBLL = MstBarangJadiBLL;
            _trnPengirimanBLL = TrnPengirimanBLL;
        }
        public TrnPengirimanModel Init(TrnPengirimanModel model)
        {
            model.CurrentUser = CurrentUser;

            model.MainMenu = Enums.MenuList.TrnPengiriman;
            model.Menu = Enums.GetEnumDescription(Enums.MenuList.Ekspedisi);
            model.Tittle = "Form Pengiriman";

            model.ChangesHistory = GetChangesHistory((int)Enums.MenuList.TrnPengiriman, model.ID);

            return model;
        }
        public ActionResult Index()
        {
            var model = new TrnPengirimanViewModel();

            model.ListData = Mapper.Map<List<TrnPengirimanModel>>(_trnPengirimanBLL.GetActiveAll());

            model.CurrentUser = CurrentUser;
            model.MainMenu = Enums.MenuList.TrnPengiriman;
            model.Menu = Enums.GetEnumDescription(Enums.MenuList.Ekspedisi);
            model.Tittle = "Form Pengiriman";

            return View(model);
        }

        #region --- Create ---
        public ActionResult Create()
        {
            var model = new TrnPengirimanModel();

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
                    model.TANGGAL = DateTime.Now;
                    model.CREATED_BY = CurrentUser.USERNAME;
                    model.CREATED_DATE = DateTime.Now;
                    model.STATUS = (int)Enums.StatusDocument.Open;

                    model.NO_SPB = model.NO_SPB.ToUpper();
                    model.NO_SPB = model.NO_SPB.TrimEnd('\r', '\n', ' ');
                    model.NO_SPB = model.NO_SPB.TrimStart('\r', '\n', ' ');

                    var GetBarang = _mstBarangJadiBLL.GetByNama(model.NAMA_BARANG);
                    if (GetBarang != null)
                    {
                        model.ID_BARANG = GetBarang.ID;
                        model.STOCK_AWAL = GetBarang.STOCK;
                        model.STOCK_AKHIR = model.STOCK_AWAL - model.KUANTUM;
                        if(model.STOCK_AKHIR < 0)
                        {
                            AddMessageInfo("Stock barang tidak mencukupi", Enums.MessageInfoType.Error);
                            return View(Init(model));
                        }
                    }

                    if (model.KUANTUM > model.SISA_KIRIM )
                    {
                        AddMessageInfo("Kuantum tidak boleh melebihi sisa Kirim", Enums.MessageInfoType.Error);
                        return View(Init(model));
                    }

                    if (model.SISA_KIRIM == 0)
                    {
                        AddMessageInfo("Jumlah barang yg dikirim sudah sesuai, sisa kirim 0", Enums.MessageInfoType.Error);
                        return View(Init(model));
                    }

                    var CheckDataSPBExist = _trnSpbBLL.GetBySPB(model.NO_SPB);
                    if (CheckDataSPBExist == null)
                    {
                        AddMessageInfo("No SPB tersebut tidak ada", Enums.MessageInfoType.Error);
                        return View(Init(model));
                    }

                    var CheckDataDOExist = _trnDoBLL.GetBySpbAndDo(model.NO_SPB,model.NO_DO.ToString());
                    if (CheckDataDOExist == null)
                    {
                        AddMessageInfo("No DO tersebut tidak ada", Enums.MessageInfoType.Error);
                        return View(Init(model));
                    }

                    var Dto = Mapper.Map<TrnPengirimanDto>(model);
                    _trnPengirimanBLL.Save(Dto, Mapper.Map<LoginDto>(CurrentUser));
                    _mstBarangJadiBLL.KurangSaldo(model.ID_BARANG, model.KUANTUM.Value);

                    AddMessageInfo("Sukses Create Form Pengiriman", Enums.MessageInfoType.Success);
                    return RedirectToAction("Index", "TrnPengiriman");
                }
                catch (Exception exp)
                {
                    LogError.LogError.WriteError(exp);
                    AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
                    return RedirectToAction("Index", "TrnPengiriman");
                }
            }
            AddMessageInfo("Gagal Create Form Pengiriman", Enums.MessageInfoType.Error);
            return View(Init(model));
        }
        #endregion

        #region --- Edit ---

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
                var model = new TrnPengirimanModel();
               
                model =Mapper.Map<TrnPengirimanModel>(_trnPengirimanBLL.GetById(id));
                if (model == null)
                {
                    return HttpNotFound();
                }

                model = Init(model);
                return View(model);
            }
            catch (Exception exp)
            {
                LogError.LogError.WriteError(exp);
                AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
                return RedirectToAction("Index", "TrnPengiriman");
            }
        }
        #endregion

        #region --- Hapus ---
        public ActionResult Hapus(int? id, string Remarks)
        {
            if (!id.HasValue)
            {
                return HttpNotFound();
            }

            try
            {
                var model = _trnPengirimanBLL.GetById(id);
                if (model == null)
                {
                    return HttpNotFound();
                }

                _trnPengirimanBLL.Delete(id.Value, Remarks);
                _mstBarangJadiBLL.TambahSaldo(model.ID_BARANG, model.KUANTUM.Value);

                AddMessageInfo("Data sukses dihapus", Enums.MessageInfoType.Success);
                return RedirectToAction("Index", "TrnPengiriman");
            }
            catch (Exception exp)
            {
                LogError.LogError.WriteError(exp);
                AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
                return RedirectToAction("Index", "TrnPengiriman");
            }
        }
        #endregion

        #region --- Json ---
        [HttpPost]
        public JsonResult GetDataSpb(string NoSpb)
        {
            var data = _trnSpbBLL.GetBySPB(NoSpb);
            if (data == null)
            {
                data = new TrnSpbDto();
            }
            return Json(data);
        }
        [HttpPost]
        public JsonResult CheckDoExistBySpb(string NoSpb)
        {
            var data = _trnDoBLL.GetBySPB(NoSpb);
            if (data == null)
            {
                data = new List<TrnDoDto>();
            }
            return Json(data);
        }
        public JsonResult GetSpbList()
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

        //Get DO
        public JsonResult GetDoList(string No_SPB)
        {
            var model = _trnDoBLL
             .GetBySPB(No_SPB)
             .Select(x
                 => new
                 {
                     DATA = x.NO_DO.ToUpper()
                 })
             .Distinct()
             .OrderBy(X => X.DATA)
             .ToList();

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetDo(string No_Spb, string No_Do)
        {
            var data = new TrnPengirimanDto();
            if(!string.IsNullOrEmpty(No_Spb) && !string.IsNullOrEmpty(No_Do))
            {
                var GetSpb = _trnSpbBLL.GetBySPB(No_Spb);
                var GetDo = _trnDoBLL.GetBySpbAndDo(No_Spb, No_Do);
                
                var GetAkumulasi = _trnPengirimanBLL.GetAkumulasi(int.Parse(No_Do), No_Spb);

                data.ALAMAT_KONSUMEN = GetDo.ALAMAT_KONSUMEN;
                data.NAMA_KONSUMEN = GetDo.NAMA_KONSUMEN;
                data.NAMA_BARANG = GetDo.NAMA_BARANG;
                data.KEMASAN = GetDo.KEMASAN;
                data.PARTAI = GetDo.JUMLAH;
                data.TOTAL_KIRIM = GetAkumulasi;
                data.SISA_KIRIM = data.PARTAI - data.TOTAL_KIRIM;
                
            }

            return Json(data);
        }
        #endregion

    }
}