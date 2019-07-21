using sistem.manajemen.ppic.core;
using sistem.manajemen.ppic.dal;
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
        private IGenericRepository<DOCUMENT_NUMBER> _docNumberRepo;
        public TrnDoServices(IUnitOfWork Uow)
        {
            _uow = Uow;
            _docNumberRepo = _uow.GetGenericRepository<DOCUMENT_NUMBER>();
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
        public TRN_DO Save(TRN_DO Db, Login Login)
        {
            try
            {
                if(Db.ID == 0)
                {
                    int Id = 0;
                    var GetLatestNumber = _docNumberRepo.Get().Where(x => x.TANGGAL.Month == DateTime.Now.Month && x.TANGGAL.Year == DateTime.Now.Year && x.FORM_ID == (int)Enums.MenuList.TrnDo).ToList();
                    if(GetLatestNumber.Count() > 0)
                    {
                        Id = GetLatestNumber.Max(x => x.NO.Value);
                    }
                    Db.NO_DO = (Id + 1).ToString();

                    DOCUMENT_NUMBER DbDocNumber = new DOCUMENT_NUMBER();
                    DbDocNumber.NO = Id + 1;
                    DbDocNumber.FORM_ID = (int)Enums.MenuList.TrnDo;
                    DbDocNumber.TANGGAL = DateTime.Now;

                    _trnDoRepo.InsertOrUpdate(Db, Login, Enums.MenuList.TrnDo);
                    _docNumberRepo.InsertOrUpdate(DbDocNumber, Login, Enums.MenuList.TrnDo);
                    
                }
                else
                {
                    _trnDoRepo.InsertOrUpdate(Db, Login, Enums.MenuList.TrnDo);
                }
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
