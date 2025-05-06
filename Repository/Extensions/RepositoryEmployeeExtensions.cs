using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Repository.Extensions;

public static class RepositoryEmployeeExtensions
{
    public static IQueryable<Employee> FilterEmployees(this IQueryable<Employee> employees, uint minAge, uint maxAge) =>
        employees.Where(entity => entity.Age >= minAge && entity.Age <= maxAge);
    public static IQueryable<Employee> Search(this IQueryable<Employee> employees, string searchTerm)
    {
        if(string.IsNullOrEmpty(searchTerm))
        {
            return employees;
        }

        var lowerCaseTerm = searchTerm.Trim().ToLower();
        return employees.Where(enitiy => enitiy.Name.ToLower().Contains(lowerCaseTerm));
    }
}
