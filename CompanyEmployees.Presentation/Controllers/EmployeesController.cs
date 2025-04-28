using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts.Interfaces;
using Shared.DataTransferObjects;

namespace CompanyEmployees.Presentation.Controllers;

[Route("api/companies/{companyId}/employees")]
[ApiController]
public class EmployeesController : ControllerBase
{
    private IServiceManager _service;
    public EmployeesController(IServiceManager service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetEmployeesForCompany(Guid companyId)
    {
        var employees = _service.EmployeeService.GetEmployees(companyId, trackChanges:false);
        return Ok(employees);
    }

    [HttpGet("{id:guid}", Name = "GetEmployeeForCompany")]
    public IActionResult GetEmployeeForCompany(Guid companyId, Guid id)
    {
        var employee = _service.EmployeeService.GetEmployee(companyId, id, trackChanges:false);
        return Ok(employee);
    }

    [HttpPost]
    public IActionResult CreateEmployeeForCompany(Guid companyId,[FromBody] EmployeeForCreationDto employeeForCreation)
    {
        if(employeeForCreation == null)
        {
            return BadRequest("Employee details is missing");
        }

        var employee = _service.EmployeeService.CreateEmployeeForComapny(companyId, employeeForCreation, trackChanges:false);

        return CreatedAtRoute("GetEmployeeForCompany", new { companyId, id = employee.Id }, employee);
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteEmplyeeForCompany(Guid companyId, Guid id)
    {
        _service.EmployeeService.DeleteEmployeeForComapny(companyId, id, trackChanges: false);
        return NoContent();
    }
}
