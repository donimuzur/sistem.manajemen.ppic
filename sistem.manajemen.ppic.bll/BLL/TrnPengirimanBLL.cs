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

namespace sistem.manajemen.ppic.BLL
{
    public class TrnPengirimanBLL : ITrnPengirimanBLL
    {
        private ITrnPengirimanServices _trnPengirimanServices;
        private IUnitOfWork _uow;
        public TrnPengirimanBLL()
        {
            _uow = new SqlUnitOfWork();
            _trnPengirimanServices = new TrnPengirimanServices(_uow);
        }
        public List<TrnPengirimanDto> GetAll()
        {
            try
            {
                var Data = _trnPengirimanServices.GetAll();
                var ReData = Mapper.Map<List<TrnPengirimanDto>>(Data);

                return ReData;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<TrnPengirimanDto> GetActiveAll()
        {
            try
            {
                var Data = _trnPengirimanServices.GetActiveAll();
                var ReData = Mapper.Map<List<TrnPengirimanDto>>(Data);

                return ReData;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<TrnPengirimanDto> GetAllByDoAndSPB(int Do, string NoSPB)
        {
            try
            {

                var Data = _trnPengirimanServices.GetAll().Where(x => x.NO_DO == Do && x.NO_SPB.ToUpper() == NoSPB.ToUpper()).ToList();
                var ReData = Mapper.Map<List<TrnPengirimanDto>>(Data);

                return ReData;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public TrnPengirimanDto GetById(object Id)
        {
            try
            {
                var Data = _trnPengirimanServices.GetTrnPengirimanMasterById(Id);
                var ReData = Mapper.Map<TrnPengirimanDto>(Data);

                return ReData;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Save(TrnPengirimanDto Dto)
        {
            try
            {
                _trnPengirimanServices.Save(Mapper.Map<TRN_PENGIRIMAN>(Dto));
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Save(TrnPengirimanDto Dto, LoginDto Login)
        {
            try
            {
                _trnPengirimanServices.Save(Mapper.Map<TRN_PENGIRIMAN>(Dto)
                    , Mapper.Map<Login>(Login));
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
                _trnPengirimanServices.Delete(id, Remarks);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public decimal? GetAkumulasi(int Do, string NoSPB)
        {
            try
            {
                var GetAll = _trnPengirimanServices.GetAll().Where(x =>  x.NO_DO == Do && x.NO_SPB.ToUpper() == NoSPB.ToUpper() && x
                                        .STATUS != (int)Enums.StatusDocument.Cancel);
                var data = GetAll.Sum(x => x.KUANTUM);
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
