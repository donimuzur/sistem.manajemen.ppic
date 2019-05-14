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
    public class TrnHasilProduksiBLL : ITrnHasilProduksiBLL
    {
        private ITrnHasilProduksiServices _trnHasilProduksiServices;
        private IMstBarangJadiServices _mstBarangJadiServices;
        private IUnitOfWork _uow;
        public TrnHasilProduksiBLL(IUnitOfWork Uow)
        {
            _uow = Uow;
            _trnHasilProduksiServices = new TrnHasilProduksiServices(_uow);
            _mstBarangJadiServices = new MstBarangJadiServices(_uow);
        }
        public List<TrnHasilProduksiDto> GetAll()
        {
            var Data = _trnHasilProduksiServices.GetAll();
            var ReData = Mapper.Map<List<TrnHasilProduksiDto>>(Data);

            return ReData;
        }
        public TrnHasilProduksiDto GetById(object Id)
        {
            var Data = _trnHasilProduksiServices.GetById(Id);
            var ReData = Mapper.Map<TrnHasilProduksiDto>(Data);

            return ReData;
        }
        public TrnHasilProduksiDto GetByBarang(int IdBarang)
        {
            var Data = _trnHasilProduksiServices.GetAll().Where(x => x.ID_BARANG== IdBarang).FirstOrDefault();
            var ReData = Mapper.Map<TrnHasilProduksiDto>(Data);

            return ReData;
        }
        public void Save(TrnHasilProduksiDto model)
        {
            try
            {
                var db = Mapper.Map<TRN_HASIL_PRODUKSI>(model);
                _trnHasilProduksiServices.Save(db);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Save(TrnHasilProduksiDto model, LoginDto LoginDto)
        {
            try
            {
                var db = Mapper.Map<TRN_HASIL_PRODUKSI>(model);
                var Login = Mapper.Map<Login>(LoginDto);
                
                _trnHasilProduksiServices.Save(db, Login);
                
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
