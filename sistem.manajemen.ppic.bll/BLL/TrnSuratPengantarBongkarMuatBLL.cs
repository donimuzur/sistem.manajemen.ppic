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
    public class TrnSuratPengantarBongkarMuatBLL : ITrnSuratPengantarBongkarMuatBLL
    {
        private IUnitOfWork _uow;
        private ITrnSuratPengantarBongkarMuatServices _trnSuratPengantarBongkarMuatServices;
        public TrnSuratPengantarBongkarMuatBLL()
        {
            _uow = new SqlUnitOfWork();
            _trnSuratPengantarBongkarMuatServices = new TrnSuratPengantarBongkarMuatServices(_uow);
        }
        public List<TrnSuratPengantarBongkarMuatDto> GetActiveAll()
        {
            try
            {
                var Data = _trnSuratPengantarBongkarMuatServices.GetActiveAll();
                var ReData = Mapper.Map<List<TrnSuratPengantarBongkarMuatDto>>(Data);

                return ReData;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        public List<TrnSuratPengantarBongkarMuatDto> GetAll()
        {
            try
            {
                var Data = _trnSuratPengantarBongkarMuatServices.GetAll();
                var ReData = Mapper.Map<List<TrnSuratPengantarBongkarMuatDto>>(Data);

                return ReData;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public TrnSuratPengantarBongkarMuatDto GetById(object Id)
        {
            try
            {
                var Data = _trnSuratPengantarBongkarMuatServices.GetById(Id);
                var ReData = Mapper.Map<TrnSuratPengantarBongkarMuatDto>(Data);

                return ReData;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public TrnSuratPengantarBongkarMuatDto GetByNama(string NamaBarang)
        {
            try
            {
                var Data = _trnSuratPengantarBongkarMuatServices.GetAll().Where(x => x.NAMA_BARANG.ToUpper() == NamaBarang.ToUpper()).FirstOrDefault();
                var ReData = Mapper.Map<TrnSuratPengantarBongkarMuatDto>(Data);

                return ReData;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public void Save(TrnSuratPengantarBongkarMuatDto model)
        {
            try
            {
                var db = Mapper.Map<TRN_SURAT_PENGANTAR_BONGKAR_MUAT>(model);
                _trnSuratPengantarBongkarMuatServices.Save(db);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Save(TrnSuratPengantarBongkarMuatDto model, LoginDto LoginDto)
        {
            try
            {
                var db = Mapper.Map<TRN_SURAT_PENGANTAR_BONGKAR_MUAT>(model);
                var Login = Mapper.Map<Login>(LoginDto);
                _trnSuratPengantarBongkarMuatServices.Save(db, Login);
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
                _trnSuratPengantarBongkarMuatServices.Delete(id, Remarks);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
