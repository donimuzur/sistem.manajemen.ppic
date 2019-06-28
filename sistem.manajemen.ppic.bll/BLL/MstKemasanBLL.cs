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
    public class MstKemasanBLL : IMstKemasanBLL
    {
        private IUnitOfWork _uow;
        private IMstKemasanServices _mstKemasanServices;
        public MstKemasanBLL(IUnitOfWork Uow)
        {
            _uow = Uow;
            _mstKemasanServices = new MstKemasanServices(_uow);
        }
        public List<KemasanDto> GetAll()
        {
            var Data = _mstKemasanServices.GetAll();
            var ReData = Mapper.Map<List<KemasanDto>>(Data);

            return ReData;
        }
        public KemasanDto GetById(object Id)
        {
            var Data = _mstKemasanServices.GetById(Id);
            var ReData = Mapper.Map<KemasanDto>(Data);

            return ReData;
        }
        public void Save(KemasanDto model)
        {
            try
            {
                var db = Mapper.Map<MST_KEMASAN>(model);
                _mstKemasanServices.Save(db);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Save(KemasanDto model, LoginDto LoginDto)
        {
            try
            {
                var db = Mapper.Map<MST_KEMASAN>(model);
                var Login = Mapper.Map<Login>(LoginDto);
                _mstKemasanServices.Save(db, Login);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
