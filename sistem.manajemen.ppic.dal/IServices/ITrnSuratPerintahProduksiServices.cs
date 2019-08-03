using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dal.IServices
{
    public interface ITrnSuratPerintahProduksiServices
    {
        List<TRN_SURAT_PERINTAH_PRODUKSI> GetAll();
        TRN_SURAT_PERINTAH_PRODUKSI GetById(object Id);
        void Save(TRN_SURAT_PERINTAH_PRODUKSI Db);
        void Save(TRN_SURAT_PERINTAH_PRODUKSI Db, Login Login);
        void Delete(int id, string Remarks);
        List<TRN_SURAT_PERINTAH_PRODUKSI> GetActiveAll();
    }
}
