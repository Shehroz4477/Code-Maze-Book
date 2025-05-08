using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.RequestFeatures;

public class EmployeeParameters : RequestParameters
{
    public EmployeeParameters() => OrderBy = "Name";
    public uint MinAge { get; set; }
    public uint MaxAge { get; set; } = int.MaxValue;
    public string SearchTerm { get; set; } = string.Empty;

    public bool ValidRange => MaxAge > MinAge;
}
