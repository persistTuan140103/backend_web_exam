using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.ValueObjects
{
    public enum TaskStatus
    {
        Pending,
        InProgress,
        Completed,
        Canceled
    }
}
