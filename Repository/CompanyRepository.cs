using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contract.Interfaces;
using Entities.Models;

namespace Repository;

public class CompanyRepository: RepositoryBase<Company>, ICompanyRepository
{
    public CompanyRepository(RepositoryContext repositoryContext):base(repositoryContext)
    {
        //TODO 
    }
    public IEnumerable<Company> GetAllCompanies(bool trackChanges) =>
        FindAll(trackChanges)
            .OrderBy(entity => entity.Name)
            .ToList();
    public Company GetCompany(Guid companyId, bool trackChanges) =>
        FindByCondition(entity => entity.Id.Equals(companyId), trackChanges)
            .SingleOrDefault();
    public void CreateCompany(Company company) => Create(company);
    public IEnumerable<Company> GetByIds(IEnumerable<Guid> ids, bool trackChanges) =>
        FindByCondition(entity => ids.Contains(entity.Id), trackChanges)
            .ToList();
}
