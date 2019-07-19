using SimpleInjector;
using SimpleInjector.Integration.Web.Mvc;
using System.Web.Mvc;
using TimeSheet.BLL.Service.Implementation;
using TimeSheet.BLL.Service.Interfaces;
using TimeSheet.DAL.Repositories.DbService.Interfaces;
using TimeSheet.DAL.Repositories.DbService.Implementation;
using TimeSheet.DAL.Repositories.Database.Implementation;
using TimeSheet.DAL.Repositories.Repository.Implementation;

namespace MVCSimpleInjectorDemo.App_Start
{
    public static class SimpleInjectorConfig
    {
        private static string _ConnectionString = "Connection";

        public static void RegisterComponents()
        {
            var container = new Container();
            // register all your components with the container here 
            // it is NOT necessary to register your controllers. 

            container.Register<ICountryService>(() => new CountryService(new CountryDAL(GetDbService())));
            container.Register<IClientService>(() => new ClientService(new ClientDAL(GetDbService())));

            //DependencyResolver.SetResolver(new UnityDependencyResolver(container)); 
            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        private static IDbService GetDbService(string connection = null)
            => new DBService(new DbConnectionService(connection ?? _ConnectionString));

    }
}