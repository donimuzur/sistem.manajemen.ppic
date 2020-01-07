using sistem.manajemen.ppic.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.bll.IBLL
{
    public interface ITrnSuratPerintahProduksiBLL
    {
        List<TrnSuratPerintahProduksiDto> GetAll();
        TrnSuratPerintahProduksiDto GetById(object Id);
        TrnSuratPerintahProduksiDto GetByNama(string NamaBarang);
        TrnSuratPerintahProduksiDto GetByNoSurat(string NoSurat);
        void Save(TrnSuratPerintahProduksiDto model);
        void Save(TrnSuratPerintahProduksiDto model, LoginDto LoginDto);
        void Delete(int id, string Remarks);
        List<TrnSuratPerintahProduksiDto> GetActiveAll();
    }
}
