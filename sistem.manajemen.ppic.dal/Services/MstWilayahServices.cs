using sistem.manajemen.ppic.core;
using sistem.manajemen.ppic.dal.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dal.Services
{
    public class MstWilayahServices: IMstWilayahServices
    {
        private IUnitOfWork _uow;
        private IGenericRepository<MST_WILAYAH> _repoMstWilayah;
        public MstWilayahServices(IUnitOfWork Uow)
        {
            _uow = Uow;
            _repoMstWilayah = _uow.GetGenericRepository<MST_WILAYAH>();
        }
        public List<MST_WILAYAH> GetAll()
        {
            var data = _repoMstWilayah.Get().ToList();
            return data;
        }
        public MST_WILAYAH GetById(object Id)
        {
            var data = _repoMstWilayah.GetByID(Id);
            return data;
        }
        public void Save(MST_WILAYAH Db)
        {
            try
            {
                _repoMstWilayah.InsertOrUpdate(Db);
                _uow.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public MST_WILAYAH Save(MST_WILAYAH Db,Login Login)
        {
            try
            {
                _repoMstWilayah.InsertOrUpdate(Db,Login,Enums.MenuList.MasterWilayah);
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
