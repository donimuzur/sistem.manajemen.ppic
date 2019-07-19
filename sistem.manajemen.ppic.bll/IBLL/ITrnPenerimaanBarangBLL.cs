using sistem.manajemen.ppic.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.bll.IBLL
{
    public interface ITrnPenerimaanBarangBLL
    {
        List<TrnPenerimaanBarangDto> GetAll();
        TrnPenerimaanBarangDto GetById(object Id);
        TrnPenerimaanBarangDto GetByNama(string NamaBarang);
        void Save(TrnPenerimaanBarangDto model);
        void Save(TrnPenerimaanBarangDto model, LoginDto LoginDto);
        void Delete(int id, string Remarks);
        List<TrnPenerimaanBarangDto> GetActiveAll();
    }
}
