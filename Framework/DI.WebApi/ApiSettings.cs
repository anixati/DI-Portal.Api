using System;
using FluentValidation.AspNetCore;

namespace DI.WebApi
{
    public class ApiSettings
    {
        private ApiSettings()
        {
            RoutePrefix = Constants.RoutePrefix;
            Validation = fv => { };
        }

        public string RoutePrefix { get; set; }
        public Action<FluentValidationMvcConfiguration> Validation { get; set; }

        public static ApiSettings Create(Action<ApiSettings> setup = null)
        {
            var api = new ApiSettings();
            setup?.Invoke(api);
            return api;
        }
    }
}