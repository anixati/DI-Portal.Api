using System;
using System.Linq;
using System.Linq.Expressions;
using DI.Domain.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace DI.Domain
{
    public static class DbExtensions
    {
        public static PropertyBuilder<T> HasJsonConversion<T>(this PropertyBuilder<T> propertyBuilder)
            where T : class, new()
        {
            var converter = new ValueConverter<T, string>
            (
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<T>(v) ?? new T()
            );

            var comparer = new ValueComparer<T>
            (
                (l, r) => JsonConvert.SerializeObject(l) == JsonConvert.SerializeObject(r),
                v => v == null ? 0 : JsonConvert.SerializeObject(v).GetHashCode(),
                v => JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(v))
            );

            propertyBuilder.HasConversion(converter);
            propertyBuilder.Metadata.SetValueConverter(converter);
            propertyBuilder.Metadata.SetValueComparer(comparer);
            propertyBuilder.HasColumnType("nvarchar(max)");

            return propertyBuilder;
        }


        public static IQueryable<T> IncludeIf<T, TProperty>(this IQueryable<T> source, bool condition,
            Expression<Func<T, TProperty>> path)
            where T : class
        {
            return condition
                ? source.Include(path)
                : source;
        }

        public static DbSet<T> DbSet<T>(this DbContext context) where T : class, IEntity
        {
            return context.Set<T>();
        }


        internal static void AddQueryFilter<T>(this EntityTypeBuilder entityTypeBuilder,
            Expression<Func<T, bool>> expression)
        {
            var paramType = Expression.Parameter(entityTypeBuilder.Metadata.ClrType);
            var expFilter =
                ReplacingExpressionVisitor.Replace(expression.Parameters.Single(), paramType, expression.Body);

            var qryFilter = entityTypeBuilder.Metadata.GetQueryFilter();
            if (qryFilter != null)
            {
                var currentExpressionFilter =
                    ReplacingExpressionVisitor.Replace(qryFilter.Parameters.Single(), paramType, qryFilter.Body);
                expFilter = Expression.AndAlso(currentExpressionFilter, expFilter);
            }

            var lambdaExpression = Expression.Lambda(expFilter, paramType);
            entityTypeBuilder.HasQueryFilter(lambdaExpression);
        }


        public static void AddQueryFilter<T>(this ModelBuilder modelBuilder, Expression<Func<T, bool>> expression)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (!typeof(T).IsAssignableFrom(entityType.ClrType))
                    continue;

                var paramType = Expression.Parameter(entityType.ClrType);
                var expFilter =
                    ReplacingExpressionVisitor.Replace(expression.Parameters.Single(), paramType, expression.Body);

                var qryFilter = entityType.GetQueryFilter();
                if (qryFilter != null)
                {
                    var currentExpressionFilter =
                        ReplacingExpressionVisitor.Replace(qryFilter.Parameters.Single(), paramType, qryFilter.Body);
                    expFilter = Expression.AndAlso(currentExpressionFilter, expFilter);
                }

                var lambdaExpression = Expression.Lambda(expFilter, paramType);
                entityType.SetQueryFilter(lambdaExpression);
            }
        }
    }
}