using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using sistem.manajemen.ppic.bll.IBLL;
using sistem.manajemen.ppic.core;
using sistem.manajemen.ppic.website.Models;
using sistem.manajemen.ppic.dto.Input;
using AutoMapper;

namespace sistem.manajemen.ppic.website.Controllers
{
    public class RptOutstandingController : BaseController
    {
        private IRptOutstandingBLL _rptOutstandingBLL;
        public RptOutstandingController(IPageBLL pageBll, IRptOutstandingBLL RptOutstandingBLL) : base(pageBll, Enums.MenuList.LaporanOutstanding)
        {
            _rptOutstandingBLL = RptOutstandingBLL;
        }

        // GET: RptOutstanding
        public ActionResult Index()
        {
            var model = new RptOutstandingListModel();

            model.CurrentUser = CurrentUser;
            model.MainMenu = Enums.MenuList.LaporanOutstanding;
            model.Menu = "Laporan Outstanding";

            return View(model);
        }
      
        private List<RptOutstandingModel> GetRptOutstanding(RptOutstandingModelSearchView filter = null)
        {
            if (filter == null)
            {
                //Get All
                var data = _rptOutstandingBLL.GetLogProductionIncome_SP(new RptOutstandingInput());
                return Mapper.Map<List<RptOutstandingModel>>(data);
            }

            //getbyparams
            var input = Mapper.Map<RptOutstandingInput>(filter);

            var dbData = _rptOutstandingBLL.GetLogProductionIncome_SP(input);
            return Mapper.Map<List<RptOutstandingModel>>(dbData);
        }

        #region --- Json ---
        [HttpPost]
        public JsonResult GetRptOutstandingList(string FromDate, string ToDate)
        {
            try
            {
                var input = new RptOutstandingModelSearchView();
                if (string.IsNullOrEmpty(FromDate))
                {
                    input.FromDate = null;
                }
                else
                {
                    input.FromDate = Convert.ToDateTime(FromDate);
                }

                if(string.IsNullOrEmpty(ToDate))
                {
                    input.ToDate = null;
                }
                else
                {
                    input.ToDate = Convert.ToDateTime(ToDate);
                }

                var data = GetRptOutstanding(input);
                return Json(data);
            }
            catch (Exception)
            {
                return Json("Error");
            }
        }
        #endregion
    }
}