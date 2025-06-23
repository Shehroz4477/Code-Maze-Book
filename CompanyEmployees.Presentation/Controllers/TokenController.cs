using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts.Interfaces;

namespace CompanyEmployees.Presentation.Controllers;

[Route("api/token")]
[ApiController]
public class TokenController:ControllerBase
{
    private readonly IServiceManager _service;

    public TokenController(IServiceManager service)
    {
        _service = service;
    }
}
