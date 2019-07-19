using sistem.manajemen.ppic.core;
using sistem.manajemen.ppic.dal.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dal.Services
{
    public class TrnMutasiBarangServices : ITrnMutasiBarangServices
    {
        private IGenericRepository<TRN_MUTASI_BARANG> _trnMutasiBarang;
        private IGenericRepository<DOCUMENT_NUMBER> _docNumberRepo;
        private IUnitOfWork _uow;
        public TrnMutasiBarangServices(IUnitOfWork Uow)
        {
            _uow = Uow;
            _trnMutasiBarang = _uow.GetGenericRepository<TRN_MUTASI_BARANG>();
            _docNumberRepo = _uow.GetGenericRepository<DOCUMENT_NUMBER>();
        }
        public List<TRN_MUTASI_BARANG> GetActiveAll()
        {
            var data = _trnMutasiBarang.Get(x => x.STATUS != (int)Enums.StatusDocument.Cancel).ToList();
            return data;
        }
        public List<TRN_MUTASI_BARANG> GetAll()
        {
            var data = _trnMutasiBarang.Get().ToList();
            return data;
        }
        public TRN_MUTASI_BARANG GetById(object Id)
        {
            var data = _trnMutasiBarang.GetByID(Id);
            return data;
        }
        public void Save(TRN_MUTASI_BARANG Db)
        {
            try
            {
                _trnMutasiBarang.InsertOrUpdate(Db);
                _uow.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Save(TRN_MUTASI_BARANG Db, Login Login)
        {
            try
            {
                if (Db.ID == 0)
                {
                    int Id = 0;
                    var GetLatestNumber = _docNumberRepo.Get().Where(x => x.TANGGAL.Month == DateTime.Now.Month && x.TANGGAL.Year == DateTime.Now.Year && x.FORM_ID == (int)Enums.MenuList.TrnSuratMutasiBarang).ToList();
                    if (GetLatestNumber.Count() > 0)
                    {
                        Id = GetLatestNumber.Max(x => x.NO.Value);
                    }
                    
                    Db.NO_SURAT = "MB" + DateTime.Today.ToString("yyyyMM") + (Id + 1).ToString().PadLeft(6, '0');
                    
                    DOCUMENT_NUMBER DbDocNumber = new DOCUMENT_NUMBER();
                    DbDocNumber.NO = Id + 1;
                    DbDocNumber.FORM_ID = (int)Enums.MenuList.TrnSuratMutasiBarang;
                    DbDocNumber.TANGGAL = DateTime.Now;

                    _docNumberRepo.InsertOrUpdate(DbDocNumber, Login, Enums.MenuList.TrnSuratMutasiBarang);

                    _trnMutasiBarang.InsertOrUpdate(Db, Login, Enums.MenuList.TrnSuratMutasiBarang);
                }
                else
                {
                    _trnMutasiBarang.InsertOrUpdate(Db, Login, Enums.MenuList.TrnSuratMutasiBarang);
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
                var db = _trnMutasiBarang.GetByID(id);
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
