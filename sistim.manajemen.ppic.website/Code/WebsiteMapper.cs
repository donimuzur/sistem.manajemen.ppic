using AutoMapper;
using sistem.manajemen.ppic.dal;
using sistem.manajemen.ppic.dto;
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