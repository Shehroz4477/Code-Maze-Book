using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contract.Interfaces;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class EmployeeRepository: RepositoryBase<Employee>, IEmployeeRepository
{
    public EmployeeRepository(RepositoryContext repositoryContext):base(repositoryContext)
    {
        //TODO
    }
    public async Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId, bool trackChanges) =>
        await FindByCondition(entity => entity.CompanyId.Equals(companyId), trackChanges)
            .OrderBy(entity => entity.Name)
            .ToListAsync();
    public async Task<Employee> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges) =>
        await FindByCondition(entity => entity.CompanyId.Equals(companyId) && entity.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();
    public void CreateEmployeeForCompany(Guid companyId, Employee employee, bool trackChanges)
    {
        employee.CompanyId = companyId;
        Create(employee);
    }
    public void DeleteEmplyee(Employee employee) => Delete(employee);
}
