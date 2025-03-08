using Core.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Abstraction
{
    public abstract class EntityAuditBase<T> : EntityBase<T>, IAuditable
    {
        public string CreatedBy { get ; set ; }
        public string? ModifyBy { get ; set ; }
        public DateTime CreatedAt { get ; set ; }
        public DateTime? UpdatedAt { get ; set ; }
    }
}
