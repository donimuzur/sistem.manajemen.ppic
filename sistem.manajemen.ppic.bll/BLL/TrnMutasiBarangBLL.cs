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
    public class TrnMutasiBarangBLL : ITrnMutasiBarangBLL
    {

        private IUnitOfWork _uow;
        private ITrnMutasiBarangServices _trnMutasiBarangServcies;
        public TrnMutasiBarangBLL()
        {
            _uow = new SqlUnitOfWork();
            _trnMutasiBarangServcies = new TrnMutasiBarangServices(_uow);
        }
        public List<TrnMutasiBarangDto> GetAll()
        {
            try
            {
                var Data = _trnMutasiBarangServcies.GetAll();
                var ReData = Mapper.Map<List<TrnMutasiBarangDto>>(Data);

                return ReData;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public TrnMutasiBarangDto GetById(object Id)
        {
            try
            {
                var Data = _trnMutasiBarangServcies.GetById(Id);
                var ReData = Mapper.Map<TrnMutasiBarangDto>(Data);

                return ReData;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public TrnMutasiBarangDto GetByNama(string NamaBarang)
        {
            try
            {
                var Data = _trnMutasiBarangServcies.GetAll().Where(x => x.NAMA_BARANG.ToUpper() == NamaBarang.ToUpper()).FirstOrDefault();
                var ReData = Mapper.Map<TrnMutasiBarangDto>(Data);

                return ReData;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public void Save(TrnMutasiBarangDto model)
        {
            try
            {
                var db = Mapper.Map<TRN_MUTASI_BARANG>(model);
                _trnMutasiBarangServcies.Save(db);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Save(TrnMutasiBarangDto model, LoginDto LoginDto)
        {
            try
            {
                var db = Mapper.Map<TRN_MUTASI_BARANG>(model);
                var Login = Mapper.Map<Login>(LoginDto);
                _trnMutasiBarangServcies.Save(db, Login);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
