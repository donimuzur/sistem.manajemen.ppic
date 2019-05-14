using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dal.IServices
{
    public interface IMstBahanBakuServices
    {
        List<MST_BAHAN_BAKU> GetAll();
        MST_BAHAN_BAKU GetById(object Id);
        void Save(MST_BAHAN_BAKU Db);
        void Save(MST_BAHAN_BAKU Db, Login Login);
    }
}
