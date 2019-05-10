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
    public class TrnDoBLL: ITrnDoBLL
    {
        private ITrnDoServices _trnDoServices;
        private IUnitOfWork _uow;
        public TrnDoBLL(IUnitOfWork Uow)
        {
            _uow = Uow;
            _trnDoServices = new TrnDoServices(_uow);
        }
        public List<TrnDoDto> GetAll()
        {
            var Data = _trnDoServices.GetAll();
            var ReData = Mapper.Map<List<TrnDoDto>>(Data);

            return ReData;
        }
        public TrnDoDto GetById(object Id)
        {
            var Data = _trnDoServices.GetById(Id);
            var ReData = Mapper.Map<TrnDoDto>(Data);

            return ReData;
        }
        public TrnDoDto GetBySPB(string SPB)
        {
            var Data = _trnDoServices.GetAll().Where(x => x.NO_SPB.ToUpper() == SPB.ToUpper()).FirstOrDefault();
            var ReData = Mapper.Map<TrnDoDto>(Data);

            return ReData;
        }
        public TrnDoDto GetByDo(string DO)
        {
            var Data = _trnDoServices.GetAll().Where(x => x.NO_DO.ToUpper() == DO.ToUpper()).FirstOrDefault();
            var ReData = Mapper.Map<TrnDoDto>(Data);

            return ReData;
        }
        public void Save(TrnDoDto model)
        {
            try
            {
                var db = Mapper.Map<TRN_DO>(model);
                _trnDoServices.Save(db);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Save(TrnDoDto model, LoginDto LoginDto)
        {
            try
            {
                var db = Mapper.Map<TRN_DO>(model);
                var Login = Mapper.Map<Login>(LoginDto);
                _trnDoServices.Save(db, Login);


            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
