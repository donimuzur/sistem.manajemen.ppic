using sistem.manajemen.ppic.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.bll.IBLL
{
    public interface IMstWilayahBLL
    {
        List<MstWilayahDto> GetAll();
        MstWilayahDto GetById(object Id);
        MstWilayahDto GetByWilayah(string Wilayah);
        void Save(MstWilayahDto model);
        MstWilayahDto Save(MstWilayahDto model, LoginDto LoginDto);
    }
}
