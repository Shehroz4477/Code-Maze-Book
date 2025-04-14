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
}
