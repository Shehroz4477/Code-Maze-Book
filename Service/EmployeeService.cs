using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Contract.Interfaces;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts.Interfaces;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service;

internal sealed class EmployeeService: IEmployeeService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly IDataShaper<EmployeeDto> _dataShaper;
    public EmployeeService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IDataShaper<EmployeeDto> dataShaper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
        _dataShaper = dataShaper;
    }
    public async Task<(IEnumerable<ExpandoObject> employees, MetaData metaData)> GetEmployeesAsync(Guid companyId, EmployeeParameters employeeParameters, bool trackChanges)
    {
        //var company = await _repository.Company.GetCompanyAsync(companyId,trackChanges);
        //if(company is null)
        //{
        //    throw new CompanyNotFoundException(companyId);
        //}
        if(!employeeParameters.ValidRange)
        {
            throw new MaxAgeRangeBadRequestException();
        }

        await _GetCompanyAndCheckIfItExists(companyId, trackChanges);

        var employeesWithMetaData = await _repository.Employee.GetEmployeesAsync(companyId, employeeParameters, trackChanges);

        var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeesWithMetaData);

        var shapedData = _dataShaper.ShapeData(employeesDto, employeeParameters.Fields);

        return (employees: shapedData, metaData: employeesWithMetaData.MetaData);
    }
    public async Task<EmployeeDto> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges)
    {
        //var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges);
        //if(company is null)
        //{
        //    throw new CompanyNotFoundException(companyId);
        //}
        await _GetCompanyAndCheckIfItExists(companyId, trackChanges);

        //var employee = await _repository.Employee.GetEmployeeAsync(companyId, id, trackChanges);
        //if(employee is null)
        //{
        //    throw new EmployeeNotFoundException(id);
        //}
        var employee = await _GetEmployeeForCompanyAndCheckIfItExists(companyId, id, trackChanges);

        var employeeDto = _mapper.Map<EmployeeDto>(employee);

        return employeeDto;
    }

    public async Task<EmployeeDto> CreateEmployeeForComapnyAsync(Guid companyId, EmployeeForCreationDto employeeForCreationDto, bool trackChanges)
    {
        //var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges);
        //if(company is null)
        //{
        //    throw new CompanyNotFoundException(companyId);
        //}
        await _GetCompanyAndCheckIfItExists(companyId, trackChanges);

        var employee = _mapper.Map<Employee>(employeeForCreationDto);

        _repository.Employee.CreateEmployeeForCompany(companyId, employee, trackChanges);
        await _repository.SaveAsync();

        var employeeDto = _mapper.Map<EmployeeDto>(employee);

        return employeeDto;
    }

    public async Task DeleteEmployeeForComapnyAsync(Guid companyId, Guid id, bool trackChanges)
    {
        var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges);
        //if(company is null)
        //{
        //    throw new CompanyNotFoundException(companyId);
        //}
        await _GetCompanyAndCheckIfItExists(companyId, trackChanges);

        //var employee = await _repository.Employee.GetEmployeeAsync(companyId, id, trackChanges);
        //if(employee is null)
        //{
        //    throw new EmployeeNotFoundException(id);
        //}
        var employee = await _GetEmployeeForCompanyAndCheckIfItExists(companyId, id, trackChanges);

        _repository.Employee.DeleteEmplyee(employee);
        await _repository.SaveAsync();
    }

    public async Task UpdateEmployeeForCompanyAsync(Guid companyId, Guid id, EmployeeForUpdateDto employeeForUpdateDto, bool comTrackChanges, bool empTrackChanges)
    {
        //var company = await _repository.Company.GetCompanyAsync(companyId, comTrackChanges);
        //if(company == null)
        //{
        //    throw new CompanyNotFoundException(companyId);
        //}
        await _GetCompanyAndCheckIfItExists(companyId, comTrackChanges);

        //var employee = await _repository.Employee.GetEmployeeAsync(companyId, id, empTrackChanges);
        //if(employee == null)
        //{
        //    throw new EmployeeNotFoundException(id);
        //}
        var employee = await _GetEmployeeForCompanyAndCheckIfItExists(companyId, id, empTrackChanges);

        _mapper.Map(employeeForUpdateDto, employee);
        await _repository.SaveAsync();
    }

    public async Task<(EmployeeForUpdateDto employeeToPatch, Employee employee)> GetEmployeeForPatchAsync(Guid companyId, Guid id, bool comTrackChanges, bool empTrackChanges)
    {
        //var company = await _repository.Company.GetCompanyAsync(companyId, empTrackChanges);
        //if( company == null)
        //{
        //    throw new CompanyNotFoundException(companyId);
        //}
        await _GetCompanyAndCheckIfItExists(companyId, comTrackChanges);

        //var employee = await _repository.Employee.GetEmployeeAsync(companyId, id, empTrackChanges);
        //if( employee == null)
        //{
        //    throw new EmployeeNotFoundException(id);
        //}
        var employee = await _GetEmployeeForCompanyAndCheckIfItExists(companyId, id, empTrackChanges);

        var employeeToPatch = _mapper.Map<EmployeeForUpdateDto>(employee);
        return (employeeToPatch: employeeToPatch, employee: employee);
    }

    public async  Task SaveChangesForPatchAsync(EmployeeForUpdateDto employeeToPatch, Employee employee)
    {
        _mapper.Map(employeeToPatch, employee);
        await _repository.SaveAsync();
    }

    // Company Check, Get Company from Database and check it
    private async Task<Company> _GetCompanyAndCheckIfItExists(Guid companyId, bool trackChanges)
    {
        var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges);
        if (company is null)
        {
            throw new CompanyNotFoundException(companyId);
        }
        return company;
    }

    // Employee Check, Get Employee from Database and check it
    private async Task<Employee> _GetEmployeeForCompanyAndCheckIfItExists(Guid companyId, Guid id, bool trackChanges)
    {
        var employee = await _repository.Employee.GetEmployeeAsync(companyId, id, trackChanges);
        if (employee is null)
        {
            throw new EmployeeNotFoundException(id);
        }
        return employee;
    }
}
