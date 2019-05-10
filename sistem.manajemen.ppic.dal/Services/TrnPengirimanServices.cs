using sistem.manajemen.ppic.core;
using sistem.manajemen.ppic.dal.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dal.Services
{
    public class TrnPengirimanServices : ITrnPengirimanServices
    {
        private IUnitOfWork _uow;
        private IGenericRepository<TRN_PENGIRIMAN> _trnPengirimanRepo;
        public TrnPengirimanServices(IUnitOfWork Uow)
        {
            _uow = Uow;
            _trnPengirimanRepo = _uow.GetGenericRepository<TRN_PENGIRIMAN>();
        }
        public List<TRN_PENGIRIMAN> GetAll()
        {
            var data = _trnPengirimanRepo.Get().ToList();
            return data;
        }
        public TRN_PENGIRIMAN GetById(object Id)
        {
            var data = _trnPengirimanRepo.GetByID(Id);
            return data;
        }
        public void Save(TRN_PENGIRIMAN Db)
        {
            try
            {
                _trnPengirimanRepo.InsertOrUpdate(Db);
                _uow.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Save(TRN_PENGIRIMAN Db, Login Login)
        {
            try
            {
                _trnPengirimanRepo.InsertOrUpdate(Db, Login, Enums.MenuList.TrnPengiriman);
                _uow.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
