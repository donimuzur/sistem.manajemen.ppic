using sistem.manajemen.ppic.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.bll.IBLL
{
    public interface IMstBarangJadiBLL
    {
        List<MstBarangJadiDto> GetAll();
        MstBarangJadiDto GetById(object Id);
        void Save(MstBarangJadiDto model);
        void Save(MstBarangJadiDto model, LoginDto LoginDto);
    }
}
