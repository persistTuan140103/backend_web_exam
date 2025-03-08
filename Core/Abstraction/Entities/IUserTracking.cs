using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Abstraction.Entities
{
    interface IUserTracking
    {
        string CreatedBy { get; set; }
        string? ModifyBy { get; set; }
    }
}
