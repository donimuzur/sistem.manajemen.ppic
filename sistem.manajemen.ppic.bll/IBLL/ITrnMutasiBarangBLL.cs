using sistem.manajemen.ppic.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.bll.IBLL
{
    public interface ITrnMutasiBarangBLL
    {
        List<TrnMutasiBarangDto> GetAll();
        TrnMutasiBarangDto GetById(object Id);
        TrnMutasiBarangDto GetByNama(string NamaBarang);
        void Save(TrnMutasiBarangDto model);
        void Save(TrnMutasiBarangDto model, LoginDto LoginDto);
    }
}
