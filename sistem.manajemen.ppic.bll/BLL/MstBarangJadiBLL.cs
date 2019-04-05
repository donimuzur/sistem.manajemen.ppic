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
    public class MstBarangJadiBLL : IMstBarangJadiBLL
    {
        private IUnitOfWork _uow;
        private IMstBarangJadiServices _mstBarangServices;
        public MstBarangJadiBLL (IUnitOfWork Uow)
        {
            _uow = Uow;
            _mstBarangServices = new MstBarangJadiServices(_uow);
        }
        public List<MstBarangJadiDto> GetAll()
        {
            var Data = _mstBarangServices.GetAll();
            var ReData = Mapper.Map<List<MstBarangJadiDto>>(Data);

            return ReData;
        }
        public MstBarangJadiDto GetById(object Id)
        {
            var Data = _mstBarangServices.GetById(Id);
            var ReData = Mapper.Map<MstBarangJadiDto>(Data);

            return ReData;
        }
        public void Save(MstBarangJadiDto model)
        {
            try
            {
                var db = Mapper.Map<MST_BARANG_JADI>(model);
                _mstBarangServices.Save(db);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Save(MstBarangJadiDto model,LoginDto LoginDto)
        {
            try
            {
                var db = Mapper.Map<MST_BARANG_JADI>(model);
                var Login= Mapper.Map<Login>(LoginDto);
                _mstBarangServices.Save(db,Login);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
