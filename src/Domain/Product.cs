using src.Domain.Abstract;

namespace src.Domain;

public class Product : BaseEntity
{
    public required string Description { get; set; }
}
