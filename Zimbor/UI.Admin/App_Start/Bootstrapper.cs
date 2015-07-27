using Microsoft.Practices.Unity;
using Data.Repositories;
using Infrastructure.IoC;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data.IoC;

namespace UI.Admin
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            IUnityContainer container = Unity.InitializeContainer(Lifetime.HttpRequest);
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}