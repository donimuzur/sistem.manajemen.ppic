using AutoMapper;
using sistem.manajemen.ppic.bll.IBLL;
using sistem.manajemen.ppic.dal;
using sistem.manajemen.ppic.dal.Services;
using sistem.manajemen.ppic.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.bll
{
    public class MstKonsumenBLL : IMstKonsumenBLL
    {
        private MstKonsumenServices _mstKonsumenServices;
        private IUnitOfWork _uow;
        public MstKonsumenBLL(IUnitOfWork Uow)
        {
            _uow = Uow;
            _mstKonsumenServices = new MstKonsumenServices(_uow);
        }
        public List<MstKonsumenDto> GetAll()
        {
            var Data = _mstKonsumenServices.GetAll();
            var ReData = Mapper.Map<List<MstKonsumenDto>>(Data);

            return ReData;
        }
        public MstKonsumenDto GetById(object Id)
        {
            var Data = _mstKonsumenServices.GetById(Id);
            var ReData = Mapper.Map<MstKonsumenDto>(Data);

            return ReData;
        }
        public MstKonsumenDto GetByNama(string Nama)
        {
            var Data = _mstKonsumenServices.GetAll().Where(x => x.NAMA_KONSUMEN.ToUpper() == Nama.ToUpper()).FirstOrDefault();
            var ReData = Mapper.Map<MstKonsumenDto>(Data);

            return ReData;
        }
        public void Save(MstKonsumenDto model)
        {
            try
            {
                var db = Mapper.Map<MST_KONSUMEN>(model);
                _mstKonsumenServices.Save(db);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public MstKonsumenDto Save(MstKonsumenDto model, LoginDto LoginDto)
        {
            try
            {
                var db = Mapper.Map<MST_KONSUMEN>(model);
                var Login = Mapper.Map<Login>(LoginDto);
                db = _mstKonsumenServices.Save(db, Login);
                return Mapper.Map<MstKonsumenDto>(db);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void InsertUpdateMstKonsumen (MstKonsumenDto model, LoginDto LoginDto)
        {
            try
            {
                var GetAll = _mstKonsumenServices.GetAll();
                var CheckExist = GetAll.Where(x => x.NAMA_KONSUMEN.ToUpper() == model.NAMA_KONSUMEN.ToUpper()).FirstOrDefault();
      
                if (CheckExist != null)
                {
                    model.ID = CheckExist.ID;
                    model.NO_TELP = CheckExist.NO_TELP;
                    model.CONTACT_PERSON = CheckExist.CONTACT_PERSON;
                }

                var db = Mapper.Map<MST_KONSUMEN>(model);
                var Login = Mapper.Map<Login>(LoginDto);
                db = _mstKonsumenServices.Save(db, Login);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
