using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using sistem.manajemen.ppic.bll;
using sistem.manajemen.ppic.bll.IBLL;
using sistem.manajemen.ppic.BLL;
using sistem.manajemen.ppic.dal;
using sistem.manajemen.ppic.website.App_Start;
using sistem.manajemen.ppic.website.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace sistem.manajemen.ppic.website
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static Container _container;
        public static TService GetInstance<TService>()
        where TService : class
        {
            return _container.GetInstance<TService>();
        }
        private static void StartMain()
        {
            WebsiteMapper.Initialize();
            // 1. Create a new Simple Injector container
            var container = new Container();

            // register unit of work / context by request
            var webLifestyle = new WebRequestLifestyle();
            container.Register<IUnitOfWork, SqlUnitOfWork>(webLifestyle);
            container.Register<ILoginBLL, LoginBLL>();
            container.Register<IPageBLL, PageBLL>();
            container.Register<IMstBarangJadiBLL, MstBarangJadiBLL>();
            container.Register<IMstBahanBakuBLL, MstBahanBakuBLL>();
            container.Register<IMstWilayahBLL, MstWilayahBLL>();
            container.Register<ITrnSpbBLL, TrnSpbBLL>();
            container.Register<ITrnDoBLL, TrnDoBLL>();
            container.Register<ITrnPengirimanBLL,TrnPengirimanBLL >();
            container.Register<ITrnHasilProduksiBLL, TrnHasilProduksiBLL>();
            container.Register<IRptOutstandingBLL, RptOutstandingBLL>();
            container.Register<IMstKemasanBLL, MstKemasanBLL>();
            container.Register<IMstUomBLL,MstUomBLL >();
            container.Register<ITrnSuratPengantarBongkarMuatBLL, TrnSuratPengantarBongkarMuatBLL>();
            container.Register<ITrnMutasiBarangBLL, TrnMutasiBarangBLL>();

            // 3. Optionally verify the container's configuration.
            container.Verify();

            // 4. Store the container for use by Page classes.
            _container = container;
        }
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            StartMain();
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(_container));
        }
    }
}
