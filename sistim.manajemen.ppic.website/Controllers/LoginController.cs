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

                    return RedirectToAction("Index","Home");
                }
            }
            login.ErrorMessage = "User id or password is not match";
            return View(login);
        }
        public ActionResult SignOut()
        {
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