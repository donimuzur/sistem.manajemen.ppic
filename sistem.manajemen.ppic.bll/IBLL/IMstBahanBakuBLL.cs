using sistem.manajemen.ppic.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.bll.IBLL
{
    public interface IMstBahanBakuBLL
    {
        List<MstBahanBakuDto> GetAll();
        MstBahanBakuDto GetById(object Id);
        void Save(MstBahanBakuDto model);
        void Save(MstBahanBakuDto model, LoginDto LoginDto);
    }
}
