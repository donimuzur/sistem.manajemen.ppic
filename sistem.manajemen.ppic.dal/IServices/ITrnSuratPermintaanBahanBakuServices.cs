using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dal.IServices
{
    public interface ITrnSuratPermintaanBahanBakuServices
    {
        List<TRN_SURAT_PERMINTAAN_BAHAN_BAKU> GetAll();
        TRN_SURAT_PERMINTAAN_BAHAN_BAKU GetById(int Id);
        List<TRN_SURAT_PERMINTAAN_BAHAN_BAKU> GetActiveAll();
        void Save(TRN_SURAT_PERMINTAAN_BAHAN_BAKU Db);
        TRN_SURAT_PERMINTAAN_BAHAN_BAKU Save(TRN_SURAT_PERMINTAAN_BAHAN_BAKU Db, Login Login);
        void Delete(int id, string Remarks);

        #region --- SuratPermintaanBahanBakuDetails ---
        List<TRN_SURAT_PERMINTAAN_BAHAN_BAKU_DETAILS> GetAllDetails();
        TRN_SURAT_PERMINTAAN_BAHAN_BAKU_DETAILS GetDetailsById(int Id);
        List<TRN_SURAT_PERMINTAAN_BAHAN_BAKU_DETAILS> GetAllDetailsByMasterId(int Id);
        void DeleteDetailsByMasterId(int MasterId);
        void Save(TRN_SURAT_PERMINTAAN_BAHAN_BAKU_DETAILS Db);
        #endregion
    }
}
