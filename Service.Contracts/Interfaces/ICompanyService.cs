using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferObjects;

namespace Service.Contracts.Interfaces;

public interface ICompanyService
{
    Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync(bool trackChanges);
    Task<CompanyDto> GetCompanyAsync(Guid companyId,  bool trackChanges);
    Task<CompanyDto> CreateCompanyAsync(CompanyForCreationDto company);
    Task<IEnumerable<CompanyDto>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
    Task<(IEnumerable<CompanyDto> companies, string ids)> CreateCompanyCollectionAsync(IEnumerable<CompanyForCreationDto> companyCollection);
    Task DeleteCompanyAsync(Guid companyId, bool trackChanges);
    Task UpdateCompanyAsync(Guid companyId, CompanyForUpdateDto companyForUpdateDto, bool trackChanges);
}
