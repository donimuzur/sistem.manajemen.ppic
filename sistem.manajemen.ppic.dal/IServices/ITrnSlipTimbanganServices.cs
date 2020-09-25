using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dal.IServices
{
    public interface ITrnSlipTimbanganServices
    {
        List<SLIP_TIMBANGAN> GetAll();
        SLIP_TIMBANGAN GetById(object id);
        void Save(SLIP_TIMBANGAN Db);
        SLIP_TIMBANGAN Save(SLIP_TIMBANGAN Db, Login Login);
    }
}
