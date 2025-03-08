using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Abstraction.Entities
{
    interface IEntityBase<T>
    {
        T Id { get; set; }
    }
}
