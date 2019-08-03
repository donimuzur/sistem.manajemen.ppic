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
    public class MstKemasanBLL : IMstKemasanBLL
    {
        private IUnitOfWork _uow;
        private IMstKemasanServices _mstKemasanServices;
        public MstKemasanBLL(IUnitOfWork Uow)
        {
            _uow = Uow;
            _mstKemasanServices = new MstKemasanServices(_uow);
        }
        public List<MstKemasanDto> GetAll()
        {
            var Data = _mstKemasanServices.GetAll();
            var ReData = Mapper.Map<List<MstKemasanDto>>(Data);

            return ReData;
        }
        public List<MstKemasanDto> GetActiveAll()
        {
            var Data = _mstKemasanServices.GetAll().Where(x => x.STATUS != (int) Enums.StatusDocument.Cancel).ToList();
            var ReData = Mapper.Map<List<MstKemasanDto>>(Data);

            return ReData;
        }
        public MstKemasanDto GetById(object Id)
        {
            var Data = _mstKemasanServices.GetById(Id);
            var ReData = Mapper.Map<MstKemasanDto>(Data);

            return ReData;
        }
        public MstKemasanDto GetByNama(string Nama)
        {
            var Data = _mstKemasanServices.GetAll().Where(x=> x.KEMASAN.ToUpper() == Nama.ToUpper()).FirstOrDefault();
            var ReData = Mapper.Map<MstKemasanDto>(Data);

            return ReData;
        }
        public void Save(MstKemasanDto model)
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
        public MstKemasanDto Save(MstKemasanDto model, LoginDto LoginDto)
        {
            try
            {
                var db = Mapper.Map<MST_KEMASAN>(model);
                var Login = Mapper.Map<Login>(LoginDto);
                db = _mstKemasanServices.Save(db, Login);
                return Mapper.Map<MstKemasanDto>(db);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
