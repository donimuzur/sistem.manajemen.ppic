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
    public class TrnSuratPerintahProduksiBLL : ITrnSuratPerintahProduksiBLL
    {
        private IUnitOfWork _uow;
        private ITrnSuratPerintahProduksiServices _trnSuratPerintahProduksiServices;
        public TrnSuratPerintahProduksiBLL()
        {
            _uow = new SqlUnitOfWork();
            _trnSuratPerintahProduksiServices = new TrnSuratPerintahProduksiServices(_uow);
        }
        public List<TrnSuratPerintahProduksiDto> GetActiveAll()
        {
            try
            {
                var Data = _trnSuratPerintahProduksiServices.GetActiveAll();
                var ReData = Mapper.Map<List<TrnSuratPerintahProduksiDto>>(Data);

                return ReData;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public List<TrnSuratPerintahProduksiDto> GetAll()
        {
            try
            {
                var Data = _trnSuratPerintahProduksiServices.GetAll();
                var ReData = Mapper.Map<List<TrnSuratPerintahProduksiDto>>(Data);

                return ReData;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public TrnSuratPerintahProduksiDto GetById(object Id)
        {
            try
            {
                var Data = _trnSuratPerintahProduksiServices.GetById(Id);
                var ReData = Mapper.Map<TrnSuratPerintahProduksiDto>(Data);

                return ReData;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public TrnSuratPerintahProduksiDto GetByNama(string NamaBarang)
        {
            try
            {
                var Data = _trnSuratPerintahProduksiServices.GetAll().Where(x => x.NAMA_PRODUK.ToUpper() == NamaBarang.ToUpper()).FirstOrDefault();
                var ReData = Mapper.Map<TrnSuratPerintahProduksiDto>(Data);

                return ReData;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public TrnSuratPerintahProduksiDto GetByNoSurat(string NoSurat)
        {
            try
            {
                var Data = _trnSuratPerintahProduksiServices.GetAll().Where(x => x.NO_SURAT.ToUpper() == NoSurat.ToUpper()).FirstOrDefault();
                var ReData = Mapper.Map<TrnSuratPerintahProduksiDto>(Data);

                return ReData;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public void Save(TrnSuratPerintahProduksiDto model)
        {
            try
            {
                var db = Mapper.Map<TRN_SURAT_PERINTAH_PRODUKSI>(model);
                _trnSuratPerintahProduksiServices.Save(db);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Save(TrnSuratPerintahProduksiDto model, LoginDto LoginDto)
        {
            try
            {
                var db = Mapper.Map<TRN_SURAT_PERINTAH_PRODUKSI>(model);
                var Login = Mapper.Map<Login>(LoginDto);
                _trnSuratPerintahProduksiServices.Save(db, Login);
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
                _trnSuratPerintahProduksiServices.Delete(id, Remarks);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
