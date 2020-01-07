using sistem.manajemen.ppic.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.bll.IBLL
{
    public interface ITrnSuratPermintaanBahanBakuBLL
    {
        List<TrnSuratPermintaanBahanBakuDto> GetAll();
        List<TrnSuratPermintaanBahanBakuDto> GetActiveAll();
        TrnSuratPermintaanBahanBakuDto GetById(int Id);
        void Save(TrnSuratPermintaanBahanBakuDto Dto);
        TrnSuratPermintaanBahanBakuDto Save(TrnSuratPermintaanBahanBakuDto Dto, LoginDto Login);
        void Delete(int id, string Remarks);
        List<TrnSuratPermintaanBahanBakuDetailsDto> GetAllDetails();
        TrnSuratPermintaanBahanBakuDetailsDto GetDetailsById(int Id);
        List<TrnSuratPermintaanBahanBakuDetailsDto> GetAllDetailsByMasterId(int MasterId);
    }
}
