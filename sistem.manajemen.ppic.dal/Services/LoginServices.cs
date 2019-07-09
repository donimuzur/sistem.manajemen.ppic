﻿using sistem.manajemen.ppic.core;
using sistem.manajemen.ppic.dal;
using sistem.manajemen.ppic.dal.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dal.Services
{
    public class LoginServices : ILoginServices
    {
        private IUnitOfWork _uow;
        private IGenericRepository<Login> _loginRepo;
        public LoginServices(IUnitOfWork uow)
        {
            _uow = uow;
            _loginRepo = _uow.GetGenericRepository<Login>();
        }
        public List<Login> GetAll()
        {
            var Data = _loginRepo.Get().ToList();
            return Data;
        }
        public Login GetById(object id)
        {
            var Data = _loginRepo.GetByID(id);
            return Data;
        }
        public void Save(Login Db)
        {
            try
            {
                _loginRepo.InsertOrUpdate(Db);
                _uow.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Save(Login Db, Login Login)
        {
            try
            {
                _loginRepo.InsertOrUpdate(Db, Login, Enums.MenuList.Login);
                _uow.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
