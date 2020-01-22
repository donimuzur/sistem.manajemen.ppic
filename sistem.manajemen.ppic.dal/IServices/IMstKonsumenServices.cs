using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dal.IServices
{
    public interface IMstKonsumenServices
    {
        List<MST_KONSUMEN> GetAll();
        MST_KONSUMEN GetById(object id);
        void Save(MST_KONSUMEN Db);
        MST_KONSUMEN Save(MST_KONSUMEN Db, Login Login);
    }
}
