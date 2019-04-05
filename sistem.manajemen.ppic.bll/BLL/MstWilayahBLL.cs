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
    public class MstWilayahBLL:IMstWilayahBLL
    {
        private IUnitOfWork _uow;
        private IMstWilayahServices _mstWilayahServices;
        public MstWilayahBLL(IUnitOfWork Uow)
        {
            _uow = Uow;
            _mstWilayahServices = new MstWilayahServices(_uow);
        }
        public List<MstWilayahDto> GetAll()
        {
            var Data = _mstWilayahServices.GetAll();
            var ReData = Mapper.Map<List<MstWilayahDto>>(Data);

            return ReData;
        }
        public MstWilayahDto GetById(object Id)
        {
            var Data = _mstWilayahServices.GetById(Id);
            var ReData = Mapper.Map<MstWilayahDto>(Data);

            return ReData;
        }
        public void Save(MstWilayahDto model)
        {
            try
            {
                var db = Mapper.Map<MST_WILAYAH>(model);
                _mstWilayahServices.Save(db);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Save(MstWilayahDto model,LoginDto LoginDto)
        {
            try
            {
                var db = Mapper.Map<MST_WILAYAH>(model);
                var Login = Mapper.Map<Login>(LoginDto);
                _mstWilayahServices.Save(db,Login);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
