using sistem.manajemen.ppic.dal;
using sistem.manajemen.ppic.dal.IServices;
using sistem.manajemen.ppic.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.bll.IBLL
{
    public interface ITrnHasilProduksiBLL
    {
        List<TrnHasilProduksiDto> GetAll();
        TrnHasilProduksiDto GetById(object Id);
        TrnHasilProduksiDto GetByBarang(int IdBarang);
        void Save(TrnHasilProduksiDto model);
        void Save(TrnHasilProduksiDto model, LoginDto LoginDto);
    }
}
