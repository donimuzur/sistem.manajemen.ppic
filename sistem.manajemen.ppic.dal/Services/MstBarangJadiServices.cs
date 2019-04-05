using sistem.manajemen.ppic.core;
using sistem.manajemen.ppic.dal.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dal.Services
{
    public class MstBarangJadiServices : IMstBarangJadiServices
    {
        private IUnitOfWork _uow;
        private IGenericRepository<MST_BARANG_JADI> _repoMstBarangJadi;
        public MstBarangJadiServices(IUnitOfWork Uow)
        {
            _uow = Uow;
            _repoMstBarangJadi = _uow.GetGenericRepository<MST_BARANG_JADI>();
        }

        public List<MST_BARANG_JADI> GetAll()
        {
            var data = _repoMstBarangJadi.Get().ToList();
            return data;
        }
        public MST_BARANG_JADI GetById(object Id)
        {
            var data = _repoMstBarangJadi.GetByID(Id);
            return data;
        }
        public void Save(MST_BARANG_JADI Db)
        {
            try
            {
                _repoMstBarangJadi.InsertOrUpdate(Db);
                _uow.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }               
        }
        public void Save(MST_BARANG_JADI Db, Login Login)
        {
            try
            {
                _repoMstBarangJadi.InsertOrUpdate(Db, Login, Enums.MenuList.GdgBarangJadi);
                _uow.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
