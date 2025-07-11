﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.ErrorModel;
using Entities.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompanyEmployees.Presentation.Controllers;

public class ApiControllerBase: ControllerBase
{
    public IActionResult ProcessError(ApiBaseResponse baseResponse)
    {
        return baseResponse switch
        {
            ApiNotFoundResponse => NotFound(new ErrorDetails
            {
                Message = ((ApiNotFoundResponse)baseResponse).Message,
                StatusCode = StatusCodes.Status404NotFound
            }),
            ApiBadRequestResponse => BadRequest(new ErrorDetails
            {
                Message = ((ApiBadRequestResponse)baseResponse).Message,
                StatusCode = StatusCodes.Status400BadRequest
            }),
            _=> throw new NotImplementedException()
        };
    }
}
