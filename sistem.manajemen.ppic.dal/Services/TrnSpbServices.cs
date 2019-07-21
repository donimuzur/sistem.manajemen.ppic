using sistem.manajemen.ppic.core;
using sistem.manajemen.ppic.dal.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dal.Services
{
    public class TrnSpbServices: ITrnSpbServices
    {
        private IUnitOfWork _uow;
        private IGenericRepository<TRN_SPB> _trnSpbRepo;
        public TrnSpbServices(IUnitOfWork Uow)
        {
            _uow = Uow;
            _trnSpbRepo = _uow.GetGenericRepository<TRN_SPB>();
        }
        public List<TRN_SPB> GetActiveAll()
        {
            try
            {
                var data = new List<TRN_SPB>();
                data = _trnSpbRepo.Get().Where(x => x.STATUS != Enums.StatusDocument.Cancel).ToList();
                return data;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<TRN_SPB> GetAll()
        {
            var data = _trnSpbRepo.Get().ToList();
            return data;
        }
        public TRN_SPB GetById(object Id)
        {
            var data = _trnSpbRepo.GetByID(Id);
            return data;
        }
        public void Save(TRN_SPB Db)
        {
            try
            {
                _trnSpbRepo.InsertOrUpdate(Db);
                _uow.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public TRN_SPB Save(TRN_SPB Db, Login Login)
        {
            try
            {
                _trnSpbRepo.InsertOrUpdate(Db, Login, Enums.MenuList.TrnSpb);
                _uow.SaveChanges();
                return Db;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void SaveList(List<TRN_SPB> Db, Login Login)
        {
            try
            {
                _trnSpbRepo.InsertOrUpdateBulk(Db, Login, Enums.MenuList.TrnSpb);
                _uow.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
