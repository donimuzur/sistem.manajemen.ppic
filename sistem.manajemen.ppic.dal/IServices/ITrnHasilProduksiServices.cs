using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dal.IServices
{
    public interface ITrnHasilProduksiServices
    {
        List<HASIL_PRODUKSI> GetAll();
        HASIL_PRODUKSI GetById(object Id);
        void Save(HASIL_PRODUKSI Db);
        void Save(HASIL_PRODUKSI Db, Login Login);
    }
}
