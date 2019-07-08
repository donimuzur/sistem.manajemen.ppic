using sistem.manajemen.ppic.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.bll.IBLL
{
    public interface ITrnSpbBLL
    {
        List<TrnSpbDto> GetAll();
        TrnSpbDto GetById(object Id);
        TrnSpbDto GetBySPB(string SPB);
        void CloseSpb(string NoSpb);
        void Save(TrnSpbDto model);
        void Save(TrnSpbDto model, LoginDto LoginDto);
        void Savelist(List<TrnSpbDto> model, LoginDto LoginDto);
    }
}
