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
        void Save(TrnPengirimanDto Dto);
        TrnPengirimanDto Save(TrnPengirimanDto Dto, LoginDto Login);
        List<TrnPengirimanDto> GetAll();
        List<TrnPengirimanDto> GetActiveAll();
        List<TrnPengirimanDto> GetAllByDoAndSPB(int Do, string NoSPB);
        TrnPengirimanDto GetById(object Id);
        void Delete(int id, string Remarks);
        decimal? GetAkumulasi(int Do, string NoSPB);
    }
}
