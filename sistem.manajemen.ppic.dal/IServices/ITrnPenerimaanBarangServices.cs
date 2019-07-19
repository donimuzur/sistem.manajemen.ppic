using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dal.IServices
{
    public interface ITrnPenerimaanBarangServices
    {
        List<TRN_PENERIMAAN_BARANG> GetAll();
        TRN_PENERIMAAN_BARANG GetById(object Id);
        void Save(TRN_PENERIMAAN_BARANG Db);
        void Save(TRN_PENERIMAAN_BARANG Db, Login Login);
        void Delete(int id, string Remarks);
        List<TRN_PENERIMAAN_BARANG> GetActiveAll();
    }
}
