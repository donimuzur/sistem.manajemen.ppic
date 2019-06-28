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
    public class MstUomBLL : IMstUomBLL
    {
        private IUnitOfWork _uow;
        private IMstUomServices _mstUomServices;
        public MstUomBLL(IUnitOfWork Uow)
        {
            _uow = Uow;
            _mstUomServices = new MstUomServices(_uow);
        }
        public List<KemasanDto> GetAll()
        {
            var Data = _mstUomServices.GetAll();
            var ReData = Mapper.Map<List<KemasanDto>>(Data);

            return ReData;
        }
        public KemasanDto GetById(object Id)
        {
            var Data = _mstUomServices.GetById(Id);
            var ReData = Mapper.Map<KemasanDto>(Data);

            return ReData;
        }
        public void Save(KemasanDto model)
        {
            try
            {
                var db = Mapper.Map<MST_UOM>(model);
                _mstUomServices.Save(db);
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
                var db = Mapper.Map<MST_UOM>(model);
                var Login = Mapper.Map<Login>(LoginDto);
                _mstUomServices.Save(db, Login);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
