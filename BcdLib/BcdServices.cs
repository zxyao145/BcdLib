using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.JSInterop;

namespace BcdLib
{
    internal class BcdServices
    {
        public static IServiceProvider ServiceProvider { get; set; }

        public static IJSRuntime JsRuntime => BcdFormContainer.BcdFormContainerInstance?.BcdJsRuntime;

        public static bool TryGetJsRuntime(out IJSRuntime jsRuntime)
        {
            jsRuntime = JsRuntime;
            return jsRuntime != null;
        }
    }

    public static class BcdServicesExtensions
    {
        /// <summary>
        /// add BcdServices
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceProvider AddBcdServices(this IServiceProvider services)
        {
            BcdServices.ServiceProvider = services;
            return services;
        }
    }
}
