using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dal.IServices
{
    public interface ITrnSuratPengantarBongkarMuatServices
    {
        List<TRN_SURAT_PENGANTAR_BONGKAR_MUAT> GetAll();
        TRN_SURAT_PENGANTAR_BONGKAR_MUAT GetById(object Id);
        void Save(TRN_SURAT_PENGANTAR_BONGKAR_MUAT Db);
        void Save(TRN_SURAT_PENGANTAR_BONGKAR_MUAT Db, Login Login);
    }
}
