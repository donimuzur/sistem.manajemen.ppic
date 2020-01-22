using sistem.manajemen.ppic.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.bll.IBLL
{
    public interface IMstKonsumenBLL
    {
        List<MstKonsumenDto> GetAll();
        MstKonsumenDto GetById(object Id);
        MstKonsumenDto GetByNama(string Nama);
        void Save(MstKonsumenDto model);
        MstKonsumenDto Save(MstKonsumenDto model, LoginDto LoginDto);
        void InsertUpdateMstKonsumen(MstKonsumenDto model, LoginDto LoginDto);

    }
}
