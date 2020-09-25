using sistem.manajemen.ppic.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dal.Services
{
    public class TrnSlipTimbanganServices
    {
        private IUnitOfWork _uow;
        private IGenericRepository<SLIP_TIMBANGAN> _trnSlipTimbanganRepo;
        private IGenericRepository<DOCUMENT_NUMBER> _docNumberRepo;
        public TrnSlipTimbanganServices(IUnitOfWork Uow)
        {
            _uow = Uow;
            _trnSlipTimbanganRepo = _uow.GetGenericRepository<SLIP_TIMBANGAN>();
            _docNumberRepo = _uow.GetGenericRepository<DOCUMENT_NUMBER>();
        }
        public List<SLIP_TIMBANGAN> GetAll()
        {
            var Data = _trnSlipTimbanganRepo.Get().ToList();
            return Data;
        }
        public SLIP_TIMBANGAN GetById(object id)
        {
            var Data = _trnSlipTimbanganRepo.GetByID(id);
            return Data;
        }
        public void Save(SLIP_TIMBANGAN Db)
        {
            try
            {
                _trnSlipTimbanganRepo.InsertOrUpdate(Db);
                _uow.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public SLIP_TIMBANGAN Save(SLIP_TIMBANGAN Db, Login Login)
        {
            try
            {
                if (Db.ID == 0)
                {
                    int Id = 0;
                    var GetLatestNumber = _docNumberRepo.Get().Where(x => x.TANGGAL.Month == DateTime.Now.Month && x.TANGGAL.Year == DateTime.Now.Year && x.FORM_ID == (int)Enums.MenuList.TrnSlipTimbangan).ToList();
                    if (GetLatestNumber.Count() > 0)
                    {
                        Id = GetLatestNumber.Max(x => x.NO.Value);
                    }

                    Db.NO_RECORD = Id + 1;

                    DOCUMENT_NUMBER DbDocNumber = new DOCUMENT_NUMBER();
                    DbDocNumber.NO = Id + 1;
                    DbDocNumber.FORM_ID = (int)Enums.MenuList.TrnSlipTimbangan;
                    DbDocNumber.TANGGAL = Db.TANGGAL;

                    _docNumberRepo.InsertOrUpdate(DbDocNumber, Login, Enums.MenuList.TrnSlipTimbangan);
                    _trnSlipTimbanganRepo.InsertOrUpdate(Db, Login, Enums.MenuList.TrnSlipTimbangan);
                }
                else
                {
                    _trnSlipTimbanganRepo.InsertOrUpdate(Db, Login, Enums.MenuList.TrnSlipTimbangan);
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
