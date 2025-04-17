using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Contract.Interfaces;
using Entities.Models;
using Service.Contracts.Interfaces;
using Shared.DataTransferObjects;

namespace Service;

internal sealed class CompanyService: ICompanyService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    public CompanyService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    {
         _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges)
    {
        //try
        //{
        //    var companies = _repository.Company.GetAllCompanies(trackChanges);
        //    var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);
        //        //companies.Select(company => new CompanyDto(company.Id, company.Name ?? "", string.Join(' ',company.Address,company.Country))).ToList();
        //    return companiesDto;
        //}
        //catch (Exception ex)
        //{
        //    _logger.LogError($"Something went wrong in the { nameof(GetAllCompanies)} service method {ex}");
        //    throw;
        //}
        var companies = _repository.Company.GetAllCompanies(trackChanges);
        var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);
        //companies.Select(company => new CompanyDto(company.Id, company.Name ?? "", string.Join(' ',company.Address,company.Country))).ToList();
        return companiesDto;
    }

    public CompanyDto GetCompany(Guid companyId, bool trackChanges)
    {
        var company = _repository.Company.GetCompany(companyId, trackChanges);
        //Check if the company is null
        var companyDto = _mapper.Map<CompanyDto>(company);
        return companyDto;
    }
}
