using sistem.manajemen.ppic.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.bll
{
    public interface ITrnSlipTimbanganBLL
    {
        List<TrnSlipTimbanganDto> GetAll();
        TrnSlipTimbanganDto GetById(object Id);
        TrnSlipTimbanganDto GetBySuratJalan(string Nama);
        void Save(TrnSlipTimbanganDto model);
        TrnSlipTimbanganDto Save(TrnSlipTimbanganDto model, LoginDto LoginDto);
    }
}
