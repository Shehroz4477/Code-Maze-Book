using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contract.Interfaces;
using Entities.Models;
using Service.Contracts.Interfaces;

namespace Service;

internal sealed class CompanyService: ICompanyService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    public CompanyService(IRepositoryManager repository, ILoggerManager logger)
    {
         _repository = repository;
        _logger = logger;
    }

    public IEnumerable<Company> GetAllCompanies(bool trackChanges)
    {
        try
        {
            var companies = _repository.Company.GetAllCompanies(trackChanges);
            return companies;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong in the { nameof(GetAllCompanies)} service method { ex}");
            throw;
        }
    }
}
