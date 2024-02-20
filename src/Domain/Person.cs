using src.Domain.Abstract;

namespace src.Domain;

public class Person : BaseEntity
{
    public required string Name { get; set; }
}
