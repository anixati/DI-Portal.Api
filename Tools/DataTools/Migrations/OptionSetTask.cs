//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Boards.Domain;
//using DI.Domain.Core;
//using DI.Domain.Options;

//namespace DataTools.Migrations
//{
//    public class OptionSetTask : IMigrationTask
//    {
//        public void Execute()
//        {
//            var lst = GetAllProps();
//            Console.WriteLine($"{lst}");
//            // foreach (var p in lst.OrderBy(x => x)) Console.WriteLine($"{p}");
//        }

//        private static string GetAllProps()
//        {
//            var list = new HashSet<string>();
//            var moduleType = typeof(BoardsDomainModule);
//            var moduleAssembly = moduleType.Assembly;

//            var type = typeof(IEntity);
//            var types = moduleAssembly.GetTypes()
//                .Where(p => type.IsAssignableFrom(p));

//            var sb = new StringBuilder();

//            foreach (var tp in types)
//            {
//                sb.AppendLine($"{tp.Name}");
//                var osProps = tp.GetProperties().ToList();

//                if (osProps.Any())
//                {
//                    sb.AppendLine("{");
//                    foreach (var p in osProps) sb.AppendLine($"\t{{\"{p.Name}\",\"{""}\"  }},");
//                    sb.AppendLine("}");
//                }

//                sb.AppendLine("  ");
//            }

//            return sb.ToString();
//        }


//        private static HashSet<string> GetAllOptionKeys()
//        {
//            var list = new HashSet<string>();
//            var moduleType = typeof(BoardsDomainModule);
//            var moduleAssembly = moduleType.Assembly;

//            var type = typeof(IEntity);
//            var types = moduleAssembly.GetTypes()
//                .Where(p => type.IsAssignableFrom(p));
//            foreach (var tp in types)
//            {
//                var osProps = tp.GetProperties().Where(x => x.PropertyType == typeof(OptionSet)).ToList();

//                if (osProps.Any())
//                    foreach (var p in osProps)
//                        list.Add(p.Name);
//            }

//            return list;
//        }
//    }
//}

