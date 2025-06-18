using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Contract.Interfaces;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Service.Contracts.Interfaces;
using Shared.DataTransferObjects;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<ICompanyService> _companyService;
        private readonly Lazy<IEmployeeService> _employeeService;
        private readonly Lazy<IAuthenticationService> _authenticationService
        public ServiceManager(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IDataShaper<EmployeeDto> dataShaper, UserManager<User> userManager, IConfiguration configuration) 
        {
            _companyService = new Lazy<ICompanyService>(() => new CompanyService(repository,logger,mapper));
            _employeeService = new Lazy<IEmployeeService>(() => new EmployeeService(repository,logger,mapper,dataShaper));
            _authenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(logger,mapper,userManager,configuration));
        }
        public ICompanyService CompanyService => _companyService.Value;
        public IEmployeeService EmployeeService => _employeeService.Value;
        public IAuthenticationService AuthenticationService => _authenticationService.Value;
    }
}
