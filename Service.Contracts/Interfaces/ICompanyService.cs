using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Service.Contracts.Interfaces;

public interface ICompanyService
{
    IEnumerable<Company> GetAllCompanies(bool trackChanges);
}
