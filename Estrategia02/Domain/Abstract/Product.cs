using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estrategia02.Domain.Abstract;

public class Product : BaseEntity
{
    public required string Description { get; set; }
}
