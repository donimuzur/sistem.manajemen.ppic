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
    public class TrnSuratPermintaanBahanBakuBLL : ITrnSuratPermintaanBahanBakuBLL
    {
        private ITrnSuratPermintaanBahanBakuServices _trnSuratPermintaanBahanBakuServices;
        private IUnitOfWork _uow;
        public TrnSuratPermintaanBahanBakuBLL()
        {
            _uow = new SqlUnitOfWork();
            _trnSuratPermintaanBahanBakuServices = new TrnSuratPermintaanBahanBakuServices(_uow);
        }
        public List<TrnSuratPermintaanBahanBakuDto> GetAll()
        {
            try
            {
                var data = _trnSuratPermintaanBahanBakuServices.GetAll();
                var redata = Mapper.Map<List<TrnSuratPermintaanBahanBakuDto>>(data);
                return redata;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<TrnSuratPermintaanBahanBakuDto> GetActiveAll()
        {
            try
            {
                var data = _trnSuratPermintaanBahanBakuServices.GetActiveAll();
                var reData = Mapper.Map<List<TrnSuratPermintaanBahanBakuDto>>(data);
                return reData;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public TrnSuratPermintaanBahanBakuDto GetById(int Id)
        {
            try
            {
                var data = _trnSuratPermintaanBahanBakuServices.GetById(Id);
                var reData = Mapper.Map<TrnSuratPermintaanBahanBakuDto>(data);
                return reData;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Save(TrnSuratPermintaanBahanBakuDto Dto)
        {
            try
            {
                var Db = Mapper.Map<TRN_SURAT_PERMINTAAN_BAHAN_BAKU>(Dto);
                _trnSuratPermintaanBahanBakuServices.Save(Db);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public TrnSuratPermintaanBahanBakuDto Save(TrnSuratPermintaanBahanBakuDto Dto, LoginDto Login)
        {
            try
            {
                var Db = Mapper.Map<TRN_SURAT_PERMINTAAN_BAHAN_BAKU>(Dto);
                Db = _trnSuratPermintaanBahanBakuServices.Save(Db, Mapper.Map<Login>(Login));

                return Mapper.Map<TrnSuratPermintaanBahanBakuDto>(Db);
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
                _trnSuratPermintaanBahanBakuServices.Delete(id, Remarks);
            }
            catch (Exception)
            {
                throw;
            }
        }


        #region --- SuratPermintaanBahanBakuDetais ---
        public List<TrnSuratPermintaanBahanBakuDetailsDto> GetAllDetails()
        {
            try
            {
                var data = _trnSuratPermintaanBahanBakuServices.GetAllDetails();

                return Mapper.Map<List<TrnSuratPermintaanBahanBakuDetailsDto>>(data);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public TrnSuratPermintaanBahanBakuDetailsDto GetDetailsById(int Id)
        {
            try
            {
                var data = _trnSuratPermintaanBahanBakuServices.GetDetailsById(Id);
                return Mapper.Map<TrnSuratPermintaanBahanBakuDetailsDto>(data);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<TrnSuratPermintaanBahanBakuDetailsDto> GetAllDetailsByMasterId(int MasterId)
        {
            try
            {
                var data = _trnSuratPermintaanBahanBakuServices.GetAllDetailsByMasterId(MasterId);
                return Mapper.Map<List<TrnSuratPermintaanBahanBakuDetailsDto>>(data);
            }
            catch (Exception)
            {
                throw;
            }
            #endregion
        }
    }
}
