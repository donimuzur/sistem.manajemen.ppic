using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dal.IServices
{
    public interface ILoginServices
    {
        List<Login> GetAll();
        Login GetById(object id);
        void Save(Login Db);
    }
}
