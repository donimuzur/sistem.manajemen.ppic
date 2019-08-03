using sistem.manajemen.ppic.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.bll.IBLL
{
    public interface IMstKemasanBLL
    {
        List<MstKemasanDto> GetAll();
        List<MstKemasanDto> GetActiveAll();
        MstKemasanDto GetById(object Id);
        MstKemasanDto GetByNama(string Nama);
        void Save(MstKemasanDto model);
        MstKemasanDto Save(MstKemasanDto model, LoginDto LoginDto);
    }
}
