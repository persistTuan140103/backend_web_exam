using Core.Abstraction.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Abstraction
{
    public abstract class EntityUserAuditBase<T> : EntityBase<T>, IUserTracking
    {
        public string CreatedBy { get ; set ; }
        public string? ModifyBy { get ; set ; }
    }
}
