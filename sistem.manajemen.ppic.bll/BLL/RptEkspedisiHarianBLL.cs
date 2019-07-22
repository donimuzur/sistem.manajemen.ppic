using AutoMapper;
using sistem.manajemen.ppic.bll.IBLL;
using sistem.manajemen.ppic.dal;
using sistem.manajemen.ppic.dal.IServices;
using sistem.manajemen.ppic.dal.Services;
using sistem.manajemen.ppic.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.bll
{
    public class RptEkspedisiHarianBLL : IRptHarianEkspedisiBLL
    {
        private IRptEkspedisiHarianServices _rptEkspedisiHarianServices;
        private IUnitOfWork _uow;

        public RptEkspedisiHarianBLL()
        {
            _uow = new SqlUnitOfWork();
            _rptEkspedisiHarianServices = new RptEkspedisiHarianServices(_uow);
        }
        public List<RptEkspedisiHarianDto> GetRpt(string Datetime)
        {
            var Data = _rptEkspedisiHarianServices.GetRpt(Datetime);
            var ReData = Mapper.Map<List<RptEkspedisiHarianDto>>(Data);

            return ReData;
        }
    }
}
