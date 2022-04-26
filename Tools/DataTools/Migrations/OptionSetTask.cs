using System;
using System.Collections.Generic;
using System.Linq;
using Boards.Domain;
using DI.Domain.Core;
using DI.Domain.Options;

namespace DataTools.Migrations
{
    public class OptionSetTask : IMigrationTask
    {
        public void Execute()
        {
            var lst = GetAllOptionKeys();
            foreach (var p in lst.OrderBy(x => x)) Console.WriteLine($" --{p}--");
        }

        private static HashSet<string> GetAllOptionKeys()
        {
            var list = new HashSet<string>();
            var moduleType = typeof(BoardsDomainModule);
            var moduleAssembly = moduleType.Assembly;

            var type = typeof(IEntity);
            var types = moduleAssembly.GetTypes()
                .Where(p => type.IsAssignableFrom(p));
            foreach (var tp in types)
            {
                var osProps = tp.GetProperties().Where(x => x.PropertyType == typeof(OptionSet)).ToList();

                if (osProps.Any())
                    foreach (var p in osProps)
                        list.Add(p.Name);
            }

            return list;
        }
    }
}