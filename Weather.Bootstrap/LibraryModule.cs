﻿
using System.Data.Entity;

using HtmlAgilityPack;

using Ninject.Modules;

using Weather.DAL;
using Weather.DAL.Repository;
using Weather.Parser;

namespace Weather.Bootstrap
{
    public class LibraryModule : NinjectModule
    {
        public override void Load()
        {
            this.InitializeRepositories();
            this.InitializeProviders();
        }

        private void InitializeRepositories()
        {
            Bind<DbContext>().To<WeatherContext>();
            Bind<CityRepository>().ToSelf();
            Bind<CountryRepository>().ToSelf();
            Bind<LinkRepository>().ToSelf();
            Bind<RegionRepository>().ToSelf();
            Bind<WeatherDataRepository>().ToSelf();
            Bind<WeatherDescriptionRepository>().ToSelf();
            Bind<WorldRepository>().ToSelf();
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

