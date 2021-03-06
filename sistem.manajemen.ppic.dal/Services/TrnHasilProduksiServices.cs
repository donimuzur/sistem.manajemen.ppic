﻿using sistem.manajemen.ppic.core;
using sistem.manajemen.ppic.dal.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dal.Services
{
    public class TrnHasilProduksiServices : ITrnHasilProduksiServices
    {
        private IGenericRepository<TRN_HASIL_PRODUKSI> _trnHasilProduksiRepo;
        private IUnitOfWork _uow;
        public TrnHasilProduksiServices (IUnitOfWork Uow)
        {
            _uow = Uow;
            _trnHasilProduksiRepo = _uow.GetGenericRepository<TRN_HASIL_PRODUKSI>();
        }
        public List<TRN_HASIL_PRODUKSI> GetAll()
        {
            var data = _trnHasilProduksiRepo.Get().ToList();
            return data;
        }
        public TRN_HASIL_PRODUKSI GetById(object Id)
        {
            var data = _trnHasilProduksiRepo.GetByID(Id);
            return data;
        }
        public void Save(TRN_HASIL_PRODUKSI Db)
        {
            try
            {
                _trnHasilProduksiRepo.InsertOrUpdate(Db);
                _uow.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Save(TRN_HASIL_PRODUKSI Db, Login Login)
        {
            try
            {
                _trnHasilProduksiRepo.InsertOrUpdate(Db, Login, Enums.MenuList.TrnHasilProduksi);
                _uow.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
