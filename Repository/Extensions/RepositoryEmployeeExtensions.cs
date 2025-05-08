

using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;
using Entities.Models;
using Repository.Extensions.Utility;

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
    public static IQueryable<Employee> Sort(this IQueryable<Employee> employees, string orderByQueryString)
    {
        if(string.IsNullOrWhiteSpace(orderByQueryString))
        {
            return employees.OrderBy(entity =>  entity.Name);
        }

        var orderQuery = OrderQueryBuilder.CreateOrderQuery<Employee>(orderByQueryString);

        if(string.IsNullOrWhiteSpace(orderQuery))
        {
            return employees.OrderBy(entity => entity.Name);
        }

        //return employees.OrderBy(entity => entity.Age);
        return employees.OrderBy(orderQuery);
    }
}
