using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contract.Interfaces;
using Entities.Models;

namespace Repository;

public class EmployeeRepository: RepositoryBase<Employee>, IEmployeeRepository
{
    public EmployeeRepository(RepositoryContext repositoryContext):base(repositoryContext)
    {
        //TODO
    }

    public IEnumerable<Employee> GetEmployees(Guid companyId, bool trackChanges) =>
        FindByCondition(entity => entity.CompanyId.Equals(companyId), trackChanges)
            .OrderBy(entity => entity.Name)
            .ToList();
}
