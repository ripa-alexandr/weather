
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
            Bind<Repository>().ToSelf();
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

