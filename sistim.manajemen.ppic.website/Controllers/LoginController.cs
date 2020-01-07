using sistem.manajemen.ppic.core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using sistem.manajemen.ppic.bll.IBLL;
using sistem.manajemen.ppic.website.Models;
using AutoMapper;
using sistem.manajemen.ppic.dto;

namespace sistem.manajemen.ppic.website.Controllers
{
    public class LoginController : BaseController
    {
        private ILoginBLL _loginBLL;

        public LoginController(ILoginBLL LoginBLL,IPageBLL pageBll) : base(pageBll, Enums.MenuList.Login)
        {
            _loginBLL = LoginBLL;
        }
        public ActionResult Index()
        {
            var model =new LoginModel();

            return View(model);
        }
        public LoginModel Init(LoginModel model)
        {
            model.CurrentUser = CurrentUser;
            model.Menu = Enums.GetEnumDescription(Enums.MenuList.Login);
            model.Tittle = "Setting Akun";
            return model;
        }
        [HttpPost]
        public ActionResult Index(LoginModel login)
        {
            if (ModelState.IsValid)
            {
                var Dto = Mapper.Map<LoginDto>(login);
                var GetData = _loginBLL.VerificationLogin(Dto);
                if(GetData != null)
                {
                    CurrentUser = new LoginModel();   
                    CurrentUser.FIRST_NAME = GetData.FIRST_NAME;
                    CurrentUser.LAST_NAME = GetData.LAST_NAME;
                    CurrentUser.PASSWORD = GetData.PASSWORD;
                    CurrentUser.STATUS = GetData.STATUS;
                    CurrentUser.USERNAME = GetData.USERNAME;
                    CurrentUser.USER_ID = GetData.USER_ID;
                    CurrentUser.EMAIL = GetData.EMAIL;
                    CurrentUser.LAST_ONLINE = GetData.LAST_ONLINE;
                    CurrentUser.POSITION = GetData.POSITION;
                    CurrentUser.ROLE_ID = GetData.ROLE_ID;

                    _loginBLL.SetLastOnline(CurrentUser.USER_ID);

                    return RedirectToAction("Index","Home");
                }
            }
            login.ErrorMessage = "User id or password is not match";
            return View(login);
        }
        public ActionResult ChangePassword()
        {
            var model = Mapper.Map<LoginModel>(_loginBLL.GetById(CurrentUser.USER_ID));
            
            return View(Init(model));
        }
        [HttpPost]
        public ActionResult ChangePassword(string USER_ID, string PasswordBaru)
        {
            try
            {
                _loginBLL.ChangePassword(USER_ID, PasswordBaru);
                _loginBLL.SetLastOnline(CurrentUser.USER_ID);
                CurrentUser = null;
                return RedirectToAction("Index", "Login");
            }
            catch (Exception)
            {
                var model = Mapper.Map<LoginModel>(_loginBLL.GetById(CurrentUser.USER_ID));
                AddMessageInfo("Gagal Ubah Password", Enums.MessageInfoType.Error);
                return View(Init(model));
            }
        }
        public ActionResult SignOut()
        {
            _loginBLL.SetLastOnline(CurrentUser.USER_ID);
            CurrentUser = null;
            return RedirectToAction("Index", "Login");
        }
        public ActionResult MessageInfo()
        {
            var model = GetListMessageInfo();
            return PartialView("_MessageInfo", model);
        }
    }
}