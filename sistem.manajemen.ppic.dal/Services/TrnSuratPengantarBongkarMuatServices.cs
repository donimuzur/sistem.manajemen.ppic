using sistem.manajemen.ppic.core;
using sistem.manajemen.ppic.dal.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dal.Services
{
    public class TrnSuratPengantarBongkarMuatServices: ITrnSuratPengantarBongkarMuatServices
    {
        private IGenericRepository<TRN_SURAT_PENGANTAR_BONGKAR_MUAT> _trnSuratPengantarBongkarMuatRepo;
        private IUnitOfWork _uow;
        public TrnSuratPengantarBongkarMuatServices(IUnitOfWork Uow)
        {
            _uow = Uow;
            _trnSuratPengantarBongkarMuatRepo = _uow.GetGenericRepository<TRN_SURAT_PENGANTAR_BONGKAR_MUAT>();
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
                _trnSuratPengantarBongkarMuatRepo.InsertOrUpdate(Db, Login, Enums.MenuList.TrnSuratPengantarBongkarMuat);
                _uow.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
