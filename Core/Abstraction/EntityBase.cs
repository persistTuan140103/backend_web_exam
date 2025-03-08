using Core.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Abstraction
{
    public abstract class EntityBase<TId> : IEntityBase<TId>
    {
        public TId Id { get; set; }
    }
}
