using sistem.manajemen.ppic.core;
using sistem.manajemen.ppic.dal.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dal.Services
{
    public class MstBahanBakuServices : IMstBahanBakuServices
    {
        private IUnitOfWork _uow;
        private IGenericRepository<MST_BAHAN_BAKU> _repoMstBahanBaku;
        public MstBahanBakuServices(IUnitOfWork Uow)
        {
            _uow = Uow;
            _repoMstBahanBaku = _uow.GetGenericRepository<MST_BAHAN_BAKU>();
        }

        public List<MST_BAHAN_BAKU> GetAll()
        {
            var data = _repoMstBahanBaku.Get().ToList();
            return data;
        }
        public MST_BAHAN_BAKU GetById(object Id)
        {
            var data = _repoMstBahanBaku.GetByID(Id);
            return data;
        }
        public void Save(MST_BAHAN_BAKU Db)
        {
            try
            {
                _repoMstBahanBaku.InsertOrUpdate(Db);
                _uow.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Save(MST_BAHAN_BAKU Db, Login Login)
        {
            try
            {
                _repoMstBahanBaku.InsertOrUpdate(Db, Login, Enums.MenuList.MstBahanBaku);
                _uow.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
