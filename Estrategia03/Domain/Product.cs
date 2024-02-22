
using Estrategia03.Domain.Abstract;

namespace Estrategia03.Domain;

public class Product : BaseEntity
{
    public required string Description { get; set; }
}
