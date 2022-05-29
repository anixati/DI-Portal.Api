using System.Reflection;
using Autofac;
using DI.Forms.Core;
using DI.Forms.Handlers;

namespace DI.Forms
{
    public static class FormExtensions
    {
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