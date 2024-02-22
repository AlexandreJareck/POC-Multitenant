using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estrategia02.Extensions;

public static class HttpContextExtensions
{
    public static string GetTenantId(this HttpContext httpContext)
    {
        if (httpContext?.Request?.Path is not null && httpContext?.Request?.Path.Value is not null)
        {
            var tenant = httpContext.Request.Path.Value.Split('/', StringSplitOptions.RemoveEmptyEntries)[0];
            return tenant;
        }

        return "";

    }
}
