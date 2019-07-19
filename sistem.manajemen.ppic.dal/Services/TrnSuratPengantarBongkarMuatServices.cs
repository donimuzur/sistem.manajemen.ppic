using sistem.manajemen.ppic.core;
using sistem.manajemen.ppic.dal.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dal.Services
{
    public class TrnSuratPengantarBongkarMuatServices: ITrnSuratPengantarBongkarMuatServices
    {
        private IGenericRepository<TRN_SURAT_PENGANTAR_BONGKAR_MUAT> _trnSuratPengantarBongkarMuatRepo;
        private IUnitOfWork _uow;
        private IGenericRepository<DOCUMENT_NUMBER> _docNumberRepo;
        public TrnSuratPengantarBongkarMuatServices(IUnitOfWork Uow)
        {
            _uow = Uow;
            _trnSuratPengantarBongkarMuatRepo = _uow.GetGenericRepository<TRN_SURAT_PENGANTAR_BONGKAR_MUAT>();
            _docNumberRepo = _uow.GetGenericRepository<DOCUMENT_NUMBER>();
        }
        public List<TRN_SURAT_PENGANTAR_BONGKAR_MUAT> GetActiveAll()
        {
            try
            {
                var data = new List<TRN_SURAT_PENGANTAR_BONGKAR_MUAT>();
                data = _trnSuratPengantarBongkarMuatRepo.Get().Where(x => x.STATUS != null && x.STATUS != (int)Enums.StatusDocument.Cancel).ToList();
                return data;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<TRN_SURAT_PENGANTAR_BONGKAR_MUAT> GetAll()
        {
            var data = _trnSuratPengantarBongkarMuatRepo.Get().ToList();
            return data;
        }
        public TRN_SURAT_PENGANTAR_BONGKAR_MUAT GetById(object Id)
        {
            var data = _trnSuratPengantarBongkarMuatRepo.GetByID(Id);
            return data;
        }
        public void Save(TRN_SURAT_PENGANTAR_BONGKAR_MUAT Db)
        {
            try
            {
                _trnSuratPengantarBongkarMuatRepo.InsertOrUpdate(Db);
                _uow.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Save(TRN_SURAT_PENGANTAR_BONGKAR_MUAT Db, Login Login)
        {
            try
            {
                if (Db.ID == 0)
                {
                    int Id = 0;
                    var GetLatestNumber = _docNumberRepo.Get().Where(x => x.TANGGAL.Month == DateTime.Now.Month && x.TANGGAL.Year == DateTime.Now.Year && x.FORM_ID == (int)Enums.MenuList.TrnSuratPengantarBongkarMuat).ToList();
                    if (GetLatestNumber.Count() > 0)
                    {
                        Id = GetLatestNumber.Max(x => x.NO.Value);
                    }

                    Db.NO_SURAT = "SPBP" + DateTime.Today.ToString("yyyyMM") + (Id + 1).ToString().PadLeft(6, '0');

                    DOCUMENT_NUMBER DbDocNumber = new DOCUMENT_NUMBER();
                    DbDocNumber.NO = Id + 1;
                    DbDocNumber.FORM_ID = (int)Enums.MenuList.TrnSuratPengantarBongkarMuat;
                    DbDocNumber.TANGGAL = DateTime.Now;

                    _docNumberRepo.InsertOrUpdate(DbDocNumber, Login, Enums.MenuList.TrnSuratPengantarBongkarMuat);

                    _trnSuratPengantarBongkarMuatRepo.InsertOrUpdate(Db, Login, Enums.MenuList.TrnSuratPengantarBongkarMuat);
                }
                else
                {
                    _trnSuratPengantarBongkarMuatRepo.InsertOrUpdate(Db, Login, Enums.MenuList.TrnSuratPengantarBongkarMuat);
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
                var db = _trnSuratPengantarBongkarMuatRepo.GetByID(id);
                db.STATUS = (int)Enums.StatusDocument.Cancel;
                db.REMARK = Remarks;

                Save(db, new Login() { USERNAME = "SYSTEM", USER_ID = "SYSTEM" });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
