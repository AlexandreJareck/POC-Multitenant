

namespace Estrategia03.Domain.Abstract;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public string? TenantId { get; set; }
}
