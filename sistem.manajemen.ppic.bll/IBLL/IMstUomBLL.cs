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
        List<UomDto> GetAll();
        UomDto GetById(object Id);
        void Save(UomDto model);
        void Save(UomDto model, LoginDto LoginDto);
    }
}
