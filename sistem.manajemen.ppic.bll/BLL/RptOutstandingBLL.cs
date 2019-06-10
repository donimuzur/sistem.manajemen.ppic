using AutoMapper;
using sistem.manajemen.ppic.bll.IBLL;
using sistem.manajemen.ppic.dal;
using sistem.manajemen.ppic.dal.IServices;
using sistem.manajemen.ppic.dal.Services;
using sistem.manajemen.ppic.dto;
using sistem.manajemen.ppic.dto.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.bll
{
    public class RptOutstandingBLL: IRptOutstandingBLL
    {
        private IUnitOfWork _uow;
        public RptOutstandingBLL(IUnitOfWork Uow)
        {
            _uow = Uow;
        }
        public List<RptOutstandingDto> GetLogProductionIncome_SP(RptOutstandingInput input)
        {
            using (var EMSDD_CONTEXT = new PPICEntities())
            {
                EMSDD_CONTEXT.Database.CommandTimeout = 1000;
                var result = EMSDD_CONTEXT.SP_GetRptOutstanding(input.FromDate.Value.ToString("yyyy-MM-dd"), input.ToDate.Value.ToString("yyyy-MM-dd"));
                var resultDto = Mapper.Map<List<RptOutstandingDto>>(result.ToList());
                return resultDto;
            }
        }
    }
}
