using AutoMapper;
using sistem.manajemen.ppic.dal;
using sistem.manajemen.ppic.dto;
using sistem.manajemen.ppic.dto.Input;
using sistem.manajemen.ppic.website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sistem.manajemen.ppic.website.Code
{
    public class WebsiteMapper
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<LoginModel, LoginDto>().ReverseMap();
                cfg.CreateMap<LoginDto, Login>().ReverseMap();

                cfg.CreateMap<MstBarangJadiModel, MstBarangJadiDto>().ReverseMap();
                cfg.CreateMap<MstBarangJadiDto, MST_BARANG_JADI>().ReverseMap();

                cfg.CreateMap<MstBahanBakuModel, MstBahanBakuDto>().ReverseMap();
                cfg.CreateMap<MstBahanBakuDto, MST_BAHAN_BAKU>().ReverseMap();
            
                cfg.CreateMap<MstWilayahModel, MstWilayahDto>().ReverseMap();
                cfg.CreateMap<MstWilayahDto, MST_WILAYAH>().ReverseMap();

                cfg.CreateMap<TrnSpbModel, TrnSpbDto>().ReverseMap();
                cfg.CreateMap<TrnSpbDto, TRN_SPB>().ReverseMap();

                cfg.CreateMap<TrnDoModel, TrnDoDto>().ReverseMap();
                cfg.CreateMap<TrnDoDto, TRN_DO>().ReverseMap();

                cfg.CreateMap<TrnPengirimanModel, TrnPengirimanDto>().ReverseMap();
                cfg.CreateMap<TrnPengirimanDto, TRN_PENGIRIMAN>().ReverseMap();

                cfg.CreateMap<TrnHasilProduksiModel, TrnHasilProduksiDto>().ReverseMap();
                cfg.CreateMap<TrnHasilProduksiDto, TRN_HASIL_PRODUKSI>().ReverseMap();
                
                cfg.CreateMap<ChangesHistoryModel, ChangesHistoryDto>().ReverseMap();
                cfg.CreateMap<ChangesHistoryDto, CHANGES_HISTORY>().ReverseMap();

                cfg.CreateMap<TrnSuratPengantarBongkarMuatModel, TrnSuratPengantarBongkarMuatDto>().ReverseMap();
                cfg.CreateMap<TrnSuratPengantarBongkarMuatDto, TRN_SURAT_PENGANTAR_BONGKAR_MUAT>().ReverseMap();

                cfg.CreateMap<TrnMutasiBarangModel, TrnMutasiBarangDto>().ReverseMap();
                cfg.CreateMap<TrnMutasiBarangDto, TRN_MUTASI_BARANG>().ReverseMap();
                
                cfg.CreateMap<RptOutstandingModel, RptOutstandingDto>().ReverseMap();
                cfg.CreateMap<RptOutstandingDto, SP_GetRptOutstanding_Result>().ReverseMap();

                cfg.CreateMap<RptEkspedisiHarianModel, RptEkspedisiHarianDto>().ReverseMap();
                cfg.CreateMap<RptEkspedisiHarianDto, SP_RealisasiHarian_Result>().ReverseMap();

                cfg.CreateMap<TrnSuratPerintahProduksiModel, TrnSuratPerintahProduksiDto>().ReverseMap();
                cfg.CreateMap<TrnSuratPerintahProduksiDto, TRN_SURAT_PERINTAH_PRODUKSI>().ReverseMap();

                cfg.CreateMap<MstKemasanModel, MstKemasanDto>().ReverseMap();
                cfg.CreateMap<MstKemasanDto, MST_KEMASAN>().ReverseMap();

                cfg.CreateMap<MstUomModel, MstUomDto>().ReverseMap();
                cfg.CreateMap<MstUomDto, MST_UOM>().ReverseMap();
                
                cfg.CreateMap<TrnSuratPermintaanBahanBakuModel, TrnSuratPermintaanBahanBakuDto>().ReverseMap();
                cfg.CreateMap<TrnSuratPermintaanBahanBakuDto, TRN_SURAT_PERMINTAAN_BAHAN_BAKU>().ReverseMap();

                cfg.CreateMap<TrnSuratPermintaanBahanBakuDetailsModel, TrnSuratPermintaanBahanBakuDetailsDto>().ReverseMap();
                cfg.CreateMap<TrnSuratPermintaanBahanBakuDetailsDto, TRN_SURAT_PERMINTAAN_BAHAN_BAKU_DETAILS>().ReverseMap();

                cfg.CreateMap<TrnSlipTimbanganModel, TrnSlipTimbanganDto>().ReverseMap();
                cfg.CreateMap<TrnSlipTimbanganDto, SLIP_TIMBANGAN>().ReverseMap();

            });
        }
    }
}

public static class MappingExpressionExtensions
{
    public static IMappingExpression<TSource, TDest> IgnoreAllUnmapped<TSource, TDest>(this IMappingExpression<TSource, TDest> expression)
    {
        expression.ForAllMembers(opt => opt.Ignore());
        return expression;
    }
}