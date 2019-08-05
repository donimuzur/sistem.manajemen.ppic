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
        public List<MstUomDto> GetAll()
        {
            var Data = _mstUomServices.GetAll();
            var ReData = Mapper.Map<List<MstUomDto>>(Data);

            return ReData;
        }
        public MstUomDto GetById(object Id)
        {
            var Data = _mstUomServices.GetById(Id);
            var ReData = Mapper.Map<MstUomDto>(Data);

            return ReData;
        }
        public MstUomDto GetByUom(string Uom)
        {
            var Data = _mstUomServices.GetAll().Where(x=> x.SATUAN.ToUpper() == Uom.ToUpper()).FirstOrDefault();
            var ReData = Mapper.Map<MstUomDto>(Data);

            return ReData;
        }
        public void Save(MstUomDto model)
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
        public MstUomDto Save(MstUomDto model, LoginDto LoginDto)
        {
            try
            {
                var db = Mapper.Map<MST_UOM>(model);
                var Login = Mapper.Map<Login>(LoginDto);
                db =_mstUomServices.Save(db, Login);
                return Mapper.Map<MstUomDto>(db);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
