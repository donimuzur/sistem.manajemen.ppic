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
        bool Save(TrnPengirimanMasterDto Dto);
        bool Save(TrnPengirimanMasterDto Dto, LoginDto Login);
        List<TrnPengirimanMasterDto> GetAll();
        List<TrnPengirimanMasterDto> GetAllByDoAndSPB(int Do, string NoSPB);
        TrnPengirimanMasterDto GetById(object Id);
        int GenerateNoSJ();
        decimal GetAkumulasi(int Do, string NoSPB);
        void SaveChanges();
    }
}
