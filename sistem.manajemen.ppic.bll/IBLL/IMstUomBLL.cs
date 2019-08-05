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
        List<MstUomDto> GetAll();
        MstUomDto GetById(object Id);
        MstUomDto GetByUom(string Uom);
        void Save(MstUomDto model);
        MstUomDto Save(MstUomDto model, LoginDto LoginDto);
    }
}
