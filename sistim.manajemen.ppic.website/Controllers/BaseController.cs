using AutoMapper;
using sistem.manajemen.ppic.bll.IBLL;
using sistem.manajemen.ppic.core;
using sistem.manajemen.ppic.dal;
using sistem.manajemen.ppic.website.Code;
using sistem.manajemen.ppic.website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace sistem.manajemen.ppic.website.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        private IPageBLL _pageBLL;
        private Enums.MenuList _menuID;

        public BaseController(IPageBLL pageBll, Enums.MenuList menuID)
        {
            _pageBLL = pageBll;
            _menuID = menuID;
        }
        protected ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        private bool IsAvailableDistrictCookie(string cookieName)
        {
            if (ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains(cookieName))
            {
                return true;
            }
            return false;
        }
        protected void CreateCookie(string cookieName, string district)
        {
            HttpCookie cookie;
            if (!IsAvailableDistrictCookie(cookieName))
            {
                cookie = new HttpCookie(cookieName);
            }
            else
            {
                cookie = ControllerContext.HttpContext.Request.Cookies[cookieName];
            }
            if (cookie != null)
            {
                cookie.Value = district;
                cookie.Expires = DateTime.Now.AddYears(1);
                ControllerContext.HttpContext.Response.Cookies.Add(cookie);
            }
        }
        public LoginModel CurrentUser
        {
            get
            {
                return (LoginModel)Session[Constans.SessionKey.CurrentUser];
            }
            set
            {
                Session[Constans.SessionKey.CurrentUser] = value;
            }
        }
        protected PAGE PageInfo
        {
            get
            {
                return _pageBLL.GetPageByID((int)_menuID);
            }
        }
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var viewResult = filterContext.Result as ViewResult;
            if (viewResult == null)
                return;
            base.OnActionExecuted(filterContext);
        }
        public List<ChangesHistoryModel> GetChangesHistory(int modulId, long formId)
        {
            var data = _pageBLL.GetChangesHistory(modulId, formId);
            return Mapper.Map<List<ChangesHistoryModel>>(data);
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            var descriptor = filterContext.ActionDescriptor;
            var actionName = descriptor.ActionName;
            var controllerName = descriptor.ControllerDescriptor.ControllerName;

            if (controllerName == "Login" && actionName == "Index") return;

            if (CurrentUser == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                   new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
                
                return;
            }
        }
        
        #region MessageInfo
        private List<MessageInfo> ListMessageInfo { get; set; }

        private void AddMessage(MessageInfo messageInfo)
        {
            ListMessageInfo = (List<MessageInfo>)TempData["MessageInfo"] ?? new List<MessageInfo>();
            ListMessageInfo.Add(messageInfo);

            TempData["MessageInfo"] = ListMessageInfo;
        }

        public void AddMessageInfo(MessageInfo messageinfo)
        {
            AddMessage(messageinfo);
        }

        public void AddMessageInfo(List<string> message, Enums.MessageInfoType messageinfotype)
        {
            AddMessage(new MessageInfo(message, messageinfotype));
        }

        public void AddMessageInfo(string message, Enums.MessageInfoType messageinfotype)
        {
            AddMessage(new MessageInfo(new List<string> { message }, messageinfotype));
        }


        public List<BaseModel> GetListMessageInfo()
        {
            var lsModel = new List<BaseModel>();
            ListMessageInfo = (List<MessageInfo>)TempData["MessageInfo"];

            if (ListMessageInfo != null)
                lsModel.AddRange(ListMessageInfo.Select(messageInfo => new BaseModel()
                {
                    MessageTitle = messageInfo.MessageInfoType.ToString(),// EnumsHelper.GetResourceDisplayEnums(messageInfo.MessageInfoType),
                    MessageBody = messageInfo.MessageText
                }));

            return lsModel;
        }
        #endregion
    }
}