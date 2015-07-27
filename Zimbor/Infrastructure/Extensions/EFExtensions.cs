using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
    public static class EntityFrameworkExtensions
    {
        /// <summary>
        /// Creats a query for the given TEntity entity.
        /// </summary>
        /// <typeparam name="TEntity"/>
        /// <param name="context"/>
        /// <returns>An ObjectQuery based on the created query.</returns>
        public static ObjectQuery<TEntity> Query<TEntity>(this ObjectContext context) where TEntity : EntityObject
        {
            var entitySetName = context.GetEntitySetName<TEntity>();
            var query = context.CreateQuery<TEntity>(entitySetName);
            return query;
        }

        /// <summary>
        /// Gets the given enity name based on the context
        /// </summary>
        /// <typeparam name="TEntity"/>
        /// <param name="context"/>
        /// <returns>The entity name</returns>
        public static string GetEntitySetName<TEntity>(this ObjectContext context) where TEntity : EntityObject
        {
            var entityTypeName = typeof(TEntity).Name;
            var container = context.MetadataWorkspace.GetEntityContainer(context.DefaultContainerName, DataSpace.CSpace);
            var entitySetName = container.BaseEntitySets.Where(meta =>
                meta.ElementType.Name == entityTypeName).Select(meta => meta.Name).First();
            return entitySetName;
        }

        private static object GetPropertyValue(object obj, string property)
        {
            System.Reflection.PropertyInfo propertyInfo = obj.GetType().GetProperty(property);
            return propertyInfo.GetValue(obj, null);
        }
    }
}
