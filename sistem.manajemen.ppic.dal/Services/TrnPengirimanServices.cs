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
        private IGenericRepository<TRN_PENGIRIMAN_MASTER> _trnPengirimanMasterRepo;
        private IGenericRepository<TRN_PENGIRIMAN_DETAILS> _trnPengirimanDetailsRepo;

        private const string TABLE_DETAILS = "TRN_PENGIRIMAN_DETAILS";
        private const string TABLE_MASTER = "TRN_PENGIRIMAN_MASTER,MST_BARANG_JADI";

        private IUnitOfWork _uow;
        public TrnPengirimanServices(IUnitOfWork Uow)
        {
            _uow = Uow;
            _trnPengirimanMasterRepo = _uow.GetGenericRepository<TRN_PENGIRIMAN_MASTER>();
            _trnPengirimanDetailsRepo = _uow.GetGenericRepository<TRN_PENGIRIMAN_DETAILS>();
        }
        public List<TRN_PENGIRIMAN_MASTER> GetAll()
        {
            var data = new List<TRN_PENGIRIMAN_MASTER>();
            data = _trnPengirimanMasterRepo.Get(null, null, TABLE_DETAILS).ToList();
            return data;
        }
        public List<TRN_PENGIRIMAN_DETAILS> GetAllDetails()
        {
            var data = new List<TRN_PENGIRIMAN_DETAILS>();
            data = _trnPengirimanDetailsRepo.Get(null, null, TABLE_MASTER).ToList();
            return data;
        }
        public TRN_PENGIRIMAN_MASTER GetTrnPengirimanMasterById(Object Id)
        {
            var data = _trnPengirimanMasterRepo.GetByID(Id);
            return data;
        }
        public TRN_PENGIRIMAN_DETAILS GetTrnPengirimanDetailsById(Object Id)
        {
            var data = _trnPengirimanDetailsRepo.GetByID(Id);
            return data;
        }
        public TRN_PENGIRIMAN_DETAILS GetTrnPengirimanDetailsByPengiriman(int Id)
        {
            var data = _trnPengirimanDetailsRepo.Get(x => x.ID_TRN_PENGIRIMAN_MASTER == Id).FirstOrDefault();
            return data;
        }
        public void Save(TRN_PENGIRIMAN_MASTER Db)
        {
            try
            {
                _trnPengirimanMasterRepo.InsertOrUpdate(Db);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Save(TRN_PENGIRIMAN_MASTER Db, Login Login)
        {
            try
            {
                _trnPengirimanMasterRepo.InsertOrUpdate(Db, Login, Enums.MenuList.Timbangan);
            }
            catch (Exception )
            {
                throw;
            }
        }
    }
}
