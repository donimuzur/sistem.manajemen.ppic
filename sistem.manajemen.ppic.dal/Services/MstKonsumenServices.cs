using sistem.manajemen.ppic.core;
using sistem.manajemen.ppic.dal.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dal.Services
{
    public class MstKonsumenServices : IMstKonsumenServices
    {
        private IUnitOfWork _uow;
        private IGenericRepository<MST_KONSUMEN> _mstKonsumenRepo;
        public MstKonsumenServices(IUnitOfWork Uow)
        {
            _uow = Uow;
            _mstKonsumenRepo = _uow.GetGenericRepository<MST_KONSUMEN>();
        }
        public List<MST_KONSUMEN> GetAll()
        {
            var Data = _mstKonsumenRepo.Get().ToList();
            return Data;
        }
        public MST_KONSUMEN GetById(object id)
        {
            var Data = _mstKonsumenRepo.GetByID(id);
            return Data;
        }
        public void Save(MST_KONSUMEN Db)
        {
            try
            {
                _mstKonsumenRepo.InsertOrUpdate(Db);
                _uow.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public MST_KONSUMEN Save(MST_KONSUMEN Db, Login Login)
        {
            try
            {
                _mstKonsumenRepo.InsertOrUpdate(Db, Login, Enums.MenuList.MasterKonsumen);
                _uow.SaveChanges();
                return Db;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
