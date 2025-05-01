using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Contract.Interfaces;
using Entities.Exceptions;
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
    public async Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync(bool trackChanges)
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
        var companies = await _repository.Company.GetAllCompaniesAsync(trackChanges);
        var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);
        //companies.Select(company => new CompanyDto(company.Id, company.Name ?? "", string.Join(' ',company.Address,company.Country))).ToList();
        return companiesDto;
    }
    public async Task<CompanyDto> GetCompanyAsync(Guid companyId, bool trackChanges)
    {
        var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges);
        if (company == null)
        {
            throw new CompanyNotFoundException(companyId);
        }

        var companyDto = _mapper.Map<CompanyDto>(company);
        return companyDto;
    }
    public async Task<CompanyDto> CreateCompanyAsync(CompanyForCreationDto companyCreation)
    {
        var company = _mapper.Map<Company>(companyCreation);

        _repository.Company.CreateCompany(company);
        await _repository.SaveAsync();

        var companyDto = _mapper.Map<CompanyDto>(company);
        return companyDto;
    }
    public async Task<IEnumerable<CompanyDto>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
    {
        if(ids == null)
        {
            throw new IdParametersBadRequestException();
        }

        var companies = await _repository.Company.GetByIdsAsync(ids, trackChanges);
        if(ids.Count() != companies.Count())
        {
            throw new CollectionByIdsBadRequestException();
        }

        var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);
        return companiesDto;
    }

    public async Task<(IEnumerable<CompanyDto> companies, string ids)> CreateCompanyCollectionAsync(IEnumerable<CompanyForCreationDto> companyCollection)
    {
        if(companyCollection == null)
        {
            throw new CompanyCollectionBadRequest();
        }

        var companies = _mapper.Map<IEnumerable<Company>>(companyCollection);
        foreach (var company in companies)
        {
            _repository.Company.CreateCompany(company);
        }
        await _repository.SaveAsync();

        var companyCollectionDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);
        var ids = string.Join(",", companyCollectionDto.Select(companyDto => companyDto.Id));
        return (companies:companyCollectionDto, ids:ids);
    }

    public async Task DeleteCompanyAsync(Guid companyId, bool trackChanges)
    {
        var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges);
        if(company == null)
        {
            throw new CompanyNotFoundException(companyId);
        }

        _repository.Company.DeleteCompany(company);
        await _repository.SaveAsync();
    }

    public async Task UpdateCompanyAsync(Guid companyId, CompanyForUpdateDto companyForUpdateDto, bool trackChanges)
    {
        var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges);
        if(company == null)
        {
            throw new CompanyNotFoundException(companyId);
        }

        _mapper.Map(companyForUpdateDto, company);
        await _repository.SaveAsync();
    }
}
