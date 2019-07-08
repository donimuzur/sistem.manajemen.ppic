using AutoMapper;
using sistem.manajemen.ppic.bll.IBLL;
using sistem.manajemen.ppic.core;
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
    public class TrnSpbBLL: ITrnSpbBLL
    {
        private ITrnSpbServices _trnSpbServices;
        private IUnitOfWork _uow;
        public TrnSpbBLL(IUnitOfWork Uow)
        {
            _uow = Uow;
            _trnSpbServices = new TrnSpbServices(_uow);
        }
        public List<TrnSpbDto> GetAll()
        {
            var Data = _trnSpbServices.GetAll();
            var ReData = Mapper.Map<List<TrnSpbDto>>(Data);

            return ReData;
        }
        public TrnSpbDto GetById(object Id)
        {
            var Data = _trnSpbServices.GetById(Id);
            var ReData = Mapper.Map<TrnSpbDto>(Data);

            return ReData;
        }
        public TrnSpbDto GetBySPB(string SPB)
        {
            var Data = _trnSpbServices.GetAll().Where(x => x.NO_SPB.ToUpper() == SPB.ToUpper()).FirstOrDefault();
            var ReData = Mapper.Map<TrnSpbDto>(Data);

            return ReData;
        }
        public void Save(TrnSpbDto model)
        {
            try
            {
                var db = Mapper.Map<TRN_SPB>(model);
                _trnSpbServices.Save(db, new Login { USERNAME="SYSTEM",USER_ID="SYSTEM"});
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Savelist(List<TrnSpbDto> model, LoginDto LoginDto)
        {
            try
            {
                var db = Mapper.Map<List<TRN_SPB>>(model);
                _trnSpbServices.SaveList(db, new Login { USERNAME = "SYSTEM", USER_ID = "SYSTEM" });
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Save(TrnSpbDto model, LoginDto LoginDto)
        {
            try
            {
                var db = Mapper.Map<TRN_SPB>(model);
                var Login = Mapper.Map<Login>(LoginDto);
                _trnSpbServices.Save(db, Login);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void CloseSpb(string NoSpb)
        {
            try
            {
                var db = Mapper.Map<TRN_SPB>(GetBySPB(NoSpb));
                db.STATUS = Enums.StatusDocument.Closed;
                _trnSpbServices.Save(db,new Login { USERNAME="SYSTEM", USER_ID="SYSTEM"});
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
