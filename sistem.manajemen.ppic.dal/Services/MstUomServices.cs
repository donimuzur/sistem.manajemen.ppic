using sistem.manajemen.ppic.core;
using sistem.manajemen.ppic.dal.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dal.Services
{
    public class MstUomServices : IMstUomServices
    {
        private IUnitOfWork _uow;
        private IGenericRepository<MST_UOM> _mstUomRepo;
        public MstUomServices(IUnitOfWork uow)
        {
            _uow = uow;
            _mstUomRepo = _uow.GetGenericRepository<MST_UOM>();
        }
        public List<MST_UOM> GetAll()
        {
            var Data = _mstUomRepo.Get().ToList();
            return Data;
        }
        public MST_UOM GetById(object id)
        {
            var Data = _mstUomRepo.GetByID(id);
            return Data;
        }
        public void Save(MST_UOM Db)
        {
            try
            {
                _mstUomRepo.InsertOrUpdate(Db);
                _uow.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public MST_UOM Save(MST_UOM Db, Login Login)
        {
            try
            {
                _mstUomRepo.InsertOrUpdate(Db, Login, Enums.MenuList.MasterUom);
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
