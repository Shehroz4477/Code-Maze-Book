using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Contract.Interfaces;
using Entities.Exceptions;
using Entities.Models;
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
    public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(Guid companyId, bool trackChanges)
    {
        var company = await _repository.Company.GetCompanyAsync(companyId,trackChanges);
        if(company is null)
        {
            throw new CompanyNotFoundException(companyId);
        }

        var employees = await _repository.Employee.GetEmployeesAsync(companyId, trackChanges);

        var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

        return employeesDto;
    }
    public async Task<EmployeeDto> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges)
    {
        var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges);
        if(company is null)
        {
            throw new CompanyNotFoundException(companyId);
        }

        var employee = await _repository.Employee.GetEmployeeAsync(companyId, id, trackChanges);
        if(employee is null)
        {
            throw new EmployeeNotFoundException(id);
        }

        var employeeDto = _mapper.Map<EmployeeDto>(employee);

        return employeeDto;
    }

    public async Task<EmployeeDto> CreateEmployeeForComapnyAsync(Guid companyId, EmployeeForCreationDto employeeForCreationDto, bool trackChanges)
    {
        var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges);
        if(company is null)
        {
            throw new CompanyNotFoundException(companyId);
        }

        var employee = _mapper.Map<Employee>(employeeForCreationDto);

        _repository.Employee.CreateEmployeeForCompany(companyId, employee, trackChanges);
        await _repository.SaveAsync();

        var employeeDto = _mapper.Map<EmployeeDto>(employee);

        return employeeDto;
    }

    public async Task DeleteEmployeeForComapnyAsync(Guid companyId, Guid id, bool trackChanges)
    {
        var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges);
        if(company is null)
        {
            throw new CompanyNotFoundException(companyId);
        }

        var employee = await _repository.Employee.GetEmployeeAsync(companyId, id, trackChanges);
        if(employee is null)
        {
            throw new EmployeeNotFoundException(id);
        }

        _repository.Employee.DeleteEmplyee(employee);
        await _repository.SaveAsync();
    }

    public async Task UpdateEmployeeForCompanyAsync(Guid companyId, Guid id, EmployeeForUpdateDto employeeForUpdateDto, bool comTrackChanges, bool empTrackChanges)
    {
        var company = await _repository.Company.GetCompanyAsync(companyId, comTrackChanges);
        if(company == null)
        {
            throw new CompanyNotFoundException(companyId);
        }

        var employee = await _repository.Employee.GetEmployeeAsync(companyId, id, empTrackChanges);
        if(employee == null)
        {
            throw new EmployeeNotFoundException(id);
        }

        _mapper.Map(employeeForUpdateDto, employee);
        await _repository.SaveAsync();
    }

    public async Task<(EmployeeForUpdateDto employeeToPatch, Employee employee)> GetEmployeeForPatchAsync(Guid companyId, Guid id, bool comTrackChanges, bool empTrackChanges)
    {
        var company = await _repository.Company.GetCompanyAsync(companyId, empTrackChanges);
        if( company == null)
        {
            throw new CompanyNotFoundException(companyId);
        }

        var employee = await _repository.Employee.GetEmployeeAsync(companyId, id, empTrackChanges);
        if( employee == null)
        {
            throw new EmployeeNotFoundException(id);
        }

        var employeeToPatch = _mapper.Map<EmployeeForUpdateDto>(employee);
        return (employeeToPatch: employeeToPatch, employee: employee);
    }

    public async  Task SaveChangesForPatchAsync(EmployeeForUpdateDto employeeToPatch, Employee employee)
    {
        _mapper.Map(employeeToPatch, employee);
        await _repository.SaveAsync();
    }
}
