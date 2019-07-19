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
        void Save(TrnSuratPengantarBongkarMuatDto model);
        void Save(TrnSuratPengantarBongkarMuatDto model, LoginDto LoginDto);
        void Delete(int id, string Remarks);
        List<TrnSuratPengantarBongkarMuatDto> GetActiveAll();
    }
}
