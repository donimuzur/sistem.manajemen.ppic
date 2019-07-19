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
    public class TrnPenerimaanBarangBLL : ITrnPenerimaanBarangBLL
    {
        private IUnitOfWork _uow;
        private ITrnPenerimaanBarangServices _trnPenerimaanBarangBLL;
        public TrnPenerimaanBarangBLL()
        {
            _uow = new SqlUnitOfWork();
            _trnPenerimaanBarangBLL = new TrnPenerimaanBarangServices(_uow);
        }
        public List<TrnPenerimaanBarangDto> GetActiveAll()
        {
            try
            {
                var Data = _trnPenerimaanBarangBLL.GetActiveAll();
                var ReData = Mapper.Map<List<TrnPenerimaanBarangDto>>(Data);

                return ReData;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<TrnPenerimaanBarangDto> GetAll()
        {
            try
            {
                var Data = _trnPenerimaanBarangBLL.GetAll();
                var ReData = Mapper.Map<List<TrnPenerimaanBarangDto>>(Data);

                return ReData;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public TrnPenerimaanBarangDto GetById(object Id)
        {
            try
            {
                var Data = _trnPenerimaanBarangBLL.GetById(Id);
                var ReData = Mapper.Map<TrnPenerimaanBarangDto>(Data);

                return ReData;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public TrnPenerimaanBarangDto GetByNama(string NamaBarang)
        {
            try
            {
                var Data = _trnPenerimaanBarangBLL.GetAll().Where(x => x.NAMA_BARANG.ToUpper() == NamaBarang.ToUpper()).FirstOrDefault();
                var ReData = Mapper.Map<TrnPenerimaanBarangDto>(Data);

                return ReData;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public void Save(TrnPenerimaanBarangDto model)
        {
            try
            {
                var db = Mapper.Map<TRN_PENERIMAAN_BARANG>(model);
                _trnPenerimaanBarangBLL.Save(db);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Save(TrnPenerimaanBarangDto model, LoginDto LoginDto)
        {
            try
            {
                var db = Mapper.Map<TRN_PENERIMAAN_BARANG>(model);
                var Login = Mapper.Map<Login>(LoginDto);
                _trnPenerimaanBarangBLL.Save(db, Login);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Delete(int id, string Remarks)
        {
            try
            {
                _trnPenerimaanBarangBLL.Delete(id, Remarks);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
