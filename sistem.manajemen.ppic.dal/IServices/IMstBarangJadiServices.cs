using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dal.IServices
{
    public interface IMstBarangJadiServices
    {
        List<MST_BARANG_JADI> GetAll();
        MST_BARANG_JADI GetById(object Id);
        void Save(MST_BARANG_JADI Db);
        void Save(MST_BARANG_JADI Db, Login Login);
    }
}
