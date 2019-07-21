using sistem.manajemen.ppic.core;
using sistem.manajemen.ppic.dal.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dal.Services
{
    public class TrnPengirimanServices : ITrnPengirimanServices
    {
        private IGenericRepository<TRN_PENGIRIMAN> _trnPengirimanRepo;
        private IGenericRepository<DOCUMENT_NUMBER> _docNumberRepo;

        private const string TABLE= "MST_BARANG_JADI";

        private IUnitOfWork _uow;
        public TrnPengirimanServices(IUnitOfWork Uow)
        {
            _uow = Uow;
            _trnPengirimanRepo = _uow.GetGenericRepository<TRN_PENGIRIMAN>();
            _docNumberRepo = _uow.GetGenericRepository<DOCUMENT_NUMBER>();
        }
        public List<TRN_PENGIRIMAN> GetAll()
        {
            try
            {
                var data = new List<TRN_PENGIRIMAN>();
                data = _trnPengirimanRepo.Get(null, null, TABLE).ToList();
                return data;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<TRN_PENGIRIMAN> GetActiveAll()
        {
            try
            {
                var data = new List<TRN_PENGIRIMAN>();
                data = _trnPengirimanRepo.Get(x =>x.STATUS != (int)Enums.StatusDocument.Cancel , null, TABLE).ToList();
                return data;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public TRN_PENGIRIMAN GetTrnPengirimanMasterById(Object Id)
        {
            var data = _trnPengirimanRepo.GetByID(Id);
            return data;
        }
     
        public void Save(TRN_PENGIRIMAN Db)
        {
            try
            {
                _trnPengirimanRepo.InsertOrUpdate(Db);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public TRN_PENGIRIMAN Save(TRN_PENGIRIMAN Db, Login Login)
        {
            try
            {
                if (Db.ID == 0)
                {
                    int Id = 0;
                    var GetLatestNumber = _docNumberRepo.Get().Where(x => x.TANGGAL.Month == DateTime.Now.Month && x.TANGGAL.Year == DateTime.Now.Year && x.FORM_ID == (int)Enums.MenuList.TrnPengiriman).ToList();
                    if (GetLatestNumber.Count() > 0)
                    {
                        Id = GetLatestNumber.Max(x => x.NO.Value);
                    }

                    Db.NO_SURAT_JALAN = (Id + 1).ToString() + "/SJ/PPIC/" + IntToRomanConverter.ToRoman(DateTime.Now.Month) +"/"+DateTime.Now.Year;

                    var GetLatestRit = _trnPengirimanRepo.Get(x => x.NO_DO == Db.NO_DO && x.NO_SPB.ToUpper() == Db.NO_SPB.ToUpper() && x.STATUS != (int)Enums.StatusDocument.Cancel).ToList();
                    if(GetLatestRit.Count > 0)
                    {
                        Db.NO_RIT = GetLatestRit.Max(x => x.NO_RIT);
                    }
                    Db.NO_RIT = Db.NO_RIT + 1;

                    DOCUMENT_NUMBER DbDocNumber = new DOCUMENT_NUMBER();
                    DbDocNumber.NO = Id + 1;
                    DbDocNumber.FORM_ID = (int)Enums.MenuList.TrnPengiriman;
                    DbDocNumber.TANGGAL = DateTime.Now;

                    _docNumberRepo.InsertOrUpdate(DbDocNumber, Login, Enums.MenuList.TrnPengiriman);

                    _trnPengirimanRepo.InsertOrUpdate(Db, Login, Enums.MenuList.TrnPengiriman);
                    
                }
                else
                {
                    _trnPengirimanRepo.InsertOrUpdate(Db, Login, Enums.MenuList.TrnPengiriman);
                }
                _uow.SaveChanges();
                return Db;
            }
            catch (Exception )
            {
                throw;
            }
        }
        
        public void Delete(int id, string Remarks)
        {
            try
            {
                var db = _trnPengirimanRepo.GetByID(id);
                db.STATUS = (int)Enums.StatusDocument.Cancel;
                db.REMARK = Remarks;

                Save(db,new Login (){USERNAME="SYSTEM",USER_ID="SYSTEM"});
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
