﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sistem.manajemen.ppic.dal.IServices
{
    public interface ITrnHasilProduksiServices
    {
        List<TRN_HASIL_PRODUKSI> GetAll();
        List<TRN_HASIL_PRODUKSI> GetActiveAll();
        TRN_HASIL_PRODUKSI GetById(object Id);
        void Save(TRN_HASIL_PRODUKSI Db);
        void Save(TRN_HASIL_PRODUKSI Db, Login Login);
        void Delete(int id, string Remarks);
    }
}
