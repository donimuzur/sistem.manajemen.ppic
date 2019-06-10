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
        public TrnPengirimanBLL(IUnitOfWork Uow)
        {
            _uow = Uow;
            _trnPengirimanServices = new TrnPengirimanServices(_uow);
        }
        public List<TrnPengirimanDto> GetAll()
        {
            var Data = _trnPengirimanServices.GetAll();
            var ReData = Mapper.Map<List<TrnPengirimanDto>>(Data);

            return ReData;
        }
        public TrnPengirimanDto GetById(object Id)
        {
            var Data = _trnPengirimanServices.GetById(Id);
            var ReData = Mapper.Map<TrnPengirimanDto>(Data);

            return ReData;
        }
        public TrnPengirimanDto GetBySPB(string SPB)
        {
            var Data = _trnPengirimanServices.GetAll().Where(x => x.NO_SPB.ToUpper() == SPB.ToUpper()).FirstOrDefault();
            var ReData = Mapper.Map<TrnPengirimanDto>(Data);

            return ReData;
        }
        public TrnPengirimanDto GetByDo(string DO)
        {
            var Data = _trnPengirimanServices.GetAll().Where(x => x.NO_DO.ToUpper() == DO.ToUpper()).FirstOrDefault();
            var ReData = Mapper.Map<TrnPengirimanDto>(Data);

            return ReData;
        }
        public TrnPengirimanDto GetBySj(string Sj)
        {
            var Data = _trnPengirimanServices.GetAll().Where(x => x.SURAT_JALAN.ToUpper() == Sj.ToUpper()).FirstOrDefault();
            var ReData = Mapper.Map<TrnPengirimanDto>(Data);

            return ReData;
        }
        public TrnPengirimanDto GetBySj(string NoSpb, string Sj)
        {
            var Data = _trnPengirimanServices.GetAll().Where(x => x.SURAT_JALAN.ToUpper() == Sj.ToUpper() && x.NO_SPB.ToUpper() == NoSpb.ToUpper()).FirstOrDefault();
            var ReData = Mapper.Map<TrnPengirimanDto>(Data);

            return ReData;
        }
        public void Save(TrnPengirimanDto model)
        {
            try
            {
                var db = Mapper.Map<TRN_PENGIRIMAN>(model);
                _trnPengirimanServices.Save(db);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Save(TrnPengirimanDto model, LoginDto LoginDto)
        {
            try
            {
                var db = Mapper.Map<TRN_PENGIRIMAN>(model);
                var Login = Mapper.Map<Login>(LoginDto);
                _trnPengirimanServices.Save(db, Login);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public decimal GetAkumulasi(string NoSpb)
        {
            try
            {
                var data = _trnPengirimanServices.GetAll().Where(x=> x.NO_SPB== NoSpb).ToList().Sum(x => x.JUMLAH);
                return data;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
