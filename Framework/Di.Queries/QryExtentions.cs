using Autofac;
using Di.Qry.Core;
using Di.Qry.Providers;
using Di.Qry.Schema.Types;
using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;

namespace Di.Qry
{
    public static class QryExtentions
    {
        public static Entity AddColExp(this Entity entity, string colExpression, string alias)
        {
            entity.Columns.Add(new Column(colExpression, alias));
            return entity;
        }

        public static Entity AddCols(this Entity entity, params string[] cols)
        {
            if (cols == null) return entity;
            foreach (var colName in cols)
                if (colName.Contains("|"))
                {
                    var cs = colName.Split(new[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                    entity.AddCol(cs[0], cs[1]);
                }
                else
                {
                    entity.AddCol(colName);
                }

            return entity;
        }

        public static Entity AddCol(this Entity entity, string colName, string alias = "")
        {
            entity.Columns.Add(new Column($"{entity.Alias}.{colName}", alias));
            return entity;
        }
        public static Entity AddSortCol(this Entity entity, string colName)
        {
            entity.SortColumns.Add($"{entity.Alias}.{colName}");
            return entity;
        }
        public static Entity AddQry(this Entity entity, string key, string name, string refDataId)
        {
            return entity.AddQry(key, FieldType.OptionSet, name, x => x.ReferenceSchema = refDataId);
        }

        public static Entity AddQry(this Entity entity, string key, FieldType fieldType, string name = "",
            Action<Field> configure = null)
        {
            var mf = new Field(entity.Alias, key, fieldType, name);
            configure?.Invoke(mf);
            entity.Fields.Add(mf);
            return entity;
        }

        public static Entity InnerLink(this Entity entity, string name, string alias, string from, string to,
            Action<Entity> configure = null)
        {
            var qe = new Entity(name, alias);
            configure?.Invoke(qe);
            return entity.AddLink(qe, from, to);
        }

        public static Entity OuterLink(this Entity entity, string name, string alias, string from, string to,
            Action<Entity> configure = null)
        {
            var qe = new Entity(name, alias);
            configure?.Invoke(qe);
            return entity.AddLink(qe, from, to, LinkType.Outer);
        }

        public static Entity AddLink(this Entity entity, Entity lnkEntity, string from, string to,
            LinkType linkType = LinkType.Default)
        {
            var key = $"{entity.Name}_{lnkEntity.Name}";
            entity.Links[key] = new Link(key)
            {
                Entity = lnkEntity,
                From = $"{lnkEntity.Alias}.{from}",
                To = $"{entity.Alias}.{to}",
                LinkType = linkType
            };
            return entity;
        }



        #region Service providers


        public static void AddQryProviders<T>(this ContainerBuilder builder)
        {

            builder.Register(cx =>
                {
                    var configuration = cx.Resolve<IConfiguration>();
                    var connStr = configuration.GetConnectionString(typeof(T).Name);
                    return new SqlServerConnFactory(connStr);
                }).As<IQryConnFactory>()
                .SingleInstance();
            builder.RegisterType<SqlServerDataSource>()
                .As<IQryDataSource>()
                .InstancePerLifetimeScope();
            builder.RegisterType<QryProvider>()
                .As<IQryProvider>()
                .SingleInstance();

        }
        public static void AddQueries(this ContainerBuilder builder, Assembly assembly)
        {
            
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.IsAssignableTo<IQrySchema>() & !t.IsAbstract)
                .Keyed<IQrySchema>(t => t.Name)
                .AsImplementedInterfaces();
        } 
        #endregion
    }




}