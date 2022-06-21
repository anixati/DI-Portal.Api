using System;
using System.Reflection;
using Autofac;
using DI.Forms.Core;
using DI.Forms.Handlers;
using DI.Forms.Types;
using Newtonsoft.Json;

namespace DI.Forms
{
    public static class FormExtensions
    {

        public static SelectItem ConvertToOption(this object value)
        {
            var inObj = $"{value}";
            if (string.IsNullOrEmpty(inObj)) return null;
            try
            {
                var option = JsonConvert.DeserializeObject<SelectItem>(inObj);
                return option;
            }
            catch (Exception ex)
            {
                var d = ex.ToString();
                return null;
            }
        }

        public static void AddFormProviders(this ContainerBuilder builder)
        {
            builder.RegisterType<FormProvider>()
                .As<IFormProvider>()
                .SingleInstance();
        }

        public static void AddFormBuilders(this ContainerBuilder builder, Assembly assembly)
        {
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.IsAssignableTo<IFormBuilder>() & !t.IsAbstract)
                .Keyed<IFormBuilder>(t => t.Name)
                .AsImplementedInterfaces();
        }
    }
}