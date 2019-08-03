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
        private IRptOutstandingServices _rptOutstandingServices;
        private IUnitOfWork _uow;

        public RptOutstandingBLL()
        {
            _uow = new SqlUnitOfWork();
            _rptOutstandingServices = new RptOutstandingServices(_uow);
        }
        public List<RptOutstandingDto> GetRpt()
        {
            var Data = _rptOutstandingServices.GetRpt();
            var ReData = Mapper.Map<List<RptOutstandingDto>>(Data);

            return ReData;
        }
    }
}
