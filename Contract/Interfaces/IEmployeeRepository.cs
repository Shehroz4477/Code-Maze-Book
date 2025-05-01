using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Contract.Interfaces;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId, bool trackChanges);
    Task<Employee> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges);
    void CreateEmployeeForCompany(Guid companyId, Employee employee, bool trackChanges);
    void DeleteEmplyee(Employee employee);
}