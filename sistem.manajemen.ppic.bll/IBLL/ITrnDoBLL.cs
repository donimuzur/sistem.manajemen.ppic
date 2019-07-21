using sistem.manajemen.ppic.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.bll.IBLL
{
    public interface ITrnDoBLL
    {
        List<TrnDoDto> GetAll();
        TrnDoDto GetById(object Id);
        List<TrnDoDto> GetBySPB(string SPB);
        TrnDoDto GetByDo(string Do);
        TrnDoDto GetBySpbAndDo(string SPB, string DO);
        void Save(TrnDoDto model);
        TrnDoDto Save(TrnDoDto model, LoginDto LoginDto);
    }
}
