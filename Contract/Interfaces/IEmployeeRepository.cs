﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Shared.RequestFeatures;

namespace Contract.Interfaces;

public interface IEmployeeRepository
{
    Task<PagedList<Employee>> GetEmployeesAsync(Guid companyId, EmployeeParameters employeeParameters, bool trackChanges);
    Task<Employee> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges);
    void CreateEmployeeForCompany(Guid companyId, Employee employee, bool trackChanges);
    void DeleteEmplyee(Employee employee);
}