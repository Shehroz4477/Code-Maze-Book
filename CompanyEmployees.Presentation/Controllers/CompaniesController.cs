using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyEmployees.Presentation.ActionFilters;
using CompanyEmployees.Presentation.ModelBinders;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts.Interfaces;
using Shared.DataTransferObjects;

namespace CompanyEmployees.Presentation.Controllers;
//[Route("api/[controller]")]
[Route("api/companies")]
[ApiController]
[ResponseCache(CacheProfileName = "120SecondsDuration")]
public class CompaniesController : ControllerBase
{
    private readonly IServiceManager _service;
    public CompaniesController(IServiceManager service)
    {
        _service = service;
    }

    [HttpOptions]
    public IActionResult GetCompaniesOptions()
    {
        Response.Headers.Add("Allow", "GET, OPTIONS, POST, PUT DELETE");
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetCompanies()
    {
        //try
        //{
        //    var companies = _service.CompanyService.GetAllCompanies(trackChanges: false);
        //    return Ok(companies);
        //}
        //catch
        //{
        //    return StatusCode(500, "Internal server error");
        //}
        //throw new Exception("Test");
        var companies = await _service.CompanyService.GetAllCompaniesAsync(trackChanges: false);
        return Ok(companies);
    }

    [HttpGet("{id:guid}", Name = "CompanyById")]
    [ResponseCache(Duration = 60)]
    public async Task<IActionResult> GetCompany(Guid id)
    {
        var company = await _service.CompanyService.GetCompanyAsync(id, trackChanges: false);
        return Ok(company);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateCompanuy([FromBody] CompanyForCreationDto company)
    {
        //if (company == null)
        //{
        //    return BadRequest("Company details is missing");
        //}

        //if(!ModelState.IsValid)
        //{
        //    return UnprocessableEntity(ModelState);
        //}

        var createdCompany = await _service.CompanyService.CreateCompanyAsync(company);
        return CreatedAtRoute("CompanyById", new { id = createdCompany.Id }, createdCompany);
    }

    [HttpGet("collection/({ids})", Name = "CompanyCollection")]
    public async Task<IActionResult> GetCompanyCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))]IEnumerable<Guid> ids)
    {
        var companies = await _service.CompanyService.GetByIdsAsync(ids, trackChanges: false);
        return Ok(companies);
    }

    [HttpPost("collection")]
    public async Task<IActionResult> CreateCompanyCollection([FromBody] IEnumerable<CompanyForCreationDto> companyCollection)
    {
        var companyCollectionDto = await _service.CompanyService.CreateCompanyCollectionAsync(companyCollection);

        return CreatedAtRoute("CompanyCollection", new { companyCollectionDto.ids }, companyCollectionDto.companies);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCompany(Guid id)
    {
        await _service.CompanyService.DeleteCompanyAsync(id, trackChanges:false);
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateCompany(Guid id, [FromBody]CompanyForUpdateDto companyForUpdateDto)
    {
        //if(companyForUpdateDto == null)
        //{
        //    return BadRequest("Company details is missing for updation.");
        //}

        //if(!ModelState.IsValid)
        //{
        //    return UnprocessableEntity(ModelState);
        //}

        await _service.CompanyService.UpdateCompanyAsync(id, companyForUpdateDto, trackChanges:true);
        return NoContent();
    }
}
