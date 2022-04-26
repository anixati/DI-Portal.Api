using System.Reflection;
using System.Text.RegularExpressions;
using Autofac;
using DI.Models;

// ReSharper disable once CheckNamespace
namespace System
{
    public static class TypeExtensions
    {
        private static readonly Regex SpaceRegex = new(@"\s+");


        public static string ToCmlCase(this string input)
        {
            if (!input.IsNull())
                return char.ToLowerInvariant(input[0]) + input[1..];
            return input;
        }

        public static string ToUpCase(this string input)
        {
            if (!input.IsNull())
                return char.ToUpperInvariant(input[0]) + input[1..];
            return input;
        }

        public static bool IsNull(this string input)
        {
            return string.IsNullOrEmpty(input);
        }

        public static VersionInfo GetVersion(this Assembly assembly)
        {
            var info = new VersionInfo {TimeStamp = DateTime.Now.ToString("MM/dd/yyyy hh:mm:sszzz")};
            var assemblyName = assembly.GetName();
            info.Name = assemblyName.Name;
            info.Version.Add($"Assembly:{assemblyName.Version}");
            var fileVer = assembly.GetCustomAttribute<AssemblyFileVersionAttribute>();
            if (fileVer != null)
                info.Version.Add($"File:{fileVer.Version}");
            var infoVer = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
            if (infoVer != null)
                info.Version.Add($"Product:{infoVer.InformationalVersion}");
            return info;
        }

        public static string ToKey(this string inputStr)
        {
            return !string.IsNullOrEmpty(inputStr) ? SpaceRegex.Replace(inputStr, string.Empty).ToLower() : inputStr;
        }

        public static string ToCode(this string inputStr)
        {
            return !string.IsNullOrEmpty(inputStr) ? SpaceRegex.Replace(inputStr, string.Empty).ToUpper() : inputStr;
        }

        public static void AddVersion(this ContainerBuilder builder, Assembly assembly)
        {
            builder.Register(x => assembly.GetVersion()).As<IVersionInfo>().SingleInstance();
        }
    }
}