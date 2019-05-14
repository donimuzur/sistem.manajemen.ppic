using sistem.manajemen.ppic.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.bll.IBLL
{
    public interface IMstBarangJadiBLL
    {
        List<MstBarangJadiDto> GetAll();
        MstBarangJadiDto GetById(object Id);
        MstBarangJadiDto GetByNama(string NamaBarang);
        void Save(MstBarangJadiDto model);
        void Save(MstBarangJadiDto model, LoginDto LoginDto);
        bool TambahSaldo(int IdBarang, decimal Jumlah);
        bool KurangSaldo(int IdBarang, decimal Jumlah);
    }
}
