using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Contract.Interfaces;
using Entities.Exceptions;
using Service.Contracts.Interfaces;
using Shared.DataTransferObjects;

namespace Service;

internal sealed class EmployeeService: IEmployeeService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    public EmployeeService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }
    public IEnumerable<EmployeeDto> GetEmployees(Guid companyId, bool trackChanges)
    {
        var company = _repository.Company.GetCompany(companyId,trackChanges);
        if(company is null)
        {
            throw new CompanyNotFoundException(companyId);
        }

        var employees = _repository.Employee.GetEmployees(companyId, trackChanges);
        var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

        return employeesDto;
    }
    public EmployeeDto GetEmployee(Guid companyId, Guid id, bool trackChanges)
    {
        var company = _repository.Company.GetCompany(companyId, trackChanges);
        if(company is null)
        {
            throw new CompanyNotFoundException(companyId);
        }

        var employee = _repository.Employee.GetEmployee(companyId, id, trackChanges);
        if(employee is null)
        {
            throw new EmployeeNotFoundException(id);
        }

        var employeeDto = _mapper.Map<EmployeeDto>(employee);
        return employeeDto;
    }
}
