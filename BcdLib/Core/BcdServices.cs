using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace BcdLib.Core
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
}
