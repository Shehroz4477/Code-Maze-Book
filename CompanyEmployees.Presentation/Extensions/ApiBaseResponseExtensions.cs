using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Response;

namespace CompanyEmployees.Presentation.Extensions;

public static class ApiBaseResponseExtensions
{
    public static TResultType GetResult<TResultType>(this ApiBaseResponse apiBaseResponse)
    {
        return ((ApiOkResponse<TResultType>)apiBaseResponse).Result;
    }
}
