using sistem.manajemen.ppic.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.bll.IBLL
{
    public interface IMstUomBLL
    {
        List<KemasanDto> GetAll();
        KemasanDto GetById(object Id);
        void Save(KemasanDto model);
        void Save(KemasanDto model, LoginDto LoginDto);
    }
}
