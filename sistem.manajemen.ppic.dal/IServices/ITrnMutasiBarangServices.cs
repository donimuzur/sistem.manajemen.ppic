using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dal.IServices
{
    public interface ITrnMutasiBarangServices
    {
        List<TRN_MUTASI_BARANG> GetAll();
        TRN_MUTASI_BARANG GetById(object Id);
        void Save(TRN_MUTASI_BARANG Db);
        void Save(TRN_MUTASI_BARANG Db, Login Login);
        void Delete(int id, string Remarks);
        List<TRN_MUTASI_BARANG> GetActiveAll();
    }
}
