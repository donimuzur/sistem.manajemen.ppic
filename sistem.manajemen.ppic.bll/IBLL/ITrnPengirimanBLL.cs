using sistem.manajemen.ppic.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.bll.IBLL
{
    public interface ITrnPengirimanBLL
    {
        List<TrnPengirimanDto> GetAll();
        TrnPengirimanDto GetById(object Id);
        TrnPengirimanDto GetBySPB(string SPB);
        TrnPengirimanDto GetByDo(string DO);
        decimal GetAkumulasi(string NoSpb, string NoDo);
        void Save(TrnPengirimanDto model);
        void Save(TrnPengirimanDto model, LoginDto LoginDto);
    }
}
