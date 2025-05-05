using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CompanyEmployees.Presentation.ActionFilters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts.Interfaces;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

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
    public async Task<IActionResult> GetEmployeesForCompany(Guid companyId, [FromQuery]EmployeeParameters employeeParameters)
    {
        var pagedResult = await _service.EmployeeService.GetEmployeesAsync(companyId, employeeParameters, trackChanges: false);

        //Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(pagedResult.metaData));
        //var employees = await _service.EmployeeService.GetEmployeesAsync(companyId, employeeParameters, trackChanges: false);
        return Ok(pagedResult.employees);
    }

    [HttpGet("{id:guid}", Name = "GetEmployeeForCompany")]
    public async Task<IActionResult> GetEmployeeForCompany(Guid companyId, Guid id)
    {
        var employee = await _service.EmployeeService.GetEmployeeAsync(companyId, id, trackChanges: false);
        return Ok(employee);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateEmployeeForCompany(Guid companyId,[FromBody] EmployeeForCreationDto employeeForCreation)
    {
        //if(employeeForCreation == null)
        //{
        //    return BadRequest("Employee details is missing");
        //}

        //if(!ModelState.IsValid)
        //{
        //    return UnprocessableEntity(ModelState); 
        //}

        var employee = await _service.EmployeeService.CreateEmployeeForComapnyAsync(companyId, employeeForCreation, trackChanges: false);

        return CreatedAtRoute("GetEmployeeForCompany", new { companyId, id = employee.Id }, employee);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteEmplyeeForCompany(Guid companyId, Guid id)
    {
        await _service.EmployeeService.DeleteEmployeeForComapnyAsync(companyId, id, trackChanges: false);
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateEmployeeForCompany(Guid companyId, Guid id, [FromBody]EmployeeForUpdateDto employeeForUpdateDto)
    {
        //if(employeeForUpdateDto == null)
        //{
        //    return BadRequest("Employee data is missing for updation");
        //}

        //if (!ModelState.IsValid)
        //{
        //    return UnprocessableEntity(ModelState);
        //}

        await _service.EmployeeService.UpdateEmployeeForCompanyAsync(companyId, id, employeeForUpdateDto, comTrackChanges: false, empTrackChanges: true);
        return NoContent();
    }

    // the PATCH request’s media type, we
    // should use application/json-patch+json
    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> PartiallyUpdateEmployeeForCompany(Guid companyId, Guid id, [FromBody] JsonPatchDocument<EmployeeForUpdateDto> employeePatchDoc)
    {
        if(employeePatchDoc == null)
        {
            return BadRequest("Employee data for updation is missing.");
        }

        var result = await _service.EmployeeService.GetEmployeeForPatchAsync(companyId, id, comTrackChanges: false, empTrackChanges: true);

        employeePatchDoc.ApplyTo(result.employeeToPatch, ModelState);
        TryValidateModel(result.employeeToPatch);

        if(!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }

        await _service.EmployeeService.SaveChangesForPatchAsync(result.employeeToPatch, result.employee);
        return NoContent();
    }
}
