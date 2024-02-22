using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Estrategia02.Domain.Abstract;

namespace Estrategia02.Domain;

public class Person : BaseEntity
{
    public required string Name { get; set; }
}
