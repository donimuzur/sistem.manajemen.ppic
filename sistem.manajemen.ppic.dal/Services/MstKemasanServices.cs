using sistem.manajemen.ppic.core;
using sistem.manajemen.ppic.dal.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dal.Services
{
    public class MstKemasanServices : IMstKemasanServices
    {
        private IUnitOfWork _uow;
        private IGenericRepository<MST_KEMASAN> _mstKemasanRepo;
        public MstKemasanServices(IUnitOfWork uow)
        {
            _uow = uow;
            _mstKemasanRepo = _uow.GetGenericRepository<MST_KEMASAN>();
        }
        public List<MST_KEMASAN> GetAll()
        {
            var Data = _mstKemasanRepo.Get().ToList();
            return Data;
        }
        public MST_KEMASAN GetById(object id)
        {
            var Data = _mstKemasanRepo.GetByID(id);
            return Data;
        }
        public void Save(MST_KEMASAN Db)
        {
            try
            {
                _mstKemasanRepo.InsertOrUpdate(Db);
                _uow.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Save(MST_KEMASAN Db, Login Login)
        {
            try
            {
                _mstKemasanRepo.InsertOrUpdate(Db, Login, Enums.MenuList.MasterKemasan);
                _uow.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
