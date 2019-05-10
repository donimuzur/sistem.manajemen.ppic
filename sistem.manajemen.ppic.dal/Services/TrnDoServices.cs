using sistem.manajemen.ppic.core;
using sistem.manajemen.ppic.dal.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dal.Services
{
    public class TrnDoServices: ITrnDoServices
    {
        private IUnitOfWork _uow;
        private IGenericRepository<TRN_DO> _trnDoRepo;
        public TrnDoServices(IUnitOfWork Uow)
        {
            _uow = Uow;
            _trnDoRepo = _uow.GetGenericRepository<TRN_DO>();
        }
        public List<TRN_DO> GetAll()
        {
            var data = _trnDoRepo.Get().ToList();
            return data;
        }
        public TRN_DO GetById(object Id)
        {
            var data = _trnDoRepo.GetByID(Id);
            return data;
        }
        public void Save(TRN_DO Db)
        {
            try
            {
                _trnDoRepo.InsertOrUpdate(Db);
                _uow.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Save(TRN_DO Db, Login Login)
        {
            try
            {
                if(Db.ID == 0)
                {
                    int Id = 0;
                    var GetAll = _trnDoRepo.Get();
                    if(GetAll.Count() > 0)
                    {
                        Id = GetAll.Max(x => int.Parse(x.NO_DO));
                    }
                    Db.NO_DO = (Id + 1).ToString();
                    _trnDoRepo.InsertOrUpdate(Db, Login, Enums.MenuList.TrnDo);
                }
                else
                {
                    _trnDoRepo.InsertOrUpdate(Db, Login, Enums.MenuList.TrnDo);
                }
                _uow.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
