using sistem.manajemen.ppic.core;
using sistem.manajemen.ppic.dal.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dal.Services
{
    public class TrnMutasiBarangServices : ITrnMutasiBarangServices
    {
        private IGenericRepository<TRN_MUTASI_BARANG> _trnMutasiBarang;
        private IUnitOfWork _uow;
        public TrnMutasiBarangServices(IUnitOfWork Uow)
        {
            _uow = Uow;
            _trnMutasiBarang = _uow.GetGenericRepository<TRN_MUTASI_BARANG>();
        }
        public List<TRN_MUTASI_BARANG> GetAll()
        {
            var data = _trnMutasiBarang.Get().ToList();
            return data;
        }
        public TRN_MUTASI_BARANG GetById(object Id)
        {
            var data = _trnMutasiBarang.GetByID(Id);
            return data;
        }
        public void Save(TRN_MUTASI_BARANG Db)
        {
            try
            {
                _trnMutasiBarang.InsertOrUpdate(Db);
                _uow.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Save(TRN_MUTASI_BARANG Db, Login Login)
        {
            try
            {
                _trnMutasiBarang.InsertOrUpdate(Db, Login, Enums.MenuList.TrnSuratMutasiBarang);
                _uow.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
