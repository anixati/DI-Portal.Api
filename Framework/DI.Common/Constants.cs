using System;

namespace DI
{
    public static class Constants
    {
        //api/v{version:apiVersion}"

        public const string RoutePrefix = "api";

        public static class AdminTenant
        {
            public static readonly Guid Code = Guid.Parse("{2FDE1AA7-3B2E-4787-9653-B19F2E3319E1}");
        }
    }
}