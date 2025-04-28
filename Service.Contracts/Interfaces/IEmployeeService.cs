using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferObjects;

namespace Service.Contracts.Interfaces;

public interface IEmployeeService
{
    IEnumerable<EmployeeDto> GetEmployees(Guid companyId, bool trackChanges);
    EmployeeDto GetEmployee(Guid companyId, Guid id, bool trackChanges);
    EmployeeDto CreateEmployeeForComapny(Guid companyId, EmployeeForCreationDto employeeForCreationDto, bool trackChanges);
    void DeleteEmployeeForComapny(Guid companyId, Guid id, bool trackChanges);
}
