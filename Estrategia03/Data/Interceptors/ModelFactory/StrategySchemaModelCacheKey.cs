using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Estrategia03.Data.Interceptors.ModelFactory;


public class StrategySchemaModelCacheKey : IModelCacheKeyFactory
{
    public object Create(DbContext context, bool designTime)
    {
        var model = new
        {
            Type = context.GetType(),
            Schema = (context as ApplicationContext)?._tenantData.TenantId
        };

        return model;
    }
}
