using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestFeatures;

public class EmployeeParameters : RequestParameters
{
    public int MinAge { get; set; }
    public int MaxAge { get; set; } = int.MaxValue;

    public bool ValidRange => MaxAge > MinAge;
}
