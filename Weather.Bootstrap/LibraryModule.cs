
using System.Data.Entity;

using HtmlAgilityPack;

using Ninject.Modules;

using Weather.AverageWeatherDataCalculator;
using Weather.AverageWeatherDataCalculator.Interfaces;
using Weather.DAL;
using Weather.DAL.Repository;
using Weather.DAL.Repository.Interface;
using Weather.Facade;
using Weather.Parser;

namespace Weather.Bootstrap
{
    public class LibraryModule : NinjectModule
    {
        public override void Load()
        {
            this.InitializeCommon();
            this.InitializeRepositories();
            this.InitializeProviders();
        }

        private void InitializeCommon()
        {
            Bind<ICalculator>().To<Calculator>();
            Bind<IWeatherFacade>().To<WeatherFacade>();
        }

        private void InitializeRepositories()
        {
            Bind<DbContext>().To<WeatherContext>();
            Bind<IRepository>().To<Repository>();
            Bind<IRepositorySlim>().To<RepositorySlim>();
        }

        private void InitializeProviders()
        {
            Bind<GismeteoProvider>().ToSelf();
            Bind<SinoptikProvider>().ToSelf();
            Bind<Rp5Provider>().ToSelf();
            Bind<HtmlWeb>().ToSelf();
        }
    }
}

