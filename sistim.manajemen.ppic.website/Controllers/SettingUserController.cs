using AutoMapper;
using sistem.manajemen.ppic.bll.IBLL;
using sistem.manajemen.ppic.core;
using sistem.manajemen.ppic.dto;
using sistem.manajemen.ppic.website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sistem.manajemen.ppic.website.Controllers
{
    public class SettingUserController : BaseController
    {
        // GET: SettingUser
        private ILoginBLL _loginBLL;

        public SettingUserController(ILoginBLL LoginBLL, IPageBLL pageBll) : base(pageBll, Enums.MenuList.SettingUser)
        {
            _loginBLL = LoginBLL;
        }
        public LoginModel Init(LoginModel model)
        {
            model.MainMenu = Enums.MenuList.SettingUser;
            model.Menu = Enums.GetEnumDescription(Enums.MenuList.Setting);
            model.CurrentUser = CurrentUser;
            model.Tittle = "Setting User";

            return model;
        }
        //GET: TrnSlipTimbangan
        public ActionResult Index()
        {
            try
            {
                var model = new LoginViewModel();
                var data = _loginBLL.GetAll();

                model.ListData = Mapper.Map<List<LoginModel>>(data);

                model.MainMenu = Enums.MenuList.SettingUser;
                model.Menu = Enums.GetEnumDescription(Enums.MenuList.Setting);
                model.CurrentUser = CurrentUser;
                model.Tittle = "Setting User";

                return View(model);
            }
            catch (Exception exp)
            {
                LogError.LogError.WriteError(exp);
                AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
                return RedirectToAction("Index", "SettingUser");
            }
        }


        #region --- Create ---
        public ActionResult Create()
        {
            var model = new LoginModel();

            model = Init(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.USERNAME = model.USER_ID;
                    model.STATUS = true;
                    var Dto = Mapper.Map<LoginDto>(model);

                    var GetDataExist = _loginBLL.GetById(model.USER_ID);
                    if (GetDataExist != null)
                    {
                        AddMessageInfo("User ID dengan Nama Tersebut Sudah ada", Enums.MessageInfoType.Success);
                        return View(Init(model));
                    }
                    Dto = _loginBLL.Save(Dto);
                    AddMessageInfo("Sukses Simpan Data", Enums.MessageInfoType.Success);
                    return RedirectToAction("Details", "SettingUser", new { id = Dto.USER_ID });
                }
                catch (Exception exp)
                {
                    LogError.LogError.WriteError(exp);
                    AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
                    return RedirectToAction("Index", "SettingUser");
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
        public ActionResult Edit(string UserId)
        {
            if (string.IsNullOrEmpty(UserId))
            {
                return HttpNotFound();
            }

            var model = new LoginModel();

            var GetData = _loginBLL.GetById(UserId);
            if (GetData == null)
            {
                return HttpNotFound();
            }

            model = Mapper.Map<LoginModel>(GetData);
            model = Init(model);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Dto = Mapper.Map<LoginDto>(model);
                    _loginBLL.Save(Dto);
                    AddMessageInfo("Sukses update data", Enums.MessageInfoType.Success);
                    return RedirectToAction("Details", "SettingUser", new { id = model.USER_ID });
                }
                catch (Exception exp)
                {
                    LogError.LogError.WriteError(exp);
                    AddMessageInfo("Telah Terjadi Kesalahan", Enums.MessageInfoType.Error);
                    return RedirectToAction("Index", "SettingUser");
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
        public ActionResult Details(string UserId)
        {
            if (!string.IsNullOrEmpty(UserId))
            {
                return HttpNotFound();
            }

            var model = new LoginModel();

            var GetData = _loginBLL.GetById(UserId);
            if (GetData == null)
            {
                return HttpNotFound();
            }

            model = Mapper.Map<LoginModel>(GetData);
            model = Init(model);

            return View(model);
        }
        #endregion
    }
}