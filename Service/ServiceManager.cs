﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Contract.Interfaces;
using Service.Contracts.Interfaces;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICompanyService> _companyService;
        private readonly Lazy<IEmployeeService> _employeeService;
        public ServiceManager(IRepositoryManager repository, ILoggerManager logger, IMapper mapper) 
        {
            _companyService = new Lazy<ICompanyService>(() => new CompanyService(repository,logger,mapper));
            _employeeService = new Lazy<IEmployeeService>(() => new EmployeeService(repository,logger,mapper));
        }
        public ICompanyService CompanyService => _companyService.Value;
        public IEmployeeService EmployeeService => _employeeService.Value;
    }
}
