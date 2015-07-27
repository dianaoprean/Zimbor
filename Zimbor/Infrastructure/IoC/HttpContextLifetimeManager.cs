using Microsoft.Practices.Unity;
using System;
using System.Web;

namespace Infrastructure.IoC
{
    /// <summary>
    /// This is a derived type of LifeTimenanager, use it to manage object lifetime within a Http Request Context
    /// see http://cnug.co.in/blogs/shijuv/archive/2008/10/24/asp-net-mvc-tip-dependency-injection-with-unity-application-block.aspx
    /// </summary>
    public class HttpContextLifetimeManager<T> : LifetimeManager, IDisposable
    {
        public override object GetValue()
        {
            return HttpContext.Current.Items[typeof(T).AssemblyQualifiedName];
        }

        public override void RemoveValue()
        {
            HttpContext.Current.Items.Remove(typeof(T).AssemblyQualifiedName);
        }

        public override void SetValue(object newValue)
        {
            HttpContext.Current.Items[typeof(T).AssemblyQualifiedName] = newValue;
        }

        public void Dispose()
        {
            RemoveValue();
        }
    }
}
