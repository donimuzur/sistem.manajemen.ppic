using sistem.manajemen.ppic.core;
using sistem.manajemen.ppic.dal.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dal.Services
{
    public class TrnHasilProduksiServices : ITrnHasilProduksiServices
    {
        private IGenericRepository<TRN_HASIL_PRODUKSI> _trnHasilProduksiRepo;
        private IGenericRepository<DOCUMENT_NUMBER> _docNumberRepo;
        private IUnitOfWork _uow;
        public TrnHasilProduksiServices (IUnitOfWork Uow)
        {
            _uow = Uow;
            _trnHasilProduksiRepo = _uow.GetGenericRepository<TRN_HASIL_PRODUKSI>();
            _docNumberRepo = _uow.GetGenericRepository<DOCUMENT_NUMBER>();
        }
        public List<TRN_HASIL_PRODUKSI> GetActiveAll()
        {
            try
            {
                var data = new List<TRN_HASIL_PRODUKSI>();
                data = _trnHasilProduksiRepo.Get().Where(x => x.STATUS != (int)Enums.StatusDocument.Cancel).ToList();
                return data;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<TRN_HASIL_PRODUKSI> GetAll()
        {
            try
            {
                var data = _trnHasilProduksiRepo.Get().ToList();
                return data;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public TRN_HASIL_PRODUKSI GetById(object Id)
        {
            try
            {
                var data = _trnHasilProduksiRepo.GetByID(Id);
                return data;
            }
            catch (Exception)
            {

                throw;
            }            
        }
        public void Save(TRN_HASIL_PRODUKSI Db)
        {
            try
            {
                _trnHasilProduksiRepo.InsertOrUpdate(Db);
                _uow.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Save(TRN_HASIL_PRODUKSI Db, Login Login)
        {
            try
            {
                if (Db.ID == 0)
                {
                    int Id = 0;
                    var GetLatestNumber = _docNumberRepo.Get().Where(x => x.TANGGAL.Month == DateTime.Now.Month && x.TANGGAL.Year == DateTime.Now.Year && x.FORM_ID == (int)Enums.MenuList.TrnHasilProduksi).ToList();
                    if (GetLatestNumber.Count() > 0)
                    {
                        Id = GetLatestNumber.Max(x => x.NO.Value);
                    }

                    Db.NO_SURAT = "HP" + DateTime.Today.ToString("yyyyMM") + (Id + 1).ToString().PadLeft(6, '0');

                    DOCUMENT_NUMBER DbDocNumber = new DOCUMENT_NUMBER();
                    DbDocNumber.NO = Id + 1;
                    DbDocNumber.FORM_ID = (int)Enums.MenuList.TrnHasilProduksi;
                    DbDocNumber.TANGGAL = DateTime.Now;

                    _docNumberRepo.InsertOrUpdate(DbDocNumber, Login, Enums.MenuList.TrnHasilProduksi);
                    _trnHasilProduksiRepo.InsertOrUpdate(Db, Login, Enums.MenuList.TrnHasilProduksi);
                }
                else
                {
                    _trnHasilProduksiRepo.InsertOrUpdate(Db, Login, Enums.MenuList.TrnHasilProduksi);
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
                var db = _trnHasilProduksiRepo.GetByID(id);
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
