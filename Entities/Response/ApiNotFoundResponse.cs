using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Response;

public abstract class ApiNotFoundResponse: ApiBaseResponse
{
    public string Message { get; set; } = string.Empty;

    protected ApiNotFoundResponse(string message):base(false)
    {
        Message = message;
    }
}
