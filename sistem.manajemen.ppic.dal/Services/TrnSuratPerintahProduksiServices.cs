using sistem.manajemen.ppic.core;
using sistem.manajemen.ppic.dal.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dal.Services
{
    public class TrnSuratPerintahProduksiServices : ITrnSuratPerintahProduksiServices
    {
        private IGenericRepository<TRN_SURAT_PERINTAH_PRODUKSI> _trnSuratperintahProduksiRepo;
        private IUnitOfWork _uow;
        private IGenericRepository<DOCUMENT_NUMBER> _docNumberRepo;
        public TrnSuratPerintahProduksiServices(IUnitOfWork Uow)
        {
            _uow = Uow;
            _trnSuratperintahProduksiRepo = _uow.GetGenericRepository<TRN_SURAT_PERINTAH_PRODUKSI>();
            _docNumberRepo = _uow.GetGenericRepository<DOCUMENT_NUMBER>();
        }
        public List<TRN_SURAT_PERINTAH_PRODUKSI> GetActiveAll()
        {
            try
            {
                var data = new List<TRN_SURAT_PERINTAH_PRODUKSI>();
                data = _trnSuratperintahProduksiRepo.Get().Where(x => x.STATUS != (int)Enums.StatusDocument.Cancel).ToList();
                return data;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<TRN_SURAT_PERINTAH_PRODUKSI> GetAll()
        {
            var data = _trnSuratperintahProduksiRepo.Get().ToList();
            return data;
        }
        public TRN_SURAT_PERINTAH_PRODUKSI GetById(object Id)
        {
            var data = _trnSuratperintahProduksiRepo.GetByID(Id);
            return data;
        }
        public void Save(TRN_SURAT_PERINTAH_PRODUKSI Db)
        {
            try
            {
                _trnSuratperintahProduksiRepo.InsertOrUpdate(Db);
                _uow.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Save(TRN_SURAT_PERINTAH_PRODUKSI Db, Login Login)
        {
            try
            {
                if (Db.ID == 0)
                {
                    int Id = 0;
                    var GetLatestNumber = _docNumberRepo.Get().Where(x => x.TANGGAL.Month == DateTime.Now.Month && x.TANGGAL.Year == DateTime.Now.Year && x.FORM_ID == (int)Enums.MenuList.TrnSuratPerintahProduksi).ToList();
                    if (GetLatestNumber.Count() > 0)
                    {
                        Id = GetLatestNumber.Max(x => x.NO.Value);
                    }

                    Db.NO_SURAT = (Id + 1).ToString().PadLeft(4, '0') + "/SPP-CDLSAR/" + IntToRomanConverter.ToRoman(Db.TANGGAL.Value.Month) +"/"+ Db.TANGGAL.Value.Year;

                    DOCUMENT_NUMBER DbDocNumber = new DOCUMENT_NUMBER();
                    DbDocNumber.NO = Id + 1;
                    DbDocNumber.FORM_ID = (int)Enums.MenuList.TrnSuratPerintahProduksi;
                    DbDocNumber.TANGGAL = DateTime.Now;

                    _docNumberRepo.InsertOrUpdate(DbDocNumber, Login, Enums.MenuList.TrnSuratPerintahProduksi);

                    _trnSuratperintahProduksiRepo.InsertOrUpdate(Db, Login, Enums.MenuList.TrnSuratPerintahProduksi);
                }
                else
                {
                    _trnSuratperintahProduksiRepo.InsertOrUpdate(Db, Login, Enums.MenuList.TrnSuratPerintahProduksi);
                }

                _uow.SaveChanges();
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
                var db = _trnSuratperintahProduksiRepo.GetByID(id);
                db.STATUS = (int)Enums.StatusDocument.Cancel;
                db.REMARKS = Remarks;

                Save(db, new Login() { USERNAME = "SYSTEM", USER_ID = "SYSTEM" });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
