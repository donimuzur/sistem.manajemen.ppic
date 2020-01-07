using sistem.manajemen.ppic.core;
using sistem.manajemen.ppic.dal.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dal.Services
{
    public class TrnSuratPermintaanBahanBakuServices : ITrnSuratPermintaanBahanBakuServices
    {
        private IGenericRepository<TRN_SURAT_PERMINTAAN_BAHAN_BAKU> _trnSuratPermintaanBahanBakuServices;
        private IGenericRepository<TRN_SURAT_PERMINTAAN_BAHAN_BAKU_DETAILS> _trnSuratPermintaanBahanBakuDetailsServices;
        private IGenericRepository<DOCUMENT_NUMBER> _docNumberRepo;

        private const string TABLE = "TRN_SURAT_PERMINTAAN_BAHAN_BAKU_DETAILS";
        private const string TABLE_DETAILS = "TRN_SURAT_PERMINTAAN_BAHAN_BAKU, MST_BAHAN_BAKU";

        private IUnitOfWork _uow;
        public TrnSuratPermintaanBahanBakuServices(IUnitOfWork Uow)
        {
            _uow = Uow;
            _trnSuratPermintaanBahanBakuServices = _uow.GetGenericRepository<TRN_SURAT_PERMINTAAN_BAHAN_BAKU>();
            _trnSuratPermintaanBahanBakuDetailsServices = _uow.GetGenericRepository<TRN_SURAT_PERMINTAAN_BAHAN_BAKU_DETAILS>();
            _docNumberRepo = _uow.GetGenericRepository<DOCUMENT_NUMBER>();
        }
        public List<TRN_SURAT_PERMINTAAN_BAHAN_BAKU> GetAll()
        {
            try
            {
                var data = new List<TRN_SURAT_PERMINTAAN_BAHAN_BAKU>();
                data = _trnSuratPermintaanBahanBakuServices.Get(null, null, TABLE).ToList();
                return data;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public TRN_SURAT_PERMINTAAN_BAHAN_BAKU GetById(int Id)
        {
            try
            {
                var data = new TRN_SURAT_PERMINTAAN_BAHAN_BAKU();
                data = _trnSuratPermintaanBahanBakuServices.Get(model => model.ID == Id ,null,TABLE).FirstOrDefault();
                return data;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<TRN_SURAT_PERMINTAAN_BAHAN_BAKU> GetActiveAll()
        {
            try
            {
                var data = new List<TRN_SURAT_PERMINTAAN_BAHAN_BAKU>();
                data = _trnSuratPermintaanBahanBakuServices.Get(x => x.STATUS != (int)Enums.StatusDocument.Cancel, null, TABLE).ToList();
                return data;
            }
            catch (Exception)
            {

                throw;
            }
        }
       
        public void Save(TRN_SURAT_PERMINTAAN_BAHAN_BAKU Db)
        {
            try
            {
                _trnSuratPermintaanBahanBakuServices.InsertOrUpdate(Db);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public TRN_SURAT_PERMINTAAN_BAHAN_BAKU Save(TRN_SURAT_PERMINTAAN_BAHAN_BAKU Db, Login Login)
        {
            try
            {
                if (Db.ID == 0)
                {
                    int Id = 0;
                    var GetLatestNumber = _docNumberRepo.Get().Where(x => x.TANGGAL.Month == DateTime.Now.Month && x.TANGGAL.Year == DateTime.Now.Year && x.FORM_ID == (int)Enums.MenuList.TrnPermintaanBahanBaku).ToList();
                    if (GetLatestNumber.Count() > 0)
                    {
                        Id = GetLatestNumber.Max(x => x.NO.Value);
                    }

                    Db.NO_SURAT = (Id + 1).ToString().PadLeft(4,'0') + "/SPBB/PPIC/" + IntToRomanConverter.ToRoman(DateTime.Now.Month) + "/" + DateTime.Now.Year;
                    
                    DOCUMENT_NUMBER DbDocNumber = new DOCUMENT_NUMBER();
                    DbDocNumber.NO = Id + 1;
                    DbDocNumber.FORM_ID = (int)Enums.MenuList.TrnPermintaanBahanBaku;
                    DbDocNumber.TANGGAL = DateTime.Now;

                    _docNumberRepo.InsertOrUpdate(DbDocNumber, Login, Enums.MenuList.TrnPermintaanBahanBaku);
                    
                    DeleteDetailsByMasterId(Db.ID);
                    _trnSuratPermintaanBahanBakuServices.InsertOrUpdate(Db, Login, Enums.MenuList.TrnPermintaanBahanBaku);
                   
                }
                else
                {
                    DeleteDetailsByMasterId(Db.ID);
                    _trnSuratPermintaanBahanBakuServices.InsertOrUpdate(Db, Login, Enums.MenuList.TrnPermintaanBahanBaku);
                }
                _uow.SaveChanges();
                return Db;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete(int id, string Remarks)
        {
            try
            {
                var db = _trnSuratPermintaanBahanBakuServices.GetByID(id);
                db.STATUS = (int)Enums.StatusDocument.Cancel;
                db.REMARKS = Remarks;

                Save(db, new Login() { USERNAME = "SYSTEM", USER_ID = "SYSTEM" });
            }
            catch (Exception)
            {
                throw;
            }
        }


        #region --- SuratPermintaanBahanBakuDetais ---
        public List<TRN_SURAT_PERMINTAAN_BAHAN_BAKU_DETAILS> GetAllDetails()
        {
            try
            {
                var data = new List<TRN_SURAT_PERMINTAAN_BAHAN_BAKU_DETAILS>();
                data = _trnSuratPermintaanBahanBakuDetailsServices.Get(null, null, TABLE_DETAILS).ToList();
                
                return data;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public TRN_SURAT_PERMINTAAN_BAHAN_BAKU_DETAILS GetDetailsById(int Id)
        {
            try
            {
                var data = new TRN_SURAT_PERMINTAAN_BAHAN_BAKU_DETAILS();
                data = _trnSuratPermintaanBahanBakuDetailsServices.GetByID(Id);
                return data;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<TRN_SURAT_PERMINTAAN_BAHAN_BAKU_DETAILS> GetAllDetailsByMasterId(int Id)
        {
            try
            {
                var data = _trnSuratPermintaanBahanBakuDetailsServices.Get().Where(x => x.ID_TRN_SURAT_PERMINTAAN_BAHAN_BAKU == Id).ToList();
                return data;
            }
            catch (Exception)
            {

                throw;
            }
           
        }
        public void DeleteDetailsByMasterId(int MasterId)
        {
            try
            {
                var data = GetAllDetailsByMasterId(MasterId);
                foreach(var item in data)
                {
                    _trnSuratPermintaanBahanBakuDetailsServices.Delete(item.ID);
                    _uow.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        public void Save(TRN_SURAT_PERMINTAAN_BAHAN_BAKU_DETAILS Db)
        {
            try
            {
                _trnSuratPermintaanBahanBakuDetailsServices.InsertOrUpdate(Db);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
