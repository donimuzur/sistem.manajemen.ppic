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
    public class MstBahanBakuBLL : IMstBahanBakuBLL
    {
        private IUnitOfWork _uow;
        private IMstBahanBakuServices _mstBahanBakuServices;
        public MstBahanBakuBLL(IUnitOfWork Uow)
        {
            _uow = Uow;
            _mstBahanBakuServices = new MstBahanBakuServices(_uow);
        }
        public List<MstBahanBakuDto> GetAll()
        {
            var Data = _mstBahanBakuServices.GetAll();
            var ReData = Mapper.Map<List<MstBahanBakuDto>>(Data);

            return ReData;
        }
        public MstBahanBakuDto GetById(object Id)
        {
            var Data = _mstBahanBakuServices.GetById(Id);
            var ReData = Mapper.Map<MstBahanBakuDto>(Data);

            return ReData;
        }
        public MstBahanBakuDto GetByNama(string NamaBarang)
        {
            try
            {
                var Data = _mstBahanBakuServices.GetAll().Where(x => x.NAMA_BARANG.ToUpper() == NamaBarang.ToUpper()).FirstOrDefault();
                var ReData = Mapper.Map<MstBahanBakuDto>(Data);

                return ReData;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Save(MstBahanBakuDto model)
        {
            try
            {
                var db = Mapper.Map<MST_BAHAN_BAKU>(model);
                _mstBahanBakuServices.Save(db);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Save(MstBahanBakuDto model, LoginDto LoginDto)
        {
            try
            {
                var db = Mapper.Map<MST_BAHAN_BAKU>(model);
                var Login = Mapper.Map<Login>(LoginDto);
                _mstBahanBakuServices.Save(db, Login);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool TambahSaldo(int IdBarang, decimal Jumlah)
        {
            try
            {
                var Db = _mstBahanBakuServices.GetById(IdBarang);
                Db.STOCK = Db.STOCK + Jumlah;

                _mstBahanBakuServices.Save(Db, new Login { FIRST_NAME = "SYSTEM", USERNAME = "SYSTEM" });

                return true;
            }
            catch (Exception exp)
            {

                LogError.LogError.WriteError(exp);
                return false;
            }
        }

        public bool KurangSaldo(int IdBarang, decimal Jumlah)
        {
            try
            {
                var Db = _mstBahanBakuServices.GetById(IdBarang);
                Db.STOCK = Db.STOCK - Jumlah;

                _mstBahanBakuServices.Save(Db, new Login { FIRST_NAME = "SYSTEM", USERNAME = "SYSTEM" });

                return true;
            }
            catch (Exception exp)
            {
                LogError.LogError.WriteError(exp);
                return false;
            }
        }
    }
}
