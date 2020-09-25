using AutoMapper;
using sistem.manajemen.ppic.dal;
using sistem.manajemen.ppic.dal.Services;
using sistem.manajemen.ppic.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.bll
{
    public class TrnSlipTimbanganBLL : ITrnSlipTimbanganBLL
    {
        private TrnSlipTimbanganServices _trnSlipTimbanganServices;
        private IUnitOfWork _uow;
        public TrnSlipTimbanganBLL(IUnitOfWork Uow)
        {
            _uow = Uow;
            _trnSlipTimbanganServices = new TrnSlipTimbanganServices(_uow);
        }
        public List<TrnSlipTimbanganDto> GetAll()
        {
            var Data = _trnSlipTimbanganServices.GetAll();
            var ReData = Mapper.Map<List<TrnSlipTimbanganDto>>(Data);

            return ReData;
        }
        public TrnSlipTimbanganDto GetById(object Id)
        {
            var Data = _trnSlipTimbanganServices.GetById(Id);
            var ReData = Mapper.Map<TrnSlipTimbanganDto>(Data);

            return ReData;
        }
        public TrnSlipTimbanganDto GetBySuratJalan(string NoSuratJalan)
        {
            var Data = _trnSlipTimbanganServices.GetAll().Where(x => x.NO_SURAT_JALAN != null && x.NO_SURAT_JALAN.ToUpper() == NoSuratJalan.ToUpper()).FirstOrDefault();
            var ReData = Mapper.Map<TrnSlipTimbanganDto>(Data);

            return ReData;
        }
        public void Save(TrnSlipTimbanganDto model)
        {
            try
            {
                var db = Mapper.Map<SLIP_TIMBANGAN>(model);
                _trnSlipTimbanganServices.Save(db);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public TrnSlipTimbanganDto Save(TrnSlipTimbanganDto model, LoginDto LoginDto)
        {
            try
            {
                var db = Mapper.Map<SLIP_TIMBANGAN>(model);
                var Login = Mapper.Map<Login>(LoginDto);
                db = _trnSlipTimbanganServices.Save(db, Login);
                return Mapper.Map<TrnSlipTimbanganDto>(db);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
