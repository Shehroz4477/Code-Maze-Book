using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Shared.DataTransferObjects;

namespace Service.Contracts.Interfaces;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(Guid companyId, bool trackChanges);
    Task<EmployeeDto> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges);
    Task<EmployeeDto> CreateEmployeeForComapnyAsync(Guid companyId, EmployeeForCreationDto employeeForCreationDto, bool trackChanges);
    Task DeleteEmployeeForComapnyAsync(Guid companyId, Guid id, bool trackChanges);
    Task UpdateEmployeeForCompanyAsync(Guid companyId, Guid id, EmployeeForUpdateDto employeeForUpdateDto, bool comTrackChanges, bool empTrackChanges);
    Task<(EmployeeForUpdateDto employeeToPatch, Employee employee)> GetEmployeeForPatchAsync(Guid companyId, Guid id, bool comTrackChanges, bool empTrackChanges);
    Task SaveChangesForPatchAsync(EmployeeForUpdateDto employeeToPatch, Employee employee);
}
