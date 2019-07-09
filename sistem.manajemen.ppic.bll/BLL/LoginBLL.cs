using AutoMapper;
using sistem.manajemen.ppic.bll.IBLL;
using sistem.manajemen.ppic.dal;
using sistem.manajemen.ppic.dal.IServices;
using sistem.manajemen.ppic.dal.Services;
using sistem.manajemen.ppic.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.bll
{
    public class LoginBLL:ILoginBLL
    {
        private IUnitOfWork _uow;
        private ILoginServices _loginServices;
        
        public LoginBLL(IUnitOfWork Uow)
        {
            _uow = Uow;
            _loginServices = new LoginServices(_uow);
        }
        public List<LoginDto> GetAll()
        {
            var Data = _loginServices.GetAll().ToList();
            var ReData = Mapper.Map<List<LoginDto>>(Data);
            return ReData;
        }
        public LoginDto GetById(object Id)
        {
            var Data = _loginServices.GetById(Id);
            var ReData = Mapper.Map<LoginDto>(Data);
            return ReData;
        }
        public LoginDto VerificationLogin(LoginDto Login)
        {
            var Data = _loginServices.GetById(Login.USER_ID);
            var ReData = Mapper.Map<LoginDto>(Data);
            
            if(ReData != null && Login.USER_ID.ToLower() == ReData.USER_ID.ToLower() && Login.PASSWORD.ToLower() == ReData.PASSWORD.ToLower())
            {
                return ReData;
            }
            else
            {
                return null;
            }
        }
        public void ChangePassword(string UserId, string PasswordBaru)
        {
            var GetData = _loginServices.GetById(UserId);
            GetData.PASSWORD = PasswordBaru;
            _loginServices.Save(GetData);
        }
        public void SetLastOnline(string UserId)
        {
            var GetData = _loginServices.GetById(UserId);
            GetData.LAST_ONLINE = DateTime.Now;
            _loginServices.Save(GetData);
        }
        public DateTime? GetLastOnline(string UserId)
        {
            var GetData = _loginServices.GetById(UserId);

            return GetData == null ? null : GetData.LAST_ONLINE;
        }
    }
}
