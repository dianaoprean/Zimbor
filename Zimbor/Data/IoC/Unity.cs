using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Domain;
using Infrastructure.IoC;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repositories;

namespace Data.IoC
{
    /// <summary>
    /// 
    /// </summary>
    public enum Lifetime : byte
    {
        HttpRequest = 1,
        Thread = 2
    }

    /// <summary>
    /// 
    /// </summary>
    public static class Unity
    {
        public static IUnityContainer Container { get; set; }

        public static T GetInstance<T>() where T : new()
        {
            if (Container == null)
                throw new MemberAccessException("Unity container is not initialized!");

            return Container.Resolve<T>();
        }

        public static IUnityContainer InitializeContainer(Lifetime lifetime)
        {
            Container = new UnityContainer();

            RegisterCommonTypes(lifetime);

            // we need to do this to have dependency injection in DAL
            Data.IoC.Unity.Container = Container;

            //DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(Container));

            return Container;
        }

        static LifetimeManager LifeTimeManagerFactory<T>(Lifetime lifetime)
        {
            switch (lifetime)
            {
                case Lifetime.HttpRequest:
                    return new HttpContextLifetimeManager<T>();
                case Lifetime.Thread:
                    return new PerThreadLifetimeManager();
                default:
                    return null;
            }
        }

        public static IUnityContainer RegisterType<TInterface, TClass>(this IUnityContainer container, Lifetime lifetime) where TClass : TInterface
        {
            return Container.RegisterType<TInterface, TClass>(LifeTimeManagerFactory<TInterface>(lifetime));
        }

        private static void RegisterCommonTypes(Lifetime lifetime)
        {
            Container
                .RegisterType<IZimborRepository<Produ>, Repository<Produ>>(lifetime)
                .RegisterType<IZimborRepository<CampanieMarketing>, Repository<CampanieMarketing>>(lifetime)
                .RegisterType<IZimborRepository<Judet>, Repository<Judet>>(lifetime)
                .RegisterType<IZimborRepository<Client>, Repository<Client>>(lifetime)
                .RegisterType<IZimborRepository<Concurenta>, Repository<Concurenta>>(lifetime)
                .RegisterType<IZimborRepository<Image>, Repository<Image>>(lifetime)
                .RegisterType<IZimborRepository<PrimireTuristica>, Repository<PrimireTuristica>>(lifetime)
                .RegisterType<IZimborRepository<StudiuPiata>, Repository<StudiuPiata>>(lifetime)
                .RegisterType<IZimborRepository<TipProdu>, Repository<TipProdu>>(lifetime)
                .RegisterType<IZimborRepository<Zona>, Repository<Zona>>(lifetime)

                .RegisterType<ZimborEntities, ZimborEntities>(lifetime)
                .Configure<InjectedMembers>()
                .ConfigureInjectionFor<ZimborEntities>(
                    new InjectionConstructor(ConfigurationManager.ConnectionStrings["ZimborEntities"].ConnectionString));
        }
    }
}
