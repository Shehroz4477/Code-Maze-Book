using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contract.Interfaces;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class CompanyRepository: RepositoryBase<Company>, ICompanyRepository
{
    public CompanyRepository(RepositoryContext repositoryContext):base(repositoryContext)
    {
        //TODO 
    }
    public async Task<IEnumerable<Company>> GetAllCompanies(bool trackChanges) =>
        await FindAll(trackChanges)
            .OrderBy(entity => entity.Name)
            .ToListAsync();
    public async Task<Company> GetCompany(Guid companyId, bool trackChanges) =>
        await FindByCondition(entity => entity.Id.Equals(companyId), trackChanges)
            .SingleOrDefaultAsync();
    public void CreateCompany(Company company) => Create(company);
    public async Task<IEnumerable<Company>> GetByIds(IEnumerable<Guid> ids, bool trackChanges) =>
        await FindByCondition(entity => ids.Contains(entity.Id), trackChanges)
            .ToListAsync();
    public void DeleteCompany(Company company) => Delete(company);
}
