using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estrategia02.Extensions;
using Estrategia02.Provider;

namespace Estrategia02.Middlewares;

public class TenantMiddleware
{
    private readonly RequestDelegate _next;

    public TenantMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        var tenant = httpContext.RequestServices.GetRequiredService<TenantData>();

        tenant.TenantId = httpContext.GetTenantId();

        await _next(httpContext);
    }
}
