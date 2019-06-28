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
        public List<TrnPengirimanMasterDto> GetAll()
        {
            var Data = _trnPengirimanServices.GetAll();
            var ReData = Mapper.Map<List<TrnPengirimanMasterDto>>(Data);

            return ReData;
        }
        public List<TrnPengirimanMasterDto> GetAllByDoAndSPB(int Do, string NoSPB)
        {
            var Data = _trnPengirimanServices.GetAll().Where(x => x.NO_DO == Do && x.NO_SPB.ToUpper() == NoSPB.ToUpper()).ToList();
            var ReData = Mapper.Map<List<TrnPengirimanMasterDto>>(Data);

            return ReData;
        }
        public TrnPengirimanMasterDto GetById(object Id)
        {
            var Data = _trnPengirimanServices.GetTrnPengirimanMasterById(Id);
            var ReData = Mapper.Map<TrnPengirimanMasterDto>(Data);

            return ReData;
        }
        public bool Save(TrnPengirimanMasterDto Dto)
        {
            try
            {
                _trnPengirimanServices.Save(Mapper.Map<TRN_PENGIRIMAN_MASTER>(Dto));

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Save(TrnPengirimanMasterDto Dto, LoginDto Login)
        {
            try
            {
                _trnPengirimanServices.Save(Mapper.Map<TRN_PENGIRIMAN_MASTER>(Dto)
                    , Mapper.Map<Login>(Login));

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void SaveChanges()
        {
            _uow.SaveChanges();
        }
        public int GenerateNoSJ()
        {
            try
            {
                var PengirimanList = _trnPengirimanServices.GetAll().Where(x => x.TANGGAL.Month == DateTime.Now.Month && x.TANGGAL.Year == DateTime.Now.Year).ToList();
                var GetNomorSJCount = PengirimanList.Count();
                GetNomorSJCount = GetNomorSJCount + 1;

                return GetNomorSJCount;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public decimal GetAkumulasi(int Do, string NoSPB)
        {
            try
            {
                var GetAll = _trnPengirimanServices.GetAllDetails().Where(x => x.TRN_PENGIRIMAN_MASTER != null && x.TRN_PENGIRIMAN_MASTER.NO_DO == Do && x.TRN_PENGIRIMAN_MASTER.NO_SPB.ToUpper() == NoSPB.ToUpper());
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
