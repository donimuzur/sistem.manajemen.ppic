using sistem.manajemen.ppic.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.bll.IBLL
{
    public interface ITrnSuratPengantarBongkarMuatBLL
    {
        List<TrnSuratPengantarBongkarMuatDto> GetAll();
        TrnSuratPengantarBongkarMuatDto GetById(object Id);
        TrnSuratPengantarBongkarMuatDto GetByNama(string NamaBarang);
        TrnSuratPengantarBongkarMuatDto GetBy_SPB_DO_Nopol(string SPB, int DO, string NoPol);
        TrnSuratPengantarBongkarMuatDto GetBy_Nopol(string NoPol);
        void Save(TrnSuratPengantarBongkarMuatDto model);
        void Save(TrnSuratPengantarBongkarMuatDto model, LoginDto LoginDto);
        void Delete(int id, string Remarks);
        List<TrnSuratPengantarBongkarMuatDto> GetActiveAll();
    }
}
